using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage3 : MonoBehaviour
{
    //생명 게이지
    float hp = 100.0f;
    //피격 시 사용할 혈흔 효과
    GameObject bloodEffect;
    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    //생명 게이지의 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 Canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillAmount 속성을 변경할 Image
    private Image hpBarImage;

    // Start is called before the first frame update
    void Start()
    {
        //혈흔 효과 프리팹을 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");
        //생명 게이지의 생성 및 초기화
        SetHpBar();
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //UI Canvas 하위로 생명 게이지를 생성
        GameObject hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        //fillAmount 속성을 변경할 Image를 추출
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];//두번째 이미지 가져오기
        //생명 게이지가 따라가야 할 대상과 오프셋 값 설정
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();
        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;

    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "BULLET")
        {
            //총알 삭제
            Destroy(coll.gameObject);
            //생명 게이지 차감
            //hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            BulletCtrl bc = coll.gameObject.GetComponent<BulletCtrl>();//실제 존재하는지 검사
            if(bc != null)//어떤 값이 들어왔을 경우에만 생명 게이지  차감 플레이어가 공격한 총알에만 체력 감소 요소가 있기 때문에 적의 총알에는 데미지를 받지 않음
            {
                //생명 게이지 차감
                hp -= bc.damage;
                //혈흔 효과를 생성하는 함수 호출
                ShowBloodEffect(coll);
                //생명 게이지의 fillAmount 속성을 변경
                hpBarImage.fillAmount = hp / 100.0f;
            }

            if (hp <= 0.0f)
            {
                //적 캐릭터의 상태를 DIE로 변경
                //GetComponent<EnemyAI5>().state = EnemyAI5.State.DIE;
                //GetComponent<EnemyAI6>().state = EnemyAI6.State.DIE;
                //GetComponent<EnemyAI6>().state = EnemyAI6.State.DIE;
                GetComponent<EnemyAI7>().state = EnemyAI7.State.DIE;
                //적 캐릭터가 사망한 이후 생명 게이지를 투명 처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;

                //적캐릭터 사망시 제거
                Destroy(gameObject, 5);
                //Kill Count 회수 증가
                GameObject.Find("GameManager").GetComponent<GameManager>().AddKillCount();
            }
        }
        
    }
    //혈흔 효과를 생성하는 함수
    void ShowBloodEffect(Collision coll)
    {
        //총알이 충돌한 지점 산출
        Vector3 pos = coll.contacts[0].point;
        //총알의 충돌했을 때의 법선 벡터(접촉한 면과의 수직벡터를 구함)
        Vector3 _normal = coll.contacts[0].normal;
        //총알의 충돌 시 방향 벡터의 회전값 계산
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);
        //혈흔 효과 생성
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
