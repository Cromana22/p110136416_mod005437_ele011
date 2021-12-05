using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectOrientatedProgramming_Assessment011
{
    class Toy: Resource
    {
        public Toy(string name, int cost, int uses, int quantity, int moodMod) //constructor
        {
            Name = name;
            Cost = cost;
            Uses = uses;
            Quantity = quantity;
            HealthModifier = 0;
            HungerModifier = 0;
            MoodModifier = moodMod;
        }
    }
}
