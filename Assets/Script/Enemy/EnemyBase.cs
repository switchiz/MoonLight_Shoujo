using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public GameObject target;
    public Rigidbody2D rb;
    public int maxHp = 10;
    private int hp = 10;

    /// <summary>
    /// Hp , Hp가 0 되면 Die 함수 실행
    /// </summary>
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            hp = Mathf.Max(hp, 0);

            if (Hp == 0)
            {
                Die();
            }

        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))   // 총알이 부딪치면 HP가 1 감소한다.
        {
            Hp--;
        }
    }

    /// <summary>
    /// 플레이어 위치 타게팅
    /// </summary>
    Vector2 targetPos;

    /// <summary>
    /// 좌우 체크
    /// </summary>
    public int checkLR = 1;

    protected virtual void Start()
    {
        target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    /// <summary>
    /// 좌 우
    /// </summary>
    protected virtual void Update()
    {
        targetPos = target.transform.position;
        if (targetPos.x < rb.position.x) checkLR = -1; // ( 왼쪽이면 - )
        else checkLR = 1;                              // 우측이면 +
         //Debug.Log($"checkLR : {checkLR} , distance : {distance} ");

    }

    protected virtual void Die()
    {
        StopAllCoroutines();
        StartCoroutine(Destroy());
    }

    // 죽으면 1초뒤 삭제 + 레이어 변경
    IEnumerator Destroy()
    {
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
