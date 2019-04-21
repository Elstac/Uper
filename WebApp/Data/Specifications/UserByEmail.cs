namespace WebApp.Data.Specifications
{
    public class UserByEmail:BaseSpecification<ApplicationUser>
    {
        public UserByEmail(string email):base(
            user => user.Email == email)
        {

        }
    }
}
