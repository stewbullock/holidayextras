using ASM.Core;
using ASM.HolidayExtra.Service.Model;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Service
{
    public interface IUserService
    {
        IResponse<List<User>> Retrieve();
        IResponse<List<User>> Create(TrackUser user);
        IResponse<List<User>> Create(string user);
        IResponse<List<User>> Change(ChangeUser user);
        IResponse<List<User>> Delete(DeleteUser user);
        IResponse<List<User>> Search(SearchUser user);
    }
}