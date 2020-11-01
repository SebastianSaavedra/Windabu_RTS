using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchandiseMinigame : MonoBehaviour
{
    // Keys to mash
    KeyCode[] keysToMash = new KeyCode[] {
        KeyCode.K,
        KeyCode.L,
    };

    public int mashLimit = 10;
    [SerializeField]int mashScore;

    public Text uiMashCounter;

    private void Start()
    {
        uiMashCounter = GetComponent<Text>();
    }

    private void Update()
    {
        // Recognize keys and add to score
        for (int i = 0; i < keysToMash.Length; i++)
        {
            if (Input.GetKeyDown(keysToMash[i]))
            {
                mashScore++;
                uiMashCounter.text = mashScore.ToString("0");
            }
        }

        // Win con
        if(mashScore >= mashLimit)
        {
            Debug.Log("Win");
            mashScore = 0;
            uiMashCounter.text = mashScore.ToString("0");

            gameObject.SetActive(false);
        }
    }
}
