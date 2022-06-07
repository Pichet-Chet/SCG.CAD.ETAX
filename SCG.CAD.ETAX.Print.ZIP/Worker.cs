using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.PRINT.ZIP.BussinessLayer;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.Print.ZIP
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        PrintZIP printZIP = new PrintZIP();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
        List<ConfigGlobal> configGlobals = new List<ConfigGlobal>();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            printZIP.ProcessPrintzip();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }

        public void GetGlobalConfig()
        {
            try
            {
                configGlobals = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckRunningTime()
        {
            bool result = false;
            try
            {
                GetGlobalConfig();
                var config = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "RUNNINGTIMEPRINTZIP");
                if (config != null)
                {
                    if (config.ConfigGlobalValue != null && !String.IsNullOrEmpty(config.ConfigGlobalValue))
                    {
                        if (config.ConfigGlobalValue.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}