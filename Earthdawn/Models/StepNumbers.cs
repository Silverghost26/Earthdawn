using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EarthDawn_Assistant.src
{
    public class StepDice
    {
        [JsonPropertyName("diceOne")]
        public int DiceOne { get; set; }

        [JsonPropertyName("diceTwo")]
        public int DiceTwo { get; set; }

        [JsonPropertyName("diceThree")]
        public int DiceThree { get; set; }

        [JsonPropertyName("diceFour")]
        public int DiceFour { get; set; }

        [JsonPropertyName("diceFive")]
        public int DiceFive { get; set; }

        [JsonPropertyName("diceSix")]
        public int DiceSix { get; set; }
    }

    //Represents a single object/element in the main array
    public class StepResult
    {
        [JsonPropertyName("stepNumber")]
        public int StepNumber { get; set; }

        [JsonPropertyName("dice")]
        public StepDice Dice { get; set; }
    }
}
