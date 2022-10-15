using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Palindrome))]
public class PalindromeController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _stringToHandle;
    [SerializeField] private int _indexToRemove;
    [SerializeField] private int _indexToAdd;
    [SerializeField] private char _charToAdd;
    [SerializeField] private int _multiplayBy;

    private Palindrome _palindrome;
    // Start is called before the first frame update
    void Start()
    {
        _palindrome = this.GetComponent<Palindrome>();
    }

    public void DoHandle()
    {
       _palindrome.Word = _stringToHandle;
       _palindrome.Show();
    }

    public void SpecialCases()
    {
        _palindrome.Show();
        _palindrome.Remove(_indexToRemove);
        _palindrome.Show();
        _palindrome.Add(_charToAdd, _indexToAdd);
        _palindrome.Show();
        _palindrome.Multiply(_multiplayBy);
        _palindrome.Show();
    }

    public void ShowCounts()
    {
        _palindrome.Show();
        Debug.Log("The count of the most repeating letters is " + _palindrome.Count);
        Debug.Log("The palindrome has " + _palindrome.Size + " characters");
    }
}

[CustomEditor(typeof(PalindromeController))]
public class PalindromeTester : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PalindromeController palindromeController = (PalindromeController)target;
        GUILayout.Space(20);
        if (GUILayout.Button("Evaluate String"))
        {
            palindromeController.DoHandle();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("Do special cases"))
        {
            palindromeController.SpecialCases();
        }


        GUILayout.Space(20);
        if (GUILayout.Button("Show Counts"))
        {
            palindromeController.ShowCounts();
        }
    }
}
