using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace FeatureToggleService.Client.Owin
{
    public class FeatureToggleWebApiMiddleware : OwinMiddleware
    {
        public FeatureToggleWebApiMiddleware(OwinMiddleware next) : base(next)
        {

        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);
        }
    }
}