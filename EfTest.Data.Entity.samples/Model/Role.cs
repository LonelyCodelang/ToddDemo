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
    /// 用户角色
    /// </summary>
    public class Role : EntityBase<long>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [StringLength(50)]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色说明
        /// </summary>
        [StringLength(100)]
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
