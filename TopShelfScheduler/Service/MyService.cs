using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfScheduler
{
    public class MyService
    {
        private readonly ILogger _logger;

        public MyService(ILogger logger)
        {
            _logger = logger;
        }

        public bool OnStart()
        {
            _logger.Info($"My service started at {DateTime.Now}");
            return true;
        }

        public bool OnStop()
        {
            _logger.Info($"Finishing My service at {DateTime.Now}");
            return true;
        }
    }
}
