using System;

namespace WebApplication1
{
    public class RandomNumber
    {
        // Constructor generates the random list
        public RandomNumber(int numbers, int min, int max)
        {
            GenerateRandomNumbers(numbers, min, max);
        }

        // This is where the generated random list of numbers is stored, not the result list
        public int[] RandomNumbers { get; set; }

        // This function generates a int[] of random numbers and is used in the constructor 
        public int[] GenerateRandomNumbers(int numbers, int min, int max)
        {
            var randomGen = new Random();
            int counter = 0;
            RandomNumbers = new int[numbers];
            for (counter = 0; counter < RandomNumbers.Length; counter++)
            {
                RandomNumbers[counter] = randomGen.Next(min, max);
            }
            return RandomNumbers;
        }
    }
}
