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

        public IList<string> GetKWords(int n)
        {
            IList<string> lt = new List<string>();
            DFS(_root, n, "", lt);
            return lt;
        }

        private void DFS(TrieNode node, int k, string curr, IList<string> lt)
        {
            if (lt.Count == k)
                return;

            if (node.IsEndOfWord)
                lt.Add(curr);

            for(int i = 0; i < 26; i++)
            {
                if(node.Child[i] != null)
                {
                    char ch = (char)('a' + i);
                    DFS(node.Child[i], k, curr + ch, lt);
                }
            }
        }

        class TrieNode
        {
            public bool IsEndOfWord = false;
            public TrieNode[] Child = new TrieNode[26];
        }
    }
}
