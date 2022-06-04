using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    public bool debugMode;

    void Start()
    {
        
    }

    void Update()
    {
        if(debugMode && InputSystem.Instance.switchButtons.Down == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
