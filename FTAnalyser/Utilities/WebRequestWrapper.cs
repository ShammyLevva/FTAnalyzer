using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using System.Net;
using System.IO;

namespace FTAnalyzer.Utilities
{
    class WebRequestWrapper
    {
        //5 seems to be the magic number when the armory is acting up.
        private const int RETRY_MAX = 5;

        public const string CONTENT_XML = "application/xml";
        public const string CONTENT_JPG = "image/jpeg";
        public const string CONTENT_GIF = "image/gif";

        private class DownloadRequest
        {
            public string contentType = CONTENT_XML;
        }
        private Queue<DownloadRequest> _downloadRequests;
        private List<DownloadRequest> _failedRequests;
        private Thread[] _webRequestThreads;
        private string _proxyServer;
        private bool _useDefaultProxy;
        private string _proxyUserName;
        private string _proxyPassword;
        private string _proxyDomain;
        private int _proxyPort;


        private static Exception _fatalError = null;

        public interface INetworkSettingsProvider
        {
            int MaxHttpRequests { get; }
            bool UseDefaultProxySettings { get; }
            string ProxyServer { get; }
            int ProxyPort { get; }
            string ProxyUserName { get; }
            string ProxyPassword { get; }
            string ProxyDomain { get; }
            string ProxyType { get; }
            string UserAgent { get; }
            bool DownloadItemInfo { get; }
            bool ProxyRequiresAuthentication { get; }
            bool UseDefaultAuthenticationForProxy { get; }
            string UserAgent_IE7 { get; }
            string UserAgent_IE6 { get; }
            string UserAgent_FireFox2 { get; }
        }

        public interface ICacheSettingsProvider
        {
            string RelativeItemImageCache { get; }
            string RelativeTalentImageCache { get; }
        }

        private class DefaultNetworkSettingsProvider : INetworkSettingsProvider
        {
            #region INetworkSettingsProvider Members

            public int MaxHttpRequests
            {
                get { return Properties.NetworkSettings.Default.MaxHttpRequests; }
            }

            public bool UseDefaultProxySettings
            {
                get { return Properties.NetworkSettings.Default.UseDefaultProxySettings; }
            }

            public string ProxyServer
            {
                get { return Properties.NetworkSettings.Default.ProxyServer; }
            }

            public int ProxyPort
            {
                get { return Properties.NetworkSettings.Default.ProxyPort; }
            }

            public string ProxyUserName
            {
                get { return Properties.NetworkSettings.Default.ProxyUserName; }
            }

            public string ProxyPassword
            {
                get { return Properties.NetworkSettings.Default.ProxyPassword; }
            }

            public string ProxyDomain
            {
                get { return Properties.NetworkSettings.Default.ProxyDomain; }
            }

            public string ProxyType
            {
                get { return Properties.NetworkSettings.Default.ProxyType; }
            }

            public string UserAgent
            {
                get { return Properties.NetworkSettings.Default.UserAgent; }
            }

            public bool DownloadItemInfo
            {
                get { return Properties.NetworkSettings.Default.DownloadItemInfo; }
            }

            public bool ProxyRequiresAuthentication
            {
                get { return Properties.NetworkSettings.Default.ProxyRequiresAuthentication; }
            }

            public bool UseDefaultAuthenticationForProxy
            {
                get { return Properties.NetworkSettings.Default.UseDefaultAuthenticationForProxy; }
            }

            public string UserAgent_IE7
            {
                get { return Properties.NetworkSettings.Default.UserAgent_IE7; }
            }

            public string UserAgent_IE6
            {
                get { return Properties.NetworkSettings.Default.UserAgent_IE6; }
            }

            public string UserAgent_FireFox2
            {
                get { return Properties.NetworkSettings.Default.UserAgent_FireFox2; }
            }

            #endregion
        }

        public static INetworkSettingsProvider NetworkSettingsProvider = new DefaultNetworkSettingsProvider();

        public WebRequestWrapper()
        {
            int maxConnections = NetworkSettingsProvider.MaxHttpRequests;
            _failedRequests = new List<DownloadRequest>();
            _webRequestThreads = new Thread[maxConnections];
            _downloadRequests = new Queue<DownloadRequest>();
            _useDefaultProxy = NetworkSettingsProvider.UseDefaultProxySettings;

            _proxyServer = NetworkSettingsProvider.ProxyServer;
            _proxyPort = NetworkSettingsProvider.ProxyPort;
            _proxyUserName = NetworkSettingsProvider.ProxyUserName;
            _proxyPassword = NetworkSettingsProvider.ProxyPassword;
            _proxyDomain = NetworkSettingsProvider.ProxyDomain;
        }

