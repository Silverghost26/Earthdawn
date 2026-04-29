using CommunityToolkit.Mvvm.ComponentModel;
using Earthdawn.Data;

namespace Earthdawn.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;
}