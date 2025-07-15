using common.DTOs;
using System.Threading.Tasks;

namespace common.Interfaces
{
    public interface IUserService : IService<UserDto>
    {
        Task ResetPasswordAsync(string email);
    }
}
