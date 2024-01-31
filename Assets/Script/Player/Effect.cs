using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{

    float angle;
    // Start is called before the first frame update
    void Start()
    {
        angle = Random.Range(0, 360.0f);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        StartCoroutine(OnDel());
        
    }

    IEnumerator OnDel()
    {

        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);


    }
}
