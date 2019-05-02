using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// // Простейшая нейронная сеть. Основа всего!
/// </summary>
public class NINetwork : MonoBehaviour
{
    // name - УНИКАЛЬНОЕ название нейронной сети.
    public string name = "Новая нейронная сеть";

    /// Тип источника примеров:
    public learnSampleInput sample_Type;
    public enum learnSampleInput
    {
        only_this,                                      //использовать внутренний список примеров.
        neuroCenterSample,                              //использовать список примеров из текущего неронного центра
    }

    /// Тип источника обрабатываемых данных:
    public work_input_type work_Type;
    public enum work_input_type
    {
        only_this,
        neuroCenterInput,
    }

    //Количество учавствующих:
    private List<NINeuron> INPUT_NEURONS = new List<NINeuron>();
    //число скрытых нейронов равно числу входных нейронов:
    private int HIDDEN_NEURONS = 1;
    private List<NINeuron> OUTPUT_NEURONS = new List<NINeuron>();

    //Коэффициент обучения:
    private float LEARN_RATE = 0.2f;

    //Случайные веса:
    private float RAND_WEIGHT = 0f;
    private float RAND_MAX = 0.5f;

    /// Список примеров: NISample.
    public List<NISample> samples = new List<NISample>();

    //Входной слой, данные от пользователя
    public NISample inputSample = null;

    //--------------------------------------------Веса
    //Вход скрытых ячеек(со смещением)
    public float[,] wih;

    //Вход выходных ячеек(со смещением)
    public float[,] who;

    //Активаторы
    public float[] inputs;
    public float[] hidden;
    public float[] target;
    public float[] actual;

    //Ошибки
    public float[] erro;
    public float[] errh;
    public float err = 0f;
    public int sum = 0;

    public int iterations = 1000;

    /// <summary>
    /// Пример нейронной сети, по параметрам которого была создана текущая сеть.
    /// </summary>
    public NIInput input = null;

    /// <summary>
    /// Осмысленное название нейронной сети.
    /// </summary>
    public string description = "Новая нейронная сеть";

    /// <summary>
    /// Список "входных" нейронов сети.
    /// </summary>
    public List<NINeuron> inputNeuronList = new List<NINeuron>();

    /// <summary>
    /// Список "выходных, результативных" нейронов сети.
    /// </summary>
    public List<NINeuron> outputNeuronList = new List<NINeuron>();

    //----------------------------------------Доступные функции-----------------------------------------

    /// <summary>
    /// Конструктор используется в NINeuroCenter
    /// </summary>
    /// <param name="first_sample"></param>
    /// <param name="name"></param>
    /// <param name="ni_center"></param>
    public NINetwork(NIInput input, NIResult result, string name) { }

    //Еще один конструктор:
    public NINetwork(string name)
    {
        this.name = name;
    }

    /// <summary>
    /// Задать коэффициент обучения:
    /// </summary>
    /// <param name="rate"></param>
    public void setLearnRate(float rate)
    {
        this.LEARN_RATE = rate;
    }

    /// <summary>
    /// Запуск первоначальной настройки сети
    /// (рандомные веса, и скрытый слой, массивы ошибок)
    /// </summary>
    public void initialize()
    {
        NISample initSample = null;
        //В зависимости от установленного источника примеров
        initSample = inputSample;
        /*
        switch (sample_Type) {
            case only_this:
                initSample = inputSample;
                break;
        }
        */

        //Получаем количество входов и выходов на основе первого примера.
        INPUT_NEURONS = initSample.input;
        HIDDEN_NEURONS = INPUT_NEURONS.Count;
        OUTPUT_NEURONS = initSample.output;

        // Вход скрытых ячеек(смещение удаляю)
        wih = new float[INPUT_NEURONS.Count, HIDDEN_NEURONS];

        // Вход выходных ячеек(смещение удаляю)
        who = new float[HIDDEN_NEURONS, OUTPUT_NEURONS.Count];

        // Активаторы
        inputs = new float[INPUT_NEURONS.Count];
        hidden = new float[HIDDEN_NEURONS];
        target = new float[OUTPUT_NEURONS.Count];
        actual = new float[OUTPUT_NEURONS.Count];

        // Ошибки
        erro = new float[OUTPUT_NEURONS.Count];
        errh = new float[HIDDEN_NEURONS];

        // Инициализировать генератор случайных чисел
        assignRandomWeights();

        if(samples.Contains(initSample))
        {
            samples.Remove(initSample);
        }
        // Если в списке примеров имеются примеры то выполняем обучение:
        if(samples.Count > 0)
        {
            learn();
        }
    }

