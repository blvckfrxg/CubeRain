using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private Color touchedColor = Color.green;
    [SerializeField] private float minLifeTime = 2f;
    [SerializeField] private float maxLifeTime = 5f;

    private CubePool pool;
    private Renderer cubeRenderer;
    private Material originalMaterial;
    private bool hasTouchedPlatform = false;
    private Coroutine lifeTimerCoroutine;

    public event System.Action<GameObject> OnLifetimeEnded;

    private void Awake()
    {
        cubeRenderer = GetComponent<Renderer>();
        originalMaterial = cubeRenderer.material;
    }

    public void Init(CubePool targetPool)
    {
        pool = targetPool;
        ResetCube();
    }

    private void ResetCube()
    {
        hasTouchedPlatform = false;
        cubeRenderer.material = originalMaterial;

        if (lifeTimerCoroutine != null)
        {
            StopCoroutine(lifeTimerCoroutine);
            lifeTimerCoroutine = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasTouchedPlatform && collision.gameObject.TryGetComponent<Platform>(out _))
        {
            HandleFirstTouch();
        }
    }

    private void HandleFirstTouch()
    {
        hasTouchedPlatform = true;
        cubeRenderer.material.color = touchedColor;

        float lifeTime = Random.Range(minLifeTime, maxLifeTime);
        lifeTimerCoroutine = StartCoroutine(LifeTimer(lifeTime));
    }

    private IEnumerator LifeTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        OnLifetimeEnded?.Invoke(gameObject);
    }
}