using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace BL
{
    public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(DbContext context) : base(context)
        {

        }
    }
}
