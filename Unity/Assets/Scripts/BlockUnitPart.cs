using UnityEngine;
using System.Collections;

public class BlockUnitPart : MonoBehaviour
{
    private Transform _body;
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float _aliveTime;

    [SerializeField]
    private float _fadeAfterTime;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _body = transform;
    }

    public void AddExplosionForce(float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier)
    {
        _rigidbody2D.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode2D.Impulse);
        _body.parent = null;
        _rigidbody2D.isKinematic = false;
        LeanTween.Destroy(gameObject, _aliveTime);
        gameObject.LeanDelayedCall(_fadeAfterTime, () => LeanTween.alpha(gameObject, 0, _aliveTime - _fadeAfterTime));
    }

    private void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
}
