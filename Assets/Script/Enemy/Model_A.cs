using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Model : EnemyBase
{
    public GameObject bullet;
    Animator anim;

    /// <summary>
    /// �Ѿ� �߻� ����
    /// </summary>
    public float bulletInterval = 2.0f;

    /// <summary>
    /// ��� �Ѿ� ��
    /// </summary>
    public int bulletvalue = 3;

    /// <summary>
    /// �ֺ��� �÷��̾ �ִ°� ( false = ���� )
    /// </summary>
    bool playerCheck = false;
 
    /// <summary>
    /// �¿� �ִϸ��̼� ��
    /// </summary>
    readonly int LRdir = Animator.StringToHash("LRdir");

    /// <summary>
    /// ���� / ����� �ִϸ��̼� �r
    /// </summary>
    readonly int atk = Animator.StringToHash("ATK");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
        if ( playerCheck) anim.SetInteger(LRdir, checkLR); // playerCheck�� �Ǹ� LRdir �� checkLR�� ����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!playerCheck && collision.CompareTag("Player"))  // �÷��̾ Ʈ���� �����ȿ� ��������
        {
            playerCheck = true;   // üũ
            StopAllCoroutines();
            StartCoroutine(shooting()); // �߻� ����
        }

    }
    /// <summary>
    /// �¾Ƶ� �߻� ����
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (!playerCheck)
        {
            playerCheck = true;
            StopAllCoroutines();
            StartCoroutine(shooting()); // �߻� ����
        }

    }

    protected override void Die()
    {
        base.Die();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = false;
        rb.AddTorque(50);
        rb.AddForce( (Vector2.left * checkLR ) * Vector2.up *10.0f, ForceMode2D.Impulse);
    }

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




}
