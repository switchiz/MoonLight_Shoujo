using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StartPoint : MonoBehaviour
{
    GameObject Player;
    public int needProgress;


    private void Start()
    {
        Player player = FindAnyObjectByType<Player>();
        if (player.Progress == needProgress)
        {
            tel();
        }
    }


    void tel()
    {
        Player = GameObject.FindWithTag("Player");

        // ĳ���� �̵�
        Player.transform.position = this.transform.position;
    }

}
