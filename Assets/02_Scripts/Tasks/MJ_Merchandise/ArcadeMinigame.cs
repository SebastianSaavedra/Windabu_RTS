using Com.MaluCompany.TestGame;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeMinigame : MonoBehaviourPunCallbacks
{
    public int mashLimit = 20;
    [SerializeField]int mashScore;

    private bool leftActive;

    public Text uiMashCounter;

    private void Start()
    {
        uiMashCounter = GetComponent<Text>();
        leftActive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && leftActive)
        {
            leftActive = false;
            mashScore++;
            uiMashCounter.text = mashScore.ToString("0");
        }
        if (Input.GetKeyDown(KeyCode.X) && !leftActive)
        {
            leftActive = true;
            mashScore++;
            uiMashCounter.text = mashScore.ToString("0");
        }

            // Win con
            if (mashScore >= mashLimit)
        {
            Debug.Log("Win");
            mashScore = 0;
            uiMashCounter.text = mashScore.ToString("0");
            gameObject.SetActive(false);
            FinishTask();

        }
    }

    public void FinishTask()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<TEST_Movement>().enabled = true;
                if (player.GetComponentInParent<PlayerTeam>().TeamA)
                {
                    player.GetComponentInParent<TEST_Interact>().speakingTo.Team1();
                }
                else if (player.GetComponentInParent<PlayerTeam>().TeamB)
                {
                    player.GetComponentInParent<TEST_Interact>().speakingTo.Team2();
                }
                player.GetComponentInParent<TEST_Interact>().objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(player.GetComponentInParent<PlayerTeam>().team);
                player.GetComponentInParent<TEST_Interact>().thisTask.RPCdata();
            }
        }
    }
}
