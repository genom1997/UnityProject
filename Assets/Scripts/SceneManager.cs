using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the changing of scenes.
/// </summary>
public class SceneManager : MonoBehaviour {
    /// <summary>
    /// Load the "Character creator" scene if found in the scenes in the build settings
    /// </summary>
    public void LoadScene1() {
        Application.LoadLevel("Character creator");
    }
    /// <summary>
    /// Load the "World" scene if found in the scenes in the build settings
    /// </summary>
    public void LoadScene2() {
        Application.LoadLevel("World");
    }
}
