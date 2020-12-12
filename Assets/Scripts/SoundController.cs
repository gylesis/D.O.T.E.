using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    [SerializeField]
    AudioSource townHall;

    [SerializeField]
    float radiusToHear = 30;

    private void Start() {
        townHall.Play();
    }

    private void Update() {
        if (Vector3.Distance(Castle.pos, Player.player.position) > 5) {
            if (townHall)
                townHall.volume -= 0.01f;
        }
        townHall.volume = Mathf.Clamp((radiusToHear - (Vector3.Distance(Castle.pos, Player.player.position) / 2)) / 100, 0, 0.08f);
        townHall.panStereo = -Player.direction * (Vector3.Distance(Castle.pos, Player.player.position)) / 100;
    }



}
