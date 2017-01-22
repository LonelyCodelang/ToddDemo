using EfTest.Data.Entity.samples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.ModelConfigurations
{
    public class DepartmentConfiguration : EntityConfigurationBase<Department, long>
    {
        public DepartmentConfiguration()
        {

        }
    }
}
