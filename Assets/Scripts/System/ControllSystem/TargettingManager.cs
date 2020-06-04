using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargettingManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject selectedTarget;

    public bool isIdle, DEBUG_MOD;

    public string PlayerTag = "Player",
                    EnemyTag = "Enemy";


    // Use this for initialization
    void Initialize()
    {
        targets = new List<GameObject>();
        selectedTarget = null;


        string thisTag = this.tag;

        //if this is Player, add all enemy, but it's Enemy, add all Player
        if (targets.Count != 0)
            TargetEnemy(targets[0]);
    }

    void Start()
    {
        Initialize();
    }

    public void AddAllEnemies()
    { AddAllTarget("Enemy"); }
    public void AddAllPlayers()
    { AddAllTarget("Player"); }


    //죽은 타겟 제거(명확힌, NULL인 타겟 제거)
    public void RemoveDeadTarget()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
                targets.Remove(targets[i]);
        }

        SortTargetByDistance();

        if (targets.Count > 0)
            TargetEnemy(targets[0]);
        else
            selectedTarget = null;
    }

    public void AddAllTarget(string tag)
    {
        // 현재 존재하는 모든 타겟을 찾아 List에 저장.
        GameObject[] go = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject gameObject in go)
            AddTarget(gameObject);

        if (targets.Count == 0)
            isIdle = true;
        else
            isIdle = false;
    }

    public void AddTarget(GameObject enemy)
    // 리스트에 KeyValuePair 인 객체를 넣는다.
    { targets.Add(enemy); }

    // 타겟들을 거리순으로 재 정렬한다
    public void SortTargetByDistance()
    {
        targets.Sort(
            //delegate-무명메소드
            delegate (GameObject t1, GameObject t2) {

                Vector3 v1 = t1.transform.position;
                Vector3 v2 = t2.transform.position;
                Vector3 thisPosition = transform.position;
                return (
                    Vector3.Distance(v1, thisPosition).CompareTo(Vector3.Distance(v2, thisPosition))
                );
            }
        );
    }

    //타겟 정보 ON/OFF. 선택된 타겟의 정보를 GUI에 뿌려주기 위해 사용
    private void ShowONSelectedInfo(GameObject target)
    {
        //target.transform.GetComponent<Enemy_PointBar_Display>().isVisible = true;
    }

    private void ShowOFFSelectedInfo(GameObject target)
    {
        //target.transform.GetComponent<Enemy_PointBar_Display>().isVisible = false;
    }

    public void TargetEnemy(GameObject enemy)
    {
        // 기존의 타겟을 prev에 저장 및 인덱스(search) 저장
        int search = targets.IndexOf(selectedTarget);
        GameObject prev = selectedTarget;

        selectedTarget = enemy;

        //기존 타겟이 null이 아닌경우에만.
        if (search != -1)
        {

            //prev에 대해 SetDefault,
            if (!prev.Equals(selectedTarget))
            {
                //이전 타겟 정보를 끄고, 새로운현재 타겟정보ON
                foreach (GameObject temp in targets)
                {
                    ShowOFFSelectedInfo(temp);
                }
                ShowOFFSelectedInfo(prev);
                ShowONSelectedInfo(selectedTarget);
            }

        }
        else
        {
            //선택된 타겟이 없는 상태, 따라서 현재 타겟만 선택색으로 변경
            ShowONSelectedInfo(selectedTarget);
        }
    }

    public void TargetEnemy()
    {   // Method For target Changing by TargetButton

        // 선택된 타겟의 index를 받아온다
        int index = targets.IndexOf(selectedTarget);

        // index==-1 이면 선택된 타겟이 list에 존재하지 않음.
        if (index == -1)
            TargetEnemy(targets[0]);
        else
            // 다음 타겟 선택-Circle
            TargetEnemy(targets[(index + 1) % targets.Count]);
    }

    // 트랜스폼을 통해 Index를 검색하는 메소드
    private int GetIndexByTransform(Transform transform)
    {
        int index = -1; // if return -1, it's Fail to search

        GameObject search;

        for (int i = 0; i < targets.Count; i++)
        {

            search = targets[i];

            //Transform이 일치하는 KeyValuePair를 찾으면 인덱스를 저장하고 루프 종료
            if (search.transform.Equals(transform))
            {
                index = i;
                break;
            }
        }

        //-1을 리턴하면 검색실패.
        return index;
    }

    // 타겟 리스트에서 enemy를 제거
    public void RemoveTargetFromList(GameObject enemy)
    {
        targets.Remove(enemy);
    }

    //타겟 사망처리. 리스트에서 remove 한 뒤, 정렬 후 Destroy 호출.
    public void TargetDead(GameObject target)
    {
        targets.Remove(target);
        SortTargetByDistance();

        if ((targets.Count > 0))
            TargetEnemy(targets[0]);
        else
            selectedTarget = null;

        Destroy(target.transform.gameObject);
    }

    public void SearchDeadTarget()
    {
        //사망한 타겟을 찾아서 TargetDead메소드 실행.
        for (int i = 0; i < targets.Count; i++)
        {

            if (tag.Equals("Player") && targets[i].transform != null)
            {
                /*
				Enemy_PointBar_Display ep;
				ep = targets [i].transform.GetComponent<Enemy_PointBar_Display> ();
                */
                StatManager enemyStat = targets[i].GetComponent<StatManager>();
                int CURRENT_HP_INDEX = (int)IndexEnumList.StatNames.currentHealth;

                if (enemyStat.CurrentStats[CURRENT_HP_INDEX]._value <= 0)
                {
                    TargetDead(targets[i]);
                }
            }
            else if (tag.Equals("Enemy"))
            {
                // EnemyPart Targetting

            }
        }
    }

    public bool IsEmpty()
    { return targets.Count == 0; }

    //Idle -> Not Idel 상태로 전환.(AutoAttack, AutoMovement 모두 동일)
    public void ChangeNotIdle()
    {

    }
    // Not Idle -> Idle 상태로 전환
    public void ChangeIdle()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {

            if (targets.Count != 0)
                TargetEnemy();
        }

        if (DEBUG_MOD)
        {

            if (targets.Count != 0)
            {

                string debug = "";

                for (int i = 0; i < targets.Count; i++)
                {
                    debug += targets[i].name + "\t";
                }

                Debug.Log(debug + "\n");
            }
            else
            {
                Debug.Log("List Empty");
            }

        }

    }
}