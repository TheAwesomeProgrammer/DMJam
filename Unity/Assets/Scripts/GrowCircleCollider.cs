using UnityEngine;
using UnityEditor;

public class GrowCircleCollider : MonoBehaviour
{
    [SerializeField]
    private float _growTime;

    [SerializeField]
    private float _grownRadius;

    [SerializeField]
    private CircleCollider2D _circleCollider2D;

    [SerializeField]
    private GameObject _rootGo;

    private float _amountToGrow;
    private float _timeGone;

    public float GrownRadius => _grownRadius;

    private void Awake()
    {
        _amountToGrow = _grownRadius - _circleCollider2D.radius;
    }

    private void Update()
    {
        Grow();
    }

    private void Grow()
    {
        _timeGone += Time.deltaTime;
        if (_circleCollider2D.radius < _grownRadius)
        {
            _circleCollider2D.radius += (Time.deltaTime / _growTime) * _amountToGrow;
        }
        else
        {
            Destroy(_rootGo);
        }
    }
}
