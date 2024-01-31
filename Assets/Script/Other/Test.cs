using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    TestInput test;
    public string sceneName;
    bool wB = false;
    private GameObject Player;
    private Player Player_;

    SpriteRenderer spriteRenderer;

    public int needProgress;

    private void Awake()
    {
        test = new TestInput();

        spriteRenderer = GetComponent<SpriteRenderer>();

        


    }

    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();

        if (player.Progress != needProgress)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.0f);
        }
    }

    private void Update()
    {
        Player player = FindAnyObjectByType<Player>();

        if (player.Progress == needProgress)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1.0f);
        }

    }

    private void OnEnable()
    {
        test.Test.Enable();
        test.Test.Test1.performed += context => OnTest1();
        test.Test.Test1.canceled += context => OnTest2();
    }

    private void OnDisable()
    {
        test.Test.Test1.performed -= context => OnTest1();
        test.Test.Test1.canceled -= context => OnTest2();
        test.Test.Disable();
    }


    void OnTest1()
    {
        wB = true;
    }

    void OnTest2()
    {
        wB = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = FindAnyObjectByType<Player>();

        if (collision.gameObject.name == "Player" && wB == true && player.Progress == needProgress)
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("ok");

        }
    }

}
