using UnityEngine;

[ExecuteAlways]
public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance = 10f;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        transform.position = target.position - transform.forward * distance;
    }
}
