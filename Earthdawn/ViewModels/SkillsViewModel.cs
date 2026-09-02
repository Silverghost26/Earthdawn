using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class SkillsViewModel : PageViewModel
{
    public SkillsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService, NavigationService navigationService)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.SkillSelection;
        _navigationService = navigationService;
        _characterSheetService = characterSheetService;
        Skills = new ObservableCollection<Skill>(_characterSheetService.CharacterCreationSheetInstance.AvailableSkillList);
        GeneralSkillPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingGeneralSkillPoints;
        KnowledgeSkillPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingKnowledgeSkillPoints;
        SpeakLanguageSkillPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingSpeakLanguageSkillPoints;
        ReadLanguageSkillPoints = _characterSheetService.CharacterCreationSheetInstance.RemainingReadWriteSkillPoints;
        
        // Start with General Skills view
        NavigateToGeneralSkills();
    }
    
    private readonly IDataServices _dataServices;
    private readonly NavigationService _navigationService;
    private ICharacterSheetService _characterSheetService;

    [ObservableProperty]
    private int _selectedIndex = 0;

    [ObservableProperty] private int _generalSkillPoints;
    
    [ObservableProperty]
    private int _knowledgeSkillPoints;
    
    [ObservableProperty]
    private int _speakLanguageSkillPoints;
    
    [ObservableProperty]
    private int _readLanguageSkillPoints;
    
    [ObservableProperty]
    private object _currentChildView;

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
    }

    [RelayCommand]
    private void Select()
    {
        if (SelectedSkill != null)
        {
            Console.WriteLine($"Selected skill: {SelectedSkill.Name}");
            // TODO: Implement actual selection logic here
        }
    }
    
    // Helper method to check if a spell is already selected
    private bool IsSkillSelected(Skill skill)
    {
        var selectedSkills = _characterSheetService.CharacterCreationSheetInstance.Skills;

        if (selectedSkills.Any(s => s.Name == skill.Name))
        {
            return true;
        }
        return false;
    }
    
    [RelayCommand]
    private void NavigateToGeneralSkills()
    {
        CurrentChildView = new GeneralSkillsViewModel(_dataServices, _characterSheetService, _navigationService);
    }
    
    [RelayCommand]
    private void NavigateToKnowledgeSkills()
    {
        CurrentChildView = new KnowledgeSkillsViewModel();
    }
    
    [RelayCommand]
    private void NavigateToLanguageSkills()
    {
        CurrentChildView = new LanguageSkillsViewModel();
    }

    [RelayCommand]
    private void SaveAndContinue()
    {
        string disciplineName = _characterSheetService.CharacterCreationSheetInstance.GetDiscipline()[0].DisciplineName;
        if (disciplineName != null
            && (disciplineName == "Wizard"
                || disciplineName == "Illusionist"
                || disciplineName == "Elementalist"
                || disciplineName == "Nethermancer"
                || disciplineName == "Shaman"))
        {
            _navigationService.GoToSpellSelectionPage();
        }
        else
        {
            _navigationService.GoToEquipmentPurchasePage();
        }
    }
}