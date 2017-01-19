using EfTest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples.Model
{
    public class UserInfo : EntityBase<long>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }
    }
}
