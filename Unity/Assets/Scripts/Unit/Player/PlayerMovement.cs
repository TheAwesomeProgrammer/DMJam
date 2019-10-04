using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_INPUT_NAME = "PlayerHorizontal";

    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Transform _bodyTransform;

    private Vector2 _currentMoveDirection;

    // Update is called once per frame
    void FixedUpdate()
    {
        SetMoveDirection();

        if(Mathf.Abs(_currentMoveDirection.x) > 0)
        {
            _rigidbody2D.AddForce(_currentMoveDirection * _acceleration, ForceMode2D.Force);
        }
    }

    private void SetMoveDirection()
    {
        _currentMoveDirection.x = Input.GetAxis(HORIZONTAL_AXIS_INPUT_NAME);
        _currentMoveDirection.Normalize();
    }

    public void AddExplosionForce(float explosionForce, Vector2 explosionPositon, float explosionRadius)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddExplosionForce(explosionForce, explosionPositon, explosionRadius, 2, ForceMode2D.Impulse);
    }

    public void Flip()
    {        
        _bodyTransform.localScale = new Vector3(_bodyTransform.lossyScale.x * -1, _bodyTransform.lossyScale.y * -1);
    }
}
