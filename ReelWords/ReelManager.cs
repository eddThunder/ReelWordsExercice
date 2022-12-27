using ReelWords.Extensions;
using ReelWords.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ReelWords
{
    public class ReelManager
    {
        public readonly Trie Trie = new Trie();

        private const int MAX_ROW_CHARS = 6;
        private const int MAX_REEL_CHARS = 7;

        private Panel Panel;
        private int totalScore = 0;
        private readonly IDictionary<char, int> ScoreDictionary;

        public int TotalScore
        {
            get
            {
               return totalScore;
            }
        }

        public ReelManager()
        {
            Panel = BuildGamePanelOBJFromReels(File.ReadAllLines(@"Resources/reels.txt"));
            Trie = InsertDataToTrie(File.ReadAllLines(@"Resources/american-english-large.txt"));
            ScoreDictionary = LoadScoreDictionary(File.ReadAllLines(@"Resources/scores.txt"));
        }


        public Trie InsertDataToTrie(string[] textLines)
        {
            Trie trie = new Trie();

            foreach (var line in textLines)
            {
                if (line.Length <= MAX_ROW_CHARS)
                {
                    var auxLine = line.ToLower().Replace("'", "").ToString();
                    trie.Insert(auxLine.RemoveDiacritics());
                }
            }

            return trie;
        }

        public List<Letter> GenerateRandomLetters()
        {
            List<Letter> randomLetters = new List<Letter>();
            Random rnd = new Random();

            foreach (var reel in Panel.Reels)
            {
                var randomIndex = rnd.Next(0, MAX_ROW_CHARS - 1);
                var letter = reel.Where(x => x.IndexInReel == randomIndex).First();
                randomLetters.Add(letter);
            }

            return randomLetters;
        }

        public List<Letter> MoveLetters(string word, List<Letter> randomLetters)
        {
            foreach (var letter in word)
            {
                bool found = false;
                int i = 0;

                while (!found && i < MAX_ROW_CHARS)
                {
                    if (letter == randomLetters[i].Character)
                    {
                        found = true;
                        var tile = randomLetters[i];

                        var newIndexInReel = tile.IndexInReel + 1 < MAX_REEL_CHARS ? tile.IndexInReel + 1 : 0;
                        randomLetters[i] = Panel.Reels[tile.ReelNumber][newIndexInReel];

                        int num;
                        ScoreDictionary.TryGetValue(letter, out num);
                        totalScore += num;
                    }
                    i++;
                }
            }
            return randomLetters;
        }

        public bool IsValidInput(string word, List<Letter> randomLetters)
        {
            bool isValid = true;
            int i = 0;

            var letters = randomLetters.WriteLetters();

            while (isValid && i < word.Length)
            {
                if (letters.IndexOf(word[i]) == -1)
                {
                    return false;
                }
                else
                {
                    i++;
                }
            }

            return isValid;
        }

        private static IDictionary<char, int> LoadScoreDictionary(string[] rawsCores)
        {
            var dictionary = new Dictionary<char, int>();

            foreach (var line in rawsCores)
            {
                var aux = line.Split(" ");
                dictionary.Add(char.Parse(aux[0]), int.Parse(aux[1]));
            }

            return dictionary;
        }

        private Panel BuildGamePanelOBJFromReels(string[] reels)
        {
            Panel panelObject = new Panel();

            for (int x = 0; x < MAX_ROW_CHARS; x++)
            {
                var chars = reels[x].Replace(" ", string.Empty);
                var reel = new List<Letter>();

                for (int y = 0; y < MAX_REEL_CHARS; y++)
                {
                    reel.Add(new Letter
                    {
                        Character = chars[y],
                        IndexInReel = y,
                        ReelNumber = x
                    });
                }
                panelObject.Reels.Add(reel);
            }

            return panelObject;
        }
    }
}
