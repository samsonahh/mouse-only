using System.Collections.Generic;
using UnityEngine;

public class ObstacleRow : MonoBehaviour
{
    [SerializeField] GameObject middleObstacle;
    [SerializeField] int obstaclesOnEachSide = 5;
    [SerializeField] PhaseToken phaseTokenPrefab;
    [SerializeField] PhaseToken bigPhaseTokenPrefab;
    public int totalObstacles => obstaclesOnEachSide * 2 + 1;
    List<GameObject> obstacles = new List<GameObject>();

    float speed;
    float phaseTokenSpawnChance;
    float bigPhaseTokenSpawnChance;

    public void Init(int emptyCount, float speed, float phaseTokenSpawnChance, float bigPhaseTokenSpawnChance)
    {
        this.speed = speed;
        this.phaseTokenSpawnChance = phaseTokenSpawnChance;
        this.bigPhaseTokenSpawnChance = bigPhaseTokenSpawnChance;

        for (int i = 0; i < obstaclesOnEachSide; i++)
        {
            var right = Instantiate(middleObstacle, middleObstacle.transform.position + (i + 1) * Vector3.right, Quaternion.identity, transform);
            var left = Instantiate(middleObstacle, middleObstacle.transform.position + (i + 1) * Vector3.left, Quaternion.identity, transform);

            obstacles.Add(right);
            obstacles.Add(left);

            if (i == obstaclesOnEachSide - 1) continue;
            if(Random.Range(0f, 1f) < phaseTokenSpawnChance)
            {
                Vector3 spawnPosition = Vector3.up + middleObstacle.transform.position + (i + 1) * (Random.Range(0, 2) == 0 ? Vector3.right : Vector3.left);
                if(Random.Range(0f, 1f) < bigPhaseTokenSpawnChance)
                {
                    Instantiate(bigPhaseTokenPrefab, spawnPosition, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(phaseTokenPrefab, spawnPosition, Quaternion.identity, transform);
                }
            }
        }

        if (emptyCount > totalObstacles) emptyCount = totalObstacles;
        for(int i = 0; i < emptyCount; i++)
        {
            int randomIndex = Random.Range(0, obstacles.Count);
            Destroy(obstacles[randomIndex]);
            obstacles.RemoveAt(randomIndex);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(totalObstacles, 1, 0));
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime * Vector3.down);
    }
}
