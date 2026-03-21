using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _touchedColor = Color.green;

    private Renderer _cubeRenderer;
    private Color _originalColor;
    private bool _hasTouchedPlatform = false;

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
        _originalColor = _cubeRenderer.material.color;
    }

    public void ChangeColorOnFirstTouch()
    {
        if (_hasTouchedPlatform)
            return;

        _hasTouchedPlatform = true;
        _cubeRenderer.material.color = _touchedColor;
    }

    public void ResetColor()
    {
        _hasTouchedPlatform = false;
        _cubeRenderer.material.color = _originalColor;
    }
}