using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rbb;
    public float speed = 20f;
   private  PlayerController pl;
    public static int hits = 0;
    void Start()
    {
        rbb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
       pl = FindObjectOfType<PlayerController>();


    }

    void Update()
    {
        rbb.velocity = new Vector2(speed, rbb.velocity.y);



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            hits++;
            if (hits==4)
            {
                Destroy(collision.gameObject);
                hits = 0;
            }
            Destroy(this.gameObject);
        }

    }
}
