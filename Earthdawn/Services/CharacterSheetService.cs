using System.ComponentModel;
using System.Runtime.CompilerServices;
using Earthdawn.Models;

namespace EarthDawn.Services;

public interface ICharacterSheetService : INotifyPropertyChanged
{
    CharacterCreationSheet CharacterCreationSheetInstance { get; }
    void SetCharacterSheet(CharacterCreationSheet creationSheet);
}

public class CharacterSheetService : ICharacterSheetService
{
    public CharacterSheetService()
    {
    }

    private CharacterCreationSheet? _characterSheetInstance;

    // Implementing INotifyPropertyChanged for the service itself 
    // (though often not necessary if only the model changes, it is good practice)
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    // Singleton pattern: Ensures only one instance of the service exists
    private static readonly CharacterSheetService Instance = new CharacterSheetService();

    // Public access point (ensures consumers always get the same instance)
    public static ICharacterSheetService InstanceAccessor => Instance;

    // private CharacterSheetService() { }
    
    // The property that holds the single source of truth
    public CharacterCreationSheet CharacterCreationSheetInstance
    {
        get 
        {
            if (_characterSheetInstance == null)
            {
                // Fallback: Provide a default instance if nothing has been set yet
                _characterSheetInstance = new CharacterCreationSheet();
            }
            return _characterSheetInstance;
        }
    }

    /// <summary>
    /// Initializes or replaces the single global character sheet instance.
    /// This should be called during application startup.
    /// </summary>
    public void SetCharacterSheet(CharacterCreationSheet creationSheet)
    {
        // Check if the sheet is actually changing to prevent unnecessary notification triggers
        if (_characterSheetInstance == null)
        {
            _characterSheetInstance = creationSheet;
        }
        
        // Because the model itself now raises PropertyChanged, 
        // we don't need to manually notify here unless we were changing 
        // a property *on the service* (like 'IsLoaded').
        // We rely on the model's own mechanism.
    }
}