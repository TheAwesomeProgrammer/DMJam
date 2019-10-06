using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseOnPlayerContact : MonoBehaviour
{
    [SerializeField]
    private float _extraRaycastDistance = 0.1f;

    [SerializeField]
    private LayerMask _raycastMask;

    [SerializeField]
    private Transform _bodyTransform;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_bodyTransform.position, Vector2.up, _bodyTransform.lossyScale.y /2 + _extraRaycastDistance, _raycastMask);
        if(hit.collider)
        {
            Player player = hit.collider.GetComponentInParent<Player>();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);            
        }
    }
}
