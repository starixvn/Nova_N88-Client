namespace Fanex.N88.Core.Auth
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Security;
    using Fanex.N88.Core.Web.Auth;

    /// <summary>
    /// <see cref="http://stackoverflow.com/questions/1064271/asp-net-mvc-set-custom-iidentity-or-iprincipal"/>
    /// </summary>
    public class N88Principal : IPrincipal
    {
        private IIdentity _identity;
        private AuthInfo _authInfo;

        public N88Principal(IIdentity identity, AuthInfo authInfo)
        {
            this._identity = identity;
            this._authInfo = authInfo;
        }

        public N88Principal(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (HttpContext.Current.User.Identity is FormsIdentity && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpCookie httpCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (httpCookie == null)
                {
                    return;
                }

                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(httpCookie.Value);
                if (ticket == null || ticket.Expired || string.IsNullOrEmpty(ticket.UserData))
                {
                    return;
                }

                this._authInfo = AuthInfo.CreateUserData(ticket.UserData);
                this._identity = new FormsIdentity(ticket);
            }
        }

        public AuthInfo AuthInfo
        {
            get { return this._authInfo; }
        }

        public IIdentity Identity
        {
            get { return this._identity; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}