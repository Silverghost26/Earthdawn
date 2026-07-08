using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class HomeViewModel : PageViewModel
{
    private readonly NavigationService _navigationService;
    public HomeViewModel(NavigationService navigationService)
    {
        PageName = ApplicationPageNames.Home;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private void CharacterCreationSelected()
    {
        _navigationService.NavigateTo(ApplicationPageNames.Races);
    }
}