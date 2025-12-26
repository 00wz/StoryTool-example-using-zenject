using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float rotationSpeed = 180f;

    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
    }

    void FixedUpdate()
    {
        var inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            inputDirection.Normalize();
            var targetPosition = _rigidbody.position + inputDirection * moveSpeed * Time.deltaTime; 
            var targetRotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(inputDirection), rotationSpeed * Time.deltaTime);
            _rigidbody.Move(targetPosition, targetRotation);
        }

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
