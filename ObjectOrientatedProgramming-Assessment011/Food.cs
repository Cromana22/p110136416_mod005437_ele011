using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectOrientatedProgramming_Assessment011
{
    class Food: Resource
    {
        public Food(string name, int cost, int uses, int quantity, int hungerMod) //constructor
        {
            Name = name;
            Cost = cost;
            Uses = uses;
            Quantity = quantity;
            HealthModifier = 0;
            HungerModifier = hungerMod;
            MoodModifier = 0;
        }
    }
}
