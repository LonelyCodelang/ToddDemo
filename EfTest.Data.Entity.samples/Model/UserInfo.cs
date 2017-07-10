using EfTest.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : EntityBase<long>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [StringLength(100)]
        public string UserName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [StringLength(100)]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户部门信息
        /// </summary>
        public virtual Department Department { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
