using System;
using System.Collections.Generic;

namespace IdentityServer.API.Application.Dto.User
{

    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public Guid TenantId { get; set; }

        public string EmployeeId { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
    }

    public class UserCreateDto 
    {
        public UserCreateDto()
        {
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public Guid TenantId { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public List<Claim.Claim> UserClaims { get; set; }

    }

    public class UserUpdateDto 
    {
        public UserUpdateDto()
        {
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public Guid TenantId { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }

    }
    public class UserDeleteDto 
    {
        public UserDeleteDto()
        {
        }

        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
    }
    public class UserBatchDeleteDto 
    {
        public UserBatchDeleteDto()
        {

        }

        public IList<Guid> Ids { get; set; }
        public Guid TenantId { get; set; }
    }
    public class UserLoginDto 
    {
        public UserLoginDto()
        {

        }

        public string name { get; set; }
        public string password { get; set; }
    }
    public class UserPasswordUpdateDto 
    {
        public UserPasswordUpdateDto()
        {
        }
        public Guid TenantId { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPassWord { get; set; }
    }
}
