using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Panel", menuName = "Panel")]
public class PanelData :ScriptableObject
{
    public GameObject PanelA;
    public GameObject PanelB;
    public GameObject PanelVSA;
    public GameObject PanelVSB;
}
