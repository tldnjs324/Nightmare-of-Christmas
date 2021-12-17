using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //게임 종료 여부를 판단할 변수
    public bool isGameOver = false;

    //Gift Count 표시
    public Text giftText;
    //적을 죽인 횟수
    public int giftCount = 0;

    //재시작 버튼
    public GameObject restartBtn;
    //게임오버 이미지
    public GameObject GameOver;
    //게임 클리어 이미지
    public GameObject GameClear;
    //다 모으지 못했다는 에러메시지
    public GameObject NotClear;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //Restart버튼 눌렀을 때 다시 시작하는 함수
    public void Restart()
    {
        //씬을 다시 로드
        SceneManager.LoadScene("NightMare_main");
        //재시작 버튼 비활성화
        restartBtn.SetActive(false);
        isGameOver = false;
    }
    //경고창의 닫기 버튼 눌렀을 때 닫는 함수
    public void Close_NotClear()
    {
        NotClear.SetActive(false);
    }
    //선물 수 증가하고 UI에 표시하는 함수
    public void AddGift()
    {
        ++giftCount;//횟수 증가
        giftText.text = "Gift " + giftCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
