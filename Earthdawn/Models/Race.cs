// using System.Text.RegularExpressions;
// using System.Collections.Generic;
// using System;
//
// namespace Earthdawn.Models;
//
// public class SpecialAbility
// {
//     public string Ability { get; set; }
//     public string Description { get; set; }
// }
// public class Race
// {
//     // Racial Desciption
//     public string Description { get; set; }
//
//     //Starting Attributes
//     public int DEX { get; set; }
//     public int STR { get; set; }
//     public int TOU { get; set; }
//     public int PER { get; set; }
//     public int WIL { get; set; }
//     public int CHA { get; set; }
//
//     // Movement Rates in yards per round
//     public int Movement { get; set; }
//     public int FlyingMovement { get; set; }
//
//     //Racial Karma Modifier
//     public int KarmaMod { get; set; }
//
//
//     //Racial Special Abilities
//     public List<SpecialAbility> Abilities 
//       { 
//         get => _abilities ?? new List<SpecialAbility>();
//         set => _abilities = value;}
//     private List<SpecialAbility>? _abilities;
//   
// }
//
// public class RaceDisplayCard
// {
//     public string Name 
//     {
//         get => _name ?? string.Empty;  
//         set
//         {
//             _name = value; 
//             ImagePath = _name;
//         } 
//     } 
//     private string? _name;
//
//     public Race NameGiverRace
//     {
//         get => _nameGiverRace ?? new Race();
//         set => _nameGiverRace = value;
//     }
//     private Race? _nameGiverRace;
//
//     public string ImagePath
//     {
//         get => _imagePath ?? string.Empty;
//         set
//         {
//             string _name = Regex.Replace(value, "[^a-zA-Z0-9]", String.Empty); // Remove whitespace from the name
//             _name = _name.ToLower(); // Convert to lowercase
//             _imagePath  = _name + "RacialPortrait.png";
//         }
//     }
//     private string? _imagePath = string.Empty;
// }