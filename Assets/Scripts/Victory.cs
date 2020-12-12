using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {

    [SerializeField]
    private AudioListener _audioListener;

    private void Start() {
        Time.timeScale = 0f;
        _audioListener.enabled = false;

    }
}
