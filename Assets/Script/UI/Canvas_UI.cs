using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_UI : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


        var obj = FindObjectsOfType<Canvas_UI>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }



}
