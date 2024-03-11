using UnityEngine;

public class CubeBehaviour : MonoBehaviour, ISpawnable, IEatable
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
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    #endregion
}
