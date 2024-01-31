using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        target = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        float width = target.transform.position.x;
        //transform.position = new Vector3(width + (width * - 0.2f) + 7.0f , 0.5f, 1);
        transform.position = new Vector3(width * 0.6f + 9.0f, 0.5f, 1);
    }
}
