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
    public KnowledgeSkillsViewModel(IDataServices dataServices, ICharacterSheetService characterSheetService, NavigationService navigationService)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.SkillSelection;
        _navigationService = navigationService;
        _characterSheetService = characterSheetService;
        KnowledgeSkills = new ObservableCollection<KnowledgeSkill>(_dataServices.LoadKnowledgeSkillsList().Select(ks => ks.KnowledgeSkill));
    }
    
    private readonly IDataServices _dataServices;
    private readonly NavigationService _navigationService;
    private ICharacterSheetService _characterSheetService;

    [ObservableProperty]
    private int _selectedIndex = 0;

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
    }

    [RelayCommand]
    private void Select()
    {
        if (SelectedKnowledgeSkill != null)
        {
            Console.WriteLine($"Selected knowledge skill: {SelectedKnowledgeSkill.Name}");
            // TODO: Implement actual selection logic here
        }
    }
    
    // Helper method to check if a knowledge skill is already selected
    private bool IsKnowledgeSkillSelected(KnowledgeSkill knowledgeSkill)
    {
        // TODO: Implement when character sheet has knowledge skills list
        return false;
    }
}