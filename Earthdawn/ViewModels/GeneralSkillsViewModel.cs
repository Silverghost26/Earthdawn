using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class GeneralSkillsViewModel : PageViewModel
{
    private readonly Action _onSkillPointsChanged;
    
    public GeneralSkillsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService, NavigationService navigationService, Action onSkillPointsChanged)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.SkillSelection;
        _navigationService = navigationService;
        _characterSheetService = characterSheetService;
        _onSkillPointsChanged = onSkillPointsChanged;
        Skills = new ObservableCollection<Skill>(_characterSheetService.CharacterCreationSheetInstance.AvailableSkillList);
        // Initialize select button text
        UpdateSelectButtonText();
        // Initialize selection status for highlighting
        UpdateIsCurrentSkillSelected();
    }
    
    private readonly IDataServices _dataServices;
    private readonly NavigationService _navigationService;
    private ICharacterSheetService _characterSheetService;

    [ObservableProperty]
    private int _selectedIndex = 0;

    [ObservableProperty]
    private string _selectButtonText = "Select";

    [ObservableProperty]
    private bool _isCurrentSkillSelected;

    [ObservableProperty]
    private int _currentRank;

    [ObservableProperty]
    private bool _canIncrementRank;

    [ObservableProperty]
    private bool _canDecrementRank;

    public ObservableCollection<Skill> Skills { get; }

    public Skill SelectedSkill => Skills.Count > 0 && SelectedIndex >= 0 ? Skills[SelectedIndex] : null;

    [RelayCommand]
    private void Previous()
    {
        if (Skills.Count == 0) return;
        
        SelectedIndex--;
        if (SelectedIndex < 0)
        {
            SelectedIndex = Skills.Count - 1; // Wrap to end
        }
        // Update button text when selection changes
        UpdateSelectButtonText();
        // Update selection status for highlighting
        UpdateIsCurrentSkillSelected();
    }

    [RelayCommand]
    private void Next()
    {
        if (Skills.Count == 0) return;
        
        SelectedIndex++;
        if (SelectedIndex >= Skills.Count)
        {
            SelectedIndex = 0; // Wrap to beginning
        }
        // Update button text when selection changes
        UpdateSelectButtonText();
        // Update selection status for highlighting
        UpdateIsCurrentSkillSelected();
    }

    [RelayCommand]
    private void Select()
    {
        if (SelectedSkill == null) return;
        
        if (IsSkillSelected(SelectedSkill))
        {
            // If skill is already selected, remove it
            if (_characterSheetService.CharacterCreationSheetInstance.RemoveGeneralSkill(SelectedSkill))
            {
                // Points are refunded in RemoveGeneralSkill method
                // Notify that skill points changed
                _onSkillPointsChanged?.Invoke();
            }
        }
        else
        {
            // If skill is not selected, add it (check if we have skill points)
            if (_characterSheetService.CharacterCreationSheetInstance.RemainingGeneralSkillPoints > 0)
            {
                // Create a copy of the skill to add
                var skillToAdd = new Skill(SelectedSkill);
                skillToAdd.Rank = 1; // Initialize rank to 1
                
                if (_characterSheetService.CharacterCreationSheetInstance.AddGeneralSkill(skillToAdd))
                {
                    // Deduct skill point
                    _characterSheetService.CharacterCreationSheetInstance.RemainingGeneralSkillPoints -= 1;
                    // Notify that skill points changed
                    _onSkillPointsChanged?.Invoke();
                }
            }
        }
        // Update the button text
        UpdateSelectButtonText();
        // Update selection status for highlighting
        UpdateIsCurrentSkillSelected();
    }

    [RelayCommand]
    private void IncrementRank()
    {
        if (SelectedSkill == null || !IsSkillSelected(SelectedSkill)) return;
        
        _characterSheetService.CharacterCreationSheetInstance.IncrementGeneralSkill(SelectedSkill.Name);
        UpdateCurrentRank();
        _onSkillPointsChanged?.Invoke();
    }

    [RelayCommand]
    private void DecrementRank()
    {
        if (SelectedSkill == null || !IsSkillSelected(SelectedSkill)) return;
        
        _characterSheetService.CharacterCreationSheetInstance.DecrementGeneralSkill(SelectedSkill.Name);
        UpdateCurrentRank();
        _onSkillPointsChanged?.Invoke();
    }
    
    private bool IsSkillSelected(Skill skill)
    {
        var selectedSkills = _characterSheetService.CharacterCreationSheetInstance.GeneralSkills;

        if (selectedSkills.Any(s => s.Name == skill.Name))
        {
            return true;
        }
        return false;
    }

    private void UpdateSelectButtonText()
    {
        if (SelectedSkill != null && IsSkillSelected(SelectedSkill))
        {
            SelectButtonText = "Remove";
        }
        else
        {
            SelectButtonText = "Select";
        }
    }

    private void UpdateIsCurrentSkillSelected()
    {
        IsCurrentSkillSelected = SelectedSkill != null && IsSkillSelected(SelectedSkill);
        OnPropertyChanged(nameof(IsCurrentSkillSelected));
        UpdateCurrentRank();
    }

    private void UpdateCurrentRank()
    {
        if (SelectedSkill != null && IsSkillSelected(SelectedSkill))
        {
            CurrentRank = _characterSheetService.CharacterCreationSheetInstance.GetGeneralSkillRank(SelectedSkill.Name);
            CanDecrementRank = CurrentRank > 0;
            // Can increment if we have points and rank is less than max (3)
            CanIncrementRank = _characterSheetService.CharacterCreationSheetInstance.RemainingGeneralSkillPoints > 0 && CurrentRank < 3;
        }
        else
        {
            CurrentRank = 0;
            CanIncrementRank = false;
            CanDecrementRank = false;
        }
    }
}