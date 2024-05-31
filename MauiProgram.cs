using Microsoft.Extensions.Logging;

namespace ScientificCalculator
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
                    fonts.AddFont("ComicNeue-Regular.ttf", "RegularFont");
                    fonts.AddFont("UbuntuSansMono-Regular.ttf", "UbuntuSansMonoRegular");
                    fonts.AddFont("Cairo-Light.ttf", "LightFont");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
