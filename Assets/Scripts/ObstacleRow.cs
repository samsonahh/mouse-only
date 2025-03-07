using System.Collections.Generic;
using UnityEngine;

public class ObstacleRow : MonoBehaviour
{
    [SerializeField] GameObject middleObstacle;
    [SerializeField] int obstaclesOnEachSide = 5;
    public int totalObstacles => obstaclesOnEachSide * 2 + 1;
    List<GameObject> obstacles = new List<GameObject>();

    public void Init(int emptyCount)
    {
        for (int i = 0; i < obstaclesOnEachSide; i++)
        {
            var right = Instantiate(middleObstacle, middleObstacle.transform.position + (i + 1) * Vector3.right, Quaternion.identity, transform);
            var left = Instantiate(middleObstacle, middleObstacle.transform.position + (i + 1) * Vector3.left, Quaternion.identity, transform);

            right.GetComponent<SpriteRenderer>().color = i % 2 == 0 ? Color.black : Color.white;
            left.GetComponent<SpriteRenderer>().color = i % 2 == 0 ? Color.black : Color.white;

            obstacles.Add(right);
            obstacles.Add(left);
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
}
