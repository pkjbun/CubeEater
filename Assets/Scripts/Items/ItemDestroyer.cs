
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {   if (other == null) return;
        other.attachedRigidbody.gameObject.GetComponent<ISpawnable>()?.DestroyObject();
    }
}
