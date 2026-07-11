using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Earthdawn.ViewModels;
using Earthdawn.Views;
using System;
using Earthdawn.Data;
using Microsoft.Extensions.DependencyInjection;
using Earthdawn.Factories;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<IDataServices,  DataServices>();
        collection.AddSingleton<StringToImageconverter>();
        collection.AddSingleton<ICharacterSheetService, CharacterSheetService>();
        collection.AddSingleton<NavigationService>(); // Register NavigationService as singleton
        
        collection.AddTransient<CharacterCustomizationsViewModel>();
        collection.AddTransient<CharacterViewModel>();
        collection.AddTransient<DisciplinesViewModel>();
        collection.AddTransient<EquipmentSelectionViewModel>();
        collection.AddTransient<ArmorSelectionViewModel>(); // Add ArmorSelectionViewModel
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<RacesViewModel>();
        collection.AddTransient<SkillsViewModel>();
        collection.AddTransient<SpellsViewModel>();
        collection.AddTransient<WeaponSelectionViewModel>(); // Add WeaponSelectionViewModel
        collection.AddTransient<MountSelectionViewModel>();

        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.HomePage => x.GetRequiredService<HomeViewModel>(),
            ApplicationPageNames.CharacterCustomizations => x.GetRequiredService<CharacterCustomizationsViewModel>(),
            ApplicationPageNames.CharacterCompletion => x.GetRequiredService<CharacterViewModel>(),
            ApplicationPageNames.DisciplineSelection => x.GetRequiredService<DisciplinesViewModel>(),
            ApplicationPageNames.EquipmentSelection => x.GetRequiredService<EquipmentSelectionViewModel>(),
            ApplicationPageNames.RaceSelection => x.GetRequiredService<RacesViewModel>(),
            ApplicationPageNames.SkillSelection => x.GetRequiredService<SkillsViewModel>(),
            ApplicationPageNames.SpellSelection => x.GetRequiredService<SpellsViewModel>(),
            ApplicationPageNames.WeaponSelection => x.GetRequiredService<WeaponSelectionViewModel>(),
            ApplicationPageNames.ArmorSelection => x.GetRequiredService<ArmorSelectionViewModel>(),
            ApplicationPageNames.MountSelection => x.GetRequiredService<MountSelectionViewModel>(),
            _ => throw new InvalidOperationException()
        });

        collection.AddSingleton<PageFactory>();
        
        var services = collection.BuildServiceProvider();
        var sheetService = services.GetRequiredService<ICharacterSheetService>();
        sheetService.SetCharacterSheet(new CharacterCreationSheet());
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindowView
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}