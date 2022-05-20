using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    public Transform cannon;
    public GameObject fire;
    List<GameObject> heroList;
    float dist = 0;
    public float timerInSeconds = 5;
    float secound = 0;
    public AudioSource improvmentSound;
    int improveTime = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heroList = HerosScript.AliveHerosList;
        for (int i = 0; i < heroList.Count; i++)
        {
            dist = Vector3.Distance(cannon.position, heroList[i].transform.position);
            if(dist <= 13)
            {
                if(timerInSeconds > 0)
                {
                    timerInSeconds -= Time.deltaTime;
                    secound--;
                    secound = Mathf.FloorToInt(timerInSeconds % 60);
                    if (secound == -1.00f)
                    {
                        Instantiate(fire, new Vector3(cannon.position.x - 2, cannon.position.y, 0), cannon.rotation);
                    }
                }
                else
                {
                    timerInSeconds = 5;
                }
            }
        }

    }

    private void OnMouseDown()
    {
        improveTime++;
        if (improveTime <= 2)
        {
            improvmentSound.PlayOneShot(improvmentSound.clip);
            EnemyFire.forse = 50f;
        }
    }
}
