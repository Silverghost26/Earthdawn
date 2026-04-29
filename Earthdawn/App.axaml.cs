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
        collection.AddTransient<CharacterCustomizationsViewModel>();
        collection.AddTransient<CharacterViewModel>();
        collection.AddTransient<DisciplinesViewModel>();
        collection.AddTransient<EquipmentSelectionViewModel>();
        collection.AddTransient<HomeViewModel>();
        collection.AddTransient<RacesViewModel>();
        collection.AddTransient<SkillsViewModel>();
        collection.AddTransient<SpellsViewModel>();
        collection.AddTransient<TalentsViewModel>();

        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.Home => x.GetRequiredService<HomeViewModel>(),
            ApplicationPageNames.CharacterCustomizations => x.GetRequiredService<CharacterCustomizationsViewModel>(),
            ApplicationPageNames.Character => x.GetRequiredService<CharacterViewModel>(),
            ApplicationPageNames.Disciplines => x.GetRequiredService<DisciplinesViewModel>(),
            ApplicationPageNames.EquipmentSelection => x.GetRequiredService<EquipmentSelectionViewModel>(),
            ApplicationPageNames.Races => x.GetRequiredService<RacesViewModel>(),
            ApplicationPageNames.Skills => x.GetRequiredService<SkillsViewModel>(),
            ApplicationPageNames.Spells => x.GetRequiredService<SpellsViewModel>(),
            ApplicationPageNames.Talents => x.GetRequiredService<TalentsViewModel>(),
            _ => throw new InvalidOperationException()
        });

        collection.AddSingleton<PageFactory>();
        
        var services = collection.BuildServiceProvider();
        
        
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