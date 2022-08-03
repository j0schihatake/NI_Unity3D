using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NINetwork : MonoBehaviour
{
    public string name = "default_book_example";

    public string description = "Сеть без имени.";

    public string tableName = "no_name_ni_network";

    public List<NISample> samples = new List<NISample>();

    // Конвертирую пример из книги:
    public int INPUT_NEURONS = 4;
    public int HIDDEN_NEURONS = 3;
    public int OUTPUT_NEURONS = 4;

    // Смещение
    private int dest = 1;

    public int HIDDEN_LAYER_COUNT = 1;

    // Коэффициент обучения:
    public float LEARN_RATE = 0.2f;

    // Случайные веса:
    public float RAND_WEIGHT = 0f;

    public float RAND_MAX = 0.5f;

    // Входные данные от пользователя:
    public NISample inputSample = null;

    // Веса:
    // Вход скрытых ячеек(со смещением):
    public float[,] wih;

    // Вход выходных ячеек(со смещением):
    public float[,] who;

    // Активаторы:
    public float[] inputs;
    public float[] hidden;
    public float[] outputs;
    public float[] actual;

    // Ошибки:
    public float[] erro;
    public float[] errh;
    public float err = 0f;
    public int sum = 0;

    //private bool learn = false;

    //--------------------------------------- Utils:

    public NINetwork initialize(NISample sample)
    {

        INPUT_NEURONS = sample.layer[0].neurons.Count;
        HIDDEN_NEURONS = INPUT_NEURONS - dest;
        OUTPUT_NEURONS = sample.layer[1].neurons.Count;

        // Веса
        // Вход скрытых ячеек(со смещением)
        wih = new float[INPUT_NEURONS + dest, HIDDEN_NEURONS];

        // Вход выходных ячеек(со смещением)
        who = new float[HIDDEN_NEURONS + dest, OUTPUT_NEURONS];

        // Активаторы
        inputs = new float[INPUT_NEURONS];
        hidden = new float[HIDDEN_NEURONS];
        outputs = new float[OUTPUT_NEURONS];
        actual = new float[OUTPUT_NEURONS];

        // Ошибки
        erro = new float[OUTPUT_NEURONS];
        errh = new float[HIDDEN_NEURONS];

        // Инициализировать генератор случайных чисел
        assignRandomWeights();
        return this;
    }

    /**
     * Выполнет обучение сети:
     * 100000
     */
    public NINetwork learn(int iterationCount)
    {

        int iterations = 0;

        // Обучить сеть
        while(iterations < iterationCount)
        {
            for(int i = 0; i < samples.Count; i++)
            {
                NISample sample = samples[i];

                // тут подаем на входы и выходы "правильные значения"
                for(int j = 0; j < sample.layer[0].neurons.Count; j++)
                {
                    inputs[j] = sample.layer[0].neurons[j].getValue();
                }

                for(int k = 0; k < sample.layer[1].neurons.Count; k++)
                {
                    outputs[k] = sample.layer[1].neurons[k].getValue();
                }

                feedForward();

                err = 0.0f;

                // Квадратичная ошибка для каждого из выходов:
                for(int m = 0; m < sample.layer[1].neurons.Count; m++)
                {
                    err += Mathf.Sqrt((sample.layer[1].neurons[m].getValue() - actual[0]));
                }

                err = 0.5f * err;

                iterations++;

                // Выполняем обучение:
                backPropagate();
            }
        }
        return this;
    }

    // Прямое распространение
    private NINetwork feedForward()
    {
        int inp, hid, outs;
        float sum;

        // Вычислить вход в скрытый слой:
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            sum = 0f;
            for(inp = 0; inp < INPUT_NEURONS; inp++)
            {
                sum += inputs[inp] * wih[inp, hid];
            }
            // Добавить смещение
            //for(int i = 0; i < dest; i++){
            sum += wih[INPUT_NEURONS, hid];
            //}
            hidden[hid] = sigmoid(sum);
        }

        // Вычислить вход в выходной слой:
        for(outs = 0; outs < OUTPUT_NEURONS; outs++)
        {
            sum = 0.0f;
            for(hid = 0; hid < HIDDEN_NEURONS; hid++)
            {
                sum += hidden[hid] * who[hid, outs];
            }
            // Добавить смещение
            //for(int i = 0; i < dest; i++){
            sum += who[HIDDEN_NEURONS, outs];
            //}
            actual[outs] = sigmoid(sum);
        }
        return this;
    }

    // Обратное распространение(обучение)
    private NINetwork backPropagate()
    {
        int inp, hid, outs;

        // Вычислить ошибку выходного слоя (шаг 3 для выходных ячеек)
        for(outs = 0; outs < OUTPUT_NEURONS; outs++) {
            erro[outs] = (outputs[outs] - actual[outs]) * sigmoidDerivative(actual[outs]);
        }

        // Вычислить ошибку скрытого слоя (шаг 3 для скрытого слоя)
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            errh[hid] = 0.0f;
            for(outs = 0; outs < OUTPUT_NEURONS; outs++) {
                errh[hid] += erro[outs] * who[hid, outs];
            }
            errh[hid] *= sigmoidDerivative(hidden[hid]);
        }

        // Обновить веса для выходного слоя(шаг 4 для выходных ячеек)
        for (outs = 0; outs < OUTPUT_NEURONS; outs++) {
            for (hid = 0; hid<HIDDEN_NEURONS; hid++) {
                who[hid, outs] += (LEARN_RATE* erro[outs] * hidden[hid]);
            }
            // Обновить смещение
            //for(int i = 0; i < dest; i++) {
            who[HIDDEN_NEURONS, outs] += (LEARN_RATE * erro[outs]);
            //}
        }

        // Обновить веса для скрытого слоя (шаг 4 для скрытого слоя)
        for(hid = 0; hid < HIDDEN_NEURONS; hid++){
            for(inp = 0; inp < INPUT_NEURONS; inp++)
            {
                wih[inp, hid] += (LEARN_RATE * errh[hid] * inputs[inp]);
            }
            // Обновить смещение
            //for(int i = 0; i < dest; i++) {
            wih[INPUT_NEURONS, hid] += (LEARN_RATE * errh[hid]);
            //}
        }
        return this;
    }

    //Метод назначает случайные веса
    private NINetwork assignRandomWeights()
    {
        int hid, inp, outs;

        // Назначаем случайные веса(по идее только первый раз)
        for(inp = 0; inp < INPUT_NEURONS + dest; inp++)
        {
            for(hid = 0; hid < HIDDEN_NEURONS; hid++)
            {
                RAND_WEIGHT = getRandomWEIGHT();
                wih[inp, hid] = RAND_WEIGHT;
            }
        }

        for(hid = 0; hid < HIDDEN_NEURONS + dest; hid++)
        {
            for(outs = 0; outs < OUTPUT_NEURONS; outs++) {
                who[hid, outs] = RAND_WEIGHT;
            }
        }
        return this;
    }

    // Получаем случайные веса
    private float getRandomWEIGHT()
    {
        return (float)RandomUtils.randomFloat(-0.5, 0.5, 0.01);
    }

    // Значение функции сжатия ?
    private float sigmoid(float val)
    {
        return (float)(1.0f / (1.0f + Mathf.Exp(-val)));
    }

    private float sigmoidDerivative(float val)
    {
        return (val * (1.0f - val));
    }

    public void addSample(NISample sample)
    {
        this.samples.Add(sample);
    }

    //------------------------ Получение результатов(на вход подается пример с пустым выходным слоем):

    // Функция победитель получает все(по идее моя выборка также длжна работать):
    public int action(float[] vector)
    {
        int index, sel;
        float max;

        sel = 0;
        max = vector[sel];

        for(index = 1; index < OUTPUT_NEURONS; index++)
        {
            if(vector[index] > max)
            {
                max = vector[index];
                sel = index;
            }
        }
        return sel;
    }

    /**
 * Метод просчитывает и проставляет результаты в указанный пример:
 * @param sample
 * @return
 */
    public NISample getResult(NISample sample)
    {

        for(int i = 0; i < sample.layer[0].neurons.Count; i++)
        {
            inputs[i] = sample.layer[0].neurons[i].getValue();
        }

        feedForward();

        for(int i = 0; i < actual.Length; i++)
        {
            sample.layer[sample.layer.Count - 1].neurons[i].setValue(actual[i]);
        }
        return sample;
    }

    public NISample first()
    {
        return samples != null && samples.Count > 0 ? samples[0] : null;
    }
}
