using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    Animator anim;

    readonly int uiHp = Animator.StringToHash("uiHp");

    void Start()
    {
        anim = GetComponent<Animator>();
        Player player = FindAnyObjectByType<Player>();

        player.onHpChange += RefreshHp;
    }

    private void RefreshHp(int nowHp)
    {
        anim.SetInteger(uiHp,nowHp);
    }
}
