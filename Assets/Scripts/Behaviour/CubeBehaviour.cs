using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour, ISpawnable
{
    
    #region Fields And Variables
    #endregion
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion
#region Custom Methods
public void OnObjectSpawned()
    {
        gameObject.transform.position += new Vector3(0, 0.1f, 0);
        Rigidbody rb = gameObject.GetOrAddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }
#endregion
}
