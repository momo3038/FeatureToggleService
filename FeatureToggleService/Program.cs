using NLog;
using Topshelf;

namespace FeatureToggleService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            HostFactory.Run(x =>
            {
                logger.Info("Starting TopShelf Service");
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