using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StatOperator();

// 스텟 연산 인터페이스
public interface I_StatOperatorable
{
	
    StatOperator IncreaseStatDelegate { get; set; }
    StatOperator MoreStatDelegate { get; set; }
	
	//합연산 스텟 델리게이터
    void AddIncreaseStatDelegate(StatOperator del);
    void RemoveIncreaseStatDelegate(StatOperator del);
	
	// 곱연산 스텟 델리게이터
    void AddMoreStatDelegate(StatOperator del);
    void RemoveMoreStatDelegate(StatOperator del);
}