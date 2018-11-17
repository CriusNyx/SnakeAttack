using System.Collections;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();


#endif
    }
    /*This code was pulled from Unity's "Creating a Main Menu" Video Tutorial
     * Author: Unity Team
     * Date: September 5th, 2016
     * Availability: https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
    */
}
