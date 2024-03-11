using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEater : MonoBehaviour, ISpawnable
{

    #region Fields And Variables
    private GameObject targetCube = null;
    [SerializeField] private float minDistance;
    [SerializeField] private bool spawned=false;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!spawned) return;
        FindAndMoveToward();
    }
    #endregion
    #region Custom Methods
    /// <summary>
    /// Finds Nearest Cube and moves toward it
    /// </summary>
  private void FindAndMoveToward()
    {
        if (targetCube == null)
        {
            targetCube = FindNearestCube();
            if (targetCube != null)
            {
                transform.DOMove(targetCube.transform.position, minDistance/speed).SetEase(Ease.Linear);
                Vector3 directionToTarget = targetCube.transform.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
                transform.DORotateQuaternion(lookRotation, 0.5f);
            }
        }
    }
    public void OnObjectSpawned()
    {   spawned = true;
        gameObject.transform.position += new Vector3(0, 0.1f, 0);
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if(rb == null ) rb= gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.angularVelocity = Vector3.zero;
    }
    /// <summary>
    /// Function to Find Closest Cube
    /// </summary>
    /// <returns></returns>
    GameObject FindNearestCube()
    {
        var cubes = ObjectSpawner.Spawns(); // Get the current list of cubes
        GameObject nearestCube = null;
        minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var cube in cubes)
        {
            float distance = Vector3.Distance(cube.transform.position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCube = cube;
            }
        }
        return nearestCube;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == targetCube)
        {
            collision.gameObject.GetComponent<ISpawnable>()?.DestroyObject(); // Destroy the cube
            targetCube = null; // Clear the current target cube
            if(rb==null) rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    #endregion
}
