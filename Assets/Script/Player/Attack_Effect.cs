using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class Attack_Effect : MonoBehaviour
{
    public GameObject PlayerObj;
    public GameObject obj;
    float PlayerLR;

    private void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        Player player = PlayerObj.GetComponent<Player>();

        PlayerLR = player.playerDirection;

        

        StartCoroutine(OnDel());
    }

    IEnumerator OnDel()
    {

        gameObject.transform.localScale = new Vector3(1.0f * PlayerLR, 1.0f, 1.0f);

        yield return new WaitForSeconds(0.1f);

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))  // 적이라면
        {
            Instantiate(obj, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);


        }
    }


}
