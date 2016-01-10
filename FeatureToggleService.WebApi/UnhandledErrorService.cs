using System.Web.Http.ExceptionHandling;
using NLog;

namespace FeatureToggleService.WebApi
{
    public class UnhandledErrorService : ExceptionHandler
    {
        Logger _logger = LogManager.GetCurrentClassLogger();

        public override void Handle(ExceptionHandlerContext context)
        {
            _logger.Error("Unhandled error occured during route {0}. Error {1}", context.Request.RequestUri, context.Exception.Message);
            context.Result = new GeneralErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "An error has occurred on the server. Please contact support!"
            };
        }
    }
}