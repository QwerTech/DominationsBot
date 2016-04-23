using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace GCR
{
        public class GcrExceptionHandler : ExceptionHandler
        {
            private readonly IIsDebug _isDebug;

            public GcrExceptionHandler(IIsDebug isDebug)
            {
                _isDebug = isDebug;
            }

            public override void Handle(ExceptionHandlerContext context)
            {
                //var logModelId = DefaultLogger.LogException(context.Exception);
                context.Result =
                    new ResponseMessageResult(context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new { /*ErrorId = logModelId,*/ Exception = _isDebug.IsAnyDebug ? context.Exception : null }));
            }

            public override bool ShouldHandle(ExceptionHandlerContext context)
            {
                return true;
            }
        }
    }
