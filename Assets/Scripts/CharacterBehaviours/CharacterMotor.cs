using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMotor : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 720f;

    [SerializeField]
    private float stoppingDistance = 1f;

    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }
    
    public IEnumerator MoveToRoutine(Transform target)
    {
        var targetCollider = target.GetComponentInChildren<Collider>();
        Func<Vector3> getClosestPoint = targetCollider != null 
            ? () => targetCollider.ClosestPoint(transform.position) 
            : () => target.position;

        yield return new WaitForFixedUpdate();
        while (target != null && Vector3.Distance(getClosestPoint(), transform.position) > stoppingDistance)
        {
            var moveDirection = getClosestPoint() - transform.position;
            moveDirection.y = 0;
            ApplyMovement(moveDirection);
            ApplyRotation(moveDirection);
            yield return new WaitForFixedUpdate();
        }
        
        while (target != null)
        {
            var lookDirection = getClosestPoint() - transform.position;
            lookDirection.y = 0;
            if (Vector3.Angle(transform.forward, lookDirection) < 1f) 
            {
                break;
            }
            ApplyRotation(lookDirection);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void ApplyMovement(Vector3 direction)
    {
        direction.Normalize();
        var targetPosition = _rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime; 
        _rigidbody.MovePosition(targetPosition);
    }

    private void ApplyRotation(Vector3 direction)
    {
        var targetRotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.fixedTime);
        _rigidbody.MoveRotation(targetRotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
