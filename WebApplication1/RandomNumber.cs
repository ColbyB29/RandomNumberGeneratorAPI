using System;

namespace WebApplication1
{
    public class RandomNumber
    {
        public RandomNumber(int numbers, int min, int max)
        {
            GenerateRandomNumbers(numbers, min, max);
        }

        public int[] RandomNumbers { get; set; }

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
