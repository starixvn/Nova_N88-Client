namespace Fanex.N88.Core.Web.Attribute
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.Caching;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Fanex.N88.Core.Business;
    using Fanex.N88.Core.Configuration;
    using Fanex.N88.Core.Service.Impl;
    using Fanex.N88.Core.Service.Interface;
    using Fanex.N88.Core.Web.Session;

    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class N88APIAuthAttribute : AuthorizeAttribute
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

        public override void OnAuthorization(HttpActionContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var session = new CurrentSession();

            if (!IsAuthenticated(session.UserId))
            {
                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
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
    }
}