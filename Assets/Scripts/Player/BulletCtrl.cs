using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{

    //총알의 파괴력
    public float damage = 20.0f;
    //총알 발사 속도
    public float speed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        //힘을 앞쪽 방향으로 나가게 한다.    
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
