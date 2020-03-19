namespace Fanex.N88.Core.Extensions
{
    using System;
    using Fanex.N88.Core.Client;

    internal static class GroupClientExtension
    {
        public static GroupClient CreateGroupClient(this NAPClients source)
        {
            var connectionString = ClientContext.GetServiceUrl();
            return CreateGroupClient(source, connectionString);
        }

        public static GroupClient CreateGroupClient(this NAPClients source, string serviceUrl)
        {
            var client = new GroupClient(new Uri(serviceUrl), source.CreateHttpClient());

            return client;
        }
    }
}