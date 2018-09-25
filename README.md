# A Topshelf application with scheduler and depency injection resolution  [![Build Status](https://travis-ci.org/vittoriazago/topshelfSchedulerIoc.svg?branch=master)](https://travis-ci.org/vittoriazago/topshelfSchedulerIoc)
A topshelf application, easy to deploy, with scheduler, log and dependency injection

You must install:

* Install-Package [Topshelf.StructureMap](https://www.nuget.org/packages/Topshelf.StructureMap)
* Install-Package [Topshelf.Quartz.StructureMap](https://www.nuget.org/packages/Topshelf.Quartz.StructureMap)
* Install-Package [NLog](https://www.nuget.org/packages/NLog)

In order to create your application you must code a HostFactory Configuration
``` c#
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

```
