using ReelWords.Extensions;
using System;

namespace ReelWords
{
    public class Program
    {
        static void Main(string[] args)
        {
            var reelManager = new ReelManager();
            bool playing = true;

            var randomLetters = reelManager.GenerateRandomLetters();

            while (playing)
            {
                Console.WriteLine(randomLetters.WriteLetters());
                Console.WriteLine("Please insert a word or press 0 to quit");

                string inputWord = Console.ReadLine();

                if(inputWord != "0")
                {
                    if (reelManager.IsValidInput(inputWord, randomLetters))
                    {
                        if (reelManager.Trie.Search(inputWord))
                        {
                            randomLetters = reelManager.MoveLetters(inputWord, randomLetters);
                            Console.WriteLine($"Exist in dictionary! - Total score {reelManager.TotalScore}\n");
                        }
                        else
                        {
                            randomLetters = reelManager.GenerateRandomLetters();
                            Console.WriteLine("Does not exist in dictionary!\n\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input\n\n");
                    }
                }
                else
                {
                    playing = false;
                }
            }
        }
    }
}