using UnityEngine;
using System.Collections;

public class CameraPlayerFollowMovement : MonoBehaviour
{
    private float DAMP_TIME = 1f;

    [SerializeField]
    private Vector2 _maxDistance;

    [SerializeField]
    private float _smoothFollowSpeed;

    [SerializeField]
    private Transform _playerBodyTransform;

    [SerializeField]
    private Transform _bodyTransform;

    private void FixedUpdate()
    {
        Vector2 normalizedDistanceToPlayer = NormalizedDirectionToPlayer();
        _bodyTransform.Translate(normalizedDistanceToPlayer * _smoothFollowSpeed * Time.deltaTime);

        
        _bodyTransform.position = GetNewCameraPositionWithinMaxDistance();
    }

    private Vector3 GetNewCameraPositionWithinMaxDistance()
    {
        Vector3 newCameraPositionWithinMaxDistance = _bodyTransform.position;
        Vector3 distanceBetweenPlayerAndCamera = _playerBodyTransform.position - _bodyTransform.position;

        if (Mathf.Abs(distanceBetweenPlayerAndCamera.x) > _maxDistance.x)
        {            
            newCameraPositionWithinMaxDistance.x = Mathf.Lerp(_bodyTransform.position.x, _playerBodyTransform.position.x, 1 / DAMP_TIME * Time.deltaTime);
        }
        if (Mathf.Abs(distanceBetweenPlayerAndCamera.y) > _maxDistance.y)
        {
            newCameraPositionWithinMaxDistance.y = Mathf.Lerp(_bodyTransform.position.y, _playerBodyTransform.position.y, 1 / DAMP_TIME * Time.deltaTime);
        }

        return newCameraPositionWithinMaxDistance;
    }

    private Vector2 NormalizedDirectionToPlayer()
    {
        return (_playerBodyTransform.position - _bodyTransform.position).normalized;;
    }
}
