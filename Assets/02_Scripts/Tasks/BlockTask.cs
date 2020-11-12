using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTask : MonoBehaviour
{

    public void CallBlock() 
    {
        StartCoroutine(BlockThisTask());
    }
public IEnumerator BlockThisTask() 
    {
        Debug.Log("Called");
        GetComponent<TaskDropDown>().enabled = false;
        yield return new WaitForSeconds(5f);
        GetComponent<TaskDropDown>().enabled = true;
        yield break;
    }
}
