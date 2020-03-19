namespace Fanex.N88.Core.Client
{
    using System;
    using System.Net.Http;
    using Fanex.N88.Core.Models.ParameterObject;

    internal class UserInfoClient : BaseClient
    {
        public UserInfoClient(Uri baseEndpoint, HttpClient httpClient)
            : base(baseEndpoint, httpClient)
        {
        }

        public bool UpdatePassword(int siteId, int userId, string password)
        {
            return Send<object, bool>(CreateRequestUri(RelativePaths.UpdatePassword, new { siteId = siteId, userId = userId, password = password }), HttpMethod.Post, null);
        }

        public bool UpdateProfile(int siteId, UpdateUserInfoParameter parameter)
        {
            return Send<UpdateUserInfoParameter, bool>(CreateRequestUri(RelativePaths.UpdateProfile, new { siteId = siteId }), HttpMethod.Post, parameter);
        }

        private static class RelativePaths
        {
            public const string UpdatePassword = "/userinfo/updatePassword";
            public const string UpdateProfile = "/userinfo/updateProfile";
        }
    }
}