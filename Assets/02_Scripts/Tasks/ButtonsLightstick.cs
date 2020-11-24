using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsLightstick : MonoBehaviour
{
    int counter;

    private void Start()
    {
        counter = 0;
    }

    public void Pressed()
    {
        counter++;
        if(counter == 5)
        {
            GetComponentInParent<ProductImpresora>().AddCounter();
        }
    }
}
