using UnityEngine;

public class LevelHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private int gameOverScene;

    public bool PlayGame { get; set; } = true;
    public int GameOverScene { get { return gameOverScene; } }

    public GameData p;

    void Awake()
    {
        if (LevelController.Instance.LevelObject != null && LevelController.Instance.LevelObject != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        LevelController.Instance.LinkLevelObject(this);
    }
}
