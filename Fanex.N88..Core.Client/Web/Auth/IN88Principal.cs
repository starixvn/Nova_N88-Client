namespace Fanex.N88.Core.Web.Auth
{
    public interface IN88Principal
    {
        int UserId { get; set; }

        int OnUserId { get; set; }
    }
}