    /// <summary>
    /// Выполнить обучение нейронной сети: return bool;
    /// </summary>
    /// <returns></returns>
    public void learn()
    {
        int iter_Count = 0;
        //Обучить сеть
        while(iter_Count < iterations)
        {
            // Пробегаем каждый пример:
            for(int i = 0; i < samples.Count; i++)
            {
                NISample current = samples[i];
                // Пробегаем каждый входной нейрон
                for(int j = 0; j < current.input.Count; j++)
                {
                    // Наполняем входной слой примерами:
                    switch(current.input[j].type)
                    {
                        case NINeuron.type_Info.int_:
                            inputs[j] = (float)current.input[j].intCount;
                            break;
                        case NINeuron.type_Info.float_:
                            inputs[j] = (float)current.input[j].floatCount;
                            break;
                        case NINeuron.type_Info.bool_:
                            if(current.input[j].boolCount)
                            {
                                inputs[j] = (float)1;
                            }
                            else
                            {
                                inputs[j] = (float)0;
                            }
                            break;
                    }
                    // Наполняем выходной слой известными результатами:
                    switch(current.output[j].type)
                    {
                        case NINeuron.type_Info.int_:
                            target[j] = (float)current.output[j].intCount;
                            break;
                        case NINeuron.type_Info.float_:
                            target[j] = (float)current.output[j].floatCount;
                            break;
                        case NINeuron.type_Info.bool_:
                            if(current.output[j].boolCount)
                            {
                                target[j] = (float)1;
                            }
                            else
                            {
                                target[j] = (float)0;
                            }
                            break;
                    }
                }

                feedForward();

                err = 0.0f;
                //Квадратичная ошибка для каждого из выходов:
                for(int k = 0; k < current.output.Count; k++)
                {
                    err += (float)Mathf.Sqrt((target[k] - actual[k]));
                }

                err = 0.5f * err;
                iter_Count++;
                //собственно выполняем обучение
                backPropagate();
            }
            //System.out.println("Всего итераций: " + iterations + ", текущая итерация обучения: " + iter_Count );
        }
    }

    /// <summary>
    /// FeedForward - прямое распространение(обработка информации)
    /// </summary>
    public void feedForward()
    {
        int inp, hid, outs;
        float sum;

        //Вычислить вход в скрытый слой
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            sum = 0f;
            for(inp = 0; inp < INPUT_NEURONS.Count; inp++)
            {
                sum += inputs[inp] * wih[inp, hid];
            }
            //Добавить смещение
            //sum += wih[INPUT_NEURONS, hid];
            hidden[hid] = sigmoid(sum);
        }

