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
        _palindrome.Remove(2);
        _palindrome.Show();
        _palindrome.Add('a');
        _palindrome.Add('b');
        _palindrome.Show();
        _palindrome.Multiply(5);
        _palindrome.Show();
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
    }
}
