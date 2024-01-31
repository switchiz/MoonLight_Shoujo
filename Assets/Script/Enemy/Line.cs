using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float dir;
    public float ScaleY = 3.00f;
    Animator animator;

    readonly int shot = Animator.StringToHash("shot");


    private void Awake()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(shotting());
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, dir);
    }


    IEnumerator shotting()
    {
        
        yield return new WaitForSeconds(1.8f);
        ScaleY = 1.20f;
        animator.SetInteger(shot, 1);
        gameObject.layer = LayerMask.NameToLayer("EnemyBullet");

        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if ( ScaleY > 0.2 ) ScaleY -= 0.01f;
        transform.localScale = new Vector3(1.5f, ScaleY, 1);
    }
}
