using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int damage = 10;

    [SerializeField]
    private float cooldown = 1f;

    private Animation _animation;

    void Awake()
    {
        _animation = GetComponentInChildren<Animation>();
    }

    public IEnumerator PerformAttackRoutine(Health target)
    {
        if (_animation != null)
        {
            _animation.Play();
        }
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        yield return new WaitForSeconds(cooldown);
    }
}
