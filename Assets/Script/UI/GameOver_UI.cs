using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject canvas; // DontDestroyOnLoad가 적용된 캔버스 
    GameObject obj; // DontDestroyOnLoad가 적용된 오브젝트 

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
            Destroy(canvas); // 캔버스 A 삭제
        }
        if (obj != null)
        {
            Destroy(obj); // 오브젝트 B 삭제
        }
    }
}
