namespace Fanex.N88.Core.Service.Interface
{
    using System.Collections.Generic;
    using Fanex.N88.Core.Models.ValueObject;
    using Models;

    public interface ISiteService
    {
        IEnumerable<SiteUrl> GetUrlById(int siteId, string environment);

        bool IsActivated(int siteId);

        IEnumerable<MenuItem> GetMenuItemBySiteId(int siteId, string enviroment, bool? isActive = null);
    }
}