using FTAnalyzer.Properties;
using System.Configuration;
using System.Xml.Linq;

namespace FTAnalyzer.Utilities
{
    static class SettingsBootstrap
    {
        // Call once, at the very start of Main() - before any Settings.Default/GeneralSettings.Default/
        // etc. property is read, even indirectly (e.g. Theme.ActiveColors' static field initializers
        // read FontSettings.Default.ThemeMode). ApplicationSettingsBase fetches every property from
        // its provider on first access and caches the result for the process lifetime, so the provider
        // swap has to land before that first touch or it has no effect.
        public static void Initialize()
        {
            MigrateLegacySettingsIfNeeded();

            PortableSettingsProvider provider = new();
            provider.Initialize(nameof(PortableSettingsProvider), null);
            ApplyProvider(Settings.Default, provider);
            ApplyProvider(GeneralSettings.Default, provider);
            ApplyProvider(FontSettings.Default, provider);
            ApplyProvider(FileHandling.Default, provider);
            ApplyProvider(MappingSettings.Default, provider);
            ApplyProvider(NonGedcomDate.Default, provider);
        }

        static void ApplyProvider(ApplicationSettingsBase settings, SettingsProvider provider)
        {
            foreach (SettingsProperty property in settings.Properties)
                property.Provider = provider;
            // Properties collection above may already have caused settings.Providers to lazily
            // pick up `provider` on its own - re-adding the same instance would throw
            // ArgumentException (a provider with that name is already registered).
            if (!settings.Providers.OfType<SettingsProvider>().Contains(provider))
                settings.Providers.Add(provider);
        }

        // One-time carry-forward: if the fixed settings file doesn't exist yet, this is either a
        // first-ever run (nothing to migrate) or the first run after switching to this provider - the
        // previous LocalFileSettingsProvider-based user.config, keyed by a hash of the exe's own path,
        // is still sitting under a differently-named folder elsewhere in %LOCALAPPDATA%. Every rebuild
        // to a new output path (or reinstall to a new folder) creates one of these, most holding only
        // a handful of settings written before the app got a chance to save the rest - so the most
        // RECENTLY written one is often one of those near-empty ones, not the real one. Pick whichever
        // candidate has the most <setting> entries instead (ties broken by recency), and carry it
        // forward so nobody loses settings on this switch.
        static void MigrateLegacySettingsIfNeeded()
        {
            if (File.Exists(PortableSettingsProvider.FilePath))
                return;

            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            XElement? richest = null;
            int richestSettingCount = -1;
            DateTime richestWriteTime = DateTime.MinValue;
            string[] productFolders = ["FTAnalyzer", "FTAnalyzer.Windows"];
            foreach (string productFolder in productFolders)
            {
                string root = Path.Combine(localAppData, productFolder);
                if (!Directory.Exists(root))
                    continue;
                foreach (string configPath in Directory.EnumerateFiles(root, "user.config", SearchOption.AllDirectories))
                {
                    XElement? userSettings;
                    try
                    {
                        userSettings = XDocument.Load(configPath).Root?.Element("userSettings");
                    }
                    catch (System.Xml.XmlException)
                    {
                        continue; // corrupt candidate - skip rather than block launch
                    }
                    if (userSettings is null)
                        continue;

                    int settingCount = userSettings.Descendants("setting").Count();
                    DateTime writeTime = File.GetLastWriteTimeUtc(configPath);
                    if (settingCount > richestSettingCount || (settingCount == richestSettingCount && writeTime > richestWriteTime))
                    {
                        richest = userSettings;
                        richestSettingCount = settingCount;
                        richestWriteTime = writeTime;
                    }
                }
            }
            if (richest is null)
                return;

            XDocument migrated = new(new XElement("configuration", new XElement(richest)));
            Directory.CreateDirectory(Path.GetDirectoryName(PortableSettingsProvider.FilePath)!);
            migrated.Save(PortableSettingsProvider.FilePath);
        }
    }
}