        //Вычислить вход в выходной слой
        for(outs = 0; outs < OUTPUT_NEURONS.Count; outs++)
        {
            sum = 0.0f;
            for(hid = 0; hid < HIDDEN_NEURONS; hid++)
            {
                sum += hidden[hid] * who[hid, outs];
            }
            //Добавить смещение
            //sum += who[HIDDEN_NEURONS, outs];
            actual[outs] = sigmoid(sum);
        }
    }

    //Метод возвращает максимальное значение(или index из списка):
    int getMaxActual()
    {

        int result = 0;
        float min = 0;

        //выполняем собственно обработку информации:
        feedForward();

        for(int i = 0; i < actual.Length; i++)
        {
            if(min < actual[i])
            {
                min = actual[i];
                result = i;
            }
        }
        //Возвращаем индекс правильного действия:
        return result;
    }

    /// <summary>
    /// BackPropagate - обратное распространение(обучение)
    /// </summary>
    void backPropagate()
    {
        int inp, hid, outs;

        //Вычислить ошибку выходного слоя (шаг 3 для выходных ячеек)
        for(outs = 0; outs < OUTPUT_NEURONS.Count; outs++)
        {
            erro[outs] = (target[outs] - actual[outs]) * sigmoidDerivative(actual[outs]);
        }
        //Вычислить ошибку скрытого слоя (шаг 3 для скрытого слоя)
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            errh[hid] = 0.0f;
            for(outs = 0; outs < OUTPUT_NEURONS.Count; outs++)
            {
                errh[hid] += erro[outs] * who[hid, outs];
            }
            errh[hid] *= sigmoidDerivative(hidden[hid]);
        }
        //Обновить веса для выходного слоя(шаг 4 для выходных ячеек)
        for(outs = 0; outs < OUTPUT_NEURONS.Count; outs++)
        {
            for(hid = 0; hid < HIDDEN_NEURONS; hid++)
            {
                who[hid, outs] += (LEARN_RATE * erro[outs] * hidden[hid]);
            }
            //Обновить смещение
            //who[HIDDEN_NEURONS][outs] += (LEARN_RATE * erro[outs]);
        }
        //Обновить веса для скрытого слоя (шаг 4 для скрытого слоя)
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            for(inp = 0; inp < INPUT_NEURONS.Count; inp++)
            {
                wih[inp, hid] += (LEARN_RATE * errh[hid] * inputs[inp]);
            }
            //Обновить смещение
            //wih[INPUT_NEURONS.size()][hid] += (LEARN_RATE * errh[hid]);
        }
    }

    /// <summary>
    /// Вернуть список(пример решения) результативных значений: return float[];
    /// </summary>
    /// <returns></returns>
    public float[] getResultList()
    {
        return actual;
    }

    /// <summary>
    /// функция вычисления рандома.
    /// </summary>
    /// <returns></returns>
    private float getRandomWEIGHT()
    {
        float rand = 0f;
        //float rand = new Random()..Range(-0.5f, 0.5f);
        return rand;
    }

    /// <summary>
    /// Назначение случайных весов(например при иницилизации)
    /// </summary>
    public void assignRandomWeights()
    {
        int hid, inp, outs;
        //Назначаем случайные веса(по идее только первый раз)
        for(inp = 0; inp < INPUT_NEURONS.Count; inp++)
        {
            for(hid = 0; hid < HIDDEN_NEURONS; hid++)
            {
                RAND_WEIGHT = getRandomWEIGHT();
                wih[inp, hid] = RAND_WEIGHT;
            }
        }
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            for(outs = 0; outs < HIDDEN_NEURONS; outs++)
            {
                who[hid, outs] = RAND_WEIGHT;
            }
        }
    }

    /// <summary>
    /// Sigmoid - функция сжатия.
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private float sigmoid(float val)
    {
        return (1.0f / (1.0f + (float)Mathf.Exp((-val))));
    }

    /// <summary>
    /// sigmoidDerigative - функция сжатия.
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    private float sigmoidDerivative(float val)
    {
        return (val * (1.0f - val));
    }

    /// <summary>
    /// Данный метод проверяет входящий пример на корректность, и применяет различные методики для предотвращения ошибок,
    /// например при большем количестве данных во входящем примере он игнорирует, отсутствующие данные.
    /// </summary>
    private NISample sample_Loader(NISample loadSample)
    {
        NISample returned = null;
        return returned;
    }
}
