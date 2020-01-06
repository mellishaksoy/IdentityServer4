using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Infrastructure.Exceptions
{
    public enum ClientErrorCodes
    {
        //Validation Errors like  1XXX
        ClientCreateDtoIsNotValid = 1000,
        ClientUpdateDtoIsNotValid = 1001,
        ClientDeleteDtoIsNotValid = 1002,
        MoreThan100RecordsCannotDelete = 1003,

        //Not Found Exceptions like 2XXX
        ClientCouldNotBeFound = 2000,
        ApiResourceCouldNotBeFound = 2001,
        ApiResourceNameCannotBeUpdated = 2002,
        

        //Field Validation Errors like 3XXX
        ClientIdCannotBeNull = 3000,
        AllowedScopesCannotBeEmpty = 3001,
        ApiResourcesOrApiScopesCannotBeNull = 3002,

        ClientIdMustBeUnique = 4000
            
    }
}
