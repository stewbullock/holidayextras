using ASM.Core;
using ASM.Core.Serialisation;
using ASM.HolidayExtra.Core.Repository;
using ASM.HolidayExtra.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASM.HolidayExtra.Core.Domain
{
    class UserResolver : IUserResolver
    {
        #region Fields 

        private readonly IUserRepository _userRepository;
        private readonly IUserMapper _userMapper;
        private readonly IDeSerializer _deSerializer;

        #endregion

        #region Methods 

        #region Constructor 

        public UserResolver(IUserRepository userRepository, IUserMapper userMapper, IDeSerializer deSerializer)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
            _deSerializer = deSerializer;
        }

        #endregion

        /// <summary>
        /// Retrieve all users from the repository
        /// </summary>
        /// <returns></returns>
        public IResponse<List<User>> RetrieveAll()
        {
            var users = _userRepository.GetAll();
            return Response<List<User>>.Success(_userMapper.Map(users));
        }

        /// <summary>
        /// Create and add new user to repository
        /// </summary>
        /// <param name="trackUser"></param>
        /// <returns></returns>
        public IResponse<List<User>> Create(TrackUser trackUser)
        {
            var lastUser = _userRepository.GetAll().OrderByDescending(user => user.Id).FirstOrDefault();
            var user = _userMapper.Map(trackUser, (lastUser?.Id ?? 1) + 1);

            _userRepository.Add(user);
            return RetrieveAll();
        }

        /// <summary>
        /// Deserialise and add new user to repository
        /// </summary>
        /// <param name="trackUser"></param>
        /// <returns></returns>
        public IResponse<List<User>> Create(string trackUser)
        {
            try
            {
                return Create(_deSerializer.DeSerializeFromXml<TrackUser>(trackUser));
            }
            catch (Exception exception)
            {
                return Response<List<User>>.Failed(string.Format("Unable to deserialise user to create due to the following: \n\n{0}", exception.Message));
            }
        }

        /// <summary>
        /// Change user attributes from repository
        /// </summary>
        /// <param name="changeUser"></param>
        /// <returns></returns>
        public IResponse<List<User>> Change(ChangeUser changeUser)
        {
            var user = _userRepository.GetById(changeUser.Id);
            if (user == null) return Response<List<User>>.Failed(string.Format("User with Id:{0} cannot be found for change", changeUser.Id));

            _userMapper.Map(changeUser, user);
            return RetrieveAll();
        }

        /// <summary>
        /// Remove user from repository
        /// </summary>
        /// <param name="deleteUser"></param>
        /// <returns></returns>
        public IResponse<List<User>> Delete(DeleteUser deleteUser)
        {
            var user = _userRepository.GetById(deleteUser.Id);
            if (user == null) return Response<List<User>>.Failed(string.Format("User with Id:{0} cannot be found for deletion", deleteUser.Id));

            _userRepository.Remove(user);
            return RetrieveAll();
        }

        public IResponse<List<User>> Search(SearchUser filter)
        {
            var users = _userRepository.Find(user =>
                Extension.Contains(user.Forename, filter.Forename, StringComparison.InvariantCultureIgnoreCase)
                || Extension.Contains(user.Surname, filter.Surname, StringComparison.InvariantCultureIgnoreCase)
                || user.Id.Equals(filter.Id ?? 0));

            return Response<List<User>>.Success(_userMapper.Map(users));
        }

        #endregion
    }
}