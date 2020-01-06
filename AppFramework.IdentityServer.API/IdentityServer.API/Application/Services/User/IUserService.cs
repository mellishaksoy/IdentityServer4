using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.API.Application.Dto.User;
using IdentityServer4.Services;

namespace IdentityServer.API.Application.Services.User
{
    public interface IUserService : IProfileService
    {
        Task<UserDto> AddAsync(UserCreateDto dto);
        Task<UserDto> UpdateAsync(UserUpdateDto dto);
        Task DeleteSingleAsync(UserDeleteDto dto);
        Task DeleteAsync(UserBatchDeleteDto dto);
        Task<UserDto> GetByIdAsync(Guid tenantId, Guid id);
        Task<List<UserDto>> GetAllAsync(Guid tenantId);
        Task<UserDto> LoginUser(Guid tenantId, UserLoginDto dto);
        Task<UserDto> ChangePassword(Guid userId, UserPasswordUpdateDto dto);
    }
}
