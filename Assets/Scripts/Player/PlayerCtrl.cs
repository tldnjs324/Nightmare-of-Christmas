using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //이동 속도 변수
    public float moveSpeed = 10.0f;
    //회전 속도 변수
    public float rotSpeed = 80.0f;
    //점프 힘 변수
    public float force = 300;
    //Animation  컴포넌트를 저장하기 위한 변수
    Animation anim;
    //Rigidbody 컴포넌트를 저장하기 위한 변수
    Rigidbody rb;

    Vector3 AXiS_X = new Vector3(1, 0, 0);
    Vector3 AXiS_Y = new Vector3(0, 1, 0);
    Vector3 AXiS_Z = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        //Animation 컴포넌트를 변수에 할당
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        float h = Input.GetAxis("Horizontal");//수평방향
        float v = Input.GetAxis("Vertical");//수직방향
        float r = Input.GetAxis("Mouse X");//마우스 좌우에 의해 회전

        //전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        //Translate(이동방향*속도*변위값*Time.deltatime, 기준좌표)
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);//Time.deltatime 안쓰면 엄청 빨리 돈다
        //Vector3.up 축을 기준으로 rotSpeed만큼의 속도로 회전
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        //키보드 입력값을 기준으로 동작할 애니메이션 수행
        if (v >= 0.1f)//위쪽 방향(키보드 위쪽)으로 이동할 때 0.3초 동안 RunF 애니메이션을 한다. 
        {
            anim.CrossFade("RunF", 0.3f);//전진 애니메이션
        }else if (v <= -0.1f)//위쪽 방향으로 이동할 때 
        {
            anim.CrossFade("RunB", 0.3f);//전진 애니메이션: 
        }
        else if (h >= 0.1f)//위쪽 방향으로 이동할 때 
        {
            anim.CrossFade("RunR", 0.3f);//전진 애니메이션: 
        }
        else if (h <= -0.1f)//위쪽 방향으로 이동할 때 
        {
            anim.CrossFade("RunL", 0.3f);//전진 애니메이션: 
        }
        else
        {
            anim.CrossFade("Idle", 0.3f);//정지시 Idle 애니메이션
        }
       
    }
    //점프 함수
    void Jump()
    {
        if (Input.GetKeyDown("space"))// && this.rb.velocity.y < 0.1 혹시 다른 층 안만든다면 ㄱㄱ
        {
            rb.AddForce(0, force, 0);
        }
    }
}
