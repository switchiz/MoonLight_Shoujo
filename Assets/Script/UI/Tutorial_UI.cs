using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tutorial_UI : MonoBehaviour
{

    Tutorial tutoInput;
    Animator animator;

    int click = 0;

    readonly int tuto = Animator.StringToHash("tutorial");
    void Awake()
    {
        tutoInput = new Tutorial();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        tutoInput.TutoInput.Enable();
        tutoInput.TutoInput.Phone.performed += context => click++;
    }

    private void OnDisable()
    {
    
        tutoInput.TutoInput.Phone.performed -= context => click++;
        tutoInput.TutoInput.Disable();
    }

    private void Update()
    {
        animator.SetInteger(tuto, click);

        if (click > 1)
        {
            Destroy(this.gameObject);
        }
    }


}
