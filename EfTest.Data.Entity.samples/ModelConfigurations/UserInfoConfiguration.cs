using EfTest.Data.Entity.samples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.ModelConfigurations
{
    public class UserInfoConfiguration : EntityConfigurationBase<UserInfo, long>
    {
        /// <summary>
        /// 初始化一个<see cref="UserConfiguration"/>类型的新实例
        /// </summary>
        public UserInfoConfiguration()
        {
            //如果A:B = 1:N，我们使用 WithMany。
            //如果A:B= 1:1，我们使用 WithOptional。1:1的关联要求外键的非空和唯一，数据库是通过表B的外键作为主键来实现 。 

            //HasOptional允许UserInfo单独存在，这将在UserInfo表中生成可空的外键
            //1:N（外键可空）
            // HasOptional(m => m.Department).WithMany();

            //HasRequired不允许UserInfo单独存在，这将在UserInfo表中生成非空的外键。
            //1:N（外键不可空）
            HasRequired(m => m.Department).WithMany();
        }
    }
}
