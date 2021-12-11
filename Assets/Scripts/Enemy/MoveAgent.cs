using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveAgent : MonoBehaviour
{

    //순찰 지점들을 저장하기 위한 List타입의 변수
    public List<Transform> wayPoints;
    //다음 순찰 지점의 배열의 Index
    public int nextIdx;

    float patrolSpeed = 1.5f;//정찰 속도
    float traceSpeed = 4.0f;//추적 속도


    //NavMeshAgent 컴포넌트를 저장할 변수
    NavMeshAgent agent;

    //순찰 여부를 판단하는 변수
    bool patrolling;
    //추적 대상의 위치를 저장하는 변수
    Vector3 traceTarget;

    public void SetPatrolling(bool patrol)
    {
        patrolling = patrol;
        agent.speed = patrolSpeed;
        agent.angularSpeed = 120;//초당 120도 회전
        MoveWayPoint();
    }

    public void SetTraceTarget(Vector3 pos)
    {
        traceTarget = pos;
        agent.speed = traceSpeed;
        agent.angularSpeed = 360;
        TraceTarget(traceTarget);
    }

    //NavMeshAgent의 이동 속도 얻기
    public float GetSpeed()
    {
        return agent.velocity.magnitude;//속도의 크기: 속력을 리턴한다. 
    }

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent 컴포넌트를 추출한 후 변수에 저장
        agent = GetComponent<NavMeshAgent>();
        //하이어아키 뷰의 WayPointGroup 게임오브젝트를 추출
        GameObject group = GameObject.Find("WayPointGroup");
        if (group != null)
        {
            //WayPointGroup 하위에 있는 모든 Transform 컴포넌트를 추출한 후 
            //List 타입의 wayPoints 배열에 추가
            group.GetComponentsInChildren<Transform>(wayPoints); //GetComponentsInChildren s주의, 스펠링 주의
            //배열의 첫 번째 항목 삭제
            wayPoints.RemoveAt(0);
            //첫번째로 이동할 위치를 랜덤하게 지정
            nextIdx = Random.Range(0, wayPoints.Count);
        }
        //MoveWayPoint();//SetPatrolling 생겨서 필요 없어짐
        SetPatrolling(true);
    }

    //다음 목적지까지 이동 명령을 내리는 함수
    void MoveWayPoint()
    {
        //최단거리 경로 계산이 끝나지 않았으면 다음을 수행하지 않음
        if (agent.isPathStale) return;
        //다음 목적지를 wayPoints 배열에서 추출한 위치로 다음 목적지를 지정
        agent.destination = wayPoints[nextIdx].position;
        //내비게이션 기능을 활성화해서 이동을 시작함
        agent.isStopped = false;
    }

    //주인공을 추적할 때 이동시키는 함수
    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
        {
            return;
        }
        agent.destination = pos;
        agent.isStopped = false;
    }

    //순찰 및 추적을 정지시키는 함수
    public void Stop()
    {
        agent.isStopped = true;
        //바로 정지하기 위해 속도를 0으로 설정
        agent.velocity = Vector3.zero;
        patrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        //순찰 모드가 아닐 경우 이후 로직을 수행하지 않음
        if (patrolling == false)
        {
            return;
        }
        //NavMeshAgent가 이동하고 있고 목적지에 도착했는지 여부를 계산
        if (agent.velocity.magnitude >= 0.2f && agent.remainingDistance <= 0.5f)
        {
            //다음 목적지의 배열 첨자를 계산
            //nextIdx = ++nextIdx % wayPoints.Count;//유니티에서 사용하는 배열의 구조체를 쓰게 되면 count라는 변수로 객체를 얻어낼 수 있다. 
            nextIdx = Random.Range(0, wayPoints.Count);

            //다음 목적지로 이동 명령을 수행
            MoveWayPoint();
        }
    }
}
