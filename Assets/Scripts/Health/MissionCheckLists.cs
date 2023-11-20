using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionCheckLists : MonoBehaviour
{
    public TextMeshProUGUI objective1;
    public TextMeshProUGUI objective2;
    public TextMeshProUGUI objective3;

    private bool objective1Completed;
    private bool objective2Completed;
    private bool objective3Completed;
    private void Start()
    {
        objective1.color = Color.red;
        objective2.color = Color.red;
        objective3.color = Color.red;
    }
    public void MarkObjective1Completed()
    {
        objective1Completed = true;
        objective1.text = "- You've Killed all the enemies";
        objective1.color = Color.green;
    }

    public void MarkObjective2Completed()
    {
        objective2Completed = true;
        objective2.text = "- Collected all the upgrades";
        objective2.color = Color.green;
    }

    public void MarkObjective3Completed()
    {
        objective3Completed = true;
        objective3.text = "- Explored the map";
        objective3.color = Color.green;
    }
}
