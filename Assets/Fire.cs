using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = 20f;
    Transform tr;
    public Rigidbody2D rb;
    GameObject heroTarget;
    HerosScript hero; 
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.right * speed * -1;
    }

    private void Update()
    {
        heroTarget = HerosScript.AliveHerosList[0];
        Vector2 targetPosition = new Vector2(heroTarget.transform.position.x - 1.1f, heroTarget.transform.position.y - 1.1f);
        tr.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "HERO_TAG")
        {
            Destroy(this.gameObject);
            hero = collision.gameObject.GetComponent<HerosScript>();
            hero.heroHealth -= 50;
        }
    }
}
