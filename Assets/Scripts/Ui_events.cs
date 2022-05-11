using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_events : MonoBehaviour
{
    int totalBalance = 999999;
    private const int KNIGHT1 = 1, KNIGHT2 = 2, KNIGHT3 = 3;

    //prefabs
    public GameObject knight1, knight2, knight3;

    [SerializeField]
    Transform castle; //forground part of the castle

    [SerializeField]
    Text wavesTxt, balanceTxt, healthTxt;

    private int currentSortingOrder = 1;

    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = new Vector3(castle.position.x + 2 ,-1,0);
        balanceTxt.text = totalBalance + " $";
    }

    public void instantiateKnight(int type)
    {
        switch (type)
        {
            case KNIGHT1:
                buyKnight1(totalBalance);
                break;

            case KNIGHT2:
                buyKnight2(totalBalance);
                break;

            case KNIGHT3:
                buyKnight3(totalBalance);
                break;
        }
    }

    private void buyKnight1(int balance)
    {
        if (balance >= 5)
        {
            totalBalance -= 5;
            GameObject heroInstance = Instantiate(knight1, spawnPosition, knight1.transform.rotation);
            heroInstance.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
            currentSortingOrder++;
            updateBalanceTxt();
            /*
             *Disable the button for a short time 
             */
        }
    }

    private void buyKnight2(int balance)
    {
        if (balance >= 15)
        {
            totalBalance -= 15;
            GameObject heroInstance = Instantiate(knight2, spawnPosition, knight1.transform.rotation);
            heroInstance.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
            currentSortingOrder++;
            updateBalanceTxt();
            /*
             *Disable the button for a short time 
             */
        }
    }

    private void buyKnight3(int balance)
    {
        if (balance >= 30)
        {
            totalBalance -= 30;
            GameObject heroInstance = Instantiate(knight3, spawnPosition, knight1.transform.rotation);
            heroInstance.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
            currentSortingOrder++;
            updateBalanceTxt();
            /*
             *Disable the button for a short time 
             */
        }
    }

    private void updateBalanceTxt()
    {
        balanceTxt.text = totalBalance + " $";
    }
}
