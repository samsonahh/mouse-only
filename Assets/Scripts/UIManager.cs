using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player player;
    [SerializeField] LevelManager levelManager;

    [Header("Texts")]
    [SerializeField] TMP_Text phaseTokensText;
    [SerializeField] TMP_Text timeAliveText;
    [SerializeField] TMP_Text recordText;

    private void Update()
    {
        phaseTokensText.text = $"Phase Tokens - {player.phaseThroughTokens}";
        timeAliveText.text = $"Time Alive - {GetFormattedFloatTimer(levelManager.timeAlive)}";
        recordText.text = $"Record - {GetFormattedFloatTimer(levelManager.recordTimeAlive)}";
    }

    string GetFormattedFloatTimer(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
