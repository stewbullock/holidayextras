using ASM.HolidayExtra.Service.Model;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Core.Domain
{
    interface IUserMapper
    {
        List<User> Map(List<Core.Model.User> users);
        void Map(ChangeUser changeUser, Core.Model.User user);
        Core.Model.User Map(TrackUser trackUser, int id);
    }
}