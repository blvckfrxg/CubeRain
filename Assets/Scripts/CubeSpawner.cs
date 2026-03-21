using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private float _spawnInterval = 1f;
    [SerializeField] private Vector3 _spawnAreaMin;
    [SerializeField] private Vector3 _spawnAreaMax;

    private WaitForSeconds _waitForSpawn;

    private void Start()
    {
        _waitForSpawn = new WaitForSeconds(_spawnInterval);

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (isActiveAndEnabled)
        {
            SpawnCube();

            yield return _waitForSpawn;
        }
    }

    private void SpawnCube()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
            Random.Range(_spawnAreaMin.y, _spawnAreaMax.y),
            Random.Range(_spawnAreaMin.z, _spawnAreaMax.z)
        );

        GameObject cube = _cubePool.GetCube();

        cube.transform.position = spawnPos;
        cube.transform.rotation = Quaternion.identity;
        cube.SetActive(true);

        if (cube.TryGetComponent<Cube>(out var cubeComponent))
        {
            cubeComponent.ResetState();
            cubeComponent.LifetimeEnded += HandleCubeLifetimeEnd;
        }
    }

    private void HandleCubeLifetimeEnd(Cube cube)
    {
        cube.LifetimeEnded -= HandleCubeLifetimeEnd;
        _cubePool.ReturnCube(cube.gameObject);
    }
}