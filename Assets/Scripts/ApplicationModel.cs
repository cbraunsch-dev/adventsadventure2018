using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationModel {
    public static StartScreenOption selectedOptionInStartScreen = StartScreenOption.newGame;
}

public enum StartScreenOption {
    newGame,
    loadGame
}
