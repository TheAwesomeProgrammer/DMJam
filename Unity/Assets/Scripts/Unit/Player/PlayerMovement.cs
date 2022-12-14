using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_INPUT_NAME = "PlayerHorizontal";   

    private Vector2 _currentMoveDirection;
    private Vector3 _startPosition;

    [SerializeField]
    private float _distanceRequiredToMoveUp = 1;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private float _acceleration;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Transform _graphicsTransform;

    public bool HasMovedUp { get; private set; }

    private void Awake()
    {
        _startPosition = _graphicsTransform.position;
        _player.PlayerFlipped += Flip;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIfHasMovedUp();
        SetMoveDirection();

        if(Mathf.Abs(_currentMoveDirection.x) > 0)
        {
            _rigidbody2D.AddForce(_currentMoveDirection * _acceleration, ForceMode2D.Force);
        }
    }

    private void CheckIfHasMovedUp()
    {
        if (!HasMovedUp)
        {
            Vector2 distanceFromStartPosition = _graphicsTransform.position - _startPosition;
            if (distanceFromStartPosition.y > _distanceRequiredToMoveUp)
            {
                HasMovedUp = true;
            }
        }       
    }

    private void SetMoveDirection()
    {
        _currentMoveDirection.x = Input.GetAxis(HORIZONTAL_AXIS_INPUT_NAME);
        _currentMoveDirection.Normalize();
    }

    public void AddRecoil(float recoilForce, Vector2 shootDirection)
    {
        _rigidbody2D.AddForce(-shootDirection * recoilForce, ForceMode2D.Impulse);
    }

    public void AddExplosionForce(float explosionForce, Vector2 explosionPositon, float explosionRadius, float upwardsModifier)
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        _rigidbody2D.AddExplosionForce(explosionForce, explosionPositon, explosionRadius, upwardsModifier, ForceMode2D.Impulse);
    }

    private void Flip()
    {        
        _graphicsTransform.localScale = new Vector3(_graphicsTransform.lossyScale.x * -1, _graphicsTransform.lossyScale.y);
    }

    public void Activate()
    {
        enabled = true;
        _rigidbody2D.isKinematic = false;
    }

    public void Deactivate()
    {
        enabled = false;
        _rigidbody2D.isKinematic = true;
    }
}
