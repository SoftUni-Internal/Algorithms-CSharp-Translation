﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BitwiseSortAlgorithm
{
    public class Element
    {
        public int Key { get; set; }
    }

    const int MaxValue = 100;
    const int TestsCount = 100;
    static Random rand = new Random();

    static void Initialize(Element[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i] = new Element();
            elements[i].Key = rand.Next(0, MaxValue * 2);
        }
    }

    static void SwapValues(ref Element first, ref Element second)
    {
        Element swapper = first;
        first = second;
        second = swapper;
    }

    static void BitwiseSort(Element[] elements, int leftIndex, int rightIndex, uint bitMask)
    {
        if (rightIndex > leftIndex && bitMask > 0)
        {
            int i = leftIndex;
            int j = rightIndex;
            while (j != i)
            {
                while ((elements[i].Key & bitMask) != bitMask && i < j)
                { i++; }
                while ((elements[j].Key & bitMask) == bitMask && i < j)
                { j--; }
                SwapValues(ref elements[i], ref elements[j]);
            }

            if ((elements[rightIndex].Key & bitMask) != bitMask)
            { j++; }
            BitwiseSort(elements, leftIndex, j - 1, bitMask >> 1);
            BitwiseSort(elements, j, rightIndex, bitMask >> 1);
        }
    }

    static void PrintElements(Element[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
            Console.Write(elements[i].Key + " ");
        Console.WriteLine();
    }

    static void Check(Element[] elements)
    {
        bool isSorted = true;
        for (int i = 0; i < elements.Length - 1; i++)
            if (elements[i].Key > elements[i + 1].Key)
            {
                isSorted = false;
                break;
            }
        if (!isSorted)
            throw new Exception("Масивът не е сортиран правилно.");
    }

    static void Main()
    {
        Element[] elements = new Element[MaxValue];
        for (int i = 0; i < TestsCount; i++)
        {
            Console.WriteLine("----------Тест " + i + "----------");
            Initialize(elements);
            Console.WriteLine("Масив преди сортиране : ");
            PrintElements(elements);
            uint bitMask = (uint)int.MaxValue + 1;
            BitwiseSort(elements, 0, MaxValue - 1, bitMask);
            Console.WriteLine("Масив след сортиране : ");
            PrintElements(elements);
            Check(elements);
        }
    }
}
