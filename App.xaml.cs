﻿using ServPay.Views;

namespace ServPay;

public partial class App : Application
{
	public App()
	{
        InitializeComponent();
        MainPage = new AppShell();
    }
}
