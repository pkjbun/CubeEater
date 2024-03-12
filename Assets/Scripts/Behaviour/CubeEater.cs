using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeEater : MonoBehaviour, ISpawnable
{

    #region Fields And Variables
    private GameObject targetCube = null;
    [SerializeField] private float minDistance;
    [SerializeField] private bool spawned=false;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private PhysicsBasedMover mover;
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {   if(rb==null) { rb = GetComponent<Rigidbody>(); }
        mover = new PhysicsBasedMover(rb,this.transform,speed);
    }
    // Update is called once per frame
    void Update()
    {   if (!spawned) return;
        if (targetCube != null)
        {
            mover.MoveTowards(targetCube.transform.position);
        }
        else
        {
            
            FindAndSetNearestCubeAsTarget();
        }
    }

    
    #endregion
    #region Custom Methods
    /// <summary>
    /// Use to find nearest cube and if so 
    /// </summary>
    private void FindAndSetNearestCubeAsTarget()
    {
        targetCube = FindNearestCube();
       
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
            mover.StopMovement();
            targetCube = null;
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    #endregion
}
