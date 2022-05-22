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
    [SerializeField]
    public SpriteRenderer castle;
    float angle = 0f;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        OrcsList = ORCsScript.AliveORCsList;
        if(OrcsList.Count > 0)
        {
            dist = Vector3.Distance(cannon.position, OrcsList[0].transform.position);
            if (dist <= 13 && HerosCastleScript.isLose == false)
            {
                angle = (Mathf.Abs(Mathf.Cos(dist / castle.bounds.size.y)) * -100);
                if(angle < -20)
                {
                    angle = (Mathf.Abs(Mathf.Cos(dist / castle.bounds.size.y)) * -100) + 20;
                }
                cannon.transform.rotation = Quaternion.Euler(new Vector3(cannon.transform.rotation.eulerAngles.x, cannon.transform.rotation.eulerAngles.y, angle));
                if (timerInSeconds > 0)
                {
                    timerInSeconds -= Time.deltaTime;
                    secound--;
                    secound = Mathf.FloorToInt(timerInSeconds % 60);
                    if (secound == -1.00f)
                    {
                        cannon.transform.rotation = Quaternion.Euler(new Vector3(cannon.transform.rotation.eulerAngles.x, cannon.transform.rotation.eulerAngles.y, angle));
                        Instantiate(fire, new Vector3(cannon.position.x, cannon.position.y, 0), cannon.rotation);
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
        if(Ui_events.totalBalance >= 50)
        {
            improveTime++;
            if (improveTime <= 2)
            {
                improvmentSound.PlayOneShot(improvmentSound.clip);
                HeroFire.forse = HeroFire.forse * 2;
                Ui_events.totalBalance -= 50;
            }
        }
    }
}
