using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class KnowledgeSkillsViewModel : PageViewModel
{
    private readonly Action _onSkillPointsChanged;
    
    public KnowledgeSkillsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService, NavigationService navigationService, Action onSkillPointsChanged)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.SkillSelection;
        _navigationService = navigationService;
        _characterSheetService = characterSheetService;
        _onSkillPointsChanged = onSkillPointsChanged;
        KnowledgeSkills = new ObservableCollection<KnowledgeSkill>(_dataServices.LoadKnowledgeSkillsList().Select(ks => ks.KnowledgeSkill));
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

    public ObservableCollection<KnowledgeSkill> KnowledgeSkills { get; }

    public KnowledgeSkill SelectedKnowledgeSkill => KnowledgeSkills.Count > 0 && SelectedIndex >= 0 ? KnowledgeSkills[SelectedIndex] : null;

    [RelayCommand]
    private void Previous()
    {
        if (KnowledgeSkills.Count == 0) return;
        
        SelectedIndex--;
        if (SelectedIndex < 0)
        {
            SelectedIndex = KnowledgeSkills.Count - 1; // Wrap to end
        }
        // Update button text when selection changes
        UpdateSelectButtonText();
        // Update selection status for highlighting
        UpdateIsCurrentSkillSelected();
    }

    [RelayCommand]
    private void Next()
    {
        if (KnowledgeSkills.Count == 0) return;
        
        SelectedIndex++;
        if (SelectedIndex >= KnowledgeSkills.Count)
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
        if (SelectedKnowledgeSkill == null) return;
        
        if (IsKnowledgeSkillSelected(SelectedKnowledgeSkill))
        {
            // If knowledge skill is already selected, remove it
            if (_characterSheetService.CharacterCreationSheetInstance.RemoveKnowledgeSkill(SelectedKnowledgeSkill))
            {
                // Points are refunded in RemoveKnowledgeSkill method
                // Notify that skill points changed
                _onSkillPointsChanged?.Invoke();
            }
        }
        else
        {
            // If knowledge skill is not selected, add it (check if we have skill points)
            if (_characterSheetService.CharacterCreationSheetInstance.RemainingKnowledgeSkillPoints > 0)
            {
                // Create a copy of the knowledge skill to add
                var skillToAdd = new KnowledgeSkill(SelectedKnowledgeSkill);
                skillToAdd.Rank = 1; // Initialize rank to 1
                
                if (_characterSheetService.CharacterCreationSheetInstance.AddKnowledgeSkill(skillToAdd))
                {
                    // Deduct skill point
                    _characterSheetService.CharacterCreationSheetInstance.RemainingKnowledgeSkillPoints -= 1;
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
        if (SelectedKnowledgeSkill == null || !IsKnowledgeSkillSelected(SelectedKnowledgeSkill)) return;
        
        _characterSheetService.CharacterCreationSheetInstance.IncrementKnowledgeSkill(SelectedKnowledgeSkill.Name);
        UpdateCurrentRank();
        _onSkillPointsChanged?.Invoke();
    }

    [RelayCommand]
    private void DecrementRank()
    {
        if (SelectedKnowledgeSkill == null || !IsKnowledgeSkillSelected(SelectedKnowledgeSkill)) return;
        
        _characterSheetService.CharacterCreationSheetInstance.DecrementKnowledgeSkill(SelectedKnowledgeSkill.Name);
        UpdateCurrentRank();
        _onSkillPointsChanged?.Invoke();
    }
    
    // Helper method to check if a knowledge skill is already selected
    private bool IsKnowledgeSkillSelected(KnowledgeSkill knowledgeSkill)
    {
        var selectedSkills = _characterSheetService.CharacterCreationSheetInstance.KnowledgeSkills;
        return selectedSkills.Any(s => s.Name == knowledgeSkill.Name);
    }

    private void UpdateSelectButtonText()
    {
        if (SelectedKnowledgeSkill != null && IsKnowledgeSkillSelected(SelectedKnowledgeSkill))
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
        IsCurrentSkillSelected = SelectedKnowledgeSkill != null && IsKnowledgeSkillSelected(SelectedKnowledgeSkill);
        OnPropertyChanged(nameof(IsCurrentSkillSelected));
        UpdateCurrentRank();
    }

    private void UpdateCurrentRank()
    {
        if (SelectedKnowledgeSkill != null && IsKnowledgeSkillSelected(SelectedKnowledgeSkill))
        {
            CurrentRank = _characterSheetService.CharacterCreationSheetInstance.GetKnowledgeSkillRank(SelectedKnowledgeSkill.Name);
            CanDecrementRank = CurrentRank > 0;
            // Can increment if we have points and rank is less than max (3)
            CanIncrementRank = _characterSheetService.CharacterCreationSheetInstance.RemainingKnowledgeSkillPoints > 0 && CurrentRank < 3;
        }
        else
        {
            CurrentRank = 0;
            CanIncrementRank = false;
            CanDecrementRank = false;
        }
    }
}