using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public string title; // Title of the cube
    public Sprite thumbnail; // Thumbnail image of the cube
    public GameObject cubePrefab; // Prefab of the cube object
}
