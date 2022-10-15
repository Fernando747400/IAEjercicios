using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Palindrome : MonoBehaviour
{
    private string _word;
    private string _palindrome;
    private string _value;

    private Queue<char> _front = new Queue<char>();
    private Stack<char> _reverse = new Stack<char>();

    public string Word { set { _word = value;  EvaluateString(_word); } }
    public int Size { get { return _palindrome.Length; } }
    public int Count { get { return GetCount(); } }

    #region Public Methods
    public void Show()
    {
        if (_palindrome != "") Debug.Log("The palindrome is: " + _palindrome);
        if (_value != "") Debug.Log("The value that isn't a palidrome is: " + _value);
    }

    public void New(string word)
    {
        Word = word;
    }

    public void Remove(int index = 0)
    {
        if (_palindrome == "") throw new ArgumentNullException("The current word is empty");
        if (index > _palindrome.Length - 1) throw new ArgumentOutOfRangeException("Index is bigger than Palindrome length");

        string ans;
        bool hasMiddle = (_palindrome.Length % 2 == 0)? false : true;
        int middle = Mathf.RoundToInt((_palindrome.Length / 2));
        if(index >= middle) index = (_palindrome.Length - 1) - index;

        if(hasMiddle && index == middle)
        {
           ans = RemoveFromString(_palindrome, index);
           New(ans);
           return;
        }

        ans = InvertAndBuildString(RemoveFromString(_palindrome, index));
        New(InvertAndBuildString(RemoveFromString(ans, index)));
    }

    public void Add(char element, int index = 0)
    {
        if (_palindrome == "") throw new ArgumentNullException("The current word is empty");
        if (index > _palindrome.Length - 1) throw new ArgumentOutOfRangeException("Index is bigger than Palindrome length");

        string ans;
        bool hasMiddle = (_palindrome.Length % 2 == 0) ? false : true;
        int middle = Mathf.RoundToInt((_palindrome.Length / 2));
        if (index >= middle) index = (_palindrome.Length - 1) - index;

        if(hasMiddle && index == middle)
        {
            ans = InsertIntoString(_palindrome, element, index);
            New(ans);
            return;
        }

        ans = InvertAndBuildString(InsertIntoString(_palindrome, element, index));
        New(InvertAndBuildString(InsertIntoString(ans, element, index)));
    }

    public void Multiply(int multiplyBy)
    {
        if (_palindrome == "") throw new ArgumentNullException("The current word is empty");
        New(MultiplyPalindrome(_palindrome, multiplyBy));
    }
    #endregion

    #region Private Methods
    private void EvaluateString(string text)
    {
        _palindrome = "";
        _value = "";
        _front.Clear();
        _reverse.Clear();
        if (IsPalindrome(PoblateQueue(_front, text.ToLower()), PoblateStack(_reverse,text.ToLower()))) _palindrome = text;
        else _value = text;
    }

    private bool IsPalindrome(Queue<char> front, Stack<char> reverse)
    {
        if (front.Count == 0 && reverse.Count == 0) return true;
        else if (front.Peek() != reverse.Peek()) return false;
        else
        {
            front.Dequeue();
            reverse.Pop();
            return IsPalindrome(front, reverse);
        }
    }

    private int GetCount()
    {
        if (_palindrome != "") return GetMostRepeatedCount(BuildDictionary(_palindrome, new Dictionary<char, int>()));
        return GetMostRepeatedCount(BuildDictionary(_value, new Dictionary<char, int>()));
    }

    private Queue<char> PoblateQueue(Queue<char> front, string text)
    {
        if (text.Length == 0) return front;
        front.Enqueue(text[0]);
        return PoblateQueue(front, text[1..]);
    }

    private Stack<char> PoblateStack(Stack<char> reverse, string text)
    {
        if (text.Length == 0) return reverse;
        reverse.Push(text[0]);
        return PoblateStack(reverse, text[1..]);
    }

    private string RemoveFromString(string text, int index, int current = 0)
    {
        if (text.Length == 0) return text;
        if (current == index) return RemoveFromString(text[1..], index, current + 1);
        return text[0] + RemoveFromString(text[1..], index, current + 1);
    }

    private string InsertIntoString(string text, char element, int index, int current = 0)
    {
        if (text.Length == 0) return text;
        if (current == index) return element + InsertIntoString(text, element, index, current +1);
        return text[0] + InsertIntoString(text[1..], element, index, current +1);
    }

    private string MultiplyPalindrome(string text, int n)
    {
        if (n == 0) return text;
        return text + MultiplyPalindrome(text, n-1);
    }

    private string InvertAndBuildString(string text)
    {
        return (BuildString(InvertString(new Stack<char>(), text)));
    }

    private Stack<char> InvertString(Stack<char> charStack, string text = "")
    {
        if (text.Length == 0) return charStack;
        charStack.Push(text[0]);
        return InvertString(charStack, text[1..]);
    }

    private string BuildString(Stack<char> charStack, string text = "")
    {
        if (charStack.Count == 0) return text;
        text += charStack.Pop();
        return BuildString(charStack, text);
    }

    private Dictionary<char, int> BuildDictionary(string text, Dictionary<char, int> dictionary)
    {
        if (text.Length == 0) return dictionary;
        if (dictionary.ContainsKey(text[0]))
        {
            dictionary[text[0]] += 1;
            return BuildDictionary(text[1..], dictionary);
        }
        dictionary.Add(text[0], 1);
        return BuildDictionary(text[1..], dictionary);
    }

    private int GetMostRepeatedCount(Dictionary<char, int> dictionary,int index = 0, int count = 0)
    {
        if (index >= dictionary.Count) return count;
        if (dictionary.Values.ElementAt(index) > count) count = dictionary.Values.ElementAt(index);
        return GetMostRepeatedCount(dictionary, index + 1, count);
    }

    #endregion
}
