using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour {
    public static GameLogic Instance;

    [SerializeField]
    public GameObject victorySign;

    [SerializeField]
    public GameObject expolionPaticles;

    private void Start() {
        Instance = this;
    }

    public static void GameOver() {
        SceneManager.LoadScene(0);
    }

    public void Victory() {
        victorySign.SetActive(true);
    }


}
