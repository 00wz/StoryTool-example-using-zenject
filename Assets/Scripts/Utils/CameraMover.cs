using UnityEngine;
using Zenject;

[ExecuteAlways]
public class CameraMover : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distance = 10f;

    private PlayerLifecycleService _playerLifecycleService;

    [Inject]
    private void Init(PlayerLifecycleService playerLifecycleService)
    {
        _playerLifecycleService = playerLifecycleService;
    }

    void LateUpdate()
    {
        if (_playerLifecycleService?.PlayerController == null)
        {
            return;
        }

        transform.position = _playerLifecycleService.PlayerController.transform.position - transform.forward * distance;
    }
}
