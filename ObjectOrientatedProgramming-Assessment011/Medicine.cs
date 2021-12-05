using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectOrientatedProgramming_Assessment011
{
    class Medicine: Resource
    {
        public Medicine(string name, int cost, int uses, int quantity, int healthMod, int hungerMod) //constructor
        {
            Name = name;
            Cost = cost;
            Uses = uses;
            Quantity = quantity;
            HealthModifier = healthMod;
            HungerModifier = hungerMod;
            MoodModifier = 0;
        }
    }
}
