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

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Image _chargeImage;

    private event ChargeEnded _chargeEnded;

    private void Awake()
    {
        _player.PlayerFlipped += OnPlayerFlipped;
    }

    private void OnPlayerFlipped()
    {
        Image.OriginHorizontal originHorizontal = (Image.OriginHorizontal)_chargeImage.fillOrigin;

        switch (originHorizontal)
        {
            case Image.OriginHorizontal.Left:
                _chargeImage.fillOrigin = (int)Image.OriginHorizontal.Right;
                break;

            case Image.OriginHorizontal.Right:
                _chargeImage.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;
        }
    }

    public void Charge(float chargeDuration, string chargeInputName, ChargeEnded chargeEnded)
    {
        ResetCharger();
        _chargeInputName = chargeInputName;
        _chargeEnded = chargeEnded;
        _isCharging = true;
        _chargeImage.enabled = true;
    }

    private void Update()
    {
        if (_isCharging)
        {
            _chargeImage.fillAmount = Mathf.Lerp(0, MAX_CHARGE_AMOUNT, _chargingTime);
            _chargingTime += Time.deltaTime;
            if (Input.GetButtonUp(_chargeInputName) || _chargeImage.fillAmount >= MAX_CHARGE_AMOUNT)
            {
                _chargeEnded?.Invoke(_chargeImage.fillAmount);
                ResetCharger();               
            }           
        }
    }

    private void ResetCharger()
    {
        _chargingTime = 0;
        _chargeImage.enabled = false;
        _chargeImage.fillAmount = 0;
        _chargeInputName = string.Empty;
        _chargeEnded = null;
        _isCharging = false;
    }
}
