using ServPay.Services;
using ServPay.View_Models;
using ServPay.Views;

namespace ServPay;

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

		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<LoginServices>();
		builder.Services.AddTransient<PaymentTransferViewModel>();
        builder.Services.AddSingleton<AccountViewModel>();
		builder.Services.AddSingleton<RegistrationViewModel>();

        builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddTransient<TransferPage>();
		builder.Services.AddSingleton<AccountPage>();
        builder.Services.AddSingleton<RegisterPage>();


        return builder.Build();
	}
}
