using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    public Button resetButton;

    private void OnEnable()
    {
        resetButton.onClick.AddListener(() => buttonCallBack());
    }

    private void buttonCallBack()
    {
        UnityEngine.Debug.Log("Clicked:" + resetButton.name);
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        resetButton.onClick.RemoveAllListeners();
    }
}
