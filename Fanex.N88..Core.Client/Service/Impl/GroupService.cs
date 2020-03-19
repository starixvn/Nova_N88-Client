namespace Fanex.N88.Core.Service.Impl
{
    using Fanex.Common.Helper;
    using Fanex.N88.Core.Client;
    using Fanex.N88.Core.Extensions;
    using Fanex.N88.Core.Models;
    using Fanex.N88.Core.Service.Interface;

    public class GroupService : IGroupService
    {
        private readonly GroupClient _groupClient;

        public GroupService()
        {
            _groupClient = ClientContext.Clients.CreateGroupClient();
        }

        public Group Get(int groupId)
        {
            Check.Argument.IsNotNegativeOrZero(groupId, "groupId");

            return _groupClient.Get(groupId);
        }
    }
}