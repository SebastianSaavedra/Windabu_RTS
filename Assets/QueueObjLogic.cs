﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueObjLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        StartCoroutine(Matate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Matate() 
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        yield break;
    }
}