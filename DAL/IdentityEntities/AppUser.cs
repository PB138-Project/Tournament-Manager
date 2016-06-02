using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.IdentityEntities
{
    public class AppUserRole : IdentityUserRole<int>
    {

    }

    public class AppRole : IdentityRole<int, AppUserRole>
    {

    }

    public class AppUserClaim : IdentityUserClaim<int>
    {

    }
    public class AppUserLogin : IdentityUserLogin<int>
    {
    }
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
    }
}
