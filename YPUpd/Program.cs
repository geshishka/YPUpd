﻿using System;
using System.Collections.Generic;
using System.Linq;

public class CustomBaseNumber
{
    public int Base { get; private set; }
    public int[] Digits { get; private set; }

    public CustomBaseNumber(int[] digits, int numberBase)
    {
        if (numberBase < 2)
        {
            throw new ArgumentException("Base must be at least 2.");
        }

        Base = numberBase;
        Digits = digits;
    }

    public override string ToString()
    {
        return string.Join("", Digits.Select(d => d < 10 ? d.ToString() : ((char)('A' + d - 10)).ToString()));
    }

    public int ToDecimal()
    {
        int decimalValue = 0;
        for (int i = 0; i < Digits.Length; i++)
        {
            decimalValue = decimalValue * Base + Digits[i];
        }
        return decimalValue;
    }

    public static CustomBaseNumber FromDecimal(int decimalValue, int numberBase)
    {
        if (decimalValue == 0) return new CustomBaseNumber(new int[] { 0 }, numberBase);

        List<int> digits = new List<int>();
        while (decimalValue > 0)
        {
            digits.Insert(0, decimalValue % numberBase);
            decimalValue /= numberBase;
        }
        return new CustomBaseNumber(digits.ToArray(), numberBase);
    }

    public CustomBaseNumber ConvertBase(int newBase)
    {
        int decimalValue = this.ToDecimal();
        return FromDecimal(decimalValue, newBase);
    }

    public static CustomBaseNumber operator +(CustomBaseNumber a, CustomBaseNumber b)
    {
        if (a.Base != b.Base)
        {
            throw new ArgumentException("Bases must be the same for addition.");
        }
        int result = a.ToDecimal() + b.ToDecimal();
        return FromDecimal(result, a.Base);
    }

    public static CustomBaseNumber operator -(CustomBaseNumber a, CustomBaseNumber b)
    {
        if (a.Base != b.Base)
        {
            throw new ArgumentException("Bases must be the same for subtraction.");
        }
        int result = a.ToDecimal() - b.ToDecimal();
        return FromDecimal(result, a.Base);
    }

    public static CustomBaseNumber operator *(CustomBaseNumber a, CustomBaseNumber b)
    {
        if (a.Base != b.Base)
        {
            throw new ArgumentException("Bases must be the same for multiplication.");
        }
        int result = a.ToDecimal() * b.ToDecimal();
        return FromDecimal(result, a.Base);
    }

    public static CustomBaseNumber operator /(CustomBaseNumber a, CustomBaseNumber b)
    {
        if (a.Base != b.Base)
        {
            throw new ArgumentException("Bases must be the same for division.");
        }
        int result = a.ToDecimal() / b.ToDecimal();
        return FromDecimal(result, a.Base);
    }

    public static CustomBaseNumber operator %(CustomBaseNumber a, CustomBaseNumber b)
    {
        if (a.Base != b.Base)
        {
            throw new ArgumentException("Bases must be the same for modulus operation.");
        }
        int result = a.ToDecimal() % b.ToDecimal();
        return FromDecimal(result, a.Base);
    }

    public static bool operator ==(CustomBaseNumber a, CustomBaseNumber b)
    {
        return a.ToDecimal() == b.ToDecimal();
    }

    public static bool operator !=(CustomBaseNumber a, CustomBaseNumber b)
    {
        return a.ToDecimal() != b.ToDecimal();
    }

    public static bool operator <(CustomBaseNumber a, CustomBaseNumber b)
    {
        return a.ToDecimal() < b.ToDecimal();
    }

    public static bool operator >(CustomBaseNumber a, CustomBaseNumber b)
    {
        return a.ToDecimal() > b.ToDecimal();
    }

    public static bool operator <=(CustomBaseNumber a, CustomBaseNumber b)
    {
        return a.ToDecimal() <= b.ToDecimal();
    }

    public static bool operator >=(CustomBaseNumber a, CustomBaseNumber b)
    {
        return a.ToDecimal() >= b.ToDecimal();
    }

    public override bool Equals(object obj)
    {
        if (obj is CustomBaseNumber)
        {
            return this == (CustomBaseNumber)obj;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return ToDecimal().GetHashCode();
    }

    public static CustomBaseNumber ReadFromConsole()
    {
        Console.Write("Введите основание числа: ");
        int numberBase = int.Parse(Console.ReadLine());

        Console.Write("Введите число в указанной базе: ");
        string input = Console.ReadLine();
        int[] digits = input.ToUpper().Select(c =>
        {
            if (char.IsDigit(c))
                return int.Parse(c.ToString());
            else
                return c - 'A' + 10;
        }).ToArray();

        return new CustomBaseNumber(digits, numberBase);
    }
}

class Program
{
    static void Main()
    {
        CustomBaseNumber num1 = CustomBaseNumber.ReadFromConsole();
        CustomBaseNumber num2 = CustomBaseNumber.ReadFromConsole();

        Console.WriteLine($"число 1: {num1}");
        Console.WriteLine($"число 2: {num2}");

        
