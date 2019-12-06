using ASM.Core.Dependency;
using ASM.HolidayExtra.Core.Domain;
using ASM.HolidayExtra.Core.Repository;
using ASM.HolidayExtra.Core.Service;
using ASM.HolidayExtra.Service;
using Ninject.Modules;
using System;
using System.Collections.Generic;

namespace ASM.HolidayExtra.Core.Dependency
{
    public class DependencyModule : NinjectModule, IDependencyModule
    {
        #region Methods 

        #region Constructor 

        private DependencyModule()
        { }

        #endregion

        public static INinjectModule Create()
        {
            return new DependencyModule();
        }

        public override void Load()
        {
            BindServices();
            BindDomain();
            BindRepositories();
        }

        private void BindServices()
        {
            Bind<IUserService>().To<UserService>();
        }

        private void BindDomain()
        {
            Bind<IUserResolver>().To<UserResolver>();
            Bind<IUserMapper>().To<UserMapper>();
        }

        private void BindRepositories()
        {
            Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("entities", CreateUsers());
        }

        private List<Model.User> CreateUsers()
        {
            return new List<Model.User>
            {
                new Model.User
                {
                    Id = 1,
                    Forename = "Stewart",
                    Surname = "Bullock",
                    Email = "stewart.bullock@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 13:42"),
                    CreatedBy = "stbk",
                    Changed = DateTime.Parse("2 Dec 2019 09:15"),
                    ChangedBy = "Stew"
                },
                new Model.User
                {
                    Id = 2,
                    Forename = "Joe",
                    Surname = "Bloggs",
                    Email = "joe.bloggs@test.co.uk",
                    Created = DateTime.Parse("1 Dec 2019 14:00"),
                    CreatedBy = "stbk",
                    Changed = DateTime.Parse("2 Dec 2019 17:05"),
                    ChangedBy = "Stew"
                }
            };
        }

        #endregion
    }
}