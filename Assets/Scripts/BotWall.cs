using UnityEngine;
using UnityEngine.SceneManagement;

public class BotWall : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckRow(collision);
        CheckPlayer(collision);
    }

    void CheckRow(Collider2D collision)
    {
        ObstacleRow row = collision.GetComponentInParent<ObstacleRow>();
        if (row != null)
        {
            Destroy(row.gameObject);
        }
    }

    void CheckPlayer(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            SceneManager.LoadScene(0);
            PlayerPrefs.SetFloat("Record", levelManager.recordTimeAlive);
        }
    }
}
