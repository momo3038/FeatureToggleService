using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace FeatureToggleService.Demo.Client
{
    public class WebApiBoostraper
    {
        private readonly string _baseAddress = ConfigurationManager.AppSettings["owin.host"];
        private IDisposable _server;

        public void Start()
        {
            _server = WebApp.Start<Startup>(_baseAddress);
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}
