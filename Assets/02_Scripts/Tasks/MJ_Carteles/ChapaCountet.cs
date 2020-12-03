using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapaCountet : MonoBehaviour
{
    [HideInInspector]
    public int chapas;

    public void NullChapas()
    {
        chapas = 0;
    }

    public void AddChapa()
    {
        chapas++;
    }
}
