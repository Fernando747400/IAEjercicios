using System.Collections.Generic;
using UnityEngine;

public partial class Recursion : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _factorial;
    [SerializeField] private int _fibonacci;
    [SerializeField] private int[] _countArray;
    [SerializeField] private List<float> _sumList;

    private void Start()
    {
        Debug.Log("El factorial de " + _factorial + " es igual a: " + Factorial(_factorial));
        Debug.Log("La serie de Fibonacci de nivel " + _fibonacci + " es igual a: " + Fibonacci(_fibonacci));
        Debug.Log("La cantidad de elementos en el array es igual a: " + CountArray(_countArray));
        Debug.Log("La suma de la lista es igual a: " + SumList(_sumList));
    }
   
   private int Factorial(int n)
    {        
        if (n == 0) return 1;
        return n * Factorial(n - 1);
    }

    private int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    private int CountArray(int[] array)
    {
        if (array.Length == 0) return 0;
        return 1 + CountArray(array[1..]);
    }

    private float SumList(List<float> list)
    {
        if (list.Count == 0) return 0;
        return list[0] + SumList(list.GetRange(1, list.Count - 1));
    }
}
