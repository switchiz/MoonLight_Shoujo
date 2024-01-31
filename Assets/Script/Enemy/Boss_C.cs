using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boss_C : EnemyBase
{
    public GameObject bullet;
    public GameObject Sbullet;
    Animator anim;

    /// <summary>
    /// 모션별 애니메이션 걊
    /// </summary>
    readonly int motion = Animator.StringToHash("motion");

    /// <summary>
    /// 패턴 랜덤
    /// </summary>
    int bossP = 0;

    /// <summary>
    /// 이동
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
        anim.SetInteger(motion, 4); // 죽는 모션
        Player player = FindAnyObjectByType<Player>();
        player.Progress += 1;

    }

    IEnumerator starter()
    {
        yield return new WaitForSeconds(2.5f);
        anim.SetInteger(motion, 1); // Idle 모션
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine(pattern());
    }
    /// <summary>
    /// 패턴 실행 + 딜타임
    /// </summary>
    /// <returns></returns>
    IEnumerator pattern()
    {

        anim.SetInteger(motion, 1); // Idle 모션
        bossP = UnityEngine.Random.Range(1, 7); // 패턴 랜덤 지정
        yield return new WaitForSeconds(1.0f); // 딜타임

        switch (bossP)
        {
            case 1: 
                StartCoroutine(pattern_1());
                break;
            case 2:
                StartCoroutine(pattern_2());
                break;
            case 3:
                StartCoroutine(pattern_3());
                break;
            case 4:
                StartCoroutine(pattern_4());
                break;
            case 5:
                StartCoroutine(pattern_5());
                break;
            case 6:
                StartCoroutine(pattern_6());
                break;

        }
    }

    IEnumerator pattern_1()
    {
        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);

        for ( int i = 0; i <8; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(0, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(0, 360);
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(pattern());

    }


    IEnumerator pattern_2()
    {
        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < 11; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(-7 + i* 1.8f, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(85, 95);
        }
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(pattern());

    }

    IEnumerator pattern_3()
    {
        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < 9; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(8.5f + i * -0.5f, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(89, 91);
            yield return new WaitForSeconds(0.1f);
        }
        GameObject line2 = Instantiate(bullet, new Vector2(0, -4.2f), Quaternion.identity);
        Line lineScript2 = line2.GetComponent<Line>();
        lineScript2.dir = 0;

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(pattern());

    }

    IEnumerator pattern_4()
    {
        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);
        anim.SetInteger(motion, 1); // 기본 모션
        for (int i = 0; i < 27; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(8.5f + i * -0.55f, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(89, 91);
            yield return new WaitForSeconds(0.1f);
        }

        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);

        for (int i = 0; i < 27; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(-8.3f + i * 0.55f, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(89, 91);
            yield return new WaitForSeconds(0.1f);
        }


        yield return new WaitForSeconds(2.0f);
        StartCoroutine(pattern());

    }

    IEnumerator pattern_5()
    {
        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);
        anim.SetInteger(motion, 1); // 기본 모션
        for (int i = 0; i < 12; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(-8.5f + i * 2.1f, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(71, 73);
        }
        yield return new WaitForSeconds(0.7f);
        anim.SetInteger(motion, 3); // 어택 모션
        yield return new WaitForSeconds(0.7f);


        for (int i = 0; i < 12; i++)
        {
            GameObject line = Instantiate(bullet, new Vector2(-8.5f + i * 2.1f, 0.5f), Quaternion.identity);
            Line lineScript = line.GetComponent<Line>();
            lineScript.dir = UnityEngine.Random.Range(109, 111);
        }

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(pattern());

    }

    IEnumerator pattern_6()
    {

        anim.SetInteger(motion, 3); // 마법 모션
        yield return new WaitForSeconds(0.7f);
        GameObject line = Instantiate(bullet, new Vector2(7, 0.5f), Quaternion.identity);
        Line lineScript = line.GetComponent<Line>();
        lineScript.dir = UnityEngine.Random.Range(88, 92);

        anim.SetInteger(motion, 1); // 일반 모션
        yield return new WaitForSeconds(1.0f);
        anim.SetInteger(motion, 2); // 마법 모션
        Instantiate(Sbullet, new Vector2(7.5f, -4), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -3.5f), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -3), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -2.5f), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Sbullet, new Vector2(7.5f, -1.5f), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -1), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -0.5f), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, 0), Quaternion.identity);

        yield return new WaitForSeconds(0.5f);
        Instantiate(Sbullet, new Vector2(7.5f, -4), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -3.5f), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -3), Quaternion.identity);
        Instantiate(Sbullet, new Vector2(7.5f, -2.5f), Quaternion.identity);
        yield return new WaitForSeconds(2.0f);

        StartCoroutine(pattern());


    }




    private void FixedUpdate()
    {
        math.clamp(transform.position.x, -7.6f, 8.0f);

        if ( move )transform.Translate(Time.deltaTime * 14 * Vector2.right); // 이동
    }




}
