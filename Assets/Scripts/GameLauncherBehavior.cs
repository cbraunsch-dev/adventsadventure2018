using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLauncherBehavior : MonoBehaviour {

    public void StartNewGame() {
        ApplicationModel.selectedOptionInStartScreen = StartScreenOption.newGame;
        SceneManager.LoadScene(SceneNames.GameBoard);
    }

    public void LoadSavedGame() {
        ApplicationModel.selectedOptionInStartScreen = StartScreenOption.loadGame;
        SceneManager.LoadScene(SceneNames.GameBoard);
    }

    public void StartTutorial() {
        ApplicationModel.selectedOptionInStartScreen = StartScreenOption.tutorial;
        SceneManager.LoadScene(SceneNames.TutorialBoard);
    }
}
