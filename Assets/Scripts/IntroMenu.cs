using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    [SerializeField]
    GameObject text;

    [SerializeField]
    float speedLerp;

    [SerializeField]
    GameObject pressE;

    [SerializeField]
    float pressSignRevealing = 5f;

    bool isSkippable = false;

    [SerializeField]
    AudioSource theme;


    private void Start() {
        StartCoroutine(StartGame());
        theme.Play();
    }

    private void Update() {
        if (Input.anyKeyDown && isSkippable) {
            SceneManager.LoadScene(1);
        }
        text.transform.position = Vector3.Lerp(text.transform.position, new Vector3(text.transform.position.x, text.transform.position.y + 30, text.transform.position.z), speedLerp);
    }


    IEnumerator StartGame() {
        yield return new WaitForSeconds(pressSignRevealing);
        isSkippable = true;
        pressE.SetActive(true);
    }

}
