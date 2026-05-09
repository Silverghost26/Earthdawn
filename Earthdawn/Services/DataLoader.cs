using System;
using System.Text.Json;
using System.IO;

namespace EarthDawn.Services
{
    public static class DataLoader
    {
        //use Example for 'headless' dictionaries.
        //object rawData = new DataLoader().LoadDataFromDisk(jsonString, dictionaryTargetType);
        //Dictionary<string, Shield> shieldDictionary = (Dictionary<string, Shield>)rawData;

        //use syntax 1: List<StepResult> stepData = loader.LoadDataFromDisk<List<StepResult>>("path/to/dice_rolls.json");
        //use syntax 2: GeneralEquipment equipmentData = loader.LoadDataFromDisk<RootModel>("path/to/equipment.json");
        public static T LoadJson<T>(string jsonString) where T : class
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                // The use of T means the compiler knows exactly what object type to return
                T data = JsonSerializer.Deserialize<T>(jsonString, options);
                return data;
            }
            catch (JsonException ex)
            {
                // ... (Existing error handling logic)
                Console.WriteLine($"JSON Parsing Error: {ex.Message}");
                throw new InvalidDataException($"Failed to deserialize JSON", ex);
            }
            catch (Exception ex)
            {
                // ... (Existing error handling logic)
                Console.WriteLine($"General Load Error: {ex.Message}");
                throw new InvalidDataException("Unable to deserialize JSON", ex);
            }
        }
    }
}
