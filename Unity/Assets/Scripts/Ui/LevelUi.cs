using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelUi : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_INPUT = "Horizontal";
    private const string SUBMIT_INPUT = "Submit";

    private float _nextTimeCanSwitch;
    private int _currentMarkerIndex;
    private Level _level;

    [SerializeField]
    private float _timeBetweenEachSwitch;

    [SerializeField]
    private Image _normalLevel;

    [SerializeField]
    private Image _hardLevel;

    [SerializeField]
    private Sprite _lockedSprite;

    [SerializeField]
    private Sprite _unlockedSprite;

    [SerializeField]
    private GameObject _marker;

    [SerializeField]
    private GameObject[] _levelMarkers;

    public bool IsUnlocked => _level.IsUnlocked;

    private void Awake()
    {
        _marker.SetActive(false);
        DeselectAll();
    }

    public void UpdateUi(Level level)
    {
        _level = level;
        UpdateStatus(_normalLevel, level.IsUnlocked);
        UpdateStatus(_hardLevel, level.IsHardModeUnlocked);
    }  

    private void UpdateStatus(Image levelImage, bool unlocked)
    {
        if (unlocked)
        {
            Unlock(levelImage);
        }
        else
        {
            Lock(levelImage);
        }
    }

    private void Lock(Image image)
    {
        image.sprite = _lockedSprite;
    }

    private void Unlock(Image image)
    {
        image.sprite = _unlockedSprite;
    }

    public void SelectMarker()
    {
        Select();
        _marker.SetActive(true);
    }

    public void Deselect()
    {
        DeselectAll();
        _marker.SetActive(false);
    }

    private void Update()
    {
        HandleSubmit();
        HandleLevelSectionSelection();
    }

    private void HandleSubmit()
    {
        if(_marker.activeSelf && Input.GetButtonDown(SUBMIT_INPUT))
        {
            LevelManager.Instance.LoadLevel(_level, _currentMarkerIndex > 0);
        }
    }

    private void HandleLevelSectionSelection()
    {
        float axisInput = Input.GetAxis(HORIZONTAL_AXIS_INPUT);
        if (_nextTimeCanSwitch < Time.time && _level.IsHardModeUnlocked && _marker.activeSelf)
        {
            if (axisInput > 0)
            {
                MoveRight();
                _nextTimeCanSwitch = Time.time + _timeBetweenEachSwitch;
            }
            if (axisInput < 0)
            {
                MoveLeft();
                _nextTimeCanSwitch = Time.time + _timeBetweenEachSwitch;
            }
        }
    }

    private void MoveLeft()
    {
        if (_currentMarkerIndex > 0)
        {
            _currentMarkerIndex--;
        }

        Select();
    }

    private void MoveRight()
    {
        if (_currentMarkerIndex + 1 < _levelMarkers.Length)
        {
            _currentMarkerIndex++;
        }

        Select();
    }

    private void Select()
    {
        DeselectAll();
         _levelMarkers[_currentMarkerIndex].SetActive(true);
    }

    private void DeselectAll()
    {
        foreach (var levelMarker in _levelMarkers)
        {
            levelMarker.SetActive(false);
        }
    }
}
