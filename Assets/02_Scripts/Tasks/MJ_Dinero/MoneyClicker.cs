using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyClicker : MonoBehaviour
{
    public GameObject shareButton, sharePanel;

    int likes;
    public bool isPublic;

    private void Start()
    {

    }

    public void Like()
    {
         if(likes <= 0)
        {
            shareButton.SetActive(true);
        }//Limit
        if(likes <= 99)
        {
            likes += 1;
            Debug.Log(likes);
        }
        else { Debug.Log("Full Money"); }

  
    }

    public void Share()
    {
        sharePanel.SetActive(true);
    }

    public void ChangePublic()
    {
        if (isPublic)
        {
            isPublic = false;
        }
        else
        {
            isPublic = true;
        }
    }

    public void Confirm()
    {
        Debug.Log("$$$");

        // Minijuegos.m_clicks(likes);

        int macroLike = 0;
        while(macroLike <= likes - 10)
        {
            macroLike += 10;
        }

        Minijuegos.m_clicks(macroLike / 10);

        //Reset
        likes -= macroLike;
        isPublic = false;
        sharePanel.SetActive(false);
        shareButton.SetActive(false);
    }
}
