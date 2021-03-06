using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFire : MonoBehaviour
{
    float speed = 7f;
    Transform tr;
    GameObject oRKTarget;
    ORCsScript oRC;
    public static float forse = 5;
    //public ParticleSystem smok;
    void Start()
    {
        //smok.Pause();
        tr = GetComponent<Transform>();
        //rb.velocity = transform.right * speed * -1;
    }

    private void Update()
    {
        if(ORCsScript.AliveORCsList.Count > 0)
        {
            oRKTarget = ORCsScript.AliveORCsList[0];
            Vector2 targetPosition = new Vector2(oRKTarget.transform.position.x - 1.1f, oRKTarget.transform.position.y - 1.1f);
            tr.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            if (this.gameObject != null)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ORC_TAG")
        {
            //smok.Play();
            oRC = collision.gameObject.GetComponent<ORCsScript>();
            oRC.orcHealth -= forse;
            Destroy(this.gameObject);
        }
    }
}
