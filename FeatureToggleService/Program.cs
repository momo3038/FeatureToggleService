using FeatureToggleService.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace FeatureToggleService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<WebApiBoostraper>(s =>
                {
                    s.ConstructUsing(name => new WebApiBoostraper());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Feature Toggle Service Host");
                x.SetDisplayName("FeatureToggleService");
                x.SetServiceName("FeatureToggleService");
            });
        }
    }
}
