using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public class MountSelectionViewModel: PageViewModel
{
    private ICharacterSheetService _characterSheetService;
    private IDataServices _dataServices;
    private NavigationService _navigationService;

    public MountSelectionViewModel(ICharacterSheetService characterSheetService, IDataServices dataServices,
        NavigationService navigationService)
    {
        PageName = ApplicationPageNames.MountSelection;
        _characterSheetService = characterSheetService;
        _dataServices = dataServices;
        _navigationService = navigationService;
    }
}