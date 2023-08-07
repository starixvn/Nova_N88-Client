namespace Fanex.N88.Core.Service.Impl
{
    using System.Collections.Generic;
    using Business;
    using Common.Helper;
    using Extensions;
    using Fanex.N88.Core.Client;
    using Interface;
    using Models;
    using Models.ValueObject;

    public class UserService : IUserService
    {
        private readonly UserClient _userClient;

        public UserService()
        {
            _userClient = ClientContext.Clients.CreateUserClient();
        }

        public bool HasAnyPermission(int userId, int siteId, string environment = Constant.UAT)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");
            Check.Argument.IsNotEmpty(environment, "environment");

            return _userClient.HasAnyPermission(userId, siteId, environment);
        }

        public IEnumerable<MenuItem> GetMenuItem(int userId, int siteId, string environment = Constant.UAT, bool? isActive = null)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");
            Check.Argument.IsNotEmpty(environment, "environment");

            return _userClient.GetMenuItem(userId, siteId, environment, isActive);
        }

        public IEnumerable<SiteLevel> GetSiteLevel(int userId, string environment = Constant.UAT)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotEmpty(environment, "environment");

            return _userClient.GetSiteLevel(userId, environment);
        }

        public User Get(int userId)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");

            return _userClient.Get(userId);
        }

        public User Get(string username, int siteId, bool isLicenseeSite = true)
        {
            Check.Argument.IsNotEmpty(username, "username");
            Check.Argument.IsNotEmpty(username, "siteId");

            return _userClient.Get(username, isLicenseeSite, siteId);
        }

        public IEnumerable<User> GetAll()
        {
            return _userClient.GetAll();
        }

        public int GetUserLevel(int userId, int siteId)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");

            return _userClient.GetUserLevel(userId, siteId);
        }

        public int GetPermission(int userId, int siteId, string functionKey)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");
            Check.Argument.IsNotEmpty(functionKey, "functionKey");

            return _userClient.GetPermission(userId, siteId, functionKey);
        }

        public bool IsHavingPermission(int userId, int siteId, string functionKey, Constant.Policy policy)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");
            Check.Argument.IsNotEmpty(functionKey, "functionKey");

            int currentPolicy = GetPermission(userId, siteId, functionKey);

            return currentPolicy >= (int)policy;
        }

        public IEnumerable<FunctionPolicy> GetPermissions(int userId, int siteId, string parentFunctionKey, string environment = Constant.UAT)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");

            return _userClient.GetPermissions(userId, siteId, parentFunctionKey, environment);
        }

        public IEnumerable<Setting> GetSettings(int userId)
        {
            Check.Argument.IsNotNegativeOrZero(userId, "userId");

            return _userClient.GetSettings(userId);
        }

        public IEnumerable<UserPolicy> GetUserPolicies(string functionKey, int siteId)
        {
            Check.Argument.IsNotEmpty(functionKey, "functionKey");
            Check.Argument.IsNotNegativeOrZero(siteId, "siteId");

            return _userClient.GetUserPolicies(functionKey, siteId);
        }
    }
}