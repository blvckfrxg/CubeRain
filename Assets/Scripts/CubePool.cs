using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    public GameObject cubePrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.SetActive(false);
            pool.Enqueue(cube);
        }
    }

    public GameObject GetCube(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            GameObject newCube = Instantiate(cubePrefab);
            newCube.SetActive(false);
            pool.Enqueue(newCube);
        }

        GameObject cube = pool.Dequeue();
        cube.transform.position = position;
        cube.transform.rotation = rotation;
        cube.SetActive(true);
        return cube;
    }

    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        pool.Enqueue(cube);
    }
}