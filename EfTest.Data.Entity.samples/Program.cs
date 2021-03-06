﻿using EfTest.Core.Initialize;
using EfTest.Data.Entity.samples.Model;
using EfTest.Data.Entity.samples.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EfTest.Data.Entity.samples
{
    class Program
    {
        static void Main(string[] args)
        {

            //IServicesBuilder builder = new ServicesBuilder();
            //IServiceCollection services = builder.Build();
            //services.AddDataServices();


            //IFrameworkInitializer initializer = new IFrameworkInitializer();
            //initializer.Initialize(new MvcAutofacIocBuilder(services));



            //Assembly assembly = Assembly.GetExecutingAssembly();
            //DatabaseInitializer.AddMapperAssembly(assembly);
            //CreateDatabaseIfNotExistsWithSeed.SeedActions.Add(new IdentitySeedAction());

            //DatabaseInitializer.Initialize();



            //IServicesBuilder builder = new ServicesBuilder(new ServiceBuildOptions());
            //IServiceCollection services = builder.Build();
            //// services.AddLog4NetServices();
            //services.AddDataServices();

            //数据库初始化
            IDatabaseInitializer databaseInitializer = new DatabaseInitializer();
            if (databaseInitializer != null)
            {
                databaseInitializer.Initialize(null);
              
            }

            Department depart = new Department();
            depart.CreateTime = DateTime.Now;
            depart.DepartName = "xxxxxxxxxxx";

            DepartmentRepository re = new DepartmentRepository();
            re.Insert(depart);
            Console.WriteLine("初始化完成");
        }
    }
}
