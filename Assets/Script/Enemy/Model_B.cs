using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Model_B : EnemyBase
{
    Animator anim;
    float speed = 1.5f;

    /// <summary>
    /// �ֺ��� �÷��̾ �ִ°� ( false = ���� )
    /// </summary>
    bool playerCheck = false;
 
    /// <summary>
    /// ���� / ����� �ִϸ��̼� �r
    /// </summary>
    readonly int attack = Animator.StringToHash("Attack");

    Vector2 dir;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        
    }

    protected override void Update()
    {
        base.Update();
        if (playerCheck) anim.SetInteger(attack, checkLR); // playerCheck�� �Ǹ� LRdir �� attack�� ����
        dir = new Vector2(anim.GetInteger(attack), 0);
        
        if ( anim.GetInteger(attack) != 0) // �� �߰��� ���� ����
            {
            gameObject.transform.localScale = new Vector3(-1.0f * anim.GetInteger(attack), 1.0f, 1.0f);
            }
     
        transform.Translate(Time.deltaTime * speed * dir);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!playerCheck && collision.CompareTag("Player"))  // �÷��̾ Ʈ���� �����ȿ� ��������
        {
            playerCheck = true;   // üũ
        }

    }

    /// <summary>
    /// �¾Ƶ�  ����
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        playerCheck = true;
    }

    protected override void Die()
    {
        base.Die();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = false;
        rb.AddTorque(50);
        rb.AddForce( (Vector2.left * checkLR ) * Vector2.up *10.0f, ForceMode2D.Impulse);
    }






}
