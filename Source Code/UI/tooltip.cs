using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tooltip : MonoBehaviour
{
    public Text text;
    public GameObject tool_tip;
    // By JB2051
    IEnumerator ShowTooltip(float time, string message)//time and message of tooltip
    {
        text.text = message;
        yield return new WaitForSeconds(time);
        tool_tip.SetActive(false); //make it disappear after timer
    }

    public void showTooltip(string message, float time)
    {
        tool_tip.SetActive(true);
        StartCoroutine(ShowTooltip(time, message));
    }
}
