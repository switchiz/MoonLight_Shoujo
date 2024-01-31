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
    /// Hp , Hp�� 0 �Ǹ� Die �Լ� ����
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
        if (collision.gameObject.CompareTag("PlayerBullet"))   // �Ѿ��� �ε�ġ�� HP�� 1 �����Ѵ�.
        {
            Hp--;
        }
    }

    /// <summary>
    /// �÷��̾� ��ġ Ÿ����
    /// </summary>
    Vector2 targetPos;

    /// <summary>
    /// �¿� üũ
    /// </summary>
    public int checkLR = 1;

    protected virtual void Start()
    {
        target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        hp = maxHp;
    }

    /// <summary>
    /// �� ��
    /// </summary>
    protected virtual void Update()
    {
        targetPos = target.transform.position;
        if (targetPos.x < rb.position.x) checkLR = -1; // ( �����̸� - )
        else checkLR = 1;                              // �����̸� +
         //Debug.Log($"checkLR : {checkLR} , distance : {distance} ");

    }

    protected virtual void Die()
    {
        StopAllCoroutines();
        StartCoroutine(Destroy());
    }

    // ������ 1�ʵ� ���� + ���̾� ����
    IEnumerator Destroy()
    {
        gameObject.layer = LayerMask.NameToLayer("EnemyDead");
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);
    }
}
