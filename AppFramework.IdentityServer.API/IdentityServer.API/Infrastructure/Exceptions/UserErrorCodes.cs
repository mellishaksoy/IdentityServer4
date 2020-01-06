namespace IdentityServer.API.Infrastructure.Exceptions
{
    public enum UserErrorCodes
    {
        //Validation Errors like  1XXX
        UserCreateDtoIsNotValid = 1000,
        UserUpdateDtoIsNotValid = 1001,
        UserDeleteDtoIsNotValid = 1002,
        MoreThan100RecordsCannotDelete = 1003,
        UserPasswordChangeDtoIsNotValid = 1005,
        
        //Not Found Exceptions like 2XXX
        UserCouldNotBeFound = 2000,

        //Field Validation Errors like 3XXX
        TenantIdCannotBeNull = 3000,
        IdsCannotBeEmpty = 3001,
        UserIdCannotBeNull = 3002,
        PageSizeIsNotInRange = 3003,
        PageIndexIsNotInRange = 3004,
        EmailCannotBeDuplicate = 3005,
        NewPasswordCannotBeSameOldPassword = 3006,
        NewPasswordandRepeatNewPasswordMustBesame = 3007,
        OldPasswordNotValid = 3008,

        UserEmployeeIdMustBeUniqueInSameTenant = 4000,
        UserAlreadyExists = 5000

    }
}
