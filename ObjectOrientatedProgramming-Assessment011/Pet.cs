using System;
using System.Collections.Generic;
using System.Threading;

namespace ObjectOrientatedProgramming_Assessment011
{
    public abstract class Pet: IUserInterface
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Hunger { get; set; }
        public int Mood { get; set; }
        public int ComfortTempFrom { get; set; }
        public int ComfortTempTo { get; set; }

        protected int maxHealth = 500;
        protected int maxHunger = 100;
        protected int maxMood = 100;
        protected int minHealth = 0;
        protected int minHunger = 0;
        protected int minMood = 0;

        public void IncreaseHunger() //regular hunger increase while pet alive
        {
            if (Health > minHealth)
            {
                if (Hunger < maxHunger)
                {
                    //standard hunger drop
                    Hunger++;

                    //mood effect on hunger
                    if (Mood < maxMood/2) { Hunger++; }
                    if (Mood < maxMood/4) { Hunger++; }
                }
            }
        }

        public void DecreaseMood() //regular mood decrease while pet alive
        {
            if (Health > minHealth)
            {
                if (Mood > minMood)
                {
                    Mood--;
                }
            }
        }

        public void UpdateHealth(int currentTemp) //regular health calculation while pet alive
        {
            if (Health > minHealth)
            {
                int decreaseAmount = 0;

                //temperature effect on health
                if (currentTemp < ComfortTempFrom || currentTemp > ComfortTempTo)
                {
                    decreaseAmount += 5;
                }

                //hunger effect on health
                if (Hunger > maxHunger/4) { decreaseAmount++; }
                if (Hunger > maxHunger/2) { decreaseAmount++; }
                if (Hunger > maxHunger/4*3) { decreaseAmount++; }
                if (Hunger == maxHunger) { decreaseAmount = 1000; }

                if (Health - decreaseAmount >= minHealth)
                {
                    Health -= decreaseAmount;
                }
                else
                {
                    Health = minHealth;
                }
            }
        }

        public Resource UseItem(Resource resource)
        {
            if (resource.Quantity > 0)
            {
                Dictionary<string, int> petStats = new Dictionary<string, int>()
                {
                    { "Health", Health },
                    { "Hunger", Hunger },
                    { "Mood", Mood }
                };

                petStats = resource.Effect(petStats);
                if (petStats["Health"] > maxHealth) { Health = maxHealth; }
                else { Health = petStats["Health"]; }
                if (petStats["Hunger"] < minHunger) { Hunger = minHunger; }
                else { Hunger = petStats["Hunger"]; }
                if (petStats["Mood"] > maxMood) { Mood = maxMood; }
                else { Mood = petStats["Mood"]; }

                resource.Uses -= 1;
                if (resource.Uses <= 0)
                {
                    resource.Quantity -= 1;
                }
            }

            return resource;
        }

        public virtual void Display()
        {

        }

        public int MaxHealth()
        {
            return maxHealth;
        }

        public int MaxHunger()
        {
            return maxHunger;
        }

        public int MaxMood()
        {
            return maxMood;
        }
    }
}
