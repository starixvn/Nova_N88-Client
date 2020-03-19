namespace Fanex.N88.Core.Client
{
    using System;
    using System.Net.Http;
    using Fanex.N88.Core.Models.ParameterObject;

    internal class LoginClient : BaseClient
    {
        public LoginClient(Uri baseEndpoint, HttpClient httpClient)
            : base(baseEndpoint, httpClient)
        {
        }

        public int Login(LoginParameter parameter)
        {
            return Send<LoginParameter, int>(CreateRequestUri(RelativePaths.Login), HttpMethod.Post, parameter);
        }

        private class RelativePaths
        {
            public const string Login = "/login/";
        }
    }
}