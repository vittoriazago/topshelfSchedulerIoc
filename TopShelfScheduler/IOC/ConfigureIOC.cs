using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using StructureMap;
using Topshelf.StructureMap;
using Topshelf;
using Topshelf.Quartz;
using Topshelf.Quartz.StructureMap;
using NLog;

namespace TopShelfScheduler
{
    public static class ConfigureIOC
    {
        public static void Configure()
        {
            HostFactory.Run(c =>
            {
                var container = new Container(cfg =>
                {
                    cfg.For(typeof(IRepository<>)).Use(typeof(Repository<>)).AlwaysUnique();
                    cfg.For<NLog.ILogger>().Use(NLog.LogManager.GetCurrentClassLogger()).AlwaysUnique();
                });

                c.UseStructureMap(container);
                c.UseNLog();

                c.Service<MyService>(s =>
                {
                    s.ConstructUsingStructureMap();

                    s.WhenStarted((service, control) => service.OnStart());
                    s.WhenStopped((service, control) => service.OnStop());
                    
                    s.UseQuartzStructureMap();

                    s.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<MyJob>().Build())
                            .AddTrigger(() =>
                                TriggerBuilder.Create()
                                    .WithSimpleSchedule(builder => builder
                                          .WithIntervalInSeconds(5)
                                           .WithRepeatCount(5))
                                    //.WithCronSchedule("")
                                    .Build())
                        );
                });
            });
        }

    }
}
