using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
    [SerializeField] private ParallaxData ParallaxData;
    [SerializeField] private UIGame UIGame;
    [SerializeField] private GameManager GameManager;

    private float size = 0;

    private void Start()
    {
        ParallaxData.currentL1Speed = ParallaxData.initialL1Speed;
        ParallaxData.currentL3Speed = ParallaxData.initialL3Speed;
        ParallaxData.currentFloorSpeed = ParallaxData.initialFloorSpeed;

        size = spriteRenderers[0].bounds.size.x;
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Vector3 pos = new Vector3(size * i, spriteRenderers[i].transform.position.y, 0);
            spriteRenderers[i].transform.position = pos;
        }

    }

    private void Update()
    {
        if (UIGame.gameStarted && !GameManager.gamePaused)
        {
            MoveBackground();
        }

        FixBackgroundPosition();
    }



    private void MoveBackground()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            Vector3 pos = spriteRenderers[i].transform.position;
            if (gameObject.tag == "Layer1" || gameObject.tag == "Layer2")
            {
                pos.x -= ParallaxData.currentL1Speed;
            }
            else if (gameObject.tag == "Layer3")
            {
                pos.x -= ParallaxData.currentL3Speed;
            }
            else
            {
                pos.x -= ParallaxData.currentFloorSpeed;
            }
            spriteRenderers[i].transform.position = pos;
        }
    }

    private void FixBackgroundPosition()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            if (spriteRenderers[i].transform.position.x < -size)
            {
                int prevIndex = i - 1;
                if (prevIndex < 0)
                    prevIndex = spriteRenderers.Count - 1;

                Vector3 pos = spriteRenderers[i].transform.position;
                pos.x = spriteRenderers[prevIndex].transform.position.x + size;
                spriteRenderers[i].transform.position = pos;
            }
        }
    }

}
