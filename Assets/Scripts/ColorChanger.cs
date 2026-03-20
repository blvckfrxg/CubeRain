using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _touchedColor = Color.green;

    private Renderer _cubeRenderer;
    private Material _originalMaterial;
    private bool _hasTouchedPlatform = false;

    private void Awake()
    {
        _cubeRenderer = GetComponent<Renderer>();
        _originalMaterial = _cubeRenderer.material;
    }

    public void ChangeColorOnFirstTouch()
    {
        if (_hasTouchedPlatform) return;

        _hasTouchedPlatform = true;
        _cubeRenderer.material.color = _touchedColor;
    }

    public void ResetColor()
    {
        _hasTouchedPlatform = false;
        _cubeRenderer.material = _originalMaterial;
    }
}