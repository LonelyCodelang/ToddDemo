using EfTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.Model
{
    /// <summary>
    /// 角色用户关系表
    /// </summary>
    public class UserRole : EntityBase<long>
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo user { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long userId { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public Role role { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public long roleId { get; set; }

    }
}
