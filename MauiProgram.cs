using Microsoft.Extensions.Logging;
using UserManagementApp.Services;
using UserManagementApp.Views;


namespace UserManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Register HttpClient with base address
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5009")
            });


            builder.Services.AddSingleton<IAuthService,AuthService>();
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IUserProfileService, UserProfileService>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<UserprofilePage>();

            return builder.Build();
        }
    }
}
