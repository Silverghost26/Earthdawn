using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Earthdawn.Data;
using Earthdawn.Models;
using EarthDawn.Services;

namespace Earthdawn.ViewModels;

public partial class SkillsViewModel : PageViewModel
{
    private readonly IDataServices _dataServices;

    [ObservableProperty]
    private int _selectedIndex = 0;

    public ObservableCollection<SkillDisplayCard> Skills { get; }

    public SkillsViewModel(IDataServices dataServices)
    {
        _dataServices = dataServices;
        PageName = ApplicationPageNames.Skills;
        Skills = new ObservableCollection<SkillDisplayCard>(dataServices.LoadSkillsList());
    }

    public Skill SelectedSkill => Skills.Count > 0 && SelectedIndex >= 0 ? Skills[SelectedIndex].Skills : null;

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
}