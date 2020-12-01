using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalPlayerManager : MonoBehaviour
{
    public TextMeshProUGUI cartel, chapitas, stick;
    #region Variables
    public static int paperCounter, stickCounter, chapaCounter;
    #endregion

    public void Update()
    {
        cartel.text = "" + paperCounter;
        chapitas.text = "" + chapaCounter;
        stick.text = "" + stickCounter;
    }
}
