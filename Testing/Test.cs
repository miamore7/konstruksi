using Xunit;
using LaptopInventory;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Testing
{
    public class LoginServiceTests
    {
        private List<User> DummyUsers => new List<User>
        {
            new User { Username = "admin", Password = "123", Role = "admin" },
            new User { Username = "user", Password = "456", Role = "user" }
        };

        // Test 1: Login sukses
        [Fact]
        public void Should_LoginSuccessfully_When_CredentialsCorrect()
        {
            var service = new LoginServiceMock(DummyUsers);
            bool result = service.Login("admin", "123");

            Assert.True(result);
            Assert.Equal("admin", service.LoggedInUser.Username);
        }

        // Test 2: Login gagal
        [Fact]
        public void Should_FailLogin_When_CredentialsWrong()
        {
            var service = new LoginServiceMock(DummyUsers);
            bool result = service.Login("admin", "wrong");

            Assert.False(result);
            Assert.Null(service.LoggedInUser);
        }

        // Test 3: Logout menghapus sesi
        [Fact]
        public void Should_LogoutSuccessfully()
        {
            var service = new LoginServiceMock(DummyUsers);
            service.Login("user", "456");
            service.Logout();

            Assert.Null(service.LoggedInUser);
        }

        //// Test 4: Tambah user baru
        //[Fact]
        //public void Should_AddNewUser_When_UsernameIsUnique()
        //{
        //    var service = new LoginServiceMock(new List<User>());
        //    var user = new User { Username = "baru", Password = "abc", Role = "user" };

        //    service.AddUser(user);

        //    Assert.Single(service.users);
        //    Assert.Equal("baru", service.users[0].Username);
        //}

        // Test 5: Handler role
        [Fact]
        public void Should_ReturnAdminDashboard_ForAdminRole()
        {
            var service = new LoginServiceMock(DummyUsers);
            var result = service.HandleByRole("admin");

            Assert.Equal("Dashboard Admin", result);
        }

        //// Test 6: Sortir user by username
        //[Fact]
        //public void Should_SortUsersByUsernameAsc()
        //{
        //    var service = new LoginServiceMock(new List<User>
        //    {
        //        new User { Username = "zeta" },
        //        new User { Username = "alpha" }
        //    });

        //    var sorted = service.GetSortedUsersByUsername();

        //    Assert.Equal("alpha", sorted[0].Username);
        //    Assert.Equal("zeta", sorted[1].Username);
        //}

        // Test 7: Filter item by kategori
        [Fact]
        public void Should_FilterItemsByCategory()
        {
            var service = new LoginServiceMock(DummyUsers);
            var items = service.GetItems("HP");

            Assert.All(items, item => Assert.Equal("HP", item.Category));
        }

        // Test 8: Validasi login dengan input kosong (DBC)
        [Fact]
        public void Should_Throw_When_UsernameIsEmpty()
        {
            var service = new LoginServiceMock(DummyUsers);
            Assert.Throws<ArgumentException>(() => service.Login("", "123"));
        }
    }

    // Mock service tanpa file JSON
    public class LoginServiceMock : LoginService
    {
        public new List<User> users;

        public LoginServiceMock(List<User> mockUsers)
        {
            users = mockUsers;
        }

        public new void AddUser<T>(T user) where T : User
        {
            base.AddUser(user);
        }

        public new List<User> GetSortedUsersByUsername()
        {
            return base.GetSortedUsersByUsername();
        }

        public new List<Item> GetItems(string category = null, bool sortByName = false)
        {
            return base.GetItems(category, sortByName);
        }
    }
}