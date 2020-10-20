using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ImageRandomPos : MonoBehaviour
{
    [SerializeField] List<GameObject> images = new List<GameObject>();

    void Start()
    {
        //GameObject randImage = images[Random.Range(0,images.Count)];
        foreach (GameObject imagen in images)
        {
            imagen.transform.localPosition = new Vector2((Random.Range(-212f, 212f)), Random.Range(-363f, 363f));
        }
        //randImage.transform.localPosition = new Vector2((Random.Range(0,100f)),Random.Range(0,100f));
        //Debug.Log(randImage.transform.localPosition);
    }
}
