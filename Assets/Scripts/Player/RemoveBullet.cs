using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;
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
        if (coll.collider.tag == "BULLET")
        {
            //스파크 효과 함수 호출
            ShowEffect(coll);//이펙트를 보여주고
            //충돌한 게임오브젝트 삭제
            Destroy(coll.gameObject);//충돌한걸 제거한다
        }
    }
    void ShowEffect(Collision coll)
    {
        //충돌 지점의 정보를 추출
        ContactPoint contact = coll.contacts[0];//가장 첫번째로 부딪힌 위치
        //법선 벡터가 이루는 회전각도를 추출
        Quaternion rot = Quaternion.FromToRotation(Vector3.back, contact.normal);
        //스파크 효과를 생성
        GameObject spark = Instantiate(sparkEffect, contact.point - (contact.normal * 0.05f), rot);
        //스파크 효과의 부모를 충돌한 물체로 설정
        spark.transform.SetParent(this.transform);
    }

}
