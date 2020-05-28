using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StatOperator();
public interface I_StatOperatorable
{
    StatOperator IncreaseStatDelegate { get; set; }
    StatOperator MoreStatDelegate { get; set; }
    void AddIncreaseStatDelegate(StatOperator del);
    void RemoveIncreaseStatDelegate(StatOperator del);
    void AddMoreStatDelegate(StatOperator del);
    void RemoveMoreStatDelegate(StatOperator del);
}