using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSharp.DataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IServicesBuilder builder = new ServicesBuilder();
            IServiceCollection services = builder.Build();
            services.AddDataServices();
            IFrameworkInitializer initializer = new FrameworkInitializer();
            initializer.Initialize(new MvcAutofacIocBuilder(services));
        }
    }
}
