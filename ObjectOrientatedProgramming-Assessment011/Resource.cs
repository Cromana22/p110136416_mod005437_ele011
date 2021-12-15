using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectOrientatedProgramming_Assessment011
{
    public class Resource
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Uses { get; set; }
        public int Quantity { get; set; }
        public int HealthModifier { get; set; }
        public int HungerModifier { get; set; }
        public int MoodModifier { get; set; }


        public Dictionary<string, int> Effect(Dictionary<string, int> petStats) //activate the effect of the item
        {
            petStats["Health"] += HealthModifier;
            petStats["Hunger"] += HungerModifier;
            petStats["Mood"] += MoodModifier;
            return petStats;
        }
    }
}
