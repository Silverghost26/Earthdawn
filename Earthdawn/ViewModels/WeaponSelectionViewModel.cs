using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public class WeaponSelectionViewModel : PageViewModel
{
    private ICharacterSheetService _characterSheetService;
    private IDataServices _dataServices;
    private NavigationService _navigationService;

    public WeaponSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        NavigationService navigationService)
    {
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        _navigationService = navigationService;
    }
}