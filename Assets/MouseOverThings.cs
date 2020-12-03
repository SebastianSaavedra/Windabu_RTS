using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverThings : MonoBehaviour
{
    [SerializeField] ShopManager manager;
    [SerializeField] GameObject sold;
    [SerializeField] GameObject requiere;
    [SerializeField] GameObject canBuy;
    [SerializeField] int wichbutton;

    private void OnMouseOver()
    {
        switch (wichbutton) 
        {
            case 0://impresora
                if (manager.canBuy_0) 
                {
                    canBuy.SetActive(true);
                }
                else 
                {
                    sold.SetActive(true);
                }
                break;

            case 1://3d
                if (manager.canBuy_1)
                {
                    canBuy.SetActive(true);
                }
                else
                {
                    sold.SetActive(true);
                }
                break;
            case 2://chapita
                if (manager.canBuy_2)
                {
                    canBuy.SetActive(true);
                }
                else
                {
                    sold.SetActive(true);
                }
                break;
            case 3://Plotter
                if (manager.canBuy_3 && !manager.canBuy_0)
                {
                    canBuy.SetActive(true);
                }
                if (manager.canBuy_3 && manager.canBuy_0)
                {
                    requiere.SetActive(true);
                }
                if (!manager.canBuy_3)
                {
                    sold.SetActive(true);
                }
                break;
            case 4://Merch
                if (manager.canBuy_4 && !manager.canBuy_1)
                {
                    canBuy.SetActive(true);
                }
                if (manager.canBuy_4 && manager.canBuy_1)
                {
                    requiere.SetActive(true);
                }
                if (!manager.canBuy_4)
                {
                    sold.SetActive(true);
                }
                break;
            case 5://ImpInd
                if (manager.canBuy_5 && !manager.canBuy_2)
                {
                    canBuy.SetActive(true);
                }
                if (manager.canBuy_5 && manager.canBuy_2)
                {
                    requiere.SetActive(true);
                }
                if (!manager.canBuy_5)
                {
                    sold.SetActive(true);
                }
                break;
            case 6://sc
                if (manager.sc0 || manager.sc1 || manager.sc2)
                {
                    canBuy.SetActive(true);
                }
                if (manager.sc0 && manager.sc1 && manager.sc2)
                {
                    sold.SetActive(true);
                }
                break;
        }
    }

    private void OnMouseExit()
    {
        canBuy.SetActive(false);
        sold.SetActive(false);
        requiere.SetActive(false);
    }

}
