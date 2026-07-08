using Earthdawn.Data;
using Earthdawn.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Earthdawn.ViewModels;

namespace EarthDawn.Services
{
    public class NavigationService
    {
        private readonly PageFactory _pageFactory;
        private PageViewModel _currentPage;

        public PageViewModel CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                CurrentPageChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? CurrentPageChanged;

        public NavigationService(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        public void NavigateTo(ApplicationPageNames pageName)
        {
            CurrentPage = _pageFactory.GetPageViewModel(pageName);
        }

        public void GoToHomePage() => NavigateTo(ApplicationPageNames.Home);
        public void GoToCharacterCustomizationPage() => NavigateTo(ApplicationPageNames.CharacterCustomizations);
        public void GoToCharacterPage() => NavigateTo(ApplicationPageNames.Character);
        public void GoToDisciplinesPage() => NavigateTo(ApplicationPageNames.Disciplines);
        public void GoToEquipmentSelectionPage() => NavigateTo(ApplicationPageNames.EquipmentSelection);
        public void GoToRacesPage() => NavigateTo(ApplicationPageNames.Races);
        public void GoToSkillsPage() => NavigateTo(ApplicationPageNames.Skills);
        public void GoToSpellsPage() => NavigateTo(ApplicationPageNames.Spells);

        public void GoToNextPage()
        {
            switch (CurrentPage.PageName)
            {
                case ApplicationPageNames.Home:
                    GoToRacesPage();
                    break;
                case ApplicationPageNames.Races:
                    GoToDisciplinesPage();
                    break;
                case ApplicationPageNames.Disciplines:
                    GoToCharacterCustomizationPage();
                    break;
                case ApplicationPageNames.CharacterCustomizations:
                    GoToSkillsPage();
                    break;
                case ApplicationPageNames.Skills:
                    GoToSpellsPage();
                    break;
                case ApplicationPageNames.Spells:
                    GoToEquipmentSelectionPage();
                    break;
                case ApplicationPageNames.EquipmentSelection:
                    GoToCharacterPage();
                    break;
                case ApplicationPageNames.Character:
                    break;
                default:
                    break;
            }
        }

        public void GoToPreviousPage()
        {
            switch (CurrentPage.PageName)
            {
                case ApplicationPageNames.Home:
                    break;
                case ApplicationPageNames.Races:
                    GoToHomePage();
                    break;
                case ApplicationPageNames.Disciplines:
                    GoToRacesPage();
                    break;
                case ApplicationPageNames.CharacterCustomizations:
                    GoToDisciplinesPage();
                    break;
                case ApplicationPageNames.Skills:
                    GoToCharacterCustomizationPage();
                    break;
                case ApplicationPageNames.Spells:
                    GoToSkillsPage();
                    break;
                case ApplicationPageNames.EquipmentSelection:
                    GoToSpellsPage();
                    break;
                case ApplicationPageNames.Character:
                    GoToEquipmentSelectionPage();
                    break;
                default:
                    break;
            }
        }
    }
}
