using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public delegate void ChargeEnded(float chargeAmount);

public class Charger : MonoBehaviour
{
    private const float MAX_CHARGE_AMOUNT = 1;

    private string _chargeInputName;
    private bool _isCharging;
    private float _chargingTime;
    private float _fillAmount;
    private float _fillAmountPerSprite;
    private float _startZoom;
    private LTDescr _currentZoomOutTween;

    [SerializeField]
    private float _zoomOutTime;

    [SerializeField]
    private float _zoomedInSize;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Image _chargeImage;

    [SerializeField]
    private Transform _bodyTransform;

    [SerializeField]
    private Sprite[] _chargeSprites;

    [SerializeField]
    private Camera _zoomCamera;

    private event ChargeEnded _chargeEnded;

    private void Awake()
    {
        _startZoom = _zoomCamera.orthographicSize;
    }

    public void Charge(float chargeDuration, string chargeInputName, ChargeEnded chargeEnded)
    {
        LeanTween.cancel(gameObject);
        ResetCharger();
        _chargeInputName = chargeInputName;
        _chargeEnded = chargeEnded;
        _isCharging = true;
        _chargeImage.enabled = true;
        _fillAmountPerSprite = MAX_CHARGE_AMOUNT / _chargeSprites.Length;
    }

    private void Update()
    {
        if (_isCharging)
        {
            _zoomCamera.orthographicSize = Mathf.Lerp(_startZoom, _zoomedInSize, _chargingTime);
            _fillAmount = Mathf.Lerp(0, MAX_CHARGE_AMOUNT, _chargingTime);
            _chargingTime += Time.deltaTime;
            _chargeImage.sprite = GetSpriteBasedOnFillAmount(_fillAmount);
            if (Input.GetButtonUp(_chargeInputName) || _fillAmount >= MAX_CHARGE_AMOUNT)
            {
                _chargeEnded?.Invoke(_fillAmount);
                ResetCharger();               
            }           
        }
    }

    private Sprite GetSpriteBasedOnFillAmount(float fillAmount)
    {
        int spriteIndex = Mathf.FloorToInt(fillAmount / _fillAmountPerSprite);
        if(spriteIndex >= _chargeSprites.Length)
        {
            spriteIndex = _chargeSprites.Length - 1;
        }
        return _chargeSprites[spriteIndex];
    }

    private void ResetCharger()
    {
        _currentZoomOutTween = LeanTween.value(gameObject, _zoomCamera.orthographicSize , _startZoom, _zoomOutTime).setOnUpdate((float newZoomSize) => {
            _zoomCamera.orthographicSize = newZoomSize;
        });
        _fillAmountPerSprite = 0;
        _chargeImage.sprite = null;
        _chargingTime = 0;
        _chargeImage.enabled = false;
        _chargeImage.fillAmount = 0;
        _chargeInputName = string.Empty;
        _chargeEnded = null;
        _isCharging = false;
    }
}
