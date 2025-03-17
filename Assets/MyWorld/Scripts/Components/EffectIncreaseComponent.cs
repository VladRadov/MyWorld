using UnityEngine;

public class EffectIncreaseComponent : MonoBehaviour
{
    private bool _startEffect;
    private Transform _transform;

    [Header("Settings")]
    [SerializeField] private Vector2 _increaseScale = new Vector2(1.3f, 1.3f);
    [SerializeField] private Vector2 _normalScale = new Vector2(1, 1);
    [SerializeField] private float _speedScale = 0.1f;

    public void Execute()
        => _startEffect = true;

    public void Cancel()
        => _startEffect = false;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        var newScale = Vector3.Lerp(_transform.localScale, _startEffect ? _increaseScale : _normalScale, _speedScale);
        _transform.localScale = newScale;
    }
}
