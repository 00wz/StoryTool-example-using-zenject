using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(Attack))]
public class EnemyController : MonoBehaviour
{
    private Attack _attack;
    private CharacterMotor _characterMotor;
    private bool isAttacking = false;

    void Awake()
    {
        _attack = GetComponent<Attack>();
        _characterMotor = GetComponent<CharacterMotor>();
        if (!TryGetComponent<Collider>(out var collider) || !collider.isTrigger)
        {
            Debug.LogWarning("EnemyController requires a Collider with isTrigger set to true.");
        }
    }

    void OnTriggerStay(Collider other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player != null && !isAttacking)
        {
            StartCoroutine(AttackPlayerRoutine(player));
        }
    }

    private IEnumerator AttackPlayerRoutine(PlayerController player)
    {
        isAttacking = true;
        try
        {
            yield return _characterMotor.MoveToRoutine(player.transform);

            if (player != null)
            {
                var playerHealth = player.GetComponentInChildren<Health>();
                if (playerHealth != null)
                {
                    yield return _attack.PerformAttackRoutine(playerHealth);
                }
            }
        }
        finally
        {
            isAttacking = false;
        }
    }
}
