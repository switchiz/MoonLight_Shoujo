using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject canvas; // DontDestroyOnLoad�� ����� ĵ���� 
    GameObject obj; // DontDestroyOnLoad�� ����� ������Ʈ 

    void Awake()
    { 
        canvas = GameObject.FindWithTag("Canvas");
        obj = GameObject.FindWithTag("Player");

        OnDestroy();

    }



    void OnDestroy()
    {
        if (canvas != null)
        {
            Destroy(canvas); // ĵ���� A ����
        }
        if (obj != null)
        {
            Destroy(obj); // ������Ʈ B ����
        }
    }
}
