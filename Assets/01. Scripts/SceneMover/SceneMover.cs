using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public void MoveToSetting() { 
        AudioManager.instance.PlaySettingBGM();
        SceneManager.LoadScene("Setting"); 
    }

    public void MoveToGame() { 
        AudioManager.instance.PlayGameBGM();
        SceneManager.LoadScene("Game"); 
    }

    public void MoveToGameStart() { 
        AudioManager.instance.PlayTutorialBGM();
        SceneManager.LoadScene("GameStart"); 
    }

    public void MoveToTutorial() {
        AudioManager.instance.PlayTutorialBGM();
        SceneManager.LoadScene("Tutorial"); 
    }

    public void MoveToRealStart() {
        AudioManager.instance.PlaySettingBGM();
        SceneManager.LoadScene("RealStart"); 
    }

    public void MoveToRanking() {
        AudioManager.instance.PlayRankingBGM();
        SceneManager.LoadScene("Ranking"); 
    }

    public void MoveToStart() { 
        AudioManager.instance.PlaySettingBGM();
        SceneManager.LoadScene("Start"); 
    }
}
