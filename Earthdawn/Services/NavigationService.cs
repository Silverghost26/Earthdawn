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

        public void GoToHomePage() => NavigateTo(ApplicationPageNames.HomePage);
        public void GoToCharacterCustomizationPage() => NavigateTo(ApplicationPageNames.CharacterCustomizations);
        public void GoToCharacterCompletionPage() => NavigateTo(ApplicationPageNames.CharacterCompletion);
        public void GoToDisciplineSelectionPage() => NavigateTo(ApplicationPageNames.DisciplineSelection);
        public void GoToEquipmentSelectionPage() => NavigateTo(ApplicationPageNames.EquipmentSelection);
        public void GoToRaceSelectionPage() => NavigateTo(ApplicationPageNames.RaceSelection);
        public void GoToSkillSelectionPage() => NavigateTo(ApplicationPageNames.SkillSelection);
        public void GoToSpellSelectionPage() => NavigateTo(ApplicationPageNames.SpellSelection);
        public void GoToWeaponSelectionPage() => NavigateTo(ApplicationPageNames.WeaponSelection);
        public void GoToArmorSelectionPage() => NavigateTo(ApplicationPageNames.ArmorSelection);
        public void GoToMountSelectionPage() => NavigateTo((ApplicationPageNames.MountSelection));

        public void GoToNextPage()
        {
            switch (CurrentPage.PageName)
            {
                case ApplicationPageNames.HomePage:
                    GoToRaceSelectionPage();
                    break;
                case ApplicationPageNames.RaceSelection:
                    GoToDisciplineSelectionPage();
                    break;
                case ApplicationPageNames.DisciplineSelection:
                    GoToCharacterCustomizationPage();
                    break;
                case ApplicationPageNames.CharacterCustomizations:
                    GoToSkillSelectionPage();
                    break;
                case ApplicationPageNames.SkillSelection:
                    GoToSpellSelectionPage();
                    break;
                case ApplicationPageNames.SpellSelection:
                    GoToWeaponSelectionPage();
                    break;
                case ApplicationPageNames.WeaponSelection:
                    GoToArmorSelectionPage();
                    break;
                case ApplicationPageNames.ArmorSelection:
                    GoToEquipmentSelectionPage();
                    break;
                case ApplicationPageNames.EquipmentSelection:
                    GoToMountSelectionPage();
                    break;
                case ApplicationPageNames.MountSelection:
                    GoToCharacterCompletionPage();
                    break;
                case ApplicationPageNames.CharacterCompletion:
                    break;
                default:
                    break;
            }
        }

        public void GoToPreviousPage()
        {
            switch (CurrentPage.PageName)
            {
                case ApplicationPageNames.HomePage:
                    break;
                case ApplicationPageNames.RaceSelection:
                    GoToHomePage();
                    break;
                case ApplicationPageNames.DisciplineSelection:
                    GoToRaceSelectionPage();
                    break;
                case ApplicationPageNames.CharacterCustomizations:
                    GoToDisciplineSelectionPage();
                    break;
                case ApplicationPageNames.SkillSelection:
                    GoToCharacterCustomizationPage();
                    break;
                case ApplicationPageNames.SpellSelection:
                    GoToSkillSelectionPage();
                    break;
                case ApplicationPageNames.WeaponSelection:
                    GoToSpellSelectionPage();
                    break;
                case ApplicationPageNames.ArmorSelection:
                    GoToWeaponSelectionPage();
                    break;
                case ApplicationPageNames.EquipmentSelection:
                    GoToArmorSelectionPage();
                    break;
                case ApplicationPageNames.MountSelection:
                    GoToEquipmentSelectionPage();
                    break;
                case ApplicationPageNames.CharacterCompletion:
                    GoToMountSelectionPage();
                    break;
                default:
                    break;
            }
        }
    }
}
