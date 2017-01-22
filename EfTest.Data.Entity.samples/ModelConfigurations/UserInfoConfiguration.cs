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
            HasOptional(m => m.Department).WithMany();
        }
    }
}
