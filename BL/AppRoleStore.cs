using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.IdentityEntities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL
{
    public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>
    {
        public AppRoleStore(DbContext context) : base(context)
        {
        }
    }
}
