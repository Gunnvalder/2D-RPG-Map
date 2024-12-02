using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void OnClick()
    {
        Application.Quit();
        Debug.Log("Exit button pressed");
    }
}
