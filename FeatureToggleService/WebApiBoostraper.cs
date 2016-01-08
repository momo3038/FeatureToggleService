using System;
using FeatureToggleService.WebApi;
using Microsoft.Owin.Hosting;

namespace FeatureToggleService
{
    public class WebApiBoostraper
    {
        private string baseAddress = "http://localhost:9000/";
        private IDisposable _server;

        public void Start()
        {
            _server = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            _server.Dispose();
        }
    }
}
