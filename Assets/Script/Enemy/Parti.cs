using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parti : MonoBehaviour
{
    float sss = 1.00f;


    // Update is called once per frame
    void Update()
    {
        sss -= 0.01f;

        transform.localScale = new Vector3(sss,sss,sss);



        if (sss < 0.1)
        {
            Destroy(this.gameObject);
        }
    }
}
