using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Brackets : MonoBehaviour
{
    private string _bracketsString;
    private int _count;
    private int _type;
    private string _values;

    public string BracketsString 
    { get { return _bracketsString; } 
      set { _bracketsString = value; UpdateValues(); } }
    public int Count { get { return _count; } }
    public int Type { get { return _type; } }
    public string Values { get { return _values; } }

    public void Show()
    {
        Debug.Log("The contet of the Brackets is: " + _bracketsString);
    }

    public bool IsComplete(string fullString)
    {
        if (CountBrackets(fullString.ToCharArray(), '(') != CountBrackets(fullString.ToCharArray(), ')')) return false;
        if (CountBrackets(fullString.ToCharArray(), '[') != CountBrackets(fullString.ToCharArray(), ']')) return false;
        if (CountBrackets(fullString.ToCharArray(), '{') != CountBrackets(fullString.ToCharArray(), '}')) return false;
        return true;
    }

    private int CountBrackets(char[] charArray, char bracket)
    {
        if (charArray.Length == 0) return 0;
        if (charArray[0] == bracket)
        {
            return 1 + CountBrackets(charArray[1..], bracket);
        }
        else return 0 + CountBrackets(charArray[1..], bracket);
    }

    private void UpdateValues()
    {
        CountComplete(_bracketsString);
        CountType(_bracketsString);
        GetValues();
    }

    private void CountComplete(string fullString)
    {
        int temp = 0;
        int answer = 0;
        temp = CountBrackets(fullString.ToCharArray(), '(') + CountBrackets(fullString.ToCharArray(), ')');
        answer += Mathf.RoundToInt(temp / 2);
        temp = CountBrackets(fullString.ToCharArray(), '[') + CountBrackets(fullString.ToCharArray(), ']');
        answer += Mathf.RoundToInt(temp / 2);
        temp = CountBrackets(fullString.ToCharArray(), '{') + CountBrackets(fullString.ToCharArray(), '}');
        answer += Mathf.RoundToInt(temp / 2);
        _count = answer;
    }

    private void CountType(string fullString)
    {
        int answer = 0;
        if (CountBrackets(fullString.ToCharArray(), '(') + CountBrackets(fullString.ToCharArray(), ')') > 0) answer++;
        if (CountBrackets(fullString.ToCharArray(), '{') + CountBrackets(fullString.ToCharArray(), '}') > 0) answer++;
        if (CountBrackets(fullString.ToCharArray(), '[') + CountBrackets(fullString.ToCharArray(), ']') > 0) answer++;
        _type = answer;
    }

    private void GetValues()
    {
        _values = RemoveBrackets(_bracketsString.ToCharArray());
    }

    private string RemoveBrackets(char[] charArray)
    {
        if (charArray.Length == 0) return "";
        if (charArray[0] == '(' || charArray[0] == ')' || charArray[0] == '{' || charArray[0] == '}' || charArray[0] == '[' || charArray[0] == ']') return " " + RemoveBrackets(charArray[1..]);
        else return charArray[0] + RemoveBrackets(charArray[1..]);
    }


    //TODO
    private void Indexes()
    {

    }

    private int GetIndex(char[] charrArray, char openingBracket, char closingBracket)
    {
        if (charrArray.Length == 0) return 0;
        if (charrArray[0] == openingBracket) return 1 + GetIndex(charrArray[1..], openingBracket, closingBracket);
        return 1 + GetIndex(charrArray, openingBracket, closingBracket);
    }
}
