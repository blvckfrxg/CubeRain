using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cube.SetActive(false);
            pool.Enqueue(cube);
        }
    }

    public GameObject GetCube()
    {
        if (pool.Count == 0)
        {
            GameObject newCube = Instantiate(cubePrefab);
            newCube.SetActive(false);
            pool.Enqueue(newCube);
        }

        return pool.Dequeue();
    }

    public void ReturnCube(GameObject cube)
    {
        cube.SetActive(false);
        pool.Enqueue(cube);
    }
}