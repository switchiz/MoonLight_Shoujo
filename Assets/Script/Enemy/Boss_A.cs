using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boss_A : EnemyBase
{
    public GameObject bullet;
    Animator anim;

    /// <summary>
    /// ��� �Ѿ� ��
    /// </summary>
    public int bulletvalue = 3;

    /// <summary>
    /// ��Ǻ� �ִϸ��̼� �r
    /// </summary>
    readonly int motion = Animator.StringToHash("motion");

    /// <summary>
    /// ���� ����
    /// </summary>
    int bossP = 0;

    /// <summary>
    /// �̵�
    /// </summary>
    bool move = false;

    private void Awake()
    {
        
        anim = GetComponent<Animator>();
        StartCoroutine(starter());
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        base.Die();
        anim.SetInteger(motion, 4); // �״� ���
        Player player = FindAnyObjectByType<Player>();
        player.Progress += 1;

    }

    IEnumerator starter()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger(motion, 1); // Idle ���
        StartCoroutine(pattern());
    }
    /// <summary>
    /// ���� ���� + ��Ÿ��
    /// </summary>
    /// <returns></returns>
    IEnumerator pattern()
    {
        anim.SetInteger(motion, 1); // Idle ���
        bossP = UnityEngine.Random.Range(1, 3); // ���� ���� ����
        yield return new WaitForSeconds(3.0f); // ��Ÿ��
        anim.SetInteger(motion, 5); // �����̵� ���
        yield return new WaitForSeconds(1.0f); // ��Ÿ��

        switch (bossP)
        {
            case 1: 
                StartCoroutine(pattern_1());
                break;

            case 2: 
                StartCoroutine(pattern_2());
                break;

        }
    }

    IEnumerator pattern_1()
    {
        anim.SetInteger(motion, 1); // Idle ���
        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        transform.position = new Vector3(8.25f, 6.0f, 0);
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger(motion, 2); // ���� ���
        yield return new WaitForSeconds(0.5f);
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 3.5f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 3.5f), Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, new Vector2(transform.position.x, transform.position.y + 3.5f), Quaternion.identity);
        StartCoroutine(pattern());

    }

    IEnumerator pattern_2()
    {
        anim.SetInteger(motion, 1); // Idle ���
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.position = new Vector3(-7.5f, 6.0f, 0);
        yield return new WaitForSeconds(1.5f);
        anim.SetInteger(motion, 3); // ��� ���
        move = true;
        yield return new WaitForSeconds(1.5f);
        move = false;
        StartCoroutine(pattern());
    }


    private void FixedUpdate()
    {

        if ( move )transform.Translate(Time.deltaTime * 12 * Vector2.right); // �̵�
    }




}
