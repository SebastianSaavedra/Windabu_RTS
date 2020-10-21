using Com.MaluCompany.TestGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerId : MonoBehaviourPunCallbacks
{
    public int id;
    [SerializeField] Renderer playerRenderer;
    [SerializeField] Color[] colors;
    private void Start()
    {
        id = photonView.OwnerActorNr;
        MaterialAssign(id);
    }

    void MaterialAssign(int value)
    {
        switch (value)
        {
            case 1:
                playerRenderer.material.SetColor("_OutlineColor", colors[1]);
                break;
            case 2:
                playerRenderer.material.SetColor("_OutlineColor", colors[2]);
                break;
            case 3:
                playerRenderer.material.SetColor("_OutlineColor", colors[3]);
                break;
            case 4:
                playerRenderer.material.SetColor("_OutlineColor", colors[4]);
                break;
            case 5:
                playerRenderer.material.SetColor("_OutlineColor", colors[5]);
                break;
            case 6:
                playerRenderer.material.SetColor("_OutlineColor", colors[6]);
                break;

        }
    }
}
