using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rbd;
    private SpriteRenderer spr;
    private Animator amtr;
    private bool cargando = false;


    private static int ANIMACION_PARADO = 0, ANIMACION_CORRER = 1, ANIMACION_SALTAR = 2, ANIMACION_CORRER_SHOT = 3;
    private bool tocandoSuelo = false;
    public GameObject bolaPequeña;
    public GameObject bolaMediana;

    private float maxTime= 5f;
    private float TimeNow = 0f;

    private float switchColorDelay = .1f;
    private float switchColorTime = 0f;

    private Color originalColor;
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        amtr = GetComponent<Animator>();
        originalColor = spr.color;
    }

    // Update is called once per frame
    void Update()
    {

        amtr.SetInteger("Estado", ANIMACION_PARADO);
        rbd.velocity = new Vector2(0, rbd.velocity.y);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            
            spr.flipX = false;
            if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.RightArrow) &&tocandoSuelo)
            {
                TimeNow += Time.deltaTime;
                switchColorTime += Time.deltaTime;
                SetCorrerDisparar(1);
                if (switchColorTime > switchColorDelay)
                {
                    SwitchColor();
                }
                Debug.Log(TimeNow);
                cargando = true;
            }
            else
            {
                
                cargando = false;
                SetCorrer(1);
            }
            if (!cargando)
            {
                Debug.Log(TimeNow);
                if (TimeNow < 3f && TimeNow > 0)
                {

                    Vector2 position = new Vector2(transform.position.x + 5.5f, transform.position.y + 1.5f);
                    Instantiate(bolaPequeña, position, bolaPequeña.transform.rotation);
                    TimeNow = 0;
                }
                if (TimeNow >= 3f && TimeNow < 5f)
                {
                    Vector2 position = new Vector2(transform.position.x + 7f, transform.position.y + 1.5f);
                    Instantiate(bolaMediana, position, bolaMediana.transform.rotation);
                    TimeNow = 0;
                }
                if (TimeNow >= 5f)
                {
                    Vector2 position = new Vector2(transform.position.x + 7f, transform.position.y + 1.5f);
                    Instantiate(bolaMediana, position, bolaMediana.transform.rotation);
                    TimeNow = 0;
                }
            }
         
            


        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spr.flipX = true;
            if (Input.GetKeyDown(KeyCode.X) && tocandoSuelo)
            {
                SetCorrerDisparar(-1);
                Vector2 position = new Vector2(transform.position.x + 3f, transform.position.y - .6f);
                Instantiate(bolaPequeña, position, bolaPequeña.transform.rotation);
            }
            else
            {

                SetCorrer(-1);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && tocandoSuelo)
        {
            amtr.SetInteger("Estado", ANIMACION_SALTAR);
            rbd.velocity = Vector2.up * 24f;
            tocandoSuelo = false;
        }
    }
    private void SwitchColor()
    {
        if (spr.color == originalColor)
            spr.color = Color.yellow;
        else
            spr.color = originalColor;
        switchColorTime = 0;

    }
    public void SetCorrer(int sentido)
    {
        rbd.velocity = new Vector2(5 * sentido, rbd.velocity.y);
        amtr.SetInteger("Estado", ANIMACION_CORRER);
    }
    public void SetCorrerDisparar(int sentido)
    {
        rbd.velocity = new Vector2(5 * sentido, rbd.velocity.y);
        amtr.SetInteger("Estado", ANIMACION_CORRER_SHOT);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terreno")
        {
            tocandoSuelo = true;
        }
      
    }
}
