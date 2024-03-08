using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectSpawner : MonoBehaviour
{

    
    #region Fields And Variables
    [SerializeField] GameObject currentObjectToSpawn;
    [SerializeField] ISpawnableButton currentPressedButton;
    [SerializeField] LayerMask detectionLayers;
    private static List<GameObject> spawns = new List<GameObject>();
    [Inject] UIListOfObjectsManager _uiListOfObjectsManager;
    #endregion
    #region Unity Methods
    // Update is called once per frame
    void Update()
    {
        if (currentObjectToSpawn != null)
        {
            RayCastHandle();
        }
        if(Input.GetKeyDown(KeyCode.Escape)) { 
            CancelSpawning();
        }
    }
    #endregion
    #region Custom Methods
    /// <summary>
    /// Cancel Spawn Process
    /// </summary>
    public void CancelSpawning()
    {
        _uiListOfObjectsManager.OnSpawnFinished();
        Destroy(currentObjectToSpawn);
            currentObjectToSpawn = null;
            currentPressedButton = null;
    }
    public static List<GameObject> Spawns()
    {
        spawns.RemoveAll(item => item == null);
        return spawns;
    }
    /// <summary>
    /// Called to Start Procedure of SpawningByButton
    /// </summary>
    /// <param name="button"></param>
    public void SpawnStart(ISpawnableButton button)
    {
        currentPressedButton = button;
        if (button.GetObjectToSpawn() != null)
        {
            currentObjectToSpawn= Instantiate(button.GetObjectToSpawn());
            currentObjectToSpawn.SetActive(false);
        }
    }
  private void RayCastHandle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast to detect floor using the detectionLayers
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectionLayers))
        {
            // Move currentObjectToSpawn to the hit point with slerp
            currentObjectToSpawn.transform.position =Vector3.Slerp(currentObjectToSpawn.transform.position, hit.point, 0.8f);
            currentObjectToSpawn.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                currentObjectToSpawn.GetComponent<ISpawnable>()?.OnObjectSpawned();
                if(currentObjectToSpawn.GetComponent<IEatable>() != null) { spawns.Add(currentObjectToSpawn); }
                if (currentPressedButton != null)
                {
                    _uiListOfObjectsManager.OnSpawnFinished();
                    currentPressedButton.OnClickToSpawn();
 
                }
                   currentObjectToSpawn = null;
                   currentPressedButton = null;
            }
        }
    }
#endregion
}
