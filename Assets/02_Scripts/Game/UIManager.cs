using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI moneyText2;
    void Update()
    {
        moneyText.text = MinigameManager.dinero.ToString();
        moneyText2.text = MinigameManager.dinero.ToString();
    }
}
