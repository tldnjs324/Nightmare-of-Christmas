using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerFire : MonoBehaviour
{
    //적 캐릭터의 총알 프리팹
    public GameObject Bullet;
    //총알의 발사 위치 정보
    public Transform firePos;


    //다음 발사할 시간 계산용 변수
    float nextFire = 0.0f;
    //총알 발사 간격
    float fireRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //현재 시간이 다음 발사 시간보다 큰지를 확인
        if (Time.time >= nextFire)
        {
            Fire();
            //다음 발사 시간 계산
            nextFire = Time.time + fireRate + Random.Range(0.0f, 0.5f);//1초~1.5초 사이에 불규칙적으로 발사
        }
    }

    void Fire()
    {
        //총알을 생성
        GameObject _bullet = Instantiate(Bullet, firePos.position, firePos.rotation);
        //일정 시간이 지난 후 삭제
        Destroy(_bullet, 3.0f);
    }
}
