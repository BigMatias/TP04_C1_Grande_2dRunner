using UnityEngine;

[CreateAssetMenu(fileName = "ParallaxData", menuName = "Parallax/Data", order = 1)]

public class ParallaxData : ScriptableObject
{
    [Header("Velocidad dependiendo del Layer del Background")]
    public float currentL1Speed;
    public float initialL1Speed;
    public float currentL3Speed;
    public float initialL3Speed;
    public float currentFloorSpeed;
    public float initialFloorSpeed;
}
