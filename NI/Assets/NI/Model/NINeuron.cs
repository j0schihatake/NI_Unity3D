using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NINeuron : MonoBehaviour
{
    public int layerId;

    public string networkName;

    public string sampleName;

    public int layerType;

    public string description = "Понятное пояснение что обозначает данный нейрон.";

    public float value;

    // --------------------------------------- Constructors:
    public NINeuron(float o)
    {
        setValue(o);
    }

    public NINeuron(int o)
    {
        setValue(o);
    }

    public NINeuron(bool o)
    {
        setValue(o);
    }

    // ---------------------------------------- Setters:

    // Значение всегда преобразуется во float;

    public void setValue(int o)
    {
        this.value = o;
    }

    public void setValue(float o)
    {
        this.value = o;
    }

    public void setValue(bool o)
    {
        this.value = !o ? 0 : 1;
    }

    public float getValue() {
        return this.value;
    }

    // ---------------------------------------- Random
    public int minRandom = 0;
    public int maxRandom = 10;

    public NINeuron() { }

    // ---------------------------------------- Constructors
    public NINeuron dublicate()
    {
        NINeuron dublicate = new NINeuron();
        dublicate.description = description;
        dublicate.layerId = layerId;
        dublicate.layerType = layerType;
        dublicate.sampleName = sampleName;
        dublicate.setValue(value);
        dublicate.minRandom = minRandom;
        dublicate.maxRandom = maxRandom;
        return dublicate;
    }
}
