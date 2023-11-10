using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using CenterBookingSystem.Controllers;
using CenterBookingSystem.Data;
using CenterBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnit.Framework;


namespace CenterBookingSystem.Tests
{
    [TestFixture]
    public class CenterBookingSystemTest
    {
        private DbContextOptions<BookingDbContext> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<BookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Add test data to the in-memory database
                var ride = new EventSpace
                {
                    SpaceID = 1,
                    Name = "Demo",
                    //Name = DateTime.Parse("2023-08-30"),
                    Capacity = 10,
                    Availability = true
                };

                dbContext.EventSpaces.Add(ride);
                dbContext.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Clear the in-memory database after each test
                dbContext.Database.EnsureDeleted();
            }
        }


        [Test]
        public void CreateNewBooking_ValidDetails_JoinsSuccessfully_EventSpaceTable()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });

                //var student = new Booking
                //{
                //    Name = "John Doe",
                //    Email = "johndoe@example.com",
                //};

                // Act
                var result = method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });

                // Assert
                Assert.IsNotNull(result);

                var ride = dbContext.EventSpaces.Include(r => r.Bookings).FirstOrDefault(r => r.SpaceID == 1);
                //Assert.IsNotNull(ride);
                Console.WriteLine(ride.Bookings.Count);
                Assert.AreEqual(1, ride.Bookings.Count);
            }
        }

        [Test]
        public void CreateNewBooking_ValidDetails_JoinsSuccessfully_BookingTable()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });

                //var student = new Student
                //{
                //    Name = "John Doe",
                //    Email = "johndoe@example.com",
                //};

                // Act
                var result = method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });

                var ride = dbContext.Bookings.FirstOrDefault(r => r.SpaceID == 1);
                Assert.IsNotNull(ride);
                Assert.AreEqual(1, ride.SpaceID);
            }
        }

        [Test]
        public void CreateNewBooking_ValidDetails_JoinsSuccessfully_BookingTable1()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });

                //var student = new Student
                //{
                //    Name = "John Doe",
                //    Email = "johndoe@example.com",
                //};

                // Act
                var result = method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });

                var ride = dbContext.Bookings.FirstOrDefault(r => r.SpaceID == 1);
                Assert.IsNotNull(ride);
                Assert.AreEqual(DateTime.Parse("2023-08-30"), ride.EventDate);
                Assert.AreEqual(TimeSpan.Parse("10:00"), ride.TimeSlot);
            }
        }

        //[Test]
        //public void ClassEnrollmentForm_Returns_EnrollmentConfirmation_Action()
        //{
        //    using (var dbContext = new BookingDbContext(_dbContextOptions))
        //    {
        //        // Arrange
        //        var controllerType = typeof(BookingController);
        //        var controller = Activator.CreateInstance(controllerType, dbContext);

        //        // Specify the method signature (parameter types)
        //        MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });

        //        //var student = new Student
        //        //{
        //        //    Name = "John Doe",
        //        //    Email = "johndoe@example.com",
        //        //};

        //        // Act
        //        var result = method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });

        //        //var ride = dbContext.Students.FirstOrDefault(r => r.StudentID == 1);
        //        Console.WriteLine(result);
        //        Assert.IsNotNull(result);
        //        //Assert.AreEqual("EnrollmentConfirmation", result.ActionName);

        //        //if (result is IActionResult redirectToAction)
        //        //{
        //        //    Console.WriteLine("dai");
        //        //    Assert.AreEqual("EnrollmentConfirmation", redirectToAction.ActionName);
        //        //}
        //    }
        //}

        //[Test]
        //public void EnrollmentConfirmation_InvalidID_Returns_NotFound()
        //{
        //    using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        //    {
        //        // Arrange
        //        var controllerType = typeof(BookingController);
        //        var controller = Activator.CreateInstance(controllerType, dbContext);

        //        // Specify the method signature (parameter types)
        //        MethodInfo method = controllerType.GetMethod("ClassEnrollmentForm", new[] { typeof(int), typeof(string), typeof(string) });

        //        MethodInfo method1 = controllerType.GetMethod("EnrollmentConfirmation", new[] { typeof(int) });

        //        var student = new Student
        //        {
        //            Name = "John Doe",
        //            Email = "johndoe@example.com",
        //        };

        //        // Act
        //        var result = method.Invoke(controller, new object[] { 1, "John Doe", "johndoe@example.com" });
        //        var result1 = method1.Invoke(controller, new object[] { 2 }) as NotFoundResult;

        //        var ride = dbContext.Students.FirstOrDefault(r => r.StudentID == 1);

        //        Assert.IsNotNull(result1);
        //        //Assert.AreEqual("EnrollmentConfirmation", result1.ActionName);
        //    }
        //}


        //[Test]
        //public void EnrollmentConfirmation_validID_Returns_View()
        //{
        //    using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        //    {
        //        // Arrange
        //        var controllerType = typeof(BookingController);
        //        var controller = Activator.CreateInstance(controllerType, dbContext);

        //        // Specify the method signature (parameter types)
        //        MethodInfo method = controllerType.GetMethod("ClassEnrollmentForm", new[] { typeof(int), typeof(string), typeof(string) });

        //        MethodInfo method1 = controllerType.GetMethod("EnrollmentConfirmation", new[] { typeof(int) });

        //        var student = new Student
        //        {
        //            Name = "John Doe",
        //            Email = "johndoe@example.com",
        //        };

        //        // Act
        //        var result = method.Invoke(controller, new object[] { 1, "John Doe", "johndoe@example.com" });
        //        var result1 = method1.Invoke(controller, new object[] { 1 }) as ViewResult;

        //        var ride = dbContext.Students.FirstOrDefault(r => r.StudentID == 1);

        //        Assert.IsNotNull(result1);
        //    }
        //}


        [Test]
        public void AvailableSpaces_Method_Exists_EventSpaceController()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(EventSpaceController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("AvailableSpaces");
                Assert.IsNotNull(method);
            }
        }

        [Test]
        public void Index_Method_Exists_BookingController_parameter()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("Index");
                Assert.IsNotNull(method);
            }
        }

        [Test]
        public void Create_Method_Exists_BookingController_4_parameter()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });
                Assert.IsNotNull(method);
            }
        }

        [Test]
        public void Confirmation_Method_Exists_BookingController_1_parameter()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);



                // Specify the method signature (parameter types)
                MethodInfo method = controllerType.GetMethod("Confirmation", new[] { typeof(int) });
                Assert.IsNotNull(method);
            }
        }
        ////// [Test]
        ////// public void JoinRide_InvalidCommuter_ModelStateInvalid()
        ////// {
        //////     using (var dbContext = new RideSharingDbContext(_dbContextOptions))
        //////     {
        //////         // Arrange
        //////         var slotController = new SlotController(dbContext);
        //////         var commuter = new Commuter(); // Invalid commuter with missing required fields

        //////         // Act
        //////         slotController.ModelState.AddModelError("Name", "Name is required");
        //////         var result = slotController.JoinRide(1, commuter) as ViewResult;

        //////         // Assert
        //////         Assert.IsNotNull(result);
        //////         Assert.AreEqual("", result.ViewName); // Returns the same view
        //////         Assert.IsFalse(result.ViewData.ModelState.IsValid);
        //////         Assert.AreEqual(1, result.ViewData.ModelState.ErrorCount);
        //////         Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Name"));
        //////     }
        ////// }

        [Test]
        public void Confirmation_InvaalidID_ReturnsNotFoundResult()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                MethodInfo method1 = controllerType.GetMethod("Confirmation", new[] { typeof(int) });
                var result = method1.Invoke(controller, new object[] { 1 }) as NotFoundResult;
                Assert.IsNotNull(result);
            }
        }

        [Test]
        public void Confirmation_ValidID_Returns_ViewResule_Booking()
        {
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                // Arrange
                var controllerType = typeof(BookingController);
                var controller = Activator.CreateInstance(controllerType, dbContext);

                MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });
                var result1 = method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });

                // Specify the method signature (parameter types)
                MethodInfo method1 = controllerType.GetMethod("Confirmation", new[] { typeof(int) });
                var result = method1.Invoke(controller, new object[] { 1 }) as ViewResult;
                Assert.IsNotNull(result);
            }
        }

        //[Test]
        //public void ClassEnrollmentForm_ThrowsException_fullyBooked_Class()
        //{
        //    using (var dbContext = new BookingDbContext(_dbContextOptions))
        //    {
        //        // Arrange
        //        var controllerType = typeof(BookingController);
        //        var controller = Activator.CreateInstance(controllerType, dbContext);

        //        MethodInfo method = controllerType.GetMethod("Create", new[] { typeof(int), typeof(DateTime), typeof(TimeSpan), typeof(string) });

        //        // Simulate booking for a fully booked class
        //        var result1 = method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });

        //        // Check the database state
        //        var ride = dbContext.Bookings.FirstOrDefault(r => r.SpaceID == 1);
        //        Console.WriteLine(ride.EventDate);
        //        var res2=method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });
        //        Console.WriteLine("jkl"+res2);

        //        // Attempt to book the same class again, which should throw an exception
        //        var exception = Assert.Throws<TargetInvocationException>(() =>
        //        {
        //            method.Invoke(controller, new object[] { 1, DateTime.Parse("2023-08-30"), TimeSpan.Parse("10:00"), "123" });
        //        });

        //        // Check the exception message and type
        //        Console.WriteLine(exception.Message);
        //        Assert.IsInstanceOf<EventBookingException>(exception.InnerException);
        //    }
        //}



        //[Test]
        //public void ClassEnrollmentForm_ThrowsException_fullyBooked_Class_With_Message()
        //{
        //    using (var dbContext = new BookingDbContext(_dbContextOptions))
        //    {
        //        // Arrange
        //        var controllerType = typeof(BookingController);
        //        var controller = Activator.CreateInstance(controllerType, dbContext);

        //        MethodInfo method = controllerType.GetMethod("ClassEnrollmentForm", new[] { typeof(int), typeof(string), typeof(string) });

        //        // Act and Assert for the first scenario

        //        var result = method.Invoke(controller, new object[] { 1, "John Doe", "johndoe@example.com" }) as RedirectToActionResult;

        //        // Assert for the first scenario
        //        Assert.IsNotNull(result);

        //        // Act and Assert for the second scenario
        //        MethodInfo method1 = controllerType.GetMethod("ClassEnrollmentForm", new[] { typeof(int) });

        //        var exception = Assert.Throws<TargetInvocationException>(() =>
        //        {
        //            method1.Invoke(controller, new object[] { 1 });
        //        });

        //        // Assert that the inner exception is of type KathakClassBookingException
        //        Assert.IsInstanceOf<KathakClassBookingException>(exception.InnerException);
        //        Assert.AreEqual("Class is fully booked.", exception.InnerException.Message);
        //    }
        //}

        //////     [Test]
        ////// public void JoinRide_DestinationSameAsDeparture_ReturnsViewWithValidationError()
        ////// {
        //////     using (var dbContext = new RideSharingDbContext(_dbContextOptions))
        //////     {
        //////         // Arrange
        //////         var slotController = new SlotController(dbContext);
        //////         var commuter = new Commuter
        //////         {
        //////             Name = "John Doe",
        //////             Email = "johndoe@example.com",
        //////             Phone = "1234567890"
        //////         };

        //////         // Act
        //////         var ride = dbContext.Rides.FirstOrDefault(r => r.RideID == 1);
        //////         ride.Destination = ride.DepartureLocation; // Set the destination as the same as departure
        //////         dbContext.SaveChanges();

        //////         var result = slotController.JoinRide(1, commuter) as ViewResult;

        //////         // Assert
        //////         Assert.IsNotNull(result);
        //////         Assert.IsFalse(result.ViewData.ModelState.IsValid);
        //////         Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Destination"));
        //////     }
        ////// }

        ////// [Test]
        ////// public void JoinRide_MaximumCapacityNotPositiveInteger_ReturnsViewWithValidationError()
        ////// {
        //////     using (var dbContext = new RideSharingDbContext(_dbContextOptions))
        //////     {
        //////         // Arrange
        //////         var slotController = new SlotController(dbContext);
        //////         var commuter = new Commuter
        //////         {
        //////             Name = "John Doe",
        //////             Email = "johndoe@example.com",
        //////             Phone = "1234567890"
        //////         };

        //////         // Act
        //////         var ride = dbContext.Rides.FirstOrDefault(r => r.RideID == 1);
        //////         ride.MaximumCapacity = -5; // Set a negative value for MaximumCapacity
        //////         dbContext.SaveChanges();

        //////         var result = slotController.JoinRide(1, commuter) as ViewResult;

        //////         // Assert
        //////         Assert.IsNotNull(result);
        //////         Assert.IsFalse(result.ViewData.ModelState.IsValid);
        //////         Assert.IsTrue(result.ViewData.ModelState.ContainsKey("MaximumCapacity"));
        //////     }
        ////// }


        [Test]
        public void EventSpace_ClassExists()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventSpace";
            Assembly assembly = Assembly.Load(assemblyName);
            Type rideType = assembly.GetType(typeName);
            Assert.IsNotNull(rideType);
            var ride = Activator.CreateInstance(rideType);
            Assert.IsNotNull(ride);
        }

        [Test]
        public void Booking_ClassExists()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.Booking";
            Assembly assembly = Assembly.Load(assemblyName);
            Type rideType = assembly.GetType(typeName);
            Assert.IsNotNull(rideType);
            var ride = Activator.CreateInstance(rideType);
            Assert.IsNotNull(ride);
        }
        [Test]
        public void EventBookingException_ClassExists()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventBookingException";
            Assembly assembly = Assembly.Load(assemblyName);
            Type rideType = assembly.GetType(typeName);
            Assert.IsNotNull(rideType);
        }



        [Test]
        public void BookingDbContextContainsDbSetEventSpacesProperty()
        {
            // var context = new ApplicationDbContext();
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                var propertyInfo = dbContext.GetType().GetProperty("EventSpaces");

                Assert.IsNotNull(propertyInfo);
                Assert.AreEqual(typeof(DbSet<EventSpace>), propertyInfo.PropertyType);
            }
        }

        [Test]
        public void BookingDbContextContainsDbSetBookingsProperty()
        {
            // var context = new ApplicationDbContext();
            using (var dbContext = new BookingDbContext(_dbContextOptions))
            {
                var propertyInfo = dbContext.GetType().GetProperty("Bookings");
                Assert.IsNotNull(propertyInfo);
                Assert.AreEqual(typeof(DbSet<Booking>), propertyInfo.PropertyType);
            }
        }


        [Test]
        public void EventSpace_Properties_SpaceID_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventSpace";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("SpaceID");
            Assert.IsNotNull(propertyInfo, "The property 'SpaceID' was not found on the EventSpace class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), propertyType, "The data type of 'SpaceID' property is not as expected (int).");
        }

        [Test]
        public void EventSpaces_Properties_Name_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventSpace";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("Name");
            Assert.IsNotNull(propertyInfo, "The property 'Name' was not found on the EventSpace class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), propertyType, "The data type of 'Name' property is not as expected (string).");
        }
        [Test]
        public void EventSpaces_Properties_Capacity_ReturnExpectedDataTypes_DateTime()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventSpace";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("Capacity");
            Assert.IsNotNull(propertyInfo, "The property 'Capacity' was not found on the EventSpaces class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), propertyType, "The data type of 'Capacity' property is not as expected (int).");
        }
        [Test]
        public void EventSpaces_Properties_Availability_ReturnExpectedDataTypes_Bool()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventSpace";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("Availability");
            Assert.IsNotNull(propertyInfo, "The property 'Availability' was not found on the EventSpace class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(bool), propertyType, "The data type of 'Availability' property is not as expected (Student).");
        }
        [Test]
        public void EventSpaces_Properties_Bookings_ReturnExpectedDataTypes_ICollection_Booking()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.EventSpace";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("Bookings");
            Assert.IsNotNull(propertyInfo, "The property 'Availability' was not found on the EventSpace class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(ICollection<Booking>), propertyType, "The data type of 'Bookings' property is as expected (ICollection_Booking).");
        }

        [Test]
        public void Booking_Properties_SpaceID_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.Booking";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("SpaceID");
            Assert.IsNotNull(propertyInfo, "The property 'StudentID' was not found on the Commuter class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), propertyType, "The data type of 'StudentID' property is not as expected (int).");
        }
        [Test]
        public void Booking_Properties_TimeSlot_ReturnExpectedDataTypes_TimeSpan()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.Booking";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("TimeSlot");
            Assert.IsNotNull(propertyInfo, "The property 'Name' was not found on the Commuter class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(TimeSpan), propertyType, "The data type of 'Name' property is not as expected (string).");
        }
        [Test]
        public void Booking_Properties_EventDate_ReturnExpectedDataTypes_DateTime()
        {
            string assemblyName = "CenterBookingSystem";
            string typeName = "CenterBookingSystem.Models.Booking";
            Assembly assembly = Assembly.Load(assemblyName);
            Type commuterType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = commuterType.GetProperty("EventDate");
            Assert.IsNotNull(propertyInfo, "The property 'EventDate' was not found on the Commuter class.");
            Type propertyType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(DateTime), propertyType, "The data type of 'EventDate' property is not as expected (EventDate).");
        }

        [Test]
        public void Test_ConfirmationViewFile_Exists_Booking()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/CenterBookingSystem/Views/Booking/", "Confirmation.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }

        [Test]
        public void Test_CreateViewFile_Exists_Booking()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/CenterBookingSystem/Views/Booking/", "Create.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }

        [Test]
        public void Test_IndexViewFile_Exists_Booking()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/CenterBookingSystem/Views/Booking/", "Index.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }
        [Test]
        public void Test_AvailableViewFile_Exists_EvantSpace()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/CenterBookingSystem/Views/EventSpace/", "AvailableSpaces.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }

        //[Test]
        //public void Student_Properties_ClassID_ReturnExpectedDataTypes_int()
        //{
        //    string assemblyName = "CenterBookingSystem";
        //    string typeName = "CenterBookingSystem.Models.Student";
        //    Assembly assembly = Assembly.Load(assemblyName);
        //    Type commuterType = assembly.GetType(typeName);
        //    PropertyInfo propertyInfo = commuterType.GetProperty("ClassID");
        //    Assert.IsNotNull(propertyInfo, "The property 'ClassID' was not found on the Student class.");
        //    Type propertyType = propertyInfo.PropertyType;
        //    Assert.AreEqual(typeof(int), propertyType, "The data type of 'ClassID' property is not as expected (int).");
        //}

        //[Test]
        //public void Student_Properties_Class_ReturnExpectedDataTypes_Class()
        //{
        //    string assemblyName = "CenterBookingSystem";
        //    string typeName = "CenterBookingSystem.Models.Student";
        //    Assembly assembly = Assembly.Load(assemblyName);
        //    Type commuterType = assembly.GetType(typeName);
        //    PropertyInfo propertyInfo = commuterType.GetProperty("Class");
        //    Assert.IsNotNull(propertyInfo, "The property 'Class' was not found on the Student class.");
        //    Type propertyType = propertyInfo.PropertyType;
        //    Assert.AreEqual(typeof(Class), propertyType, "The data type of 'Class' property is not as expected (Class).");
        //}

        ////[Test]
        ////public void Ride_Properties_MaximumCapacity_ReturnExpectedDataTypes()
        ////{
        ////    // Arrange
        ////    Ride ride = new Ride();

        ////    Assert.That(ride.MaximumCapacity, Is.TypeOf<int>());
        ////}

        ////[Test]
        ////public void Commuter_Ride_ReturnsExpectedValue()
        ////{
        ////    // Arrange
        ////    Ride expectedRide = new Ride { RideID = 2 };
        ////    Commuter commuter = new Commuter { Ride = expectedRide };

        ////    // Assert
        ////    Assert.AreEqual(expectedRide, commuter.Ride);
        ////}

        ////[Test]
        ////public void Commuter_Properties_CommuterID_ReturnExpectedValues()
        ////{
        ////    // Arrange
        ////    int expectedCommuterID = 1;

        ////    // Act
        ////    Commuter commuter = new Commuter
        ////    {
        ////        CommuterID = expectedCommuterID,
        ////    };

        ////    // Assert
        ////    Assert.AreEqual(expectedCommuterID, commuter.CommuterID);
        ////}

        ////[Test]
        ////public void Commuter_Properties_Name_ReturnExpectedValues()
        ////{

        ////    string expectedName = "John Doe";

        ////    // Act
        ////    Commuter commuter = new Commuter
        ////    {
        ////        Name = expectedName,
        ////    };

        ////    // Assert
        ////    Assert.AreEqual(expectedName, commuter.Name);
        ////}

        ////[Test]
        ////public void Commuter_Properties_Email_ReturnExpectedValues()
        ////{
        ////    string expectedEmail = "john@example.com";

        ////    // Act
        ////    Commuter commuter = new Commuter
        ////    {
        ////        Email = expectedEmail,
        ////    };

        ////    Assert.AreEqual(expectedEmail, commuter.Email);
        ////}

        ////[Test]
        ////public void Commuter_Properties_Phone_ReturnExpectedValues()
        ////{

        ////    string expectedPhone = "1234567890";

        ////    // Act
        ////    Commuter commuter = new Commuter
        ////    {
        ////        Phone = expectedPhone,
        ////    };

        ////    Assert.AreEqual(expectedPhone, commuter.Phone);
        ////}

        ////[Test]
        ////public void Commuter_Properties_RideID_ReturnExpectedValues()
        ////{
        ////    int expectedRideID = 2;

        ////    // Act
        ////    Commuter commuter = new Commuter
        ////    {
        ////        RideID = expectedRideID
        ////    };
        ////    Assert.AreEqual(expectedRideID, commuter.RideID);
        ////}

        ////[Test]
        ////public void test_case12()
        ////{
        ////    using (var dbContext = new RideSharingDbContext(_dbContextOptions))
        ////    {
        ////        // Arrange
        ////        var slotController = new SlotController(dbContext);
        ////        var commuter = new Commuter
        ////        {
        ////            Name = "John Doe",
        ////            Email = "johndoe@example.com",
        ////            Phone = "1234567890"
        ////        };

        ////        // Act
        ////        var ride = dbContext.Rides.FirstOrDefault(r => r.RideID == 1);
        ////        ride.Destination = ride.DepartureLocation; // Set the destination as the same as departure
        ////        dbContext.SaveChanges();

        ////        var result = slotController.JoinRide(1, commuter) as ViewResult;

        ////        // Assert
        ////        Assert.IsNotNull(result);
        ////        Assert.IsFalse(result.ViewData.ModelState.IsValid);
        ////        Assert.IsTrue(result.ViewData.ModelState.ContainsKey("Destination"));
        ////    }
        ////}

        ////[Test]
        ////public void test_case13()
        ////{
        ////    using (var dbContext = new RideSharingDbContext(_dbContextOptions))
        ////    {
        ////        // Arrange
        ////        var slotController = new SlotController(dbContext);
        ////        var commuter = new Commuter
        ////        {
        ////            Name = "John Doe",
        ////            Email = "johndoe@example.com",
        ////            Phone = "1234567890"
        ////        };

        ////        // Act
        ////        var ride = dbContext.Rides.FirstOrDefault(r => r.RideID == 1);
        ////        ride.MaximumCapacity = -5; // Set a negative value for MaximumCapacity
        ////        dbContext.SaveChanges();

        ////        var result = slotController.JoinRide(1, commuter) as ViewResult;

        ////        // Assert
        ////        Assert.IsNotNull(result);
        ////        Assert.IsFalse(result.ViewData.ModelState.IsValid);
        ////        Assert.IsTrue(result.ViewData.ModelState.ContainsKey("MaximumCapacity"));
        ////    }
        ////}
    }

}