using ASM.Core;
using ASM.HolidayExtra.CompositionRoot;
using ASM.HolidayExtra.Service;
using ASM.HolidayExtra.Service.Model;
using System;
using System.Collections.Generic;

namespace ASM.HolidayExtra.ConsoleTest
{
    class Program
    {
        static void Main()
        {
            var dependencyResolver = DependencyResolver.Create();
            var userService = dependencyResolver.Get<IUserService>();

            // List all users
            var response = userService.Retrieve();
            WriteResponse(response);

            // Change user and list
            response = userService.Change(new ChangeUser
            {
                Id = 1,
                ChangedBy = "Stew Bullock",
                Forename = "Stew",
                Surname = "Bullock 1",
                Email = "MyTestEmail"
            });
            WriteResponse(response);

            // Track new user and list
            response = userService.Create(new TrackUser
            {
                Forename = "Jack",
                Surname = "Smith",
                Email = "Jack.Smith@Test.co.uk",
                CreatedBy = "stbk"
            });
            WriteResponse(response);

            response = userService.Create("<?xml version=\"1.0\" encoding=\"utf - 8\"?><TrackUser><Forename>Jim</Forename><Surname>Beam</Surname><Email>jim.beam@test.co.uk</Email><CreatedBy>Test User 44</CreatedBy></TrackUser>");
            WriteResponse(response);

            // Delete user and list.
            response = userService.Delete(new DeleteUser
            {
                Id = 2,
                ChangedBy = "stbk"
            });
            WriteResponse(response);



            // Find user
            response = userService.Search(new SearchUser
            {
                Surname = "smith"
            });
            WriteResponse(response);

            Console.ReadKey();


        }

        private static void WriteResponse(IResponse<List<User>> response)
        {
            Console.WriteLine(response.ResponseState);
            if (response.ResponseState == ResponseState.Failed)
            {
                Console.WriteLine(response.Aggregate());
            }
            else
            {
                response.ResponseObject.ForEach(user => Console.WriteLine("({0}) {1}, {2} [{3}] - Created: {4} by {5}, Changed: {6} by {7}", user.Id, user.Surname, user.Forename, user.Email, user.Created, user.CreatedBy, user.Changed, user.ChangedBy));
            }
        }
    }
}