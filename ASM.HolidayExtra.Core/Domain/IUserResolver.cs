using ASM.Core;
using ASM.HolidayExtra.Service.Model;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Core.Domain
{
    interface IUserResolver
    {
        IResponse<List<User>> RetrieveAll();
        IResponse<List<User>> Change(ChangeUser changeUser);
        IResponse<List<User>> Create(TrackUser trackUser);
        IResponse<List<User>> Create(string trackUser);
        IResponse<List<User>> Delete(DeleteUser deleteUser);
        IResponse<List<User>> Search(SearchUser filter);
    }
}