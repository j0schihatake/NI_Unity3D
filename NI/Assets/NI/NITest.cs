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
            Debug.Log("Ситуация следующая:");
            foreach(NINeuron neuron in nextRand.input().neurons){
                neuron.setValue(RandomUtils.getRandomNumber(neuron.minRandom, neuron.maxRandom));
                Debug.Log(neuron.description + ":   " + neuron.getValue());
            }
            NISample resultSample = niNetwork.getResult(nextRand);
            int maxIndex = resultSample.output().getMax();
            NINeuron max = resultSample.output().neurons[maxIndex];
            Debug.Log("Немного подумав принимаем решение: " + max.description);
        }
    }

    public static NINetwork createTestContent(){

        NINetwork bookNetwork = new NINetwork();
        bookNetwork.tableName = "exampleBookNetwork".ToLower();
        bookNetwork.description = "Нейронная сеть по примеру из замечательной книги";

        NISample nextSample = null;

        //----------------------------------------- Формируем первый пример:

        NISample firstSample = new NISample();
        firstSample.tableName = "wonder";
        firstSample.description = "Пример когда следует применить поведение: wonder.";

        NILayer wonderInputLayer = new NILayer();

        NINeuron health = new NINeuron(2);
        health.description = "Нейрон отражающий состояние здоровья.";
        health.minRandom = 0;
        health.maxRandom = 3;

        NINeuron knifle = new NINeuron(0);
        knifle.description = "Нейрон отражающий наличие ножа.";
        knifle.minRandom = 0;
        knifle.maxRandom = 2;

        NINeuron gun = new NINeuron(0);
        gun.description = "Нейрон отражающий наличие стрелкового оружия.";
        gun.minRandom = 0;
        gun.maxRandom = 2;

        NINeuron enemy = new NINeuron(0);
        enemy.description = "Нейрон отражающий количество противников.";
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
        atack.description = "Действие : атака";

        NINeuron run = new NINeuron(0);
        run.description = "Действие : побег";

        NINeuron wander = new NINeuron(1);
        wander.description = "Действие : наблюдение";

        NINeuron hide = new NINeuron(0);
        hide.description = "Действие : спрятаться";

        firstOutputLayer.neurons.Add(atack);
        firstOutputLayer.neurons.Add(run);
        firstOutputLayer.neurons.Add(wander);
        firstOutputLayer.neurons.Add(hide);

        firstSample.layer.Add(wonderInputLayer);
        firstSample.layer.Add(firstOutputLayer);

        bookNetwork.addSample(firstSample);

        // -------------------Далее через дублирование другие примеры при необходимости:

        // Пример 2:
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

        // Пример 3:
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

        // Пример 4:
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

        // Пример 5:
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

        // Пример 6:
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

        // Пример 7:
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

        // Пример 8:
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

        // Пример 9:
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

        // Пример 10:
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

        // Пример 11:
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

        // Пример 12:
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

        // Пример 13:
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

        // Пример 14:
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

        // Пример 15:
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

        // Пример 16:
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

        // Пример 17:
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

        // Пример 18:
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

        // Пример 19:
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

        // Теперь выполняем весь цикл обучения:
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
