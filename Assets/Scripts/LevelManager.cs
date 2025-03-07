using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Transform startSpawnTransform;
    [SerializeField] ObstacleRow obstacleRowPrefab;
    [SerializeField] float speed = 1f;
    [SerializeField] float emptyChance = 0.25f;
    [SerializeField] Vector2Int distanceToSpawnIntervalRange = new Vector2Int(2, 3);
    [SerializeField] float phaseTokenSpawnChance = 0.02f;
    [SerializeField] float bigPhaseTokenSpawnChance = 0.2f;
    ObstacleRow topObstacleRow;
    int randomDistanceToSpawnRow;

    public float timeAlive;
    public float recordTimeAlive;

    private void Awake()
    {
        recordTimeAlive = PlayerPrefs.GetFloat("Record");
    }

    private void Start()
    {
        SpawnRow();
        randomDistanceToSpawnRow = Random.Range(distanceToSpawnIntervalRange.x, distanceToSpawnIntervalRange.y + 1);
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive > recordTimeAlive) recordTimeAlive = timeAlive;

        if (topObstacleRow == null) return;
        if(startSpawnTransform.position.y - topObstacleRow.transform.position.y > randomDistanceToSpawnRow)
        {
            SpawnRow();
            randomDistanceToSpawnRow = Random.Range(distanceToSpawnIntervalRange.x, distanceToSpawnIntervalRange.y + 1);
        }
    }

    void SpawnRow()
    {
        topObstacleRow = Instantiate(obstacleRowPrefab, startSpawnTransform.position, Quaternion.identity, transform);
        topObstacleRow.Init(Mathf.RoundToInt(topObstacleRow.totalObstacles * emptyChance), speed, phaseTokenSpawnChance, bigPhaseTokenSpawnChance);
    }
}
