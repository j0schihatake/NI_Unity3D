using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NILayer
{
    public int layerId;                                                // порядковый номер слоя слева направо: 0.1.2...

    public string networkName;

    public string sampleName;

    public int layerType = 0;                                          // 0 - входной, 1 - скрытый, 2 - выходной

    public string tableName = "no_name_layer_table";

    public List<NINeuron> neurons = new List<NINeuron>();

    //-------------------------------- Constructors:

    public NILayer() {}

    public NILayer(string tableName, List<NINeuron> neurons)
    {
        this.tableName = tableName;
        this.neurons = neurons;
    }

    public NILayer dublicate()
    {
        NILayer dublicateLayer = new NILayer();
        dublicateLayer.layerType = this.layerType;
        dublicateLayer.layerId = this.layerId;
        dublicateLayer.networkName = this.networkName;
        dublicateLayer.tableName = this.tableName;
        dublicateLayer.sampleName = this.sampleName;
        for(int i = 0; i < neurons.Count; i++)
        {
            dublicateLayer.neurons.Add(neurons[i].dublicate());
        }
        return dublicateLayer;
    }

    //--------------------------------------- Utils:

    public int getMax()
    {
        NINeuron resultNeuron = null;
        int result = 0;
        for(int i = 0; i < this.neurons.Count; i++)
        {
            NINeuron neuron = this.neurons[i];
            if(resultNeuron == null || resultNeuron.getValue() < neuron.getValue())
            {
                resultNeuron = neuron;
                result = i;
            }
        }
        return result;
    }

    public NILayer getRandomValue()
    {
        NILayer result = dublicate();
        foreach(NINeuron neuron in result.neurons)
        {
            //neuron.setValue();
        }
        return result;
    }
}
