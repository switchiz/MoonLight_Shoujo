using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Start_Canvas_UI : MonoBehaviour
{
    StartInput startInput;
    Animator animator;

    readonly int Start = Animator.StringToHash("Start");

    void Awake()
    {
        startInput = new StartInput();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        startInput.Start.Enable();
        startInput.Start.Reset.performed += context => StartCoroutine(Reset());
    }

    private void OnDisable()
    {
        startInput.Start.Reset.performed -= context => StartCoroutine(Reset());
        startInput.Start.Disable();
    }

    IEnumerator Reset()
    {
        animator.SetInteger(Start, 1);
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Load");

    }


}
