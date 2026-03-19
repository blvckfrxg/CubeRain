using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private bool hasTouchedPlatform = false;
    private CubePool pool;
    private Material originalMaterial;
    public Color touchedColor = Color.green;

    void Start()
    {
        pool = FindFirstObjectByType<CubePool>();
        originalMaterial = GetComponent<Renderer>().material;
    }

    public void ResetCube()
    {
        hasTouchedPlatform = false;
        GetComponent<Renderer>().material = originalMaterial;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") && !hasTouchedPlatform)
        {
            HandleFirstTouch();
        }
    }

    private void HandleFirstTouch()
    {
        hasTouchedPlatform = true;

        GetComponent<Renderer>().material.color = touchedColor;

        float lifeTime = Random.Range(2f, 5f);
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnCube(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}