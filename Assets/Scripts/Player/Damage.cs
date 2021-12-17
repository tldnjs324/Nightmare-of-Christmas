using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    public float currHp = 100;
    //BloodScreen 텍스처를 저장하기 위한 변수
    public Image bloodScreen;
    //HP Bar Image를 저장하기 위한 변수
    public Image hpBar;
    //GameManager 저장 변수
    public GameManager gameMng;

    //생명 게이지의 처음 색상(녹색)
    Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);//r,g, b, a 순으로 되어있어서 g가 1이기에 녹색으로 표시됨 (a는 투명도)
    Color currColor;

    // Start is called before the first frame update
    void Start()
    {
        //생명 게이지의 초기 색상을 설정
        hpBar.color = initColor;
        currColor = initColor;
    }

    //충돌한 Collider의 IsTrigger 옵션이 체크됐을 때 발생
    void OnTriggerEnter(Collider coll)//OnCollisionEnter는 isTrigger를 체크하지 않았을 때(해제했을 때, OnTriggerEnter는 isTrigger를 체크했을 때
    {
        //충돌한 Collider의 태그가 BULLET이면 Player의 currHp를 차감
        if(coll.tag == "BULLET")
        {
            Destroy(coll.gameObject);
            currHp -= 5.0f;
            Debug.Log("Player HP = " + currHp.ToString());
            //Player의 생명이 0 이하이면 사망 처리
            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
            //혈흔 효과를 표현할 코루틴 함수 호출
            StartCoroutine(ShowBloodScreen());
            //생명 게이지의 색상 및 크기 변경 함수를 호출
            DisplayHpBar();
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, transform.rotation.y, 0.0f));
        }
    }

    //Player의 사망 처리 루틴
    void PlayerDie()
    {
        //Debug.Log("PlayerDie!!");
        //"ENEMY" 태그로 지정된 모든 적 캐릭터를 추출해 배열에 저장
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY");
        //배열의 처음부터 순회하면서 적 캐릭터의 OnPlayerDie 함수를 호출
        for(int i = 0; i<enemies.Length; i++)
        {
            enemies[i].SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);//더이상 응답을 보낼 필요가 없다->send 메시지의 부하를 줄이기 위함
        }
        //게임메니저에서 재시작 버튼 활성화
        gameMng.restartBtn.SetActive(true);
        //게임메니저에서 게임오버 이미지 활성화
        gameMng.GameOver.SetActive(true);
        //게임메니저에서 게임오버
        gameMng.isGameOver = true;
    }

    IEnumerator ShowBloodScreen()
    {
        //BloodScreen 텍스처의 알파값을 불규칙하게 변경
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.5f, 1.0f));//붉은 색 bloodScreen 표시
        yield return new WaitForSeconds(0.1f);//0.1초 기다림
        //BloodScreen 텍스처의 색상을 모두 0으로 변경
        bloodScreen.color = Color.clear;//다시 사라짐
    }

    void DisplayHpBar()
    {
        float ratio = currHp / 100.0f;
        //생명 수치가 50%일 때 까지는 녹색에서 노란색으로 변경
        if (ratio > 0.5f)
        {
            currColor.r = (1 - ratio) * 2.0f;//빨간색 값을 늘림
        }
        else//생명 수치가 0%일 때까지는 노란색에서 빨간색으로 변경
        {
            currColor.g = ratio * 2.0f;//녹색 값을 줄임
        }
        //hpBar의 색상 변경
        hpBar.color = currColor;
        //hpBar의 크기 변경
        hpBar.fillAmount = ratio;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
