using Com.MaluCompany.TestGame;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchandiseMinigame : MonoBehaviourPunCallbacks
{
    // Keys to mash
    KeyCode[] keysToMash = new KeyCode[] {
        KeyCode.K,       KeyCode.L,        KeyCode.Q,       KeyCode.W,
        KeyCode.R,        KeyCode.T,        KeyCode.Y,        KeyCode.U,
        KeyCode.I,        KeyCode.O,        KeyCode.P,       KeyCode.A,
        KeyCode.S,        KeyCode.D,      KeyCode.F,       KeyCode.J,    
        KeyCode.Z,       KeyCode.X,     KeyCode.C,     KeyCode.V,
       KeyCode.B,      KeyCode.N,     KeyCode.M,
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
            FinishTask();

        }
    }

    void FinishTask()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<TEST_Interact>().objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
                player.GetComponentInParent<TEST_Movement>().enabled = true;
            }
        }
    }
}
