namespace Fanex.N88.Core.Client
{
    using System;
    using System.Net.Http;

    internal class LogoutClient : BaseClient
    {
        public LogoutClient(Uri baseEndpoint, HttpClient httpClient)
            : base(baseEndpoint, httpClient)
        {
        }

        public void Logout(int onlineUserId)
        {
            Send<int>(CreateRequestUri(RelativePaths.Logout), HttpMethod.Post, onlineUserId);
        }

        private class RelativePaths
        {
            public const string Logout = "/logout/";
        }
    }
}