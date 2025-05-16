namespace SearchEngine.Service.Helpers
{
    public static class SearchHelper
    {
        public static string GoogleStr = "google";
        public static string BingStr = "bing";

        /// <summary>
        /// Helper to check validity of the url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="checkedUrl"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool EnsureHttps(this string url, out string checkedUrl)
        {
            var isValid = false;
            checkedUrl = url;
            if (!string.IsNullOrWhiteSpace(url))
            {
                // If the URL starts with "http://" or "https://", return it as is
                if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    // check valid URL
                    if (Uri.TryCreate(url, UriKind.Absolute, out Uri validUri))
                    {
                        checkedUrl = url;
                        isValid = true;
                    }
                }
                else
                {
                    string fullUrl = "https://" + url;
                    // check valid URL
                    if (Uri.TryCreate(fullUrl, UriKind.Absolute, out Uri validUrl))
                    {
                        checkedUrl = fullUrl;
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}
