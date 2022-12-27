using ReelWords.DataStructures.Trie;

namespace ReelWords
{
    public class Trie
    {
        private Node RootNode;

        public Trie()
        {
            RootNode = new Node('^');
        }

        public bool Search(string word)
        {
            var node = GetNode(word);
            return node != null && node.Child[0]?.Character == '=';

        }

        public void Insert(string word)
        {
            Node currentNode = RootNode;

            for(var i = 0; i < word.Length; i++)
            {
                char currentCharacter = word[i];
                if(IsFreeNode(currentNode, currentCharacter))
                {
                    currentNode.Child[currentCharacter - 'a'] = new Node(currentCharacter);
                }
                currentNode = currentNode.Child[currentCharacter - 'a'];
            }

            currentNode.Child[0] = new Node('=');
        }

        public void Delete(string word)
        {
            var node = GetNode(word);
            if(node != null)
            {
                node.Child[0] = null;
            }
        }


        private Node GetNode(string word)
        {
            Node currentNode = RootNode;

            for (var i = 0; i < word.Length; i++)
            {
                char currentCharacter = word[i];
                if (IsFreeNode(currentNode, currentCharacter))
                {
                    return null;
                }
                currentNode = currentNode.Child[currentCharacter - 'a'];
            }
            return currentNode;
        }

        private bool IsFreeNode(Node currentNode, char c)
        {
            //we are substracting the ASCII values between 'a' the first element of alphabet in lowercase (26 characters) and the evaluated one.
            //with the substracted value (a position in the Child nodes) we could evaluate if this child node is "free"(null) or not.
            return currentNode.Child[c - 'a'] == null ? true : false;
        }

    }
}