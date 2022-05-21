using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    float timeValue;
    [SerializeField]
    Text timeToDisplay;

    [SerializeField]
    RectTransform buyButtons;

    private void Awake()
    {
        timeValue = 4;
        ActivateUIButtons(false);
    }



    void Update()
    {
        if (timeValue > 1)
        {
            timeValue -= Time.deltaTime;
            timeToDisplay.text = Mathf.FloorToInt(timeValue).ToString();
        }
        else
        {
            Destroy(this.gameObject);
            ActivateUIButtons(true);
        }
    }

    private void ActivateUIButtons(bool b)
    {
        buyButtons.gameObject.SetActive(b);
    }
}