        public string GetLatestVersionString()
        {
            string html = DownloadText("http://www.codeplex.com/FTAnalyzer");
            if (html == null || !html.Contains("{Current Version: ")) return string.Empty;
            html = html.Substring(html.IndexOf("{Current Version: ") + "{Current Version: ".Length);
            if (!html.Contains("}")) return string.Empty;
            html = html.Substring(0, html.IndexOf("}"));
            return html;
        }

        public string GetBetaVersionString()
        {
            string html = DownloadText("http://www.codeplex.com/FTAnalyzer");
            if (html == null || !html.Contains("{Beta Version: ")) return string.Empty;
            html = html.Substring(html.IndexOf("{Beta Version: ") + "{Beta Version: ".Length);
            if (!html.Contains("}")) return string.Empty;
            html = html.Substring(0, html.IndexOf("}"));
            return html;
        }

        public string GetRandomDidYouKnow()
        {
            string html = DownloadText("http://FTAnalyzer.codeplex.com/Wiki/View.aspx?title=DidYouKnow");
            if (html == null || !html.Contains("-------<br />") || !(html.Contains("&nbsp;by&nbsp;<a id=\"wikiEditByLink\" href=\"http://www.codeplex.com/site/users/view/Astrylian\">Astrylian</a>") || html.Contains("&nbsp;by&nbsp;<a id=\"wikiEditByLink\" href=\"http://www.codeplex.com/site/users/view/Kavan\">Kavan</a>"))) return string.Empty;
            html = html.Substring(html.IndexOf("-------<br />") + 13);
            if (!html.Contains("<br />-------")) return string.Empty;
            html = html.Substring(0, html.IndexOf("<br />-------"));
            html = html.Replace("<br />", "|");
            string[] dyks = html.Split('|');
            Random r = new Random();
            List<string> randomDyks = new List<string>();
            while (randomDyks.Count < 3 && randomDyks.Count < dyks.Length)
            {
                string dyk = dyks[r.Next(dyks.Length)];
                if (!randomDyks.Contains(dyk)) randomDyks.Add(dyk);
            }
            return string.Join("\r\n\r\n", randomDyks.ToArray());
        }

        public string GetKnownIssues()
        {
            string html = DownloadText("http://FTAnalyzer.codeplex.com/Wiki/View.aspx?title=KnownIssues");
            if (html == null || !html.Contains("-------<br />") || !(html.Contains("&nbsp;by&nbsp;<a id=\"wikiEditByLink\" href=\"http://www.codeplex.com/site/users/view/Astrylian\">Astrylian</a>") || html.Contains("&nbsp;by&nbsp;<a id=\"wikiEditByLink\" href=\"http://www.codeplex.com/site/users/view/Kavan\">Kavan</a>"))) return string.Empty;
            html = html.Substring(html.IndexOf("-------<br />") + 13);
            if (!html.Contains("<br />-------")) return string.Empty;
            html = html.Substring(0, html.IndexOf("<br />-------"));
            html = html.Replace("<br />", "\r\n");
            return html;
        }

        /// <summary>
        /// Gets the number of request failures since the last time the failure list was cleared.
        /// </summary>
        public int QueueFailureCount
        {
            get { return _failedRequests.Count; }
        }


        /// <summary>
        /// Count of the currently queued download requests.
        /// </summary>
        public int RequestQueueCount
        {
            get { return _downloadRequests.Count; }
        }

        /// <summary>
        /// If the last request received a 407 or no response. Used to prevent a lot of bad calls.
        /// It also has the good side effect of not locking someone's account out if they enter the proxy info incorrectly
        /// by sending lots of bad authorization attempts.
        /// </summary>
        public static bool LastWasFatalError
        {
            get { return _fatalError != null; }
        }

        public static Exception FatalError
        {
            get { return _fatalError; }
        }

        public static void ResetFatalErrorIndicator()
        {
            _fatalError = null;
        }

        /// <summary>
        /// Used to create a web client with all of the appropriote proxy/useragent/etc settings
        /// </summary>
        private WebClient CreateWebClient()
        {
            WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
            client.Headers.Add("user-agent", NetworkSettingsProvider.UserAgent);
            if (NetworkSettingsProvider.ProxyType == "Http")
            {
                if (_useDefaultProxy)
                {
                    client.Proxy = HttpWebRequest.DefaultWebProxy;
                }
                else if (!String.IsNullOrEmpty(_proxyServer))
                {
                    client.Proxy = new WebProxy(_proxyServer, _proxyPort);
                }
                if (client.Proxy != null && NetworkSettingsProvider.ProxyRequiresAuthentication)
                {
                    if (NetworkSettingsProvider.UseDefaultAuthenticationForProxy)
                    {
                        client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    }
                    else
                    {
                        client.Proxy.Credentials = new NetworkCredential(_proxyUserName, _proxyPassword, _proxyDomain);
                    }
                }
            }
            return client;
        }

