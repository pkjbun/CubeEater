using UnityEngine;

public interface ISpawnableButton
{
    public void OnClickToSpawn();
    public GameObject GetObjectToSpawn();
}
