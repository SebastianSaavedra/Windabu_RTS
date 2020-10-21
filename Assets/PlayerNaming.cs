using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Com.MaluCompany.TestGame;
using Photon.Pun.UtilityScripts;

public class PlayerNaming : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI playerId;
    [SerializeField] GameManager gameManager;
    private TEST_Movement target;

    public void SetTarget(TEST_Movement _target)
    {
        if (_target == null)
        {
            return;
        }
        target = _target;
        if (playerName != null)
        {
            playerName.text = target.photonView.Owner.NickName;
            playerId.text = "" + target.photonView.OwnerActorNr;
        }
    }
}
