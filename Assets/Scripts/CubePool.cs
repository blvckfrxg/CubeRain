using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _poolSize = 20;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject cube = Instantiate(_cubePrefab.gameObject);
            cube.SetActive(false);
            _pool.Enqueue(cube);
        }
    }

    public GameObject GetCube()
    {
        if (_pool.Count == 0)
        {
            GameObject newCube = Instantiate(_cubePrefab.gameObject);
            newCube.SetActive(false);
            _pool.Enqueue(newCube);
        }

        return _pool.Dequeue();
    }

    public void ReturnCube(GameObject cube)
    {
        if (cube.TryGetComponent<Cube>(out var cubeComponent))
        {
            cubeComponent.ResetState();
        }

        cube.SetActive(false);
        _pool.Enqueue(cube);
    }
}