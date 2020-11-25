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
    private int bgCounter = 0;
    private bool leftActive;

    public Text uiMashCounter;
    public GameObject p1, p1z, p1x, p2, p2z, p2x;
    public GameObject[] screens;
    int screenCounter;
    [SerializeField] ManagerMinijuegos managerMinijuegos;

    private void Start()
    {
        uiMashCounter = GetComponent<Text>();
        leftActive = true;
        managerMinijuegos = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && leftActive)
        {
            leftActive = false;
            mashScore++;
            uiMashCounter.text = mashScore.ToString("0");

            // UI
            p1.SetActive(false);
            p1z.SetActive(true);
            p1x.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.X) && !leftActive)
        {
            leftActive = true;
            mashScore++;
            uiMashCounter.text = mashScore.ToString("0");

            #region UI
            p1z.SetActive(false);
            p1x.SetActive(true);

            if (screenCounter >= screens.Length - 1)
            {
                screenCounter = 0;
            }
            else
            {
                screens[screenCounter].gameObject.SetActive(false);
                screenCounter += 1;
                screens[screenCounter].gameObject.SetActive(true);
            }
            #endregion
        }

        // Win con
        if (mashScore >= mashLimit)
        {
            // UI
            p1.SetActive(true);
            p1z.SetActive(false);
            p1x.SetActive(false);

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
                Debug.Log("Completado");
                player.GetComponentInParent<TEST_Interact>().thisTask.RPCdata();
            }
        }
    }
}