        private void DownloadFile(string URI, string localPath)
        {
            DownloadFile(URI, localPath, CONTENT_XML);
        }

        /// <summary>
        /// Download a given file with the appropriote configuration information
        /// </summary>
        /// <param name="serverPath">URI to download</param>
        /// <param name="localPath">local path, including file name,  where the downloaded file will be saved</param>
        private void DownloadFile(string URI, string localPath, string contentType)
        {
            int retry = 0;
            bool success = false;
            //occasionally a zero byte file slips through without throwing an exception
            if (!File.Exists(localPath) || new FileInfo(localPath).Length <= 0)
            {
                do
                {
                    if (!LastWasFatalError)
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                        }
                        using (WebClient client = CreateWebClient())
                        {
                            try
                            {
                                client.DownloadFile(URI, localPath);
                                if (!client.ResponseHeaders[HttpResponseHeader.ContentType].StartsWith(contentType))
                                {
                                    throw new Exception("invalid content type");
                                }
                                success = true;
                            }
                            catch (Exception ex)
                            {
                                CheckExceptionForFatalError(ex);
                                //if on a client file download, there is an exception, 
                                //it will create a 0 byte file. We don't want that empty file.
                                if (File.Exists(localPath))
                                {
                                    File.Delete(localPath);
                                }
                                retry++;
                                if (retry == RETRY_MAX || LastWasFatalError)
                                {
                                    throw;
                                }
                            }
                        }
                    }
                } while (retry <= RETRY_MAX && !success && !LastWasFatalError);
            }
        }

        /// <summary>
        /// This is used to prevent multiple attempts at network traffic when its not working and 
        /// continuing to issue requests could cause serious problems for the user.
        /// </summary>
        /// <param name="ex"></param>
        private void CheckExceptionForFatalError(Exception ex)
        {
            //Log.Write("Exception trying to download: "+ ex);
            //Log.Write(ex.StackTrace);
            if (ex.Message.Contains("407") /*proxy auth required */
                || ex.Message.Contains("403") /*proxy info probably wrong, if we keep issuing requests, they will probably get locked out*/
                || ex.Message.Contains("timed out") /*either proxy required and firewall dropped the request, or armory is down*/
                //|| ex.Message.Contains("invalid content type") /*unexpected content type returned*/
                || ex.Message.Contains("The remote name could not be resolved") /* DNS problems*/
                )
            {
                _fatalError = ex;
            }
        }

        public string DownloadText(string URI)
        {
            WebClient webClient = CreateWebClient();
            string value = null;
            int retry = 0;
            bool success = false;
            do
            {
                if (!LastWasFatalError)
                {
                    try
                    {
                        value = webClient.DownloadString(URI);
                        if (!String.IsNullOrEmpty(value))
                        {
                            success = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        CheckExceptionForFatalError(ex);
                    }
                }
                retry++;
            } while (retry <= RETRY_MAX && !success && !LastWasFatalError);
            return value;
        }

        private XmlDocument DownloadXml(string URI) { return DownloadXml(URI, false); }
        private XmlDocument DownloadXml(string URI, bool allowTable)
        {
            XmlDocument returnDocument = null;
            int retry = 0;
            //Download Text has retry logic in it as well, but that just makes sure it gets a response, this
            //makes sure we get a valid XML response.
            do
            {
                string xml = DownloadText(URI);
                //If it contains "<table", then the armory accidentally returned it as html instead of xml.
                if (!string.IsNullOrEmpty(xml) && (allowTable || !xml.Contains("<table")))
                {
                    try
                    {
                        returnDocument = new XmlDocument();
                        returnDocument.XmlResolver = null;
                        returnDocument.LoadXml(xml.Replace("&", ""));
                        if (returnDocument == null || returnDocument.DocumentElement == null
                                    || !returnDocument.DocumentElement.HasChildNodes
                            /*|| !returnDocument.DocumentElement.ChildNodes[0].HasChildNodes*/) // this check is no longer valid
                        {
                            //document returned no data we care about.
                            returnDocument = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                retry++;
            } while (returnDocument == null && !LastWasFatalError && retry < RETRY_MAX);

            return returnDocument;
        }
    }
}
