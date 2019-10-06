using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFollowOnStart : MonoBehaviour
{
    private bool _hasReachedPlayer;

    [SerializeField]
    private float _distanceToPlayerToReach;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Transform _cameraRootTransform;

    [SerializeField]
    private Transform _playerBodyTransform;

    private void Start()
    {
        if (!LevelManager.Instance.HasLoadedLevel(SceneManager.GetActiveScene().name))
        {
            _cameraRootTransform.parent = null;
            _player.PlayerMovement.Deactivate();
            LevelManager.Instance.LevelWasLoaded(SceneManager.GetActiveScene().name);
        }
        else
        {
            ReachedPlayer();
        }
    }

    private void Update()
    {
        if (!_hasReachedPlayer)
        {
            _cameraRootTransform.Translate(GetNormalizedDirectionToPlayer() * _speed * Time.deltaTime);
            if (GetDirectionToPlayer().magnitude < _distanceToPlayerToReach)
            {
                ReachedPlayer();
            }
        }        
    }

    private Vector2 GetNormalizedDirectionToPlayer()
    {
        return GetDirectionToPlayer().normalized;
    }

    private Vector2 GetDirectionToPlayer()
    {
        return (_player.CollisionTransform.position - _cameraRootTransform.position);
    }
     
    private void ReachedPlayer()
    {
        _hasReachedPlayer = true;
        _cameraRootTransform.parent = _playerBodyTransform;
        _cameraRootTransform.localPosition = new Vector3(0, 0, -10);
        _player.PlayerMovement.Activate();
    }

    
}
