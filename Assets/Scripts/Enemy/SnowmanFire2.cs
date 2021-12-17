using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanFire2 : MonoBehaviour
{
    //적 캐릭터의 총알 프리팹
    public GameObject Bullet;
    //총알의 발사 위치 정보
    public Transform firePos;
    //적 캐릭터의 Transform 컴포넌트
    Transform smanTr;
    //주인공 캐릭터의 Transform 컴포넌트
    Transform playerTr;

    //다음 발사할 시간 계산용 변수
    float nextFire = 0.0f;
    //총알 발사 간격
    float fireRate = 1.0f;
    //주인공을 향해 회전할 속도 계수
    float damping = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //컴포넌트 추출 및 변수 저장
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        smanTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTr.position.y > 11.8)
        {
            //현재 시간이 다음 발사 시간보다 큰지를 확인
            if (Time.time >= nextFire)
            {
                Fire();
                //다음 발사 시간 계산
                nextFire = Time.time + fireRate + Random.Range(0.0f, 0.5f);//1초~1.5초 사이에 불규칙적으로 발사
            }
        }
        
        //주인공이 있는 위치까지의 회전 각도 계산
        Quaternion rot = Quaternion.LookRotation(playerTr.position - smanTr.position);
        //보관 함수를 사용해 점진적으로 회전시킴
        smanTr.rotation = Quaternion.Slerp(smanTr.rotation, rot, Time.deltaTime * damping);
    }

    void Fire()
    {
        //총알을 생성
        GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
        //일정 시간이 지난 후 삭제
        Destroy(_bullet, 3.0f);
    }
}
