using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence.Identity
{
    public class AppUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AppUser(string firstName, string lastName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            UserName = email;
        }
    }
}