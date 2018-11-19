using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerBehavior : MonoBehaviour {

    public void TryToMove() {
        SceneManager.LoadScene(SceneNames.Movement);    
    }
}
