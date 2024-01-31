using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Bullet : MonoBehaviour
{
    public GameObject target;

    Vector3 targetPos;

    Vector3 moveDirection;

    public Rigidbody2D rb;

    public float speed = 10.0f;
    public float gravity = 1.0f;
    public float dir;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(remove());
        shooting();  
    }

    /// <summary>
    /// 벽, 플레이어에 부딪히면 사라지는 함수
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject); // 총알이 블럭 or 플레이어와 부딪히면 삭제
        }
    }

    /// <summary>
    /// 시간이 지나면 사라지는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator remove()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
    }
        

    private void shooting()
    {


        targetPos = target.transform.position;

        float distance = Vector2.Distance(transform.position, targetPos)*0.5f;

        targetPos.y += Random.Range( distance - 2.0f, distance); // 2~4가 = 7
        moveDirection = (targetPos - transform.position).normalized;

    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * moveDirection);
        
    }


}
