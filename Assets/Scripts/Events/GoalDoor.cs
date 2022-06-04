using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDoor : ChecklistEvent
{
    public override void CompleteEvent()
    {
        if (LevelChecklistManager.Instance.ListCompleted)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SceneController.Instance.ReloadScene();
        }
        else
        {
            Debug.Log("Not all events completed");
        }
    }
}
