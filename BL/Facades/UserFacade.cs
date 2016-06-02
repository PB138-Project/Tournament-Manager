using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BL.DTO;
using DAL;
using DAL.Entities;
using DAL.IdentityEntities;
using Microsoft.AspNet.Identity;

namespace BL.Facades
{
    public class UserFacade
    {
        public void Register(UserDTO user)
        {
            var userManager = new AppUserManager(new AppUserStore(new AppDbContext()));

            AppUser appUser = Mapping.Mapper.Map<AppUser>(user);

            userManager.Create(appUser, user.Password);

            var ourUser = userManager.FindByName(appUser.UserName);


            if (user.SecretCode != null)
            {
                if (user.SecretCode.Equals("CubaLibre"))
                {
                    var roleManager = new AppRoleManager(new AppRoleStore(new AppDbContext()));

                    if (!roleManager.RoleExists("Admin"))
                    {
                        roleManager.Create(new AppRole { Name = "Admin" });
                    }

                    userManager.AddToRole(ourUser.Id, "Admin");
                }
            }
        }

        public ClaimsIdentity Login(string email, string password)
        {
            var userManager = new AppUserManager(new AppUserStore(new AppDbContext()));

            var wantedUser = userManager.FindByEmail(email);

            if (wantedUser == null)
            {
                return null;
            }

            AppUser user = userManager.Find(wantedUser.UserName, password);

            if (user == null)
            {
                return null;
            }

            return userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
