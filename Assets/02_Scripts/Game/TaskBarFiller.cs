using Com.MaluCompany.TestGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskBarFiller : MonoBehaviour
{

    [SerializeField] GameManager taskFiller;
    [SerializeField] Slider taskBar;
public void OnTaskComplete() 
    {
        taskBar.value = taskBar.value + 1;
    }
}
