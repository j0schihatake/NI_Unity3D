using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NINetwork : MonoBehaviour
{
    public string name = "default_book_example";

    public string description = "���� ��� �����.";

    public string tableName = "no_name_ni_network";

    public List<NISample> samples = new List<NISample>();

    // ����������� ������ �� �����:
    public int INPUT_NEURONS = 4;
    public int HIDDEN_NEURONS = 3;
    public int OUTPUT_NEURONS = 4;

    // ��������
    private int dest = 1;

    public int HIDDEN_LAYER_COUNT = 1;

    // ����������� ��������:
    public float LEARN_RATE = 0.2f;

    // ��������� ����:
    public float RAND_WEIGHT = 0f;

    public float RAND_MAX = 0.5f;

    // ������� ������ �� ������������:
    public NISample inputSample = null;

    // ����:
    // ���� ������� �����(�� ���������):
    public float[,] wih;

    // ���� �������� �����(�� ���������):
    public float[,] who;

    // ����������:
    public float[] inputs;
    public float[] hidden;
    public float[] outputs;
    public float[] actual;

    // ������:
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

        // ����
        // ���� ������� �����(�� ���������)
        wih = new float[INPUT_NEURONS + dest, HIDDEN_NEURONS];

        // ���� �������� �����(�� ���������)
        who = new float[HIDDEN_NEURONS + dest, OUTPUT_NEURONS];

        // ����������
        inputs = new float[INPUT_NEURONS];
        hidden = new float[HIDDEN_NEURONS];
        outputs = new float[OUTPUT_NEURONS];
        actual = new float[OUTPUT_NEURONS];

        // ������
        erro = new float[OUTPUT_NEURONS];
        errh = new float[HIDDEN_NEURONS];

        // ���������������� ��������� ��������� �����
        assignRandomWeights();
        return this;
    }

    /**
     * �������� �������� ����:
     * 100000
     */
    public NINetwork learn(int iterationCount)
    {

        int iterations = 0;

        // ������� ����
        while(iterations < iterationCount)
        {
            for(int i = 0; i < samples.Count; i++)
            {
                NISample sample = samples[i];

                // ��� ������ �� ����� � ������ "���������� ��������"
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

                // ������������ ������ ��� ������� �� �������:
                for(int m = 0; m < sample.layer[1].neurons.Count; m++)
                {
                    err += Mathf.Sqrt((sample.layer[1].neurons[m].getValue() - actual[0]));
                }

                err = 0.5f * err;

                iterations++;

                // ��������� ��������:
                backPropagate();
            }
        }
        return this;
    }

    // ������ ���������������
    private NINetwork feedForward()
    {
        int inp, hid, outs;
        float sum;

        // ��������� ���� � ������� ����:
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            sum = 0f;
            for(inp = 0; inp < INPUT_NEURONS; inp++)
            {
                sum += inputs[inp] * wih[inp, hid];
            }
            // �������� ��������
            //for(int i = 0; i < dest; i++){
            sum += wih[INPUT_NEURONS, hid];
            //}
            hidden[hid] = sigmoid(sum);
        }

        // ��������� ���� � �������� ����:
        for(outs = 0; outs < OUTPUT_NEURONS; outs++)
        {
            sum = 0.0f;
            for(hid = 0; hid < HIDDEN_NEURONS; hid++)
            {
                sum += hidden[hid] * who[hid, outs];
            }
            // �������� ��������
            //for(int i = 0; i < dest; i++){
            sum += who[HIDDEN_NEURONS, outs];
            //}
            actual[outs] = sigmoid(sum);
        }
        return this;
    }

    // �������� ���������������(��������)
    private NINetwork backPropagate()
    {
        int inp, hid, outs;

        // ��������� ������ ��������� ���� (��� 3 ��� �������� �����)
        for(outs = 0; outs < OUTPUT_NEURONS; outs++) {
            erro[outs] = (outputs[outs] - actual[outs]) * sigmoidDerivative(actual[outs]);
        }

        // ��������� ������ �������� ���� (��� 3 ��� �������� ����)
        for(hid = 0; hid < HIDDEN_NEURONS; hid++)
        {
            errh[hid] = 0.0f;
            for(outs = 0; outs < OUTPUT_NEURONS; outs++) {
                errh[hid] += erro[outs] * who[hid, outs];
            }
            errh[hid] *= sigmoidDerivative(hidden[hid]);
        }

        // �������� ���� ��� ��������� ����(��� 4 ��� �������� �����)
        for (outs = 0; outs < OUTPUT_NEURONS; outs++) {
            for (hid = 0; hid<HIDDEN_NEURONS; hid++) {
                who[hid, outs] += (LEARN_RATE* erro[outs] * hidden[hid]);
            }
            // �������� ��������
            //for(int i = 0; i < dest; i++) {
            who[HIDDEN_NEURONS, outs] += (LEARN_RATE * erro[outs]);
            //}
        }

        // �������� ���� ��� �������� ���� (��� 4 ��� �������� ����)
        for(hid = 0; hid < HIDDEN_NEURONS; hid++){
            for(inp = 0; inp < INPUT_NEURONS; inp++)
            {
                wih[inp, hid] += (LEARN_RATE * errh[hid] * inputs[inp]);
            }
            // �������� ��������
            //for(int i = 0; i < dest; i++) {
            wih[INPUT_NEURONS, hid] += (LEARN_RATE * errh[hid]);
            //}
        }
        return this;
    }

    //����� ��������� ��������� ����
    private NINetwork assignRandomWeights()
    {
        int hid, inp, outs;

        // ��������� ��������� ����(�� ���� ������ ������ ���)
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

    // �������� ��������� ����
    private float getRandomWEIGHT()
    {
        return (float)RandomUtils.randomFloat(-0.5, 0.5, 0.01);
    }

    // �������� ������� ������ ?
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

    //------------------------ ��������� �����������(�� ���� �������� ������ � ������ �������� �����):

    // ������� ���������� �������� ���(�� ���� ��� ������� ����� ����� ��������):
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
 * ����� ������������ � ����������� ���������� � ��������� ������:
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
