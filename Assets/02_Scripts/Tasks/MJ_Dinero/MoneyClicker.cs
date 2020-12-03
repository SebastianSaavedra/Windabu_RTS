using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyClicker : MonoBehaviour
{
    //public GameObject shareButton, sharePanel;
    [SerializeField] bool team;
    [SerializeField] TextMeshProUGUI dineroText;
    int likes;
    //public bool isPublic;

    //private void Start()
    //{

    //}

    public void Like()
    {
        //Limit
        if(likes <= 99)
        {
            likes += 1;
            dineroText.text = ""+ likes;
            Debug.Log("Cantidad de Likes: " + likes);
        }
        else { Debug.Log("Full Money"); }

        //if(!shareButton.activeInHierarchy)
        //{
        //    shareButton.SetActive(true);
        //}
    }

    //public void Share()
    //{
    //    sharePanel.SetActive(true);
    //}

    //public void ChangePublic()
    //{
    //    if (isPublic)
    //    {
    //        isPublic = false;
    //    }
    //    else
    //    {
    //        isPublic = true;
    //    }
    //}

    public void Confirm()
    {

        // Minijuegos.m_clicks(likes);
        //if (isPublic)
        //{
        if (team)
        {
            int macroLike = 0;
            while (macroLike <= likes - 10)
            {
                macroLike += 10;
            }
            Minijuegos.m_clicksA(macroLike);

            likes -= macroLike;
            dineroText.text = "" + likes;
        }
        else 
        {
            int macroLike = 0;
            while (macroLike <= likes - 10)
            {
                macroLike += 10;
            }
            Minijuegos.m_clicksB(macroLike);

            likes -= macroLike;
            dineroText.text = "" + likes;
        }
        //}

        //Reset
        //isPublic = false;
        //sharePanel.SetActive(false);
        //shareButton.SetActive(false);

        Debug.Log("Reset");
    }
}
