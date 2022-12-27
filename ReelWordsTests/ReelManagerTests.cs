using ReelWords;
using ReelWords.Extensions;
using ReelWords.Models;
using System.Collections.Generic;
using Xunit;

namespace ReelWordsTests
{
    public class ReelManagerTests
    {
        private readonly List<Letter> RandomLetters;

        public ReelManagerTests()
        {
            RandomLetters = new List<Letter>
            {
                new Letter { Character = 'x', IndexInReel = 2, ReelNumber = 0  },
                new Letter { Character = 'y', IndexInReel = 1, ReelNumber = 1  },
                new Letter { Character = 'm', IndexInReel = 4, ReelNumber = 2  },
                new Letter { Character = 's', IndexInReel = 4, ReelNumber = 3  },
                new Letter { Character = 'e', IndexInReel = 4, ReelNumber = 4  },
                new Letter { Character = 't', IndexInReel = 3, ReelNumber = 5  }
            };
        }

        [Fact]
        public void InsertDataToTrie_should_return_a_trie()
        {
            //Arrange
            var mngr = new ReelManager();
            var words = new string[] { "cat", "can", "Squirrel's", "Slót"};
            var exception = Record.Exception(() => mngr.InsertDataToTrie(words));

            //Act
            var sut = mngr.InsertDataToTrie(words);

            //Assert
            Assert.IsType<Trie>(sut);
            Assert.Null(exception);
        }

        [Fact]
        public void GenerateRandomLetters_should_retrieve_LetterList()
        {
            //Arrange
            var mngr = new ReelManager();

            //Act
            var randomLetters = mngr.GenerateRandomLetters();

            //Assert
            Assert.IsType<List<Letter>>(randomLetters);
        }

        [Fact]
        public void MoveLetters_should_return_concrete_letterList()
        {
            //Arrange
            var mngr = new ReelManager();
            string inputWord = "set";
            string outputLetters = "xymved";
  
            //Act
            var sut = mngr.MoveLetters(inputWord, RandomLetters);

            //Assert
            Assert.Equal(outputLetters, sut.WriteLetters());
        }

        [Fact]
        public void MoveLetters_should_set_scorepoints()
        {
            //Arrange
            var mngr = new ReelManager();
            string inputWord = "set";
            int expectedScore = 3;

            //Act
            mngr.MoveLetters(inputWord, RandomLetters);

            //Assert
            Assert.Equal(expectedScore, mngr.TotalScore);
        }

        [Fact]
        public void IsValidInput()
        {
            var mngr = new ReelManager();

            string invalidSequence = "hi";
            string validSequence = "set";

            Assert.False(mngr.IsValidInput(invalidSequence, RandomLetters));
            Assert.True(mngr.IsValidInput(validSequence, RandomLetters));
        }
    }
}