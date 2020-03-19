namespace Fanex.N88.Core.Web.Attribute
{
    using System;
    using System.Runtime.Caching;
    using System.Web.Mvc;
    using System.Web.Security;
    using Fanex.N88.Core.Auth;
    using Fanex.N88.Core.Business;
    using Fanex.N88.Core.Client.Web;
    using Fanex.N88.Core.Configuration;
    using Fanex.N88.Core.Service.Impl;
    using Fanex.N88.Core.Service.Interface;

    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class N88AuthAttribute : AuthorizeAttribute
    {
        private static readonly IUserService UserService = new UserService();
        private static readonly ObjectCache Cache = MemoryCache.Default;
        private string _functionKey;
        private Constant.Policy _policy;
        private bool _permissionCache = false;
        private int _cacheDuration = 0;

        public string FunctionKey
        {
            get
            {
                return this._functionKey == null ? string.Empty : this._functionKey;
            }

            set
            {
                this._functionKey = value;
            }
        }

        public Constant.Policy Policy
        {
            get
            {
                return this._policy == Constant.Policy.None ? Constant.Policy.Read : this._policy;
            }

            set
            {
                this._policy = value;
            }
        }

        public bool PermissionCache
        {
            get { return _permissionCache; }
            set { _permissionCache = value; }
        }

        public int CacheDuration
        {
            get { return _cacheDuration == 0 ? N88Settings.Settings.CacheDuration : _cacheDuration; }
            set { _cacheDuration = value; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext == null)
            {
                this.CloseConnection(filterContext);

                return;
            }

            var principal = filterContext.HttpContext.User as N88Principal;

            if (principal == null)
            {
                principal = new N88Principal(filterContext.HttpContext);
            }

            if (object.ReferenceEquals(principal, null) || object.ReferenceEquals(principal.AuthInfo, null))
            {
                this.CloseConnection(filterContext);

                return;
            }

            if (!IsAuthenticated(principal.AuthInfo.UserId))
            {
                this.CloseConnection(filterContext);

                return;
            }
        }

        protected bool IsAuthenticated(int userId)
        {
            if (userId <= 0)
            {
                return false;
            }

            int policy = 0;

            if (PermissionCache)
            {
                var cacheKey = string.Format("{0}_{1}_{2}_{3}", N88Settings.Settings.AccessKey, userId, N88Settings.Settings.SiteId, this.FunctionKey);
                var cachedPolicy = Cache.Get(cacheKey);

                if (cachedPolicy == null)
                {
                    policy = UserService.GetPermission(userId, N88Settings.Settings.SiteId, this.FunctionKey);
                    Cache.Set(cacheKey, policy, DateTime.Now.AddSeconds(CacheDuration));
                }
                else
                {
                    policy = (int)cachedPolicy;
                }
            }
            else
            {
                policy = UserService.GetPermission(userId, N88Settings.Settings.SiteId, this.FunctionKey);
            }

            return policy >= (int)this.Policy;
        }

        protected void CloseConnection(AuthorizationContext filterContext)
        {
            if (filterContext == null
                || filterContext.HttpContext == null
                || filterContext.HttpContext.Request == null)
            {
                this.SignOut();
                return;
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                HandleForbiddenAjaxRequest(filterContext);
            }
            else if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                this.SignOut();
            }
            else
            {
                string settingUrl = N88Settings.Settings.UnauthorizeUrl;
                filterContext.Result = new RedirectResult(settingUrl);
            }
        }

        protected virtual void HandleForbiddenAjaxRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult("<center><b>" + ViewResources.UnauthorizeMsg + "</b></center>");
        }

        protected void SignOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}