using NLog;
using Quartz;
using System;
using System.Threading.Tasks;

namespace TopShelfScheduler
{
    public class MyJob : IJob, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IRepository<Registry> _repository;

        public MyJob(IRepository<Registry> repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void Dispose()
        {
            _repository.Clear();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _repository.Add(new Registry
            {
                Date = DateTime.Now,
                Name = "Executing my job"
            });
            _logger.Info($"JobFinished with {_repository.Search().Count}");
        }
    }

    public class Registry
    {
        public DateTime Date { get; set; }

        public String Name { get; set; }
    }
}
