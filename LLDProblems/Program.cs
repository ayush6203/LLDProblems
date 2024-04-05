// See https://aka.ms/new-console-template for more information
using LLDProblems.DataStructuresDesign;
using LLDProblems.StandardProblems;


Console.WriteLine("Here you will find all LLD problems");
//TicTacToe ticTacToe = new TicTacToe();
//ticTacToe.StartGame();









# region Data Structures Design

// LRU Desing
//LRU lru = new LRU(5);
//lru.AddKeyValue("ayush", "ayush is good");
//lru.AddKeyValue("piyush", "piyush is good");
//lru.AddKeyValue("saroj", "saroj is good");
//lru.AddKeyValue("arsh", "arsh is good");
//lru.AddKeyValue("siya", "siya is good");
//Console.WriteLine(lru.GetValue("ayush"));
//lru.AddKeyValue("babi", "babi is good");
//Console.WriteLine(lru.GetValue("saroj"));
//lru.AddKeyValue("laliya", "laliya is good");
//Console.WriteLine(lru.GetValue("ayush"));


// Trie Design
Trie trie = new Trie();
trie.Insert("a");
trie.Insert("ab");
trie.Insert("abc");
trie.Insert("apple");
trie.Insert("applea");
trie.Search("apple");
trie.Search("applea");


#endregion