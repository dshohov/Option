﻿using Microsoft.AspNetCore.Identity;
using OptionWebApplication.Data;
using OptionWebApplication.Models;

namespace OptionWebApplication.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
                if (!context.Assemblies.Any())
                {
                    context.Assemblies.AddRange(new List<Assembly>()
                    {
                        new Assembly()
                        {
                            SerialNumber = 111111,
                            TypeDevice = "Компьютер",
                            ChangeComponents = "Материнская плата",
                            OtherWork = "Гравировка",
                            People = "Дмитрий"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Guarentes.Any())
                {
                    context.Guarentes.AddRange(new List<Guarentee>()
                    {
                        new Guarentee()
                        {
                            SerialNumber = 222222,
                            TypeDevice = "Компьютер",
                            DateIn = new DateTime(2022,08,26),
                            DateOut = new DateTime(2022,08,30),
                            Details = "Не включаеться",
                            FaultDetection = "Материнская плата не работает",
                            Conclusion = "Замена материнской платы по гарантии",
                            DiagnosticPeople = "Дмитрий 1",
                            ComplectedWork = "Замена метринской платы, замена термопасты, чистка от пыли",
                            RepairPeople = "Дмитрий 2"
                            
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}