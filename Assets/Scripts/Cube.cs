using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private ColorChanger _colorChanger;
    private Coroutine _lifeTimerCoroutine;

    public event System.Action<Cube> LifetimeEnded;
    public event System.Action<Cube> TouchedPlatform;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    public void ResetState()
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

        TouchedPlatform?.Invoke(this);

        float lifeTime = Random.Range(_minLifeTime, _maxLifeTime);
        _lifeTimerCoroutine = StartCoroutine(LifeTimer(lifeTime));
    }

    private IEnumerator LifeTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        LifetimeEnded?.Invoke(this);
    }
}