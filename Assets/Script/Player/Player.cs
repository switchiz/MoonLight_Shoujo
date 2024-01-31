using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class Player : MonoBehaviour
{
    PlayerInput inputActions;
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// hp
    /// </summary>
    private int hp = 5;

    public float a;

    /// <summary>
    /// Hp , Hp�� 0 �Ǹ� Die �Լ� ����
    /// </summary>
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            hp = Mathf.Clamp(hp, 0, 5);
            onHpChange?.Invoke(hp);
            

            if (Hp == 0)
            {
                StartCoroutine(Dead());
            }

        }
    }


    IEnumerator Dead()
    {
        inputActions.Player.Disable();
        anim.SetInteger(Alive, 1);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("GameOver");
    }

    /// <summary>
    /// Hp ��������Ʈ
    /// </summary>
    public Action<int> onHpChange;


    private int progress = 0;

    /// <summary>
    /// ���൵
    /// </summary>
    public int Progress
    {
        get { return progress; }
        set
        {
            progress = value;
            onProgress?.Invoke(progress);
        }
    }

    public Action<int> onProgress;


    /// <summary>
    /// ���Ÿ�
    /// </summary>
    public GameObject atk_bullet; 

    /// <summary>
    /// �ٰŸ�
    /// </summary>
    public GameObject atk;

    /// <summary>
    /// ���� ������
    /// </summary>
    bool charge = false;



    /// <summary>
    /// ���� ������
    /// </summary>
    float charge_gauge = 0.0f;

    Vector2 pDirection;

    public float playerDirection = 1.0f;

    /// <summary>
    /// �̵��ӵ�
    /// </summary>
    public float playerSpeed = 5.0f;

    /// <summary>
    /// ������
    /// </summary>
    public float playerJump = 12f;
    readonly int State = Animator.StringToHash("State");
    readonly int Jump_State = Animator.StringToHash("Jump_State");
    readonly int Attack_State = Animator.StringToHash("Attack_State");
    readonly int Alive = Animator.StringToHash("Alive");

    /// <summary>
    /// �̵��� �� �̸�
    /// </summary>
    public string MapName;


    // anim.SetInteger(State_string, 1);


    private void Awake()
    {
        inputActions = new PlayerInput();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        var obj = FindObjectsOfType <Player>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        DontDestroyOnLoad(this.gameObject); // ���� ������Ʈ �ı�����
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();                       // Ȱ��ȭ�� �� Player�׼Ǹ��� Ȱ��ȭ
        inputActions.Player.LRMove.performed += context => { anim.SetInteger(State, 1); pDirection = context.ReadValue<Vector2>(); Move(); };
        inputActions.Player.LRMove.canceled += context => { anim.SetInteger(State, 0); pDirection = context.ReadValue<Vector2>(); Move(); };
        inputActions.Player.Attack.performed += context => Attack();
        inputActions.Player.Attack.canceled += context => Attack_cancle();
        inputActions.Player.Jump.performed += context => onJump();
        


    }
    private void OnDisable()
    {
        inputActions.Player.LRMove.performed -= context => { anim.SetInteger(State, 1); pDirection = context.ReadValue<Vector2>(); Move(); };
        inputActions.Player.LRMove.canceled -= context => { anim.SetInteger(State, 0); pDirection = context.ReadValue<Vector2>(); Move(); };
        inputActions.Player.Attack.performed -= context => Attack();
        inputActions.Player.Attack.canceled -= context => Attack_cancle();

        inputActions.Player.Jump.performed -= context => onJump();

        inputActions.Player.Disable();                      // Player�׼Ǹ��� ��Ȱ��ȭ
    }

    private void Attack() // Ŭ����
    {
        if (anim.GetInteger("Jump_State") == 0 && anim.GetInteger(Attack_State) == 0) // �������� �ƴҶ�
        {
            anim.SetInteger(Attack_State, 1); // ��� ����
            charge = true; // ����
        }
    }

    private void Attack_cancle() // Ŭ�� ����
    {
        if (anim.GetInteger("Jump_State") == 0 & anim.GetInteger(Attack_State) == 1) // ���� , ���ݸ�� ���϶�
        {
            StartCoroutine(attack());
            charge_gauge = 0.0f;
            charge = false;
        }
    }

    IEnumerator attack() // ���� ��� / ����
    {
        anim.SetInteger(Attack_State, 2);
        if (charge_gauge < 27)
        {
            Instantiate(atk, new Vector2(transform.position.x + (1.2f * -playerDirection), transform.position.y + 2.4f), Quaternion.identity);
        }
        else
        {
            Instantiate(atk_bullet, new Vector2(transform.position.x + (1.2f * -playerDirection), transform.position.y + 2), Quaternion.identity);
        }
        yield return new WaitForSeconds( 0.2f);
        anim.SetInteger(Attack_State, 0);

    }


    private void onJump()
    {
        if( anim.GetInteger("Jump_State") == 0 && anim.GetInteger("Attack_State") == 0 )
        { 
        rb.velocity = new Vector2(rb.velocity.x, playerJump);
        }
    }

    private void Move()
    {


        if (pDirection.x < 0)
            playerDirection = 1;
        else if (pDirection.x > 0)
            playerDirection = -1;





    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))   // �ε�����
        {
            Hp--; // HP 1 ���� 

            StartCoroutine(invi());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))   // �ε�����
        {
            Hp--; // HP 1 ���� 

            StartCoroutine(invi());
        }
    }


    IEnumerator invi() // 1.5�� ����
    {
        gameObject.layer = LayerMask.NameToLayer("Player_invi");
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        yield return new WaitForSeconds(1.5f);
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void FixedUpdate()
    {

        if (anim.GetInteger("Attack_State") == 0)
        {
            transform.Translate(Time.fixedDeltaTime * playerSpeed * pDirection); // �̵�
            gameObject.transform.localScale = new Vector3(1.0f * playerDirection, 1.0f, 1.0f); // ��������Ʈ ���¹���
        }

        FloorCheck(); // �ٴ� üũ

        if (charge) charge_gauge++; // ��� Ŭ���� ���Ÿ����� ����
    
    }


    /// <summary>
    ///  �ٴ� üũ
    /// </summary>
    void FloorCheck()
    {

        Vector2 selfPos = rb.position;
        selfPos.x -= playerDirection * 0.1f;
        Debug.DrawRay(selfPos, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(selfPos, Vector2.down, 1.5f, LayerMask.GetMask("Block", "MoveBlock")); // �浹 ����

        selfPos.x += playerDirection * 0.54f;
        Debug.DrawRay(selfPos, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D hit2 = Physics2D.Raycast(selfPos, Vector2.down, 1.5f, LayerMask.GetMask("Block", "MoveBlock")); // �浹 ����

        if (rb.velocity.y != 0)
        {
            anim.SetInteger(Jump_State, 1);
            if (hit.collider != null || hit2.collider != null) // ���� ����
                anim.SetInteger(Jump_State, 2);
        }
        else// �� ��
        {
            anim.SetInteger(Jump_State, 0);
        }

    }

}
