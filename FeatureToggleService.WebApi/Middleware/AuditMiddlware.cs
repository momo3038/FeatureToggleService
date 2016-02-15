using System.Threading.Tasks;
using Microsoft.Owin;
using NLog;

namespace FeatureToggleService.WebApi
{
    public class AuditMiddlware : OwinMiddleware
    {
        readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public AuditMiddlware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            _logger.Info("Receiving request from {0} for route [{1}] {2}", context.Request.RemoteIpAddress, context.Request.Method, context.Request.Path);
            await Next.Invoke(context);
            _logger.Info("End Request");
        }
    }
}