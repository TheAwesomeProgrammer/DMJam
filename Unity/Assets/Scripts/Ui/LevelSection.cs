using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class LevelSection : MonoBehaviour
{
    private const string VERTICAL_AXIS_INPUT = "Vertical";

    private List<LevelUi> _levelUis;
    private float _nextTimeCanSwitch;
    private int _currentMarkerIndex;

    [SerializeField]
    private float _timeBetweenEachSwitch;

    [SerializeField]
    private LevelUi _levelUiPrefab;

    [SerializeField]
    private Transform _levelUiParent;

    [SerializeField]
    private GameObject _bottomMarker;

    public bool IsSelected { get; set; }
   
    public void Init(IEnumerable<Level> levels)
    {
        _bottomMarker.SetActive(false);
        _levelUis = new List<LevelUi>();
        foreach (var level in levels)
        {
            AddLevel(level);
        }
        Select();
    }

    private void AddLevel(Level level)
    {
        LevelUi levelUi = Instantiate(_levelUiPrefab, _levelUiParent);
        levelUi.UpdateUi(level);
        _levelUis.Add(levelUi);
    }

    private void Update()
    {
        if (IsSelected)
        {
            HandleMarkerSelection();
        }
           
    }

    private void HandleMarkerSelection()
    {
        float axisInput = Input.GetAxis(VERTICAL_AXIS_INPUT);
        if (_nextTimeCanSwitch < Time.time)
        {
            if (axisInput > 0)
            {
                MoveUp();
                _nextTimeCanSwitch = Time.time + _timeBetweenEachSwitch;
            }
            if (axisInput < 0)
            {
                MoveDown();
                _nextTimeCanSwitch = Time.time + _timeBetweenEachSwitch;
            }
        }
    }

    private void MoveUp()
    {
        if (_currentMarkerIndex > 0)
        {
            _currentMarkerIndex--;
        }

        Select();
    }

    private void MoveDown()
    {
        if (_currentMarkerIndex < _levelUis.Count)
        {
            _currentMarkerIndex++;
        }
        if (_currentMarkerIndex >= _levelUis.Count)
        {
            _currentMarkerIndex = _levelUis.Count;
            _bottomMarker.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_bottomMarker);
        }
        else
        {
            Select();
        }       
    }

    private void Select()
    {
        _bottomMarker.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_levelUis[_currentMarkerIndex].gameObject);
    }
}
