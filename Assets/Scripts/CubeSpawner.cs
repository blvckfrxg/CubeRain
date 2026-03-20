using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool cubePool;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Vector3 spawnAreaMin;
    [SerializeField] private Vector3 spawnAreaMax;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCube();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCube()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        GameObject cube = cubePool.GetCube();
        cube.transform.position = spawnPos;
        cube.transform.rotation = Quaternion.identity;
        cube.SetActive(true);

        CubeBehaviour behaviour = cube.GetComponent<CubeBehaviour>();
        if (behaviour != null)
        {
            behaviour.Init(cubePool);
            behaviour.OnLifetimeEnded += HandleCubeLifetimeEnd;
        }
        else
        {
            Debug.LogError("Cube prefab does not have CubeBehaviour component!", cube);
        }
    }

    private void HandleCubeLifetimeEnd(GameObject cube)
    {
        CubeBehaviour behaviour = cube.GetComponent<CubeBehaviour>();
        if (behaviour != null)
        {
            behaviour.OnLifetimeEnded -= HandleCubeLifetimeEnd;
        }

        cubePool.ReturnCube(cube);
    }
}