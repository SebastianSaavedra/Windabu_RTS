using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillItself : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(killitself());
    }
    IEnumerator killitself() 
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        yield break;
    }

}
