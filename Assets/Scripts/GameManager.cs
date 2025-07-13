using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string gameOverSceneName = "S_GameOver";
    public string nextLevelSceneName = "S_BossLevel";

    private PlayerController player;
    private EnemyBehavior[] enemies;

    void Start()
    {
        player = Object.FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        // Controlla se il player è stato distrutto
        if (player == null)
        {
            SceneManager.LoadScene(gameOverSceneName);
            return;
        }

        // Controlla se ci sono nemici attivi
        enemies = Object.FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
        if (enemies.Length == 0)
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
    }
}