using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleSort : MonoBehaviour
{
    // Driver method
    public void Start()
    {
        int[] arr = { 64, 34, 25, 12, 22, 11, 90 };
        bubbleSort(arr);
        print("Sorted array");
     
        //Array.Sort(arr);
        printArray(arr);
    }

    public void bubbleSort(int[] arrayToSort)
    {
        int lengthOfArray = arrayToSort.Length;

        for (int i = 0; i < lengthOfArray - 1; i++)

            for (int j = 0; j < lengthOfArray - i - 1; j++)
                if (arrayToSort[j] > arrayToSort[j + 1])
                {  
                    // swap temp and arr[i]
                    int temp = arrayToSort[j];

                    arrayToSort[j] = arrayToSort[j + 1];

                    arrayToSort[j + 1] = temp;
                }
    }


    /* Prints the array */
   public void printArray(int[] arr)
   {
        int lengthOfArray = arr.Length;
        for (int i = 0; i < lengthOfArray; ++i)
            print(arr[i] + " ");
   }

    public void Showing()
    {

        for (int i = 0; i < 1000; i++)
        {
            if (i % 4 == 0)
            { 
            // Do Something!
            }

        }
    }

    public void BubbleSortFunction(int[] arrayToSort)
    { 
        //Bubble Sort Stuff with Ints
    }

    public void BubbleSortFunction(float[] arrayToSort)
    {
        //Bubble Sort Stuff with Floats
    }
}
