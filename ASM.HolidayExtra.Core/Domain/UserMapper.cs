using ASM.HolidayExtra.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASM.HolidayExtra.Core.Domain
{
    class UserMapper : IUserMapper
    {
        #region Methods 

        /// <summary>
        /// Map to service interface model
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public List<User> Map(List<Core.Model.User> users)
        {
            return users?.Select(user => new User
            {
                Id = user.Id,
                Forename = user.Forename,
                Surname = user.Surname,
                Email = user.Email,
                Created = user.Created,
                CreatedBy = user.CreatedBy,
                Changed = user.Changed,
                ChangedBy = user.ChangedBy
            }).ToList();
        }

        /// <summary>
        /// Map changes from service interface model
        /// </summary>
        /// <param name="changeUser"></param>
        /// <param name="user"></param>
        public void Map(ChangeUser changeUser, Core.Model.User user)
        {
            if (changeUser == null || user == null || changeUser.Id != user.Id) return;

            user.Forename = changeUser.Forename;
            user.Surname = changeUser.Surname;
            user.Email = changeUser.Email;
            user.Changed = DateTime.Now;
            user.ChangedBy = changeUser.ChangedBy;
        }

        /// <summary>
        /// Map creation from service interface model
        /// </summary>
        /// <param name="trackUser"></param>
        /// <param name="id">New user identity</param>
        /// <returns></returns>
        public Model.User Map(TrackUser trackUser, int id)
        {
            if (trackUser == null) return null;

            return new Model.User
            {
                Id = id,
                Forename = trackUser.Forename,
                Surname = trackUser.Surname,
                Email = trackUser.Email,
                CreatedBy = trackUser.CreatedBy,
                Created = DateTime.Now
            };
        }

        #endregion
    }
}