using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Com.MaluCompany.TestGame;

public class PlayerNaming : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    private TEST_Movement target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        }
    }
}
