using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControl : MonoBehaviour
{
    public GameObject mario;
    private bool canCreate = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canCreate)
        {
            Vector2 position = new Vector2(transform.position.x, mario.transform.position.y);
            Instantiate(mario, position, mario.transform.rotation);
            canCreate = false;
            Invoke("crearEnemigo", 5f);
        }
    }
    private void crearEnemigo()
    {
        canCreate = true;
    }
}
