using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEater : MonoBehaviour, ISpawnable
{

    #region Fields And Variables
    [SerializeField] private GameObject targetCube = null;
    [SerializeField] private float minDistance;
    [SerializeField] private bool spawned=false;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isMoving;
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
        if (targetCube != null)
        {   isMoving = true;
            MoveTowardTarget();
        }
        else
        {
            targetCube= FindNearestCube();
        }
    }
    #endregion
    #region Custom Methods
    /// <summary>
    /// MoveToward targetCube
    /// </summary>
    private void MoveTowardTarget()
    {
        if (!isMoving) return;
        if (targetCube == null) return;
        Vector3 directionToTarget = targetCube.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

        // Move towards the target cube
        Vector3 moveDirection = directionToTarget.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

       
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
            isMoving = false;
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    #endregion
}
