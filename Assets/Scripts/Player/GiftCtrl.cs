using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        //BULLET과 부딪히면
        if (coll.collider.tag == "Player")
        {
            //Gift Count 횟수 증가
            GameObject.Find("GameManager").GetComponent<GameManager>().AddGift();
            //충돌한 게임오브젝트 삭제
            Destroy(gameObject);//이 물체 제거
        }
    }
}
