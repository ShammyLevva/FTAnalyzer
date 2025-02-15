using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace FTAnalyzer.Utilities
{
    internal static class ZipHelper
    {
        public static void ExtractZipFile(string archivePath, string password, string outFolder)
        {
            using var fsInput = File.OpenRead(archivePath);
            using var zf = new ZipFile(fsInput);
            if (!string.IsNullOrEmpty(password))
            {
                // AES encrypted entries are handled automatically
                zf.Password = password;
            }

            foreach (ZipEntry zipEntry in zf)
            {
                if (zipEntry.IsFile)
                {
                    string entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:
                    //entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here
                    // to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    // Manipulate the output filename here as desired.
                    var fullZipToPath = Path.Combine(outFolder, entryFileName);
                    var directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    // 4K is optimum
                    var buffer = new byte[4096];

                    // Unzip file in buffered chunks. This is just as fast as unpacking
                    // to a buffer the full size of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using var zipStream = zf.GetInputStream(zipEntry);
                    using Stream fsOutput = File.Create(fullZipToPath);
                    StreamUtils.Copy(zipStream, fsOutput, buffer);
                }
            }
        }
    }
}
