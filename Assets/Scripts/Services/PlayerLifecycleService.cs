using UnityEngine;

public class PlayerLifecycleService : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private PlayerController playerPrefab;

    private PlayerController _playerController;

    public PlayerController PlayerController => _playerController;

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _playerController = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        _playerController.GetComponentInChildren<Health>().Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        SpawnPlayer();
    }
}
