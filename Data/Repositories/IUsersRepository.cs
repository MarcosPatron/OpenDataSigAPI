using OpenDataSigAPI.Data.Entities;

namespace OpenDataSigAPI.Data.Repositories
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        bool IsUser(string username);
        decimal GetUserIdByUsername(string username);
        Task<User> GetUserByUsername(string username);
        Task<User> UpdateAvatarAsyncByUsername(byte[] avatar,string username);
        Task<byte[]> GetAvatarByUsername(string username);
    }
}
