using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.Logging;
using Hangfire.States;
using Hangfire.Storage;
using System.Web;

namespace Commons.Framework.Notifications
{
    public class LogFailureAttribute : JobFilterAttribute, IApplyStateFilter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is FailedState failedState)
            {
                Logger.ErrorException(
                    message: $"Background job #{context.BackgroundJob.Id} was failed with an exception.",
                    exception: failedState.Exception);
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }
    }

    public class HangfireDashboardAuthFilter : IDashboardAuthorizationFilter
    {

        public bool Authorize(DashboardContext context)
        {

            
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return HttpContext.Current.User.Identity.Name.ToLower().Equals("admin");
            }
            return false;

            //// In case you need an OWIN context, use the next line, `OwinContext` class
            //// is the part of the `Microsoft.Owin` package.
            //var owinContext = new OwinContext(context.GetOwinEnvironment());

            //// Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }
}

