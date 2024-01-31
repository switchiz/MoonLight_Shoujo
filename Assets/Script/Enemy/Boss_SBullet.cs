using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SBullet : MonoBehaviour
{
    public float speed = 0.0f;
    public GameObject Parti;

    void Start()
    {

        StartCoroutine(remove());

        StartCoroutine(Particle());
    }

    /// <summary>
    /// ��, �÷��̾ �ε����� ������� �Լ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject); // �Ѿ��� �� or �÷��̾�� �ε����� ����
        }
    }

    /// <summary>
    /// �ð��� ������ ������� �Լ�
    /// </summary>
    /// <returns></returns>
    IEnumerator remove()
    {
        yield return new WaitForSeconds(7.0f);
        Destroy(this.gameObject);
    }

    IEnumerator Particle()
    {
        while (true)
        {
            Instantiate(Parti, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }


    }


    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.left);


        if( speed < 15.0f )
        speed += 0.02f;
        
    }


}
