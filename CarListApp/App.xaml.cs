﻿using CarListApp.Models;
using CarListApp.Services;

namespace CarListApp;

public partial class App : Application
{
    public static UserInfo UserInfo;
    public static CarDatabaseService CarDatabaseService { get; private set; }

    public App(CarDatabaseService carDatabaseService)
	{
		InitializeComponent();

		MainPage = new AppShell();
        CarDatabaseService = carDatabaseService;
    }
}
