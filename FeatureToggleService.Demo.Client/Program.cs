using Topshelf;

namespace FeatureToggleService.Demo.Client
{
    internal class Program
    {
        private static void Main(string[] args)
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

                x.SetDescription("Feature Toggle Service Demo");
                x.SetDisplayName("Demo Service");
                x.SetServiceName("Demo Service");
            });
        }
    }
}