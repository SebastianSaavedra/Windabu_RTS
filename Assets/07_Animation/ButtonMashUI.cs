using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMashUI : MonoBehaviour
{
    public Image P1Neutral, p1z, p1x;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            P1Neutral.sprite = p1z.sprite;
        }
    }
}
