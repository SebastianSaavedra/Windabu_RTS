using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorThings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    [SerializeField] GameObject padre;

    private void Start()
    {
        parent = this.gameObject;
    }
    public void Desaparecer()
    {
        parent.SetActive(false);
    }

    public void DesaparecerUngAMEobject() 
    {
        gameObject.SetActive(false);
    }
    public void DestruirPadre() 
    {
        Destroy(padre);
    }
}
