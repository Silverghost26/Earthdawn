// using System.Collections.Generic;
//
// namespace Earthdawn.Models;
// public class Discipline
//     {
//         public string ImportantAttributes { get; set; } = string.Empty;
//         public string StartingPowers { get; set; } = string.Empty;  
//         public string HalfMagic { get; set; } = string.Empty;
//         public string Durability { get; set; } = string.Empty;
//         public Dictionary<string, List<string>> TalentOptions { get; set; } = new();
//         public Dictionary<string, Circle> Circles { get; set; } = new();
//     }
//
//     public class Circle
//     {
//         public string PhysicalDefense { get; set; } = string.Empty;
//         public string MysticalDefense { get; set; } = string.Empty;
//         public string SocialDefense { get; set; } = string.Empty;
//         public string PhysicalArmor { get; set; } = string.Empty;
//         public string MysticalArmor { get; set; } = string.Empty;
//         public string Karma { get; set; } = string.Empty;
//         public string Initiative { get; set; } = string.Empty;
//         public string Recovery { get; set; } = string.Empty;
//         public string Special { get; set; } = string.Empty;
//         public List<string> FreeTalents { get; set; } = new();
//         public List<string> Talents { get; set; } = new();
//     }
//
// public class DisciplineDisplayCard
// {
//     public string Name 
//     {
//         get => _name ?? string.Empty;  
//         set => _name = value; 
//     }
//     private string? _name;
//     public Discipline Disciplines 
//     {
//         get => _discipline ?? new Discipline();
//         set => _discipline = value;
//     }
//     private Discipline? _discipline;
// }