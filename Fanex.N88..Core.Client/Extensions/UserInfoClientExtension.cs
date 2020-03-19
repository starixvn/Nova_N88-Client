namespace Fanex.N88.Core.Extensions
{
    using System;
    using Fanex.N88.Core.Client;

    internal static class UserInfoClientExtension
    {
        public static UserInfoClient CreateUserInfoClient(this NAPClients source)
        {
            var connectionString = ClientContext.GetServiceUrl();
            return CreateUserInfoClient(source, connectionString);
        }

        public static UserInfoClient CreateUserInfoClient(this NAPClients source, string serviceUrl)
        {
            var client = new UserInfoClient(new Uri(serviceUrl), source.CreateHttpClient());

            return client;
        }
    }
}