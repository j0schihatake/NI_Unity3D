using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NITest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void testNetwork(NINetwork niNetwork){

        for(int i = 0; i< 30; i++){
            Debug.Log("NINetwork." + niNetwork.description + " next();");
            Debug.Log("");
            NISample nextRand = niNetwork.first();
            Debug.Log("�������� ���������:");
            foreach(NINeuron neuron in nextRand.input().neurons){
                neuron.setValue(RandomUtils.getRandomNumber(neuron.minRandom, neuron.maxRandom));
                Debug.Log(neuron.description + ":   " + neuron.getValue());
            }
            NISample resultSample = niNetwork.getResult(nextRand);
            int maxIndex = resultSample.output().getMax();
            NINeuron max = resultSample.output().neurons[maxIndex];
            Debug.Log("������� ������� ��������� �������: " + max.description);
        }
    }

    public static NINetwork createTestContent(){

        NINetwork bookNetwork = new NINetwork();
        bookNetwork.tableName = "exampleBookNetwork".ToLower();
        bookNetwork.description = "��������� ���� �� ������� �� ������������� �����";

        NISample nextSample = null;

        //----------------------------------------- ��������� ������ ������:

        NISample firstSample = new NISample();
        firstSample.tableName = "wonder";
        firstSample.description = "������ ����� ������� ��������� ���������: wonder.";

        NILayer wonderInputLayer = new NILayer();

        NINeuron health = new NINeuron(2);
        health.description = "������ ���������� ��������� ��������.";
        health.minRandom = 0;
        health.maxRandom = 3;

        NINeuron knifle = new NINeuron(0);
        knifle.description = "������ ���������� ������� ����.";
        knifle.minRandom = 0;
        knifle.maxRandom = 2;

        NINeuron gun = new NINeuron(0);
        gun.description = "������ ���������� ������� ����������� ������.";
        gun.minRandom = 0;
        gun.maxRandom = 2;

        NINeuron enemy = new NINeuron(0);
        enemy.description = "������ ���������� ���������� �����������.";
        enemy.minRandom = 0;
        enemy.maxRandom = 4;

        wonderInputLayer.neurons.Add(health);
        wonderInputLayer.neurons.Add(knifle);
        wonderInputLayer.neurons.Add(gun);
        wonderInputLayer.neurons.Add(enemy);

        //"Attack", "Run", "Wander", "Hide"

        NILayer firstOutputLayer = new NILayer();
        firstOutputLayer.layerType = 2;

        NINeuron atack = new NINeuron(0);
        atack.description = "�������� : �����";

        NINeuron run = new NINeuron(0);
        run.description = "�������� : �����";

        NINeuron wander = new NINeuron(1);
        wander.description = "�������� : ����������";

        NINeuron hide = new NINeuron(0);
        hide.description = "�������� : ����������";

        firstOutputLayer.neurons.Add(atack);
        firstOutputLayer.neurons.Add(run);
        firstOutputLayer.neurons.Add(wander);
        firstOutputLayer.neurons.Add(hide);

        firstSample.layer.Add(wonderInputLayer);
        firstSample.layer.Add(firstOutputLayer);

        bookNetwork.addSample(firstSample);

        // -------------------����� ����� ������������ ������ ������� ��� �������������:

        // ������ 2:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(2);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(1);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 3:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(2);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(1);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(1);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 4:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(2);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(1);
        nextSample.input().neurons[3].setValue(2);

        nextSample.output().neurons[0].setValue(1);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 5:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(2);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(2);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 6:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(2);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(1);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 7:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(2);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(1);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 8:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(1);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(0);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(1);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 9:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(1);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 10:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(1);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(1);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(1);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 11:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(1);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(1);
        nextSample.input().neurons[3].setValue(2);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 12:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(1);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(2);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 13:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(1);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 14:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(0);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(0);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(1);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 15:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(0);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 16:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(0);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(1);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ 17:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(0);
        nextSample.input().neurons[1].setValue(0);
        nextSample.input().neurons[2].setValue(1);
        nextSample.input().neurons[3].setValue(2);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(1);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 18:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(0);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(2);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(1);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(0);
        bookNetwork.addSample(nextSample);

        // ������ 19:
        nextSample = firstSample.dublicate();
        nextSample.input().neurons[0].setValue(0);
        nextSample.input().neurons[1].setValue(1);
        nextSample.input().neurons[2].setValue(0);
        nextSample.input().neurons[3].setValue(1);

        nextSample.output().neurons[0].setValue(0);
        nextSample.output().neurons[1].setValue(0);
        nextSample.output().neurons[2].setValue(0);
        nextSample.output().neurons[3].setValue(1);
        bookNetwork.addSample(nextSample);

        // ������ ��������� ���� ���� ��������:
        bookNetwork.initialize(bookNetwork.samples[0]);
        bookNetwork.learn(10000);

        // niService.insertOrUpdateNINetwork(bookNetwork);

        // SqLiteUtil.insertNINetwork(bookNetwork);

        return bookNetwork;
    }

    public static List<NINetwork> readNINetwork(){
        /*
        List<string> allNetworkNames =  SqLiteUtil.selectAllNINetworkNameList();
        List<NINetwork> allNetwork = new List<NINetwork>();

        if(allNetworkNames != null && !allNetworkNames.isEmpty())
        {
            for(int i = 0; i < allNetworkNames.Count; i++)
            {
                string nextNetworkName = allNetworkNames[i];
                NINetwork next = SqLiteUtil.selectNINetwork(nextNetworkName.ToLower());
                allNetwork.Add(next);
            }
        }

        Debug.Log(allNetwork);

        return allNetwork;
        */
        return null;
    }
}
