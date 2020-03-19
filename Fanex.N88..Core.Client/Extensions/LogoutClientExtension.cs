namespace Fanex.N88.Core.Extensions
{
    using System;
    using Fanex.N88.Core.Client;

    internal static class LogoutClientExtension
    {
        public static LogoutClient CreateLogoutClient(this NAPClients source)
        {
            var connectionString = ClientContext.GetServiceUrl();
            return CreateLogoutClient(source, connectionString);
        }

        public static LogoutClient CreateLogoutClient(this NAPClients source, string serviceUrl)
        {
            var client = new LogoutClient(new Uri(serviceUrl), source.CreateHttpClient());

            return client;
        }
    }
}