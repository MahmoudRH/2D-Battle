using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    float timeValue = 4;
   [SerializeField]
    Text timeToDisplay;
    void Update()
    {
        if(timeValue > 1)
        {
            timeValue -= Time.deltaTime;
            timeToDisplay.text = Mathf.FloorToInt(timeValue).ToString();
        }
        else
        {
            Destroy(this.gameObject);
        }



    }
}
