using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Attack))]
public class PlayerController : MonoBehaviour
{    
    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float rotationSpeed = 720f;

    [SerializeField]
    private Vector3 attackAreaCenter = new Vector3(0f, 0f, 2f);

    [SerializeField]
    private float attackAreaRadius = 0.5f;

    private Rigidbody _rigidbody;
    private bool isAttacking = false;
    private Attack _attack;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        _attack = GetComponent<Attack>();
    } 
    
    void FixedUpdate()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            moveDirection.Normalize();
            var targetPosition = _rigidbody.position + moveDirection * moveSpeed * Time.fixedDeltaTime; 
            var targetRotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.fixedTime);
            _rigidbody.Move(targetPosition, targetRotation);
        }

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        try
        {
            var targets = Physics.OverlapSphere(transform.TransformPoint(attackAreaCenter), attackAreaRadius, ~LayerMask.GetMask("Player")) 
                .Where(collider => !collider.isTrigger)
                .Select(collider => collider.GetComponentInParent<Health>())
                .Distinct();
            yield return _attack.PerformAttackRoutine(targets.ToArray());
        }
        finally
        {
            isAttacking = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.TransformPoint(attackAreaCenter), attackAreaRadius);
    }
}
