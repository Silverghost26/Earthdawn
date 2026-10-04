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
                // Restore skill point
                _characterSheetService.CharacterCreationSheetInstance.RemainingKnowledgeSkillPoints += 1;
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
    }
}