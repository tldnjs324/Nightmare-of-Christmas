using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    //총알 프리팹
    public GameObject bullet;
    //탄피 추출 파티클
    public ParticleSystem cartridge;
    //총알 발사 좌표
    public Transform firePos;
    //오디오 클립을 저장할 변수(발사 사운드)
    public AudioClip fireSound;

    //총구 화염 파티클
    ParticleSystem muzzleFlash;
    //AudioSource 컴포넌트를 저장할 변수
    AudioSource _audio;//꼭 _언더바 붙여야함! 왜냠 구 버전에서 쓰던 오디오 컴포넌트 이름이기 때문

    // Start is called before the first frame update
    void Start()
    {
        //FirePos 하위에 있는 컴포넌트 추출
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        //AudioSource 컴포넌트 추출
        _audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        
    }
    void Fire()
    {
        //Bulltet 프리팹을 동적으로 생성
        Instantiate(bullet, firePos.position, firePos.rotation);
        //파티클 실행
        cartridge.Play();
        //총구 화염 파티클 실행
        muzzleFlash.Play();
        //사운드 발생
        _audio.PlayOneShot(fireSound, 0.5f);
    }
}
