using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDoor : Interactable
{
    public override bool OnInteractBegin()
    {
        if (LevelChecklistManager.Instance.ListCompleted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneController.Instance.ReloadScene();
            return true;
        }
        else
        {
            Debug.Log("Not all events completed");
            return false;
        }
    }
}
