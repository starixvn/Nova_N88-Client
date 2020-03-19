namespace Fanex.N88.Core.Extensions
{
    using System;
    using Fanex.N88.Core.Client;

    internal static class SSOClientExtension
    {
        public static SSOClient CreateSSOClient(this NAPClients source)
        {
            var connectionString = ClientContext.GetServiceUrl();
            return CreateGroupClient(source, connectionString);
        }

        public static SSOClient CreateGroupClient(this NAPClients source, string serviceUrl)
        {
            var client = new SSOClient(new Uri(serviceUrl), source.CreateHttpClient());

            return client;
        }
    }
}