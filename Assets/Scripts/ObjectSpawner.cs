using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject currentObjectToSpawn;
    [SerializeField] ISpawnableButton currentPressedButton;
    [SerializeField] LayerMask detectionLayers;
    #region Fields And Variables
    #endregion
    #region Unity Methods
    // Update is called once per frame
    void Update()
    {
        if (currentObjectToSpawn != null)
        {
            RayCastHandle();
        }
    }

  
    #endregion
    #region Custom Methods
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
        }
    }
  private void RayCastHandle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast to detect floor using the detectionLayers
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, detectionLayers))
        {
            // Move currentObjectToSpawn to the hit point using DOTween
            currentObjectToSpawn.transform.DOMove(hit.point, 0.1f); 

        
            if (Input.GetMouseButtonDown(0))
            {
                currentObjectToSpawn.GetComponent<ISpawnable>()?.OnObjectSpawned();
                if (currentPressedButton != null)
                {
                    currentPressedButton.OnClickToSpawn();
 
                }
                   currentObjectToSpawn = null;
                   currentPressedButton = null;
            }
        }
    }
#endregion
}
