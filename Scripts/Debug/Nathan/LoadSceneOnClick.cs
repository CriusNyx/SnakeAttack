using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene (sceneIndex);
    }
    /*This code was pulled from Unity's "Creating a Main Menu" Video Tutorial
     * Author: Unity Team
     * Date: September 5th, 2016
     * Availability: https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
    */
}
