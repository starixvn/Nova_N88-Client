using System.Web.Hosting;

namespace Fanex.N88.Core.SignIn
{
    public static class SignInHandler
    {
        public static void Register()
        {
            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedViewPathProvider());
        }
    }
}