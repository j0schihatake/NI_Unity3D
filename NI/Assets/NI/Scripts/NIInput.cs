using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NIInput : MonoBehaviour
{
    // Уникальное, смысловое название набора входных данных(input).
    public string description = "Набор входных данных.";

    // Список входных нейронов со значениями:
    public List<NINeuron> input = new List<NINeuron>();
}
