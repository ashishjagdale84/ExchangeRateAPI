using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAccess
{
    internal static class RestRequestHelper
    {
        internal static async Task<string> Get(string requestUrl)
        {
            string _return = string.Empty;

            HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
            request.Proxy = System.Net.WebRequest.GetSystemWebProxy();
            request.ContentType = "appication/json";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));

                StreamReader reader = new StreamReader(response.GetResponseStream());
                _return = reader.ReadToEnd();
            }
            return _return;
        }
    }
}