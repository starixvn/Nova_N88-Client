namespace Fanex.N88.Core.Extensions
{
    using System;
    using Fanex.N88.Core.Client;

    internal static class CheckinClientExtension
    {
        public static CheckinClient CreateReCheckinClient(this NAPClients source)
        {
            var connectionString = ClientContext.GetServiceUrl();
            return CreateReCheckinClient(source, connectionString);
        }

        public static CheckinClient CreateReCheckinClient(this NAPClients source, string serviceUrl)
        {
            var client = new CheckinClient(new Uri(serviceUrl), source.CreateHttpClient());

            return client;
        }
    }
}