using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Model_C : EnemyBase
{
    public GameObject bullet;
    Animator anim;

    /// <summary>
    /// 속도
    /// </summary>
    float speed = 0.5f;

    /// <summary>
    /// 총알 발사 간격
    /// </summary>
    public float bulletInterval = 2.0f;

    /// <summary>
    /// 뱉는 총알 수
    /// </summary>
    public int bulletvalue = 3;

    /// <summary>
    /// 주변에 플레이어가 있는가 ( false = 없음 )
    /// </summary>
    bool playerCheck = false;
 
    /// <summary>
    /// 좌우 애니메이션 값
    /// </summary>
    readonly int LRdir = Animator.StringToHash("LRdir");

    /// <summary>
    /// 공격 / 비공격 애니메이션 걊
    /// </summary>
    readonly int atk = Animator.StringToHash("ATK");

    Vector2 dir;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        dir = new Vector2(anim.GetInteger(LRdir),0);
        
        if ( playerCheck) anim.SetInteger(LRdir, checkLR); // playerCheck가 되면 LRdir 을 checkLR로 설정

        transform.Translate(Time.deltaTime * speed * dir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!playerCheck && collision.CompareTag("Player"))  // 플레이어가 트리거 영역안에 들어왔으면
        {
            playerCheck = true;   // 체크
            StopAllCoroutines();
            StartCoroutine(shooting()); // 발사 시작
        }

    }

    /// <summary>
    /// 맞아도 발사 시작
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!playerCheck)
        {
            playerCheck = true;
            StopAllCoroutines();
            StartCoroutine(shooting()); // 발사 시작
        }

    }


    /// <summary>
    /// 발사
    /// </summary>
    /// <returns></returns>
    IEnumerator shooting()
    {
        while (true)
        {
            anim.SetInteger(atk, 1);

            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);

            anim.SetInteger(atk, 0);
            yield return new WaitForSeconds(bulletInterval);
        }
        
    }

    /// <summary>
    /// 죽음
    /// </summary>
    protected override void Die()
    {
        base.Die();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = false;
        rb.AddTorque(50);
        rb.AddForce((Vector2.left * checkLR) * Vector2.up * 10.0f, ForceMode2D.Impulse);
    }

}
