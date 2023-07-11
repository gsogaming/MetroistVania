using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This class is meant to be used on buttons as a quick easy way to load levels (scenes)
/// </summary>
public class LevelLoadButton : MonoBehaviour
{
    public Animator transition;

    public float transitionTime;


    /// <summary>
    /// Description:
    /// Loads a level according to the name provided
    /// Input:
    /// string levelToLoadName
    /// Return:
    /// void (no return)
    /// </summary>
    /// <param name="levelToLoadName">The name of the level to load</param>
    public void LoadLevelByName(string levelToLoadName)
    {
        StartCoroutine(LoadLevel(levelToLoadName));

        //this code is not working for the pause / gamewin/lose menus because the object 
        //it is attached to disappears before the couroutine can finish.
    }

    IEnumerator LoadLevel(string levelToLoadName)
    {
        if (transition != null)
        {
            transition.SetTrigger("Start");
        }        

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelToLoadName);

    }
}
