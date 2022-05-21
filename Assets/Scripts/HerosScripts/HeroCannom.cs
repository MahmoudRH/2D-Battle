using System.Collections.Generic;
using UnityEngine;

public class HeroCannom : MonoBehaviour
{
    public Transform cannon;
    public GameObject fire;
    List<GameObject> OrcsList;
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
        OrcsList = ORCsScript.AliveORCsList;
        for (int i = 0; i < OrcsList.Count; i++)
        {
            dist = Vector3.Distance(cannon.position, OrcsList[i].transform.position);
            if (dist <= 13 && HerosCastleScript.isLose == false)
            {
                if (timerInSeconds > 0)
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
            HeroFire.forse = HeroFire.forse * 2;
        }
    }
}
