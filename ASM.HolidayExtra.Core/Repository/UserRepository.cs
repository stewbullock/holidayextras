using ASM.Core.Repository;
using ASM.HolidayExtra.Core.Model;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Core.Repository
{
    class UserRepository : Repository<User>, IUserRepository
    {
        #region Methods 

        #region Constructor 

        public UserRepository(List<User> entities)
            : base(entities)
        { }

        #endregion

        #endregion
    }
}