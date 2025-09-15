using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIGame UIGame;
    [SerializeField] private FloorData FloorData;
    [SerializeField] private GameObject leftBorder;

    [SerializeField] private GameObject[] floorTiles;

    private float tileWidth;
    private float leftBorderPos = -8.902f;

    private void Awake()
    {
        tileWidth = floorTiles[0].GetComponent<BoxCollider2D>().size.x * floorTiles[0].transform.localScale.x;
    }

    private void Start()
    {
        FloorData.floorSpeed = 8;

        for (int i = 0; i < floorTiles.Length; i++)
        {
            float xPos = tileWidth * i;
            floorTiles[i].transform.position = new Vector3(xPos, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (UIGame.gameStarted)
        {
            MoveFloor();
        }
    }

    private void MoveFloor()
    {
        for (int i = 0; i < floorTiles.Length; i++)
        {
            Rigidbody2D rb = floorTiles[i].GetComponent<Rigidbody2D>();

            Vector2 newPosition = rb.position + Vector2.left * FloorData.floorSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            if (floorTiles[i].transform.position.x <= leftBorderPos)
            {
                float newXPos = 25f;
                rb.MovePosition(new Vector2(newXPos, 0));
            }
        }
    }

    public void IncreaseSpeedOverTime()
    {
        StartCoroutine(IncreaseSpeed());
    }
    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            FloorData.floorSpeed += 1;
        }
    }
}