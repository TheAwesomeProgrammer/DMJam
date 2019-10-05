using UnityEngine;
using System.Collections;

public class BazookaAim : MonoBehaviour
{
    [SerializeField]
    private Transform _bazookaBodyTransform;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Direction _startLookingDirection;

    [SerializeField]
    private int _lookingDirectionMaxAimAngle;

    [SerializeField]
    private int _lookingDirectionMinAimAngle;

    private float _lastAngle;
    private Direction _currentLookingDirection;

    private void Start()
    {
        _currentLookingDirection = _startLookingDirection;
    }

    private void Update()
    {          
        if (Vector2.Angle(GetStartLookDirection(), GetMouseDirectionRelativeToLookingDirection()) > _lookingDirectionMaxAimAngle)
        {
            _player.Flip();
            _currentLookingDirection = (Direction)((int)_currentLookingDirection * -1);
            _bazookaBodyTransform.rotation = Quaternion.Euler(0, 0, _lookingDirectionMaxAimAngle - 1);           
        }
        else if(Vector2.Angle(GetStartLookDirection(), GetMouseDirectionRelativeToLookingDirection()) > _lookingDirectionMinAimAngle)
        {
            _player.Flip();
            _currentLookingDirection = (Direction)((int)_currentLookingDirection * -1);
            _bazookaBodyTransform.rotation = Quaternion.Euler(0, 0, _lookingDirectionMinAimAngle - 1);
        }

        _bazookaBodyTransform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(GetStartLookDirection(), GetMouseDirectionRelativeToLookingDirection()));
    }

    private Vector2 GetMouseDirectionRelativeToLookingDirection()
    {
        if(_currentLookingDirection == _startLookingDirection)
        {
            return GetMouseDirection();
        }
        else
        {
            return -GetMouseDirection();
        }       
    }

    private Vector2 GetMouseDirection()
    {
        return (Camera.main.ScreenPointToRay(Input.mousePosition).origin - _bazookaBodyTransform.position).normalized;
    }

    private Vector2 GetStartLookDirection()
    {
        switch (_startLookingDirection)
        {
            case Direction.Left:
                return Vector2.left;

            case Direction.Right:
                return Vector2.right;

            default:
                return Vector2.zero;
        }

    }
}
