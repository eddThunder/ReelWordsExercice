
using ReelWords;
using Xunit;

namespace ReelWordsTests
{
    public class TrieTests
    {
        private const string AWESOME_CO = "pierplay";

        [Fact]
        public void TrieInsertTest()
        {
            Trie trie = new Trie();
            trie.Insert(AWESOME_CO);
            Assert.True(trie.Search(AWESOME_CO));
        }

        [Fact]
        public void TrieDeleteTest()
        {
            Trie trie = new Trie();
            trie.Insert(AWESOME_CO);
            Assert.True(trie.Search(AWESOME_CO));
            trie.Delete(AWESOME_CO);
            Assert.False(trie.Search(AWESOME_CO));
        }
    }
}