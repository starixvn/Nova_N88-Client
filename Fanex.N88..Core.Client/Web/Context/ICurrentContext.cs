namespace Fanex.N88.Core.Web.Context
{
    using Fanex.N88.Core.Configuration;

    public interface ICurrentContext
    {
        string IP { get; }

        string DomainName { get; }

        string BrowserName { get; }

        string Platform { get; }

        string CountryCode { get; }

        DomainElement CurrentDomain { get; }
    }
}