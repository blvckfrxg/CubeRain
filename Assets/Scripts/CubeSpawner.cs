using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public CubePool cubePool;
    public float spawnInterval = 1f;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0f, spawnInterval);
    }

    void SpawnCube()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );

        GameObject cube = cubePool.GetCube(spawnPos, Quaternion.identity);

        CubeBehaviour behaviour = cube.GetComponent<CubeBehaviour>();
        if (behaviour != null)
        {
            behaviour.ResetCube();
        }
    }
}