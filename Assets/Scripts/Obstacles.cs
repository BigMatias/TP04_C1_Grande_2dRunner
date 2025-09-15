using System.Collections;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UIGame UIGame;
    [Header("Obstacles")]
    [SerializeField] ObstacleData obstacleData;
    [SerializeField] GameObject saw;
    [SerializeField] GameObject spike;
    [SerializeField] GameObject bat;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject potion;

    private GameObject[] bats;
    private GameObject[] saws;
    private GameObject[] spikes;
    private Rigidbody2D batRb;
    private Rigidbody2D sawRb;
    private Rigidbody2D spikeRb;
    private Rigidbody2D platformRb;
    private Rigidbody2D potionRb;

    private float xLeftLimit = -11f;

    private void Awake()
    {
        obstacleData.obstacleSpeed = 8;

        batRb = bat.GetComponent<Rigidbody2D>();
        sawRb = saw.GetComponent<Rigidbody2D>();
        spikeRb = spike.GetComponent<Rigidbody2D>();
        platformRb = platform.GetComponent<Rigidbody2D>();
        potionRb = potion.GetComponent<Rigidbody2D>();

        saw.gameObject.SetActive(false);
        spike.gameObject.SetActive(false);
        bat.gameObject.SetActive(false);
        platform.gameObject.SetActive(false);
        potion.gameObject.SetActive(false);

        bats = new GameObject[5];
        saws = new GameObject[5];
        spikes = new GameObject[5];

        CreateExtraObstacles();
    }
    void Start()
    {
        InvokeRepeating("CreateObstacle", Random.Range(1, 5), Random.Range(1, 5));
        InvokeRepeating("CreateObstacle", Random.Range(1, 5), Random.Range(1, 5));
        InvokeRepeating("CreateObstacle", Random.Range(1, 5), Random.Range(1, 5));
        InvokeRepeating("CreatePlatformAndPotion", 15, Random.Range(20f, 35f));
    }

    private void FixedUpdate()
    {
        MoveObstacles();
    }

    private void MoveObstacles()
    {
        for (int i = 0; i <= bats.Length - 1; i++)
        {
            if (bats[i].activeSelf)
            {
                GameObject bat = bats[i];
                Rigidbody2D rb = bat.GetComponent<Rigidbody2D>();
                Vector2 newPosition = rb.position + Vector2.left * obstacleData.obstacleSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);

                if (bat.transform.position.x <= xLeftLimit)
                {
                    bat.SetActive(false);
                }
            }
        }
        for (int i = 0; i <= saws.Length - 1; i++)
        {
            if (saws[i].activeSelf)
            {
                GameObject saw = saws[i];
                Rigidbody2D rb = saw.GetComponent<Rigidbody2D>();
                Vector2 newPosition = rb.position + Vector2.left * obstacleData.obstacleSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);

                if (saw.transform.position.x <= xLeftLimit)
                {
                    saw.SetActive(false);
                }
            }
        }
        for (int i = 0; i <= spikes.Length - 1; i++)
        {
            if (spikes[i].activeSelf)
            {
                GameObject spike = spikes[i];
                Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
                Vector2 newPosition = rb.position + Vector2.left * obstacleData.obstacleSpeed * Time.fixedDeltaTime;
                rb.MovePosition(newPosition);

                if (spike.transform.position.x <= xLeftLimit)
                {
                    spike.SetActive(false);
                }
            }
        }

        if (platform.activeSelf)
        {
            Vector2 newPositionPlat = platformRb.position + Vector2.left * obstacleData.obstacleSpeed * Time.fixedDeltaTime;
            Vector2 newPositionPot = potionRb.position + Vector2.left * obstacleData.obstacleSpeed * Time.fixedDeltaTime;

            platformRb.MovePosition(newPositionPlat);
            potionRb.MovePosition(newPositionPot);

            if (platform.transform.position.x <= xLeftLimit)
            {
                platform.gameObject.SetActive(false);
            }
            if (potion.transform.position.x <= xLeftLimit)
            {
                potion.gameObject.SetActive(false);
            }

        }
    }

    private void CreateExtraObstacles()
    {
        bats[0] = bat;
        saws[0] = saw;
        spikes[0] = spike;

        for (int i = 1; i <= bats.Length - 1; i++)
        {
            GameObject newGameObject = Instantiate(bat);
            newGameObject.name = "bat" + i;
            bats[i] = newGameObject;
        }
        for (int i = 1; i <= saws.Length - 1; i++)
        {
            GameObject newGameObject = Instantiate(saw);
            newGameObject.name = "saw" + i;
            saws[i] = newGameObject;
        }
        for (int i = 1; i <= spikes.Length - 1; i++)
        {
            GameObject newGameObject = Instantiate(spike);
            newGameObject.name = "spike" + i;
            spikes[i] = newGameObject;
        }
    }

    private void CreateObstacle()
    {
        if (UIGame.gameStarted == true)
        {
            float choice = Random.Range(0f, 3f);
            if (choice >= 0 && choice <= 1)
            {
                if (saw.activeSelf == false)
                {
                    saw.gameObject.SetActive(true);
                    saw.transform.position = new Vector3(13.17f, -4.04f, 0);

                }
                else
                {
                    CheckForExistingObstacles(saws, sawRb);
                }
            }
            if (choice >= 1.1 && choice <= 2)
            {
                if (spike.activeSelf == false)
                {
                    spike.gameObject.SetActive(true);
                    spike.transform.position = new Vector3(13.17f, -3.2f, 0);
                }
                else
                {
                    CheckForExistingObstacles(spikes, spikeRb);
                }
            }
            if (choice >= 2.1 && choice <= 3)
            {
                if (bat.activeSelf == false)
                {
                    float random = Random.Range(0f, 3f);
                    float ySpawnPos;
                    if (random >= 0 || random <= 0.9)
                    {
                        ySpawnPos = -1.91f;
                    }
                    else if (random >= 1 || random <= 1.9)
                    {
                        ySpawnPos = -3.03f;
                    }
                    else
                    {
                        ySpawnPos = 1.36f;
                    }
                    bat.gameObject.SetActive(true);
                    bat.transform.position = new Vector3(13.17f, ySpawnPos, 0);
                }
                else
                {
                    CheckForExistingObstacles(bats, batRb);
                }

            }
        }
    }

    private void CheckForExistingObstacles(GameObject[] gameObjectArr, Rigidbody2D rb)
    {
        for (int i = 0; gameObjectArr.Length >= i; i++)
        {
            if (gameObjectArr[i].activeSelf == false)
            {
                gameObjectArr[i].SetActive(true);
                break;
            }
        }
    }

    private void CreatePlatformAndPotion()
    {
        if (UIGame.gameStarted)
        {
            float spawnLocPlatX = 13.16f;
            float spawnLocPlatY = -1.57f;

            float spawnLocPotX = Random.Range(13.15f, 18.06f);
            float spawnLocPotY = Random.Range(2.33f, -0.3f);

            platform.gameObject.SetActive(true);
            potion.gameObject.SetActive(true);

            platform.transform.position = new Vector3(spawnLocPlatX, spawnLocPlatY);
            potion.transform.position = new Vector3(spawnLocPotX, spawnLocPotY);
        }
    }

    public void IncreaseSpeedOverTime()
    {
        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator IncreaseSpeed()
    {
        while (UIGame.gameStarted)
        {
            yield return new WaitForSeconds(10);

            obstacleData.obstacleSpeed += 1;
        }
    }


}
