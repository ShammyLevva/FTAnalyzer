using System.Configuration;
using System.Xml.Linq;

namespace FTAnalyzer.Utilities
{
    // LocalFileSettingsProvider (the default ApplicationSettingsBase backing store) derives its
    // storage folder from a hash of the exe's own file path - any time the build output path,
    // install location, or extraction folder changes (a new TargetFramework, a redeployed beta,
    // a fresh unzip), every user setting silently resets to its default (see bug where bumping
    // the TFM's Windows SDK version wiped Recent Files, the Google API key, theme mode, etc.).
    // This provider instead always reads/writes one fixed, deterministic file, so settings
    // survive regardless of where the exe happens to run from.
    sealed class PortableSettingsProvider : SettingsProvider
    {
        public static readonly string FilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FTAnalyzer", "settings.config");

        readonly XDocument _document = LoadOrCreate(FilePath);

        public override string ApplicationName { get => "FTAnalyzer"; set { } }

        public override void Initialize(string? name, System.Collections.Specialized.NameValueCollection? config)
            => base.Initialize(string.IsNullOrEmpty(name) ? nameof(PortableSettingsProvider) : name, config ?? []);

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection properties)
        {
            SettingsPropertyValueCollection values = [];
            XElement? group = GroupElement(context, createIfMissing: false);
            foreach (SettingsProperty property in properties)
            {
                SettingsPropertyValue value = new(property) { IsDirty = false };
                XElement? valueNode = group?.Elements("setting")
                    .FirstOrDefault(e => (string?)e.Attribute("name") == property.Name)
                    ?.Element("value");
                if (valueNode is not null)
                {
                    value.SerializedValue = property.SerializeAs == SettingsSerializeAs.Xml
                        ? valueNode.Elements().FirstOrDefault()?.ToString() ?? string.Empty
                        : valueNode.Value;
                }
                values.Add(value);
            }
            return values;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection values)
        {
            try
            {
                XElement group = GroupElement(context, createIfMissing: true)!;
                foreach (SettingsPropertyValue value in values.Cast<SettingsPropertyValue>())
                {
                    if (value.Property.Attributes.ContainsKey(typeof(ApplicationScopedSettingAttribute)))
                        continue; // application-scoped settings are read-only defaults, never persisted

                    group.Elements("setting").FirstOrDefault(e => (string?)e.Attribute("name") == value.Name)?.Remove();

                    XElement valueNode = new("value");
                    if (value.Property.SerializeAs == SettingsSerializeAs.Xml && value.SerializedValue is string xml && xml.Length > 0)
                        valueNode.Add(XDocument.Parse(xml).Root);
                    else
                        valueNode.Value = value.SerializedValue?.ToString() ?? string.Empty;

                    group.Add(new XElement("setting",
                        new XAttribute("name", value.Name),
                        new XAttribute("serializeAs", value.Property.SerializeAs.ToString()),
                        valueNode));
                }
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
                _document.Save(FilePath);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or System.Xml.XmlException)
            {
                throw new ConfigurationErrorsException($"Unable to save settings to {FilePath}.", ex);
            }
        }

        XElement? GroupElement(SettingsContext context, bool createIfMissing)
        {
            string groupName = context["GroupName"] as string ?? throw new InvalidOperationException("SettingsContext is missing GroupName.");
            XElement root = _document.Root!;
            XElement? userSettings = root.Element("userSettings");
            if (userSettings is null)
            {
                if (!createIfMissing)
                    return null;
                userSettings = new XElement("userSettings");
                root.Add(userSettings);
            }
            XElement? group = userSettings.Elements(groupName).FirstOrDefault();
            if (group is null && createIfMissing)
            {
                group = new XElement(groupName);
                userSettings.Add(group);
            }
            return group;
        }

        static XDocument LoadOrCreate(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    return XDocument.Load(path);
                }
                catch (System.Xml.XmlException)
                {
                    // corrupt file - start fresh rather than block the app from launching
                }
            }
            return new XDocument(new XElement("configuration", new XElement("userSettings")));
        }
    }
}
