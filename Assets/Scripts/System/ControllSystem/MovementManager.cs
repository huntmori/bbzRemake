using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class MovementManager : MonoBehaviour
{
    public CharacterController characterController;
    public Transform thisTransform;
    public Transform targetTransform;

    public CharacterStat _moveSpeed, _rotationSpeed;
    public CharacterStat _attackRange;

    public readonly int INDEX_MOVE_SPEED = (int)IndexEnumList.StatNames.moveSpeed;
    public readonly int INDEX_ROTATION_SPEED = (int)IndexEnumList.StatNames.rotationSpeed;
    public readonly int INDEX_ATTACK_RANGE = (int)IndexEnumList.StatNames.attackRange;

    public bool isMine = false;
    public bool isMoveState;

    public bool DEBUG_MOD;

    public Vector3 TargetVector;

    public NavMeshAgent navMeshAgent;
    public TargettingManager targetManager;

    // Use this for initialization
    private void Initialize()
    {
        targetTransform = GetComponent<Transform>();
        thisTransform = GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = 0f;

        TargetVector = this.transform.position;
    }

    //참조 초기화
    private void InitializeReference()
    {
        StatManager tempStat = GetComponent<StatManager>();
        _moveSpeed = tempStat.CurrentStats[INDEX_MOVE_SPEED];
        _rotationSpeed = tempStat.CurrentStats[INDEX_ROTATION_SPEED];
        _attackRange = tempStat.CurrentStats[INDEX_ATTACK_RANGE];

        targetManager = GetComponent<TargettingManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        characterController = GetComponent<CharacterController>();

        isMoveState = false;
    }

    void Start()
    {
        Initialize();
    }
    void Update()
    {
        navMeshAgent.destination = TargetVector;
    }

    //목적지 설정
    public void SetTargetTransform(Vector3 vector3)
    {
        TargetVector = vector3;
    }

    //정지 - 클릭해도 움직이지 않음 / 이동중이던 위치 해제-현재 미작동중
    public void Stop()
    {
        TargetVector = transform.position;
        //navMeshAgent.Stop();
        navMeshAgent.isStopped = true;
    }

    // 정지해제
    public void DisableStop()
    {
        navMeshAgent.path = new NavMeshPath();
        //navMeshAgent.Resume();
        navMeshAgent.isStopped = false;
    }
    /*/
    private void Awake()
    {
        Initialize();
    }
    void Start ()
    {
        InitializeReference();               
    }
	
    float GetStoppingDistance()
    {
        float returnValue;

        returnValue = characterController.radius + _attackRange._value;

        return returnValue;
    }

    void MappingStats()
    {
        navMeshAgent.speed = _moveSpeed._value;
        navMeshAgent.radius = characterController.radius;
        navMeshAgent.height = characterController.height;
    }

	// Update is called once per frame
	void Update ()
    {
        //navMeshAgent.s
    }
*/

    // 타겟 방향으로 회전(바로 바라보게)
    public void LookTarget(Vector3 _target)
    {
        // 방향과 y축을 정면으로 보정한 방향을 구함
        Vector3 direction = _target - transform.position;
        Vector3 directionXZ = new Vector3(direction.x,
                                            0f,
                                            direction.z);

        // 해당 방향으로 바로 전환(회전X)
        Quaternion look = Quaternion.LookRotation(directionXZ);
        transform.rotation = look;

        //TargetVector = directionXZ;
    }
    /*/
        public void MovingToTarget(Vector3 _target)
        {
            MappingStats();

            Vector3 direction = _target - transform.position;
            Vector3 directionXZ = new Vector3(  direction.x,
                                                0f,
                                                direction.z);

            Vector3 targetPosition = transform.position - directionXZ;

            isMoveState = true;
            navMeshAgent.SetDestination(targetPosition);
            navMeshAgent.destination = targetPosition;
            navMeshAgent.stoppingDistance = _attackRange._value;


        }

        public void Stop()
        {
            navMeshAgent.Stop();
            isMoveState = false;
        }
        */
}