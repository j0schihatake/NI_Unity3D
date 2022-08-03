using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NISample
{
    public static string SAMPLE_PREFIX = "_samples";

    private string networkName;

    private string sampleName;

    public string tableName = "test_nisample";

    public string description = "Набор входных данных + заданных выходных данных(пример).";

    public List<NILayer> layer = new List<NILayer>();

    public NISample dublicate()
    {

        NISample dublicate = new NISample();
        dublicate.tableName = tableName;
        dublicate.description = description;
        dublicate.sampleName = sampleName;
        dublicate.networkName = networkName;

        List<NILayer> dublicateLayers = new List<NILayer>();
        for(int i = 0; i < layer.Count; i++)
        {
            dublicate.layer.Add(layer[i].dublicate());
        }
        return dublicate;
    }

    public NILayer input()
    {
        return layer[0];
    }

    public NILayer output()
    {
        return layer[1];
    }
}
