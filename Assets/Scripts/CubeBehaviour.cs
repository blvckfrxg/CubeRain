using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private CubePool _pool;
    private ColorChanger _colorChanger;
    private Coroutine _lifeTimerCoroutine;

    public event System.Action<GameObject> LifetimeEnded;
    public event System.Action<GameObject> TouchedPlatform;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        if (_colorChanger == null)
            Debug.LogError("ColorChanger component missing!", this);
    }

    public void Init(CubePool targetPool)
    {
        _pool = targetPool;
        ResetCube();
    }

    private void ResetCube()
    {
        if (_colorChanger != null)
            _colorChanger.ResetColor();

        if (_lifeTimerCoroutine != null)
        {
            StopCoroutine(_lifeTimerCoroutine);
            _lifeTimerCoroutine = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _))
        {
            HandleFirstTouch();
        }
    }

    private void HandleFirstTouch()
    {
        if (_colorChanger != null)
            _colorChanger.ChangeColorOnFirstTouch();

        TouchedPlatform?.Invoke(gameObject);

        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        _lifeTimerCoroutine = StartCoroutine(LifeTimer(lifeTime));
    }

    private IEnumerator LifeTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        LifetimeEnded?.Invoke(gameObject);
    }
}