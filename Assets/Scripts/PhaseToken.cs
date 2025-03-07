using UnityEngine;

public class PhaseToken : MonoBehaviour
{
    [SerializeField] int phaseTokenValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            player.AddPhaseToken(phaseTokenValue);
            Destroy(gameObject);
        }
    }
}
