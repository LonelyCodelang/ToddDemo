using EfTest.Data.Entity.samples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.ModelConfigurations
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRoleConfigurations : EntityConfigurationBase<UserRole, long>
    {
        public UserRoleConfigurations()
        {
            this.HasRequired(x => x.user).WithMany(m => m.UserRoles).HasForeignKey(o => o.roleId);
            this.HasRequired(x => x.role).WithMany(m => m.UserRoles).HasForeignKey(o => o.userId);
        }
    }
}
