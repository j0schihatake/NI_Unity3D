using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NISample - пример входной набор данных + заданный(известный) результат.
/// </summary>
public class NISample : MonoBehaviour
{
    // Уникальное, смысловое название набора входных + заданных выходных данных(пример).
    public string description = "Набор входных данных + заданных выходных данных(пример).";

    // Список входных нейронов со значениями:
    public List<NINeuron> input = new List<NINeuron>();

    // Список выходных нейронов со значениями:
    public List<NINeuron> output = new List<NINeuron>();

    public NISample cloneNISample(NISample sample, string description_new_sample)
    {

        NISample returned = new NISample();

        returned.description = description_new_sample;

        //Клонируем каждый входной нейрон:
        for(int i = 0; i < sample.input.Count; i++)
        {
            NINeuron next = sample.input[i];
            NINeuron nextClone = new NINeuron(next.typeIndex);
            nextClone.intCount = next.intCount;
            nextClone.boolCount = next.boolCount;
            nextClone.floatCount = next.floatCount;
            returned.input.Add(nextClone);
        }

        //Клонируем каждый выходной нейрон:
        for(int j = 0; j < sample.output.Count; j++)
        {
            NINeuron next = sample.output[j];
            NINeuron nextClone = new NINeuron(next.typeIndex);
            nextClone.intCount = next.intCount;
            nextClone.boolCount = next.boolCount;
            nextClone.floatCount = next.floatCount;
            returned.output.Add(nextClone);
        }

        return returned;
    }
}
