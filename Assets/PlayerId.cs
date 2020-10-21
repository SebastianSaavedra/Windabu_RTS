using Com.MaluCompany.TestGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerId : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public int id;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.players.Add(gameObject);
        id = gameManager.players.IndexOf(gameObject) +1;
    }
}
