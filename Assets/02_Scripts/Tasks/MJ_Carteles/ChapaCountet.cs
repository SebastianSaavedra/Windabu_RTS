using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapaCountet : MonoBehaviour
{
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
