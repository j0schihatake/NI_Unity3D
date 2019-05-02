using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NIResult : MonoBehaviour
{
    // Уникальное, смысловое название выходных данных(обычно это результат работы сети).
    public string description = "Набор выходных данных(обычно это результат работы сети).";

    // Список выходных нейронов со значениями:
    public List<NINeuron> output = new List<NINeuron>();
}
