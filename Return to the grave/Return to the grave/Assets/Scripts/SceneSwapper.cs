using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync(1);
                Debug.Log(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync(0);
                Debug.Log(SceneManager.GetActiveScene().name);
            }
        }
    }
    public void ClickStart()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ClickQuit()
    {
        Application.Quit();
        Debug.Log("Out");
    }
}
