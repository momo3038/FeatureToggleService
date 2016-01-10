using System;
using FeatureToggleService.WebApi;
using Microsoft.Owin.Hosting;
using NLog;

namespace FeatureToggleService
{
    public class WebApiBoostraper
    {
        private string baseAddress = "http://localhost:9000/";
        private IDisposable _server;
        readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Start()
        {
            _logger.Info("Starting Web application on address {0}", baseAddress);
            _server = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            _logger.Info("Stoping Web application");
            _server.Dispose();
        }
    }
}
