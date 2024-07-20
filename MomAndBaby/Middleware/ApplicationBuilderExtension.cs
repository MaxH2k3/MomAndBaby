using MomAndBaby.Subscribe;

namespace MomAndBaby.Middleware
{
    public static class ApplicationBuilderExtension
    {
        public static void UseNotificationTableDependency(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var hubContext = serviceProvider.GetService<SubscribeNotification>();
            hubContext.SubscribeTableDependency();
        }
    }
}
