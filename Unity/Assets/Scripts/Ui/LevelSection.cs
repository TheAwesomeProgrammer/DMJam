using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

public class LevelSection : MonoBehaviour
{
    private const string VERTICAL_AXIS_INPUT = "Vertical";

    private List<LevelUi> _levelUis;
    private List<LevelUi> _unlockedlevelUis;
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

    [SerializeField]
    private TextMeshProUGUI _title;

    public bool IsSelected { get; private set; }
    public bool IsUnlocked => _unlockedlevelUis.Count > 0;
    public bool IsBottomMarkerActive => _bottomMarker.activeSelf;
   
    public void Init(IEnumerable<Level> levels, int index)
    {
        _title.text = "Stage " + (index + 1);
        _bottomMarker.SetActive(false);
        _levelUis = new List<LevelUi>();
        _unlockedlevelUis = new List<LevelUi>();

        foreach (var level in levels)
        {
            AddLevel(level);
        }
        if (IsUnlocked && IsSelected)
        {
            SelectMarker();
        }
        _currentMarkerIndex = _unlockedlevelUis.Count + 1;
    }

    private void AddLevel(Level level)
    {
        LevelUi levelUi = Instantiate(_levelUiPrefab, _levelUiParent);
        levelUi.UpdateUi(level);
        _levelUis.Add(levelUi);
        if (level.IsUnlocked)
        {
            _unlockedlevelUis.Add(levelUi);
        }
    }

    private void Update()
    {
        if (IsSelected && IsUnlocked)
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

        SelectMarker();
    }

    private void MoveDown()
    {
        if (_currentMarkerIndex < _unlockedlevelUis.Count)
        {
            _currentMarkerIndex++;
        }
        if (_currentMarkerIndex >= _unlockedlevelUis.Count)
        {
            _currentMarkerIndex = _unlockedlevelUis.Count;          
        }

        SelectMarker();          
    }

    private void SelectMarker()
    {
        foreach(var levelUi in _unlockedlevelUis)
        {
            levelUi.Deselect();
        }

        if (_currentMarkerIndex >= _unlockedlevelUis.Count)
        {
            _bottomMarker.SetActive(true);
        }
        else
        {
            _bottomMarker.SetActive(false);
            _unlockedlevelUis[_currentMarkerIndex].SelectMarker();
        }            
    }

    public void Select()
    {
        IsSelected = true;
        SelectMarker();
    }

    public void DeSelect()
    {
        IsSelected = false;
        _bottomMarker.SetActive(false);
        foreach (var levelUi in _unlockedlevelUis)
        {
            levelUi.Deselect();
        }
    }
}
