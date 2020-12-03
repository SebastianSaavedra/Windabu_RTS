using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Com.MaluCompany.TestGame;

public class Wifi : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject celu,wifi, wifint;
    [SerializeField] Animator celuAnim;
    [SerializeField] GameObject player;
    [SerializeField] bool team;
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collision.GetComponent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    if (collision.GetComponent<PlayerTeam>().team == team) 
                    {
                celuAnim.SetTrigger("OpenCell");
                collision.GetComponent<TEST_Movement>().enabled = false;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (collision.GetComponent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    if (collision.GetComponent<PlayerTeam>().team == team)
                    {
                        collision.GetComponent<TEST_Movement>().enabled = true;
                celuAnim.SetTrigger("CloseCell");
                }
                }
            }
            wifint.SetActive(false);
            wifi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
               if(collision.GetComponent<PlayerId>().id== PhotonNetwork.LocalPlayer.ActorNumber) 
            {
                collision.GetComponent<TEST_Movement>().enabled = true;
            }
        }
    }
}
