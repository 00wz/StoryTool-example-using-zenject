using System.Collections;
using UnityEngine;

public class PlayerLifecycleService : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private PlayerController playerPrefab;

    [SerializeField]
    private float respawnDelay = 2f;

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
        StartCoroutine(RespavnPlayerRoutine());
    }

    private IEnumerator RespavnPlayerRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnPlayer();
    }
}
