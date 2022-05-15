using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_events : MonoBehaviour
{
    int totalBalance = 9999;
    private const int KNIGHT1 = 1, KNIGHT2 = 2, KNIGHT3 = 3;

    //prefabs
    public GameObject knight1, knight2, knight3;

    [SerializeField]
    Transform castle; //-> Hero's castle

    [SerializeField]
    Text wavesTxt, balanceTxt, healthTxt;

    [SerializeField]
    Button knight1Btn, knight2Btn, knight3Btn;

    [SerializeField]
    Image knight1BtnOverlay, knight2BtnOverlay, knight3BtnOverlay;

    private bool knight1Delay, knight2Delay, knight3Delay;

    private int currentSortingOrder = 1;

    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = new Vector3(castle.position.x + 2 ,-1,0);
        balanceTxt.text = totalBalance + " $";
    }
    private void Update()
    {
        knight1Btn.interactable = (totalBalance >= 5); 
        knight2Btn.interactable = (totalBalance >= 15); 
        knight3Btn.interactable = (totalBalance >= 30);

        knight1Delay =  ControlBtn(knight1Delay, knight1Btn, knight1BtnOverlay);
        knight2Delay =  ControlBtn(knight2Delay, knight2Btn, knight2BtnOverlay);
        knight3Delay =  ControlBtn(knight3Delay, knight3Btn, knight3BtnOverlay);

    }

    public void instantiateKnight(int type)
    {
        buyKnight(type, totalBalance);
    }

    private void buyKnight(int type, int balance)
    {
        GameObject heroInstance= null;
        switch (type)
        {
            case KNIGHT1:
                if (balance >= 5)
                {
                    totalBalance -= 5;
                    heroInstance = Instantiate(knight1, spawnPosition, knight1.transform.rotation);
                    knight1BtnOverlay.gameObject.SetActive(true);
                    knight1Delay = true;
                }
                break;

            case KNIGHT2:
                if (balance >= 15)
                {
                    totalBalance -= 15;
                    heroInstance = Instantiate(knight2, spawnPosition, knight2.transform.rotation);
                    knight2BtnOverlay.gameObject.SetActive(true);
                    knight2Delay = true;
                }
                break;

            case KNIGHT3:
                if (balance >= 30)
                {
                    totalBalance -= 30;
                    heroInstance = Instantiate(knight3, spawnPosition, knight3.transform.rotation);
                    knight3BtnOverlay.gameObject.SetActive(true);
                    knight3Delay = true;
                }
                break;
        }
        heroInstance.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
        currentSortingOrder++;
        updateBalanceTxt();
    }

    private void updateBalanceTxt()
    {
        balanceTxt.text = totalBalance + " $";
    }

    private bool ControlBtn(bool isDelaied,Button btn, Image overlay)
    {
        if (isDelaied)
        {
            btn.interactable = false;
            overlay.fillAmount -= 0.35f * Time.deltaTime;
            if (overlay.fillAmount <= 0)
            {
                isDelaied = false;
                overlay.fillAmount = 1;
                overlay.gameObject.SetActive(false);
                btn.interactable = true;
            }
        }

        return isDelaied;

    }
}
