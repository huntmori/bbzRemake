using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class StatManager : MonoBehaviour
{
    public int _Level;
    public int _MaxLevel;

    public bool _isMelee;
    public bool _isSplash;
    public bool _isPlayer;
    public bool _isMine;


    //경험치 수치도 스텟으로 관리하는게 편해보임-참조로 인해 동기화 필요X
    public int _CurrentExp;
    public int _MaxExp;

    public int[] ExpTable;

    //상수 영역
    public readonly int ATTACK_STAT = (int)IndexEnumList.Stat.attack,
                        ATTACK_DAMAGE = (int)IndexEnumList.StatNames.attackDamage,
                        ATTACK_SPEED = (int)IndexEnumList.StatNames.attackSpeed,
                        ATTACK_RANGE = (int)IndexEnumList.StatNames.attackRange,
                        ATTACK_RADIUS = (int)IndexEnumList.StatNames.attackRadius,

                        MOVE_STAT = (int)IndexEnumList.Stat.move,
                        MOVE_SPEED = (int)IndexEnumList.StatNames.moveSpeed,
                        ROTATION_SPEED = (int)IndexEnumList.StatNames.rotationSpeed,
                        DETECT_RANGE = (int)IndexEnumList.StatNames.detectRange,

                        DEFFENCE_STAT = (int)IndexEnumList.Stat.deffence,
                        DEFFENCE_POINT = (int)IndexEnumList.StatNames.deffencePoint,
                        CURRENT_HEALTH = (int)IndexEnumList.StatNames.currentHealth,
                        MAX_HEALTH = (int)IndexEnumList.StatNames.maxHealth,
                        HEALTH_REGENERATION = (int)IndexEnumList.StatNames.healthRegeneration,
                        DAMAGE_REDUCE = (int)IndexEnumList.StatNames.damageReduce,

                        SKILL_STAT = (int)IndexEnumList.Stat.skill,
                        TOTAL_SKILL_COUNT = (int)IndexEnumList.StatNames.totalSkillCount,
                        SKILL_POINT = (int)IndexEnumList.StatNames.skillPoint,
                        CURRENT_SKILL_RESOURCE = (int)IndexEnumList.StatNames.currentSkillResource,
                        MAX_SKILL_RESOURCE = (int)IndexEnumList.StatNames.maxSkillResource,
                        SKILL_RESOURCE_REGEN = (int)IndexEnumList.StatNames.skillResourceRegeneration;
    /// <summary>
    /// 미구현
    /// </summary>
    //List<int> Skill_Id;
    //List<Interface_Skill_Base> Skills

    //    public CharacterStat currentHP;
    //    public CharacterStat currentMP;

    public CharacterStat[] BaseStats;
    public CharacterStat[] ModStats;
    public CharacterStat[] CurrentStats;

    public int Level
    {
        get { return _Level; }
        set { _Level = value; }
    }
    public int MaxLevel
    {
        get { return _MaxLevel; }
        set { _MaxLevel = value; }
    }

    public bool DEBUG_MOD = false;

    void Awake()
    {
        // 컴포넌트 초기화
        InitializeVars();

        //체력/마나재생, 경험치체크 코루틴 시작
        StartCoroutine(RegenerateHealth());
        StartCoroutine(RegenerateMana());
        StartCoroutine(ExpChecker());
    }

    // Update is called once per frame
    void Update()
    {
        //CurrentExpCheck();
        CurrentStatCalculation();

    }

    void InitializeVars()
    {
        _Level = 1;
        _MaxLevel = 20;

        // 경험치 테이블 생성    
        ExpTable = new int[_MaxLevel];
        for (int i = 0; i < ExpTable.Length; i++)
            ExpTable[i] = i * 100;

        _CurrentExp = 0;
        _MaxExp = ExpTable[Level - 1];

        // 스텟 배열 초기화를 위한 배열 사이즈 = Enum의 Statnames 사이즈
        // Enum을 배열로 받아온 뒤 그 Length값을 사용.
        int sizeofstats = Enum.GetValues(typeof(IndexEnumList.StatNames)).Length;

        BaseStats = new CharacterStat[sizeofstats];
        ModStats = new CharacterStat[sizeofstats];
        CurrentStats = new CharacterStat[sizeofstats];

        for (int i = 0; i < BaseStats.Length; i++)
        {
            // ENum에서 이름을 받아옴.
            String tempStatName = Enum.GetName(typeof(IndexEnumList.StatNames), i);

            BaseStats[i] = new CharacterStat(tempStatName);
            ModStats[i] = new CharacterStat(tempStatName);
            CurrentStats[i] = new CharacterStat(tempStatName);
        }

        //        currentHP = new CharacterStat("CurrentHP",BaseStats[HEALTH_POINT]._value, false , 0f);

        //        currentMP = new CharacterStat("CurrentMP", BaseStats[SKILL_RESOURCE]._value, false, 0f);

        /*          미구현
         *        Skill_Id = new List<int>();
         *        Skills = new List<Interface_Skill_Base>();
        */
    }


    //경험치 체크 코루틴
    IEnumerator ExpChecker()
    {
        while (true)
        {
            if (_CurrentExp >= _MaxExp)
            {
                if (_Level < _MaxLevel)
                    LevelUp();
                else if (_Level == _MaxLevel)
                    _CurrentExp = 0;
            }
            yield return null;
        }
    }
    // 레벨업 경험치 체커
    void CurrentExpCheck()
    {
        if (_CurrentExp >= _MaxExp)
        {
            //최대 레벨 미 도달 시
            if (_Level < _MaxLevel)
            {
                LevelUp();
            }
            // 최대레벨 도달 시
            else if (_Level == _MaxLevel)
            {
                //경험치를 0
                _CurrentExp = 0;
            }
        }
        else
            return;
    }

    // 레벨업 함수
    void LevelUp()
    {
        if (_MaxLevel >= _Level)
        {
            _CurrentExp = _MaxExp - _CurrentExp;   //초과 경험치 보존
            _Level++;    //레벨 증가
            _MaxExp = ExpTable[_Level - 1];   //최대 경험치를 다음 레벨에 맞게 변경
            AdjustLevelUp();    //능력치 증가
        }
    }

    // 경험치 증감
    void GainExp(int get)
    {
        _CurrentExp += get;

        if (_CurrentExp >= _MaxExp)
            LevelUp();
    }

    void AdjustLevelUp()
    {//레벨업 적용
        // 성장하는 스텟 인 경우 랩업치 적용
        for (int i = 0; i < BaseStats.Length; i++)
        {

            if (BaseStats[i]._isGrow)
            {
                BaseStats[i].AdjustLevelUp();
            }
        }
    }

    //1초당 체력재생 코루틴
    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            if ((CurrentStats[CURRENT_HEALTH]._value < CurrentStats[MAX_HEALTH]._value))
            {
                BaseStats[CURRENT_HEALTH]._value += CurrentStats[HEALTH_REGENERATION]._value;
                CurrentStats[CURRENT_HEALTH]._value += CurrentStats[HEALTH_REGENERATION]._value;
            }
            // 회복 된 뒤 1초를 대기한다
            yield return new WaitForSeconds(1);
        }
    }
    // 1초당 마나 재생 코루틴
    IEnumerator RegenerateMana()
    {
        while (true)
        {
            if ((CurrentStats[CURRENT_SKILL_RESOURCE]._value < CurrentStats[MAX_SKILL_RESOURCE]._value))
            {
                BaseStats[CURRENT_SKILL_RESOURCE]._value += CurrentStats[SKILL_RESOURCE_REGEN]._value;
                CurrentStats[CURRENT_SKILL_RESOURCE]._value += CurrentStats[SKILL_RESOURCE_REGEN]._value;
            }

            yield return new WaitForSeconds(1);
        }

    }

    void CurrentStatCalculation()
    {
        for (int i = 0; i < BaseStats.Length; i++)
        {
            CurrentStats[i]._value = BaseStats[i]._value + ModStats[i]._value;
        }
    }
    /*
    CharacterStat CurrentStatCalculation(int index)
    {
        return BaseStats[index]._value + ModStats[index]._value;
    }*/

    /// <summary>
    /// 미구현 -버프소멸
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void BuffExpire(object sender, EventArgs e)
    {
        if (DEBUG_MOD)
            Debug.Log("BUFF Expired\n");
    }

    /// <summary>
    /// 미구현-Item등의 반영구적인 스텟의 증감치를 추가하는 method.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    void AddModEffect(int index, float value)
    {
        ModStats[index]._value += value;
        CurrentStats[index]._value = BaseStats[index]._value + ModStats[index]._value;
    }

    /// <summary>
    /// 미구현-Item 등의 반영구적인 스텟의 증감치를 제거하는 Method.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    void RemoveModEffect(int index, float value)
    {
        ModStats[index]._value -= value;
        CurrentStats[index]._value = BaseStats[index]._value + ModStats[index]._value;
    }

    /// <summary>
    /// 미구현-소멸 될 여지가 있는(시간의 흐름이나 기타 조건에 따라) 증감값을 추가하는 메소드
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    float AddMultiplyMod(int index, float value)
    {
        float returnValue = CurrentStats[index]._value * value;
        CurrentStats[index]._value = CurrentStats[index]._value + returnValue;

        return returnValue;
    }

    /// <summary>
    /// (미구현)소멸 될 여지가 있는(시간의 흐름이나 기타 조건에 따라) 증감값을 제거하는 메소드
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    void RemoveMultplyMod(int index, float value)
    {
        CurrentStats[index]._value -= value;
    }

    // 데미지를 받는 메소드.
    public void TakeDamage(float damage)
    {
        float totalDmg = damage;

        //데미지 감소율 계산
        if (CurrentStats[DAMAGE_REDUCE]._value != 0)
            totalDmg = damage - damage * CurrentStats[DAMAGE_REDUCE]._value;

        //방어력 계산
        if (CurrentStats[DEFFENCE_POINT]._value != 0)
            totalDmg -= CurrentStats[DEFFENCE_POINT]._value;

        BaseStats[CURRENT_HEALTH]._value -= totalDmg;

        CurrentStats[CURRENT_HEALTH]._value -= totalDmg;

        if (DEBUG_MOD)
            Debug.Log("BASE:" + BaseStats[CURRENT_HEALTH]._value
                    + "\tDMG: " + totalDmg + "\tCURRENT:" + CurrentStats[CURRENT_HEALTH]._value);

    }

    // 데미지를 주는 메소드. 피격자의 TakeDamage를 호출한다.
    public void DealDamage(StatManager target, float damage)
    {
        target.TakeDamage(damage);
    }

    /// <summary>
    /// 참조 문제로 사용 권장되지 않음. CurrentStat에 접근하는 Indexer
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public CharacterStat this[int i]
    {
        get
        {
            CurrentStats[i]._value = BaseStats[i]._value + ModStats[i]._value;

            return CurrentStats[i];
        }
    }


}