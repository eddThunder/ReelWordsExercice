namespace ReelWords.DataStructures.Trie
{
    public class Node
    {
        public char Character;
        public Node[] Child;

        public Node(char c)
        {
            Character = c;
            Child = new Node[Constants.MAXNUM_CHARACTERS];
        }
    }
}
