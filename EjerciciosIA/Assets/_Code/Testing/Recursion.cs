using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recursion : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i <= 50; i++)
        {
            Debug.Log("El factorial de " + i + " es igual a " + Factorial(i));
        }
        
    }
   
   private int Factorial(int n)
    {        
        if (n <= 1) return n;
        n = n * Factorial(n - 1);
        return n;
    }
}
