using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public void MoveToSetting() { SceneManager.LoadScene("Setting"); }

    public void MoveToGame() { SceneManager.LoadScene("Game"); }

    public void MoveToGameStart() { SceneManager.LoadScene("GameStart"); }

    public void MoveToTutorial() { SceneManager.LoadScene("Tutorial"); }
}
