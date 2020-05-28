using UnityEngine;

[System.Serializable]
public class CharacterStat : I_StatOperatorable
{
    public string _statName;
    public float _value;

    public  bool _isGrow;
    public  float _mod;
    public char _operator;

    StatOperator    _increaseStatOperator,
                    _moreStatOperator;

    public StatOperator IncreaseStatDelegate
    {
        get { return _increaseStatOperator; }
        set { _increaseStatOperator = value; }

    }
    public StatOperator MoreStatDelegate
    {
        get { return _moreStatOperator; }
        set { _moreStatOperator = value; }
    }

    // 각 값을 설정하는 생성자
    public CharacterStat(string name, float value, bool growable, float growValue)
    {
        _statName = name;
        _value = value;
        _isGrow = growable;
        _mod = growValue;
    }
    //이름만 설정하는 생성자.
    public CharacterStat(string name)
    {
        _statName=name;
        _value=0;
        _isGrow=false;
        _mod = 0;
    }
    public CharacterStat(string name, float value, bool growable, float growValue, char op)
    {
        _statName = name;
        _value = value;
        _isGrow = growable;
        _mod = growValue;
        _operator = op;
    }

    public void AdjustLevelUp()
    {
        AdjustModifier(_mod);
    }
    public void AdjustModifier(float mod)
    {
        _value += (_value * mod);
    }
    /// <summary>
    /// 증가 연산(+)
    /// </summary>
    /// <param name="mod"></param>
    public void AddStatValue(float mod)
    {   _value += mod;  }
    /// <summary>
    /// 곱셈연산 ( n%증가)
    /// </summary>
    /// <param name="mod"></param>
    public void MultipleStatValue(float mod)
    {   _value += _value * mod; }

    public float GetModfiedValue(CharacterStat _base, float modValue)
    {
        return _base._value * modValue;
    }
    public float GetModfiedValue(float modValue)
    {
        return GetModfiedValue(this, modValue);
    }


    public void AddIncreaseStatDelegate(StatOperator del)
    {
        _increaseStatOperator += del;

    }

    public void RemoveIncreaseStatDelegate(StatOperator del)
    {
        _increaseStatOperator -= del;
    }

    public void AddMoreStatDelegate(StatOperator del)
    {
        _moreStatOperator += del;
    }

    public void RemoveMoreStatDelegate(StatOperator del)
    {
        _moreStatOperator -= del;
    }
}