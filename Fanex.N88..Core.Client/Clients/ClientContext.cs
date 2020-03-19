namespace Fanex.N88.Core.Client
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Fanex.N88.Core.Configuration;
    using Fanex.N88.Core.Exceptions;
    using Fanex.N88.Core.Web.Handler;

    internal static class ClientContext
    {
        private static Random _randomHandler = new Random();

        static ClientContext()
        {
            Configuration = N88Settings.Settings;
            Clients = new NAPClients
            {
                CreateHttpClient = CreateHttpClient
            };
        }

        public static NAPClients Clients { get; internal set; }

        public static N88Settings Configuration { get; internal set; }

        private static HttpClient CreateHttpClient()
        {
            return HttpClientFactory.Create(new NAPDelegatingHandler(Configuration.SiteId.ToString(), Configuration.AccessKey));
        }

        public static string GetServiceUrl()
        {
            if (string.IsNullOrWhiteSpace(Configuration.ServiceUrls))
            {
                throw new N88ApiException("Service Urls cannot be null or white space");
            }

            var serviceUrls = Configuration.ServiceUrls.Split(',');
            var numOfUrl = serviceUrls.Count();

            if (numOfUrl == 1)
            {
                return serviceUrls[0];
            }

            var selectedServiceUrl = serviceUrls[_randomHandler.Next(0, numOfUrl)];

            return selectedServiceUrl;
        }
    }
}