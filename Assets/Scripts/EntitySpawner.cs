using UnityEngine;

public class EntitySpawner : MonoBehaviour {

    private GameManager gameManager;
    [SerializeField]
    private GameObject wordPrefab;
    [SerializeField]
    private float verticalSpawnOffset;
    [SerializeField]
    private float horizontalSpawnOffset;
    private RandomWordGenerator wordGenerator;

    private void Start() {
        wordGenerator = GetComponent<RandomWordGenerator>();
        gameManager = GameManager.Instance;
    }

    public WordController SpawnWordGameObject(DifficultyLevel difficulty) {
        // Generate random word
        Word word = wordGenerator.generateWord(difficulty);
        // Instantiate word GO
        WordController controller = Instantiate(wordPrefab, GenerateSpawnPosition(difficulty), Quaternion.identity).GetComponent<WordController>();
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

        float xCoord = difficulty == DifficultyLevel.Easy ?
            Random.Range(topRightCorner.x - horizontalSpawnOffset, topLeftCorner.x + horizontalSpawnOffset)
            :
            (topLeftCorner.x + topRightCorner.x) / 2;

        float yCoord = topRightCorner.y + verticalSpawnOffset;

        return new Vector3(xCoord, yCoord);
    }

}
