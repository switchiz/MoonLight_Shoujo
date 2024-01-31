using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack_Bullet : MonoBehaviour
{


    /// <summary>
    /// ���콺 ��ġ
    /// </summary>
    Vector3 moveDirection;

    public GameObject obj;
    public float speed;
 


    // Start is called before the first frame update
    void Start()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // 2D ������ ��� z ��ǥ�� ����
        mousePosition.y += 0.0f;

        
        moveDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        angle -= 180; // ��������Ʈ�� ������ ���ϰ� �ִٸ� 180�� ȸ��
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        StartCoroutine(OnDel());

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    IEnumerator OnDel()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))  // ���̶��
        {
            Instantiate(obj, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);


        }
    }


}
