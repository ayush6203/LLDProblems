namespace LLDProblems.DataStructuresDesign
{
    public class Trie
    {
        TrieNode _root;
        public Trie()
        {
            _root = new TrieNode();
        }

        public void Insert(string word)
        {
            TrieNode currNode = _root;
            foreach(char c in word)
            {
                if (currNode.Child[c - 'a'] is null)
                    currNode.Child[c - 'a'] = new TrieNode();

                currNode = currNode.Child[c - 'a'];
            }

            currNode.IsEndOfWord = true;
        }

        public bool Search(string word)
        {
            TrieNode currNode = _root;
            foreach (char c in word)
            {
                if (currNode.Child[c - 'a'] is null)
                    return false;

                currNode = currNode.Child[c - 'a'];
            }

            return currNode.IsEndOfWord;
        }

        public bool Prefix(string word)
        {
            TrieNode currNode = _root;
            foreach (char c in word)
            {
                if (currNode.Child[c - 'a'] is null)
                    return false;

                currNode = currNode.Child[c - 'a'];
            }

            return true;
        }

        class TrieNode
        {
            public bool IsEndOfWord = false;
            public TrieNode[] Child = new TrieNode[26];
        }
    }
}
