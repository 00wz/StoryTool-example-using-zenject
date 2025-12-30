using System.Collections;
using System.Linq;
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

    public IEnumerator PerformAttackRoutine(params Health[] targets)
    {
        if (_animation != null)
        {
            _animation.Play();
        }
        foreach (var target in targets?.Where(health => health != null))
        {
            target.TakeDamage(damage);
        }
        yield return new WaitForSeconds(cooldown);
    }
}
