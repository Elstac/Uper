using WebApp.Data;
using WebApp.Models;
using Xunit;

namespace Tests.ViewModelGeneratorsTests
{
    public class ApplicationUserViewModelCreatorTests
    {
        private ApplicationUserViewModelGenerator generator;

        public ApplicationUserViewModelCreatorTests()
        {
            generator = new ApplicationUserViewModelGenerator();
        }
        [Fact]
        public void CreateCorrectViewModel()
        {
            var dataModel = new ApplicationUser
            {
                UserName = "username",
                Name = "name",
                Surname = "surname",
                Email = "email",
                PhoneNumber = "666",
                Rating = 0,
                NumOfVote = 10,
                Description = "description",
                EmailConfirmed = false
            };

            var vm = generator.ConvertAppUserToViewModel(dataModel);

            Assert.Equal("username", vm.UserName);
            Assert.Equal("name", vm.Name);
            Assert.Equal("surname", vm.Surname);
            Assert.Equal("email", vm.Email);
            Assert.Equal("666", vm.PhoneNumber);
            Assert.Equal(0, vm.Rating);
            Assert.Equal(10, vm.NumOfVotes);
            Assert.False(vm.Confirmed);
        }
    }
}
