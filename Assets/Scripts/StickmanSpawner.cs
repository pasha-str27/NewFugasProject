using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> stickmanPrefabs;
    [SerializeField] BoxCollider spawnZone;

    [SerializeField] int minStartStickmanCount = 5;
    [SerializeField] int maxStartStickmanCount = 15;

    [SerializeField] int maxStickmanCountOnScreen = 25;

    [SerializeField] float minTimeDelayForSpawn = 0.5f;
    [SerializeField] float maxTimeDelayForSpawn = 2f;

    public static int stickmansCount;


    private void Start()
    {
        stickmansCount = 0;

        int stickmanCount = Random.Range(minStartStickmanCount, maxStartStickmanCount);

        for (int i = 0; i < stickmanCount; ++i)
            SpawnStickManOnRandomPosition();

        StartCoroutine(SpawnIEnumerator());
    }

    IEnumerator SpawnIEnumerator()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeDelayForSpawn, maxTimeDelayForSpawn));

            if (stickmansCount < maxStickmanCountOnScreen) 
                SpawnStickManOnRandomPosition();
        }
    }

    void SpawnStickManOnRandomPosition()
    {
        var xPos = Random.Range(-spawnZone.size.x / 2 + transform.position.x, spawnZone.size.x / 2 + transform.position.x);
        var yPos = Random.Range(-spawnZone.size.y / 2 + transform.position.y, spawnZone.size.y / 2 + transform.position.y);
        var pos = new Vector2(xPos, yPos);

        Instantiate(stickmanPrefabs[Random.Range(0, stickmanPrefabs.Count)], pos, Quaternion.identity);
    }
}
