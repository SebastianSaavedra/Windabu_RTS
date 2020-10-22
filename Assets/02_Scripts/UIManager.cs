using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    void Update()
    {
        moneyText.text = MinigameManager.dinero.ToString();
    }
}
