using UnityEngine;

public class EntitySpawner : MonoBehaviour {

    private GameManager gameManager;
    private GameConfig config;
    [SerializeField]
    private GameObject wordPrefab;
    [SerializeField]
    private GameObject wordPowerupPrefab;
    [SerializeField]
    private float verticalSpawnOffset;
    [SerializeField]
    private float horizontalSpawnOffset;
    [SerializeField]
    private float zSpawnOffset;
    private RandomWordGenerator wordGenerator;
    public float powerupChance = 1f;

    private void Start() {
        wordGenerator = GetComponent<RandomWordGenerator>();
        gameManager = GameManager.Instance;
        config = gameManager.config;
        powerupChance = PlayerPrefs.GetFloat("PowerupChance", config.powerupChance);
        Debug.Log(powerupChance);
    }

    public WordController SpawnWordGameObject(DifficultyLevel difficulty) {
        // Generate random word
        Word word = wordGenerator.generateWord(difficulty);
        WordController controller = null;
        // Instantiate word GO

        int randomNumber = Random.Range(0, 10);
        //Debug.Log(randomNumber);
        if (randomNumber < powerupChance)
        {
            controller = Instantiate(wordPowerupPrefab, GenerateSpawnPosition(difficulty), Quaternion.identity).GetComponent<WordController>();
        }
        else
        {
            controller = Instantiate(wordPrefab, GenerateSpawnPosition(difficulty), Quaternion.identity).GetComponent<WordController>();
        }
        controller.SetWord(word);
        gameManager.AddWordController(controller);
        return controller;
    }

    /// <summary>
    /// Generates a random position, y coordinate slightly above screen
    /// and x coordinate between camera bounds
    /// Hard words will always spawn in the center
    /// </summary>
    private Vector3 GenerateSpawnPosition(DifficultyLevel difficulty) {
        Camera cam = Camera.main;

        Vector3 topLeftCorner = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.nearClipPlane));
        Vector3 topRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
        float middlePoint = (topLeftCorner.x + topRightCorner.x) / 2;
        float randomOffset = Random.Range(topRightCorner.x - horizontalSpawnOffset, topLeftCorner.x + horizontalSpawnOffset);

        float xCoord = difficulty != DifficultyLevel.Hard ?
            randomOffset
            :
            middlePoint + randomOffset;

        float yCoord = topRightCorner.y + verticalSpawnOffset;

        return new Vector3(xCoord, yCoord, zSpawnOffset);
    }

}
