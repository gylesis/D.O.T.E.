using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance;

    [SerializeField]
    public List<GameObject> enemies = new List<GameObject>();

    public static List<GameObject> spawnedEnemies = new List<GameObject>();

    public static int killedEnemies = 0;

    [SerializeField]
    [Range(0, 3)]
    int spawnInterval;

    [SerializeField]
    [Range(0, 10)]
    int maxEnemies;


    bool canSpawn = true;

    private void Start() {
        Instance = this;
        spawnedEnemies.Clear();
    }

    private void Update() {
        if (spawnedEnemies.Count < maxEnemies && canSpawn) {
            canSpawn = false;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(spawnInterval);

        var randomIndex = Random.Range(0, 3);
        SpawnEnemy(randomIndex);
        canSpawn = true;
    }


    void SpawnEnemy(int randomIndex) {
        var side = 0;
        var a = Random.value;
        var b = Random.value;
        if (a > b) side = 1;
        else side = -1;

        var range = Random.Range(70, 90);

        spawnedEnemies.Add(Instantiate(enemies[randomIndex], Castle.pos + new Vector3(side * range, 0, 0), Quaternion.identity));

    }


}
