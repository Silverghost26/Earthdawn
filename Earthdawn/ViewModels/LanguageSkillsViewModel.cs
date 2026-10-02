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
    private readonly Action _onSkillPointsChanged;
    
    public LanguageSkillsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService, NavigationService navigationService, Action onSkillPointsChanged)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.SkillSelection;
        _navigationService = navigationService;
        _characterSheetService = characterSheetService;
        _onSkillPointsChanged = onSkillPointsChanged;
        LanguageSkills = new ObservableCollection<LanguageSkill>(_dataServices.LoadLanguageSkillsList().Select(ls => ls.LanguageSkill));
        // Initialize select button text
        UpdateSelectButtonText();
    }
    
    private readonly IDataServices _dataServices;
    private readonly NavigationService _navigationService;
    private ICharacterSheetService _characterSheetService;

    [ObservableProperty]
    private int _selectedIndex = 0;

    [ObservableProperty]
    private string _selectButtonText = "Select (Speak)";

    [ObservableProperty]
    private string _selectReadWriteButtonText = "Select (Read/Write)";

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
        // Update button text when selection changes
        UpdateSelectButtonText();
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
        // Update button text when selection changes
        UpdateSelectButtonText();
    }

    [RelayCommand]
    private void SelectSpeak()
    {
        if (SelectedLanguageSkill == null) return;
        
        if (IsLanguageSkillSelected(SelectedLanguageSkill, false))
        {
            // If language skill (speaking only) is already selected, remove it
            if (RemoveLanguageSkill(SelectedLanguageSkill, false))
            {
                // Restore speak language skill point
                _characterSheetService.CharacterCreationSheetInstance.RemainingSpeakLanguageSkillPoints += 1;
                // Notify that skill points changed
                _onSkillPointsChanged?.Invoke();
            }
        }
        else
        {
            // If language skill is not selected for speaking, add it (check if we have skill points)
            if (_characterSheetService.CharacterCreationSheetInstance.RemainingSpeakLanguageSkillPoints > 0)
            {
                // Create a copy of the language skill to add (speaking only)
                var skillToAdd = new LanguageSkill(SelectedLanguageSkill);
                skillToAdd.ReadWrite = false; // Speaking only
                
                if (_characterSheetService.CharacterCreationSheetInstance.AddLanguageSkill(skillToAdd))
                {
                    // Deduct speak language skill point
                    _characterSheetService.CharacterCreationSheetInstance.RemainingSpeakLanguageSkillPoints -= 1;
                    // Notify that skill points changed
                    _onSkillPointsChanged?.Invoke();
                }
            }
        }
        // Update the button text
        UpdateSelectButtonText();
    }

    [RelayCommand]
    private void SelectReadWrite()
    {
        if (SelectedLanguageSkill == null) return;
        
        if (IsLanguageSkillSelected(SelectedLanguageSkill, true))
        {
            // If language skill (read/write) is already selected, remove it
            if (RemoveLanguageSkill(SelectedLanguageSkill, true))
            {
                // Restore read/write language skill point
                _characterSheetService.CharacterCreationSheetInstance.RemainingReadWriteSkillPoints += 1;
                // Notify that skill points changed
                _onSkillPointsChanged?.Invoke();
            }
        }
        else
        {
            // If language skill is not selected for reading/writing, add it (check if we have skill points)
            if (_characterSheetService.CharacterCreationSheetInstance.RemainingReadWriteSkillPoints > 0)
            {
                // Create a copy of the language skill to add (reading/writing)
                var skillToAdd = new LanguageSkill(SelectedLanguageSkill);
                skillToAdd.ReadWrite = true; // Reading/writing
                
                if (_characterSheetService.CharacterCreationSheetInstance.AddLanguageSkill(skillToAdd))
                {
                    // Deduct read/write language skill point
                    _characterSheetService.CharacterCreationSheetInstance.RemainingReadWriteSkillPoints -= 1;
                    // Notify that skill points changed
                    _onSkillPointsChanged?.Invoke();
                }
            }
        }
        // Update the button text
        UpdateSelectButtonText();
    }
    
    // Helper method to check if a language skill is already selected
    private bool IsLanguageSkillSelected(LanguageSkill languageSkill, bool checkForReadWrite)
    {
        var selectedSkills = _characterSheetService.CharacterCreationSheetInstance.LanguageSkills;
        return selectedSkills.Any(s => 
            s.Language == languageSkill.Language && 
            s.ReadWrite == checkForReadWrite);
    }

    // Helper method to remove a language skill
    private bool RemoveLanguageSkill(LanguageSkill languageSkill, bool isReadWrite)
    {
        var skillToRemove = _characterSheetService.CharacterCreationSheetInstance.LanguageSkills
            .FirstOrDefault(s => s.Language == languageSkill.Language && s.ReadWrite == isReadWrite);
        
        if (skillToRemove != null)
        {
            return _characterSheetService.CharacterCreationSheetInstance.RemoveLanguageSkill(skillToRemove);
        }
        return false;
    }

    private void UpdateSelectButtonText()
    {
        if (SelectedLanguageSkill != null)
        {
            // Update speak button text
            if (IsLanguageSkillSelected(SelectedLanguageSkill, false))
            {
                SelectButtonText = "Remove (Speak)";
            }
            else
            {
                SelectButtonText = "Select (Speak)";
            }

            // Update read/write button text
            if (IsLanguageSkillSelected(SelectedLanguageSkill, true))
            {
                SelectReadWriteButtonText = "Remove (Read/Write)";
            }
            else
            {
                SelectReadWriteButtonText = "Select (Read/Write)";
            }
        }
    }
}