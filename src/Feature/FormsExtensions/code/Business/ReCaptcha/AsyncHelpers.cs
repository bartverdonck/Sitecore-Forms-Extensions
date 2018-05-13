using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Feature.FormsExtensions.Business.ReCaptcha
{
    public static class AsyncHelpers
    {
        private static readonly TaskFactory MyTaskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
        public static T RunSync<T>(Func<Task<T>> func)
        {
            CultureInfo cultureUi = CultureInfo.CurrentUICulture;
            CultureInfo culture = CultureInfo.CurrentCulture;
            return MyTaskFactory.StartNew<Task<T>>(delegate
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;
                return func();
            }).Unwrap().GetAwaiter().GetResult();
        }
    }
}