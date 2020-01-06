using IdentityServer.API.Application.Dto.User;
using IdentityServer.API.Models;
using Newtonsoft.Json;
using Platform360.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.API.IntegrationTests
{
    public class UserControllerTests
    {
        //[Fact]
        //public async Task CreateUser_ValidUserCreateDto_HttpStatusCreated()
        //{
        //    var userCreateDto = new UserCreateDto()
        //    {
        //        CompanyId = Guid.NewGuid(),
        //        Email = "test@as.mail.com",
        //        UserName = "Test User Name"
        //    };
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Post,
        //        RequestUri = new Uri(TestServerHelper.CreateUsersRequestUrl()),
        //        Content = new StringContent(JsonConvert.SerializeObject(userCreateDto), Encoding.UTF8,
        //            "application/json")
        //    };
        //    var response = await TestServerHelper.GetClient().SendAsync(request);
        //    var user = TestServerHelper.GetContext().Users
        //        .FirstOrDefault(x => x.UserName == userCreateDto.UserName);

        //    Assert.NotNull(user);
        //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        //    Assert.Equal(user.TenantId, TestServerHelper.GetTenantId());
        //    Assert.Equal(user.CompanyId, userCreateDto.CompanyId);
        //    Assert.Equal(user.Email, userCreateDto.Email);
        //    Assert.Equal(user.UserName, userCreateDto.UserName);
        //}

        //[Fact]
        //public async Task CreateUser_InValidUserCreateDto_HttpStatusBadRequest()
        //{
        //    var userCreateDto = new UserCreateDto()
        //    {
        //        CompanyId = Guid.NewGuid(),
        //        Email = "test@as.mail.com",
        //        UserName = "Test User Name"
        //    };
        //    try
        //    {
        //        var uri = new Uri(TestServerHelper.CreateUsersRequestUrl());
        //    }
        //    catch(Exception ex)
        //    {

        //    }
            
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Post,
        //        RequestUri = new Uri(TestServerHelper.CreateUsersRequestUrl()),
        //        Content = new StringContent(JsonConvert.SerializeObject(userCreateDto), Encoding.UTF8,
        //            "application/json")
        //    };
        //    var response = await TestServerHelper.GetClient().SendAsync(request);

        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        //[Fact]
        //public async Task UpdateUser_ValidUserUpdateDto_HttpStatusCreated()
        //{
        //    var user = CreateUser();
        //    var userUpdateDto = new UserUpdateDto()
        //    {
        //        UserName = "Updated User",
        //        TenantId = user.TenantId,
        //        CompanyId = user.CompanyId,
        //        Id = user.Id,
        //        Email = "Updated Mail"
        //    };
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Put,
        //        RequestUri = new Uri(TestServerHelper.CreateUsersRequestUrl(user.Id)),
        //        Content = new StringContent(JsonConvert.SerializeObject(userUpdateDto), Encoding.UTF8, "application/json")
        //    };
        //    var response = await TestServerHelper.GetClient().SendAsync(request);
        //    var updatedUser = TestServerHelper.GetContext().Users.FirstOrDefault(x => x.Id == user.Id);

        //    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        //    Assert.Equal(updatedUser?.TenantId, TestServerHelper.GetTenantId());
        //    Assert.Equal(updatedUser?.CompanyId, userUpdateDto.CompanyId);
        //    Assert.Equal(updatedUser?.Email, userUpdateDto.Email);
        //    Assert.Equal(updatedUser?.UserName, userUpdateDto.UserName);
        //}

        //[Fact]
        //public async Task UpdateUser_InValidUserUpdateDto_HttpStatusBadRequest()
        //{
        //    var user = CreateUser();
        //    var userUpdateDto = new UserUpdateDto()
        //    {
        //        CompanyId = user.CompanyId,
        //        Email = "Updated Mail"
        //    };
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Put,
        //        RequestUri = new Uri(TestServerHelper.CreateUsersRequestUrl(user.Id)),
        //        Content = new StringContent(JsonConvert.SerializeObject(userUpdateDto), Encoding.UTF8, "application/json")
        //    };
        //    var response = await TestServerHelper.GetClient().SendAsync(request);

        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        //[Fact]
        //public async Task DeleteUserById_IdAndTenantId_HttpStatusNoContent()
        //{
        //    var user = CreateUser();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Delete,
        //        RequestUri = new Uri(TestServerHelper.CreateUsersRequestUrl(user.Id)),
        //        Content = new StringContent(JsonConvert.SerializeObject(new UserDeleteDto()
        //        {

        //        }), Encoding.UTF8, "application/json")
        //    };
        //    var response = await TestServerHelper.GetClient().SendAsync(request);
        //    var deletedUser = TestServerHelper.GetContext().Users.FirstOrDefault(x => x.Id == user.Id);

        //    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        //    Assert.NotNull(deletedUser);
        //}

        //[Fact]
        //public async Task DeleteUserById_InvalidIdAndTenantId_HttpStatusBadRequest()
        //{
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Delete,
        //        RequestUri = new Uri(TestServerHelper.CreateUsersRequestUrl(Guid.NewGuid())),
        //        Content = new StringContent(JsonConvert.SerializeObject(new UserDeleteDto()
        //        {

        //        }), Encoding.UTF8, "application/json")
        //    };
        //    var response = await TestServerHelper.GetClient().SendAsync(request);

        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        //[Fact]
        //public async Task GetUserById_InvalidIdAndTenantId_HttpStatusNotFound()
        //{
        //    var response = await TestServerHelper.GetClient().GetAsync(TestServerHelper.CreateUsersRequestUrl(Guid.NewGuid()));
        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}

        //[Fact]
        //public async Task GetUserById_ValidIdAndTenantId_HttpStatusOk()
        //{
        //    var user = CreateUser();
        //    var response = await TestServerHelper.GetClient().GetAsync(TestServerHelper.CreateUsersRequestUrl(user.Id));
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<UserDto>(content);

        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    Assert.Equal(user.Id, result.Id);
        //    Assert.Equal(user.UserName, result.UserName);
        //    Assert.Equal(user.CompanyId, result.CompanyId);
        //    Assert.Equal(user.TenantId, result.TenantId);
        //    Assert.Equal(user.Email, result.Email);
        //}

        //[Fact]
        //public async Task GetUsers_WithCompanyIdFilter_HttpStatusOk()
        //{
        //    var companyId = Guid.NewGuid();
        //    var users = CreateUsers(null, companyId, 12);
        //    var response = await TestServerHelper.GetClient().GetAsync(TestServerHelper.CreateUsersRequestUrl());
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<Paged<UserDto>>(content);

        //    Assert.Equal(1, result.TotalPageCount);
        //    Assert.Equal(1, result.PageIndex);
        //    Assert.Equal(250, result.PageSize);
        //    Assert.Equal(users.Count, result.List.Count);
        //}

        //[Fact]
        //public async Task GetUsers_With_HttpStatusOk()
        //{
        //    CreateUsers(null, Guid.NewGuid(), 26);
        //    var response = await TestServerHelper.GetClient().GetAsync(TestServerHelper.CreateUsersRequestUrl());
        //    response.EnsureSuccessStatusCode();
        //    var content = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<Paged<UserDto>>(content);

        //    Assert.Equal(1, result.TotalPageCount);
        //    Assert.Equal(1, result.PageIndex);
        //    Assert.Equal(250, result.PageSize);
        //    Assert.True(result.List.Count >= 25);
        //}

        //#region Helpers

        //private static ICollection<ApplicationUser> CreateUsers(Guid? id = null, Guid? companyId = null, int count = 10)
        //{
        //    var list = new List<ApplicationUser>();
        //    for (var i = 0; i < count; i++)
        //    {
        //        var user = new ApplicationUser()
        //        {
        //            Id = id ?? Guid.NewGuid(),

        //            TenantId = TestServerHelper.GetTenantId(),
        //            UserName = "Test Name" + i,
        //            CompanyId = companyId ?? Guid.NewGuid(),
        //            Email = "mail" + i + ".m@m.com"
        //        };
        //        list.Add(user);
        //        TestServerHelper.GetContext().Users.Add(user);
        //        TestServerHelper.GetContext().SaveChanges();
        //    }
        //    return list;
        //}

        //private static ApplicationUser CreateUser(Guid? id = null, Guid? companyId = null)
        //{
        //    var user = new ApplicationUser()
        //    {
        //        Id = id ?? Guid.NewGuid(),
        //        TenantId = TestServerHelper.GetTenantId(),
        //        UserName = "Test Name",
        //        CompanyId = companyId ?? Guid.NewGuid(),
        //        Email = "mail.m@m.com"
        //    };
        //    TestServerHelper.GetContext().Users.Add(user);
        //    TestServerHelper.GetContext().SaveChanges();
        //    return TestServerHelper.GetContext().Users.FirstOrDefault(c => c.UserName == user.UserName && c.CompanyId == user.CompanyId);
        //}
        //#endregion
    }
}
