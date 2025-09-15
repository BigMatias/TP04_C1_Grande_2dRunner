using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data", order = 1)]
public class PlayerData : ScriptableObject
{
    public KeyCode Jump = KeyCode.Space;
    public float JumpSpeed;
}
