using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class LanguageSkillsViewModel : PageViewModel
{
    public LanguageSkillsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService, NavigationService navigationService)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.SkillSelection;
        _navigationService = navigationService;
        _characterSheetService = characterSheetService;
        LanguageSkills = new ObservableCollection<LanguageSkill>(_dataServices.LoadLanguageSkillsList().Select(ls => ls.LanguageSkill));
    }
    
    private readonly IDataServices _dataServices;
    private readonly NavigationService _navigationService;
    private ICharacterSheetService _characterSheetService;

    [ObservableProperty]
    private int _selectedIndex = 0;

    public ObservableCollection<LanguageSkill> LanguageSkills { get; }

    public LanguageSkill SelectedLanguageSkill => LanguageSkills.Count > 0 && SelectedIndex >= 0 ? LanguageSkills[SelectedIndex] : null;

    [RelayCommand]
    private void Previous()
    {
        if (LanguageSkills.Count == 0) return;
        
        SelectedIndex--;
        if (SelectedIndex < 0)
        {
            SelectedIndex = LanguageSkills.Count - 1; // Wrap to end
        }
    }

    [RelayCommand]
    private void Next()
    {
        if (LanguageSkills.Count == 0) return;
        
        SelectedIndex++;
        if (SelectedIndex >= LanguageSkills.Count)
        {
            SelectedIndex = 0; // Wrap to beginning
        }
    }

    [RelayCommand]
    private void Select()
    {
        if (SelectedLanguageSkill != null)
        {
            Console.WriteLine($"Selected language skill: {SelectedLanguageSkill.Language}");
            // TODO: Implement actual selection logic here
        }
    }
    
    // Helper method to check if a language skill is already selected
    private bool IsLanguageSkillSelected(LanguageSkill languageSkill)
    {
        // TODO: Implement when character sheet has language skills list
        return false;
    }
}