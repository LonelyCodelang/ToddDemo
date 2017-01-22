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
    /// 部门信息
    /// </summary>
    public class Department : EntityBase<long>
    {
        /// <summary>
        /// 部门名
        /// </summary>
        [StringLength(100)]
        public string DepartName { get; set; }

        /// <summary>
        /// 部门说明
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
    }
}
