using EfTest.Data.Entity.samples.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.Repository
{
    public class DepartmentRepository : Repository<Department, long>
    {
        public DepartmentRepository(IDbContextTypeResolver contextTypeResolver) : base(contextTypeResolver)
        {
        }
    }
}
