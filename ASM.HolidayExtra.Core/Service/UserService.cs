using ASM.Core;
using ASM.HolidayExtra.Core.Domain;
using ASM.HolidayExtra.Service;
using ASM.HolidayExtra.Service.Model;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Core.Service
{
    class UserService : IUserService
    {
        #region Fields 

        private readonly IUserResolver _userResolver;

        #endregion

        #region Methods 

        #region Constructor 

        public UserService(IUserResolver userResolver)
        {
            _userResolver = userResolver;
        }

        #endregion

        /// <summary>
        /// Retrieve all users
        /// </summary>
        /// <returns></returns>
        public IResponse<List<User>> Retrieve()
        {
            return _userResolver.RetrieveAll();
        }

        /// <summary>
        /// Resolve user to track to the repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IResponse<List<User>> Create(TrackUser user)
        {
            return _userResolver.Create(user);
        }

        /// <summary>
        /// Resolve user to track to the repository
        /// </summary>
        /// <param name="user">XML serialised TrackUser</param>
        /// <returns></returns>
        public IResponse<List<User>> Create(string user)
        {
            return _userResolver.Create(user);
        }

        /// <summary>
        /// Resolve user changes to the repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IResponse<List<User>> Change(ChangeUser user)
        {
            return _userResolver.Change(user);
        }

        /// <summary>
        /// Delete user from the repository
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IResponse<List<User>> Delete(DeleteUser user)
        {
            return _userResolver.Delete(user);
        }

        /// <summary>
        /// Search for users
        /// </summary>
        /// <param name="user">Search Filter</param>
        /// <returns></returns>
        public IResponse<List<User>> Search(SearchUser user)
        {
            return _userResolver.Search(user);
        }

        #endregion
    }
}