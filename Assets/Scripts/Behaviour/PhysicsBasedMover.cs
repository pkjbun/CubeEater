using UnityEngine;
[System.Serializable]
public class PhysicsBasedMover
{
    private readonly Rigidbody rb;
    private readonly float speed;
    private readonly Transform transform;
  
    public PhysicsBasedMover(Rigidbody rb,  Transform transform,float speed)
    {
        Debug.Assert(rb != null, "Rigidbody is null.");
        Debug.Assert(transform != null, "Transform is null.");
        this.rb = rb;
        this.speed = speed;
        this.transform = transform;
    }

  
    public void MoveTowards(Vector3 targetPosition)
    {
        if (rb == null || transform == null)
        {
            Debug.LogError("Rigidbody or Transform is null in PhysicsBasedMover.");
            return;
        }
        Vector3 directionToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

        Vector3 moveDirection = directionToTarget.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }


    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}