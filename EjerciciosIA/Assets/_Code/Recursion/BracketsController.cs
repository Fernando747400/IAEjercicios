using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Brackets))]
public class BracketsController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string _stringToHandle;

    private Brackets _brackets;

    private void Start()
    {
        _brackets = this.GetComponent<Brackets>();
        DoHandle();
    }

    public void DoHandle()
    {
        _brackets.BracketsString = _stringToHandle;
        _brackets.Show();
        ShowValues();
    }

    private void ShowValues()
    {
        Debug.Log("Has complete brackets: " + _brackets.IsComplete(_brackets.BracketsString));
        Debug.Log("Complete pairs: " + _brackets.Count);
        Debug.Log("Different type of brackets: " + _brackets.Type);
        Debug.Log("String without brackets: " + _brackets.Values);
    }

}

[CustomEditor(typeof(BracketsController))]
public class UITester : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BracketsController bracketsController = (BracketsController)target;
        GUILayout.Space(20);
        if (GUILayout.Button("Evaluate String"))
        {          
            bracketsController.DoHandle();
        }
    }
}
