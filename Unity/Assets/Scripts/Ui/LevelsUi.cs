using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsUi : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_INPUT = "Horizontal";

    private bool _isFirstLevelSelection = true;
    private float _nextTimeCanSwitch;
    private int _currentMarkerIndex;

    [SerializeField]
    private float _timeBetweenEachSwitch;

    [SerializeField]
    private int _levelsInASection;

    [SerializeField]
    private LevelSection _levelSectionPrefab;

    [SerializeField]
    private Transform _levelSectionParent;

    private void Awake()
    {
        List<Level> levelsForSection = new List<Level>();
        foreach(var level in LevelManager.Instance.Levels)
        {
            levelsForSection.Add(level);
            if(levelsForSection.Count >= _levelsInASection)
            {
                LevelSection levelSection = Instantiate(_levelSectionPrefab, _levelSectionParent);
                levelSection.Init(levelsForSection);
                levelsForSection.Clear();   
                levelSection.IsSelected = _isFirstLevelSelection;
                _isFirstLevelSelection = false;
            }
        }

        if(levelsForSection.Count > 0)
        {
            LevelSection levelSection = Instantiate(_levelSectionPrefab, _levelSectionParent);
            levelSection.Init(levelsForSection);
        }
    }

    private void Update()
    {
        
    }

    private void HandleLevelSectionSelection()
    {
        float axisInput = Input.GetAxis(HORIZONTAL_AXIS_INPUT);
        if (_nextTimeCanSwitch < Time.time)
        {
            if (axisInput > 0)
            {
                _nextTimeCanSwitch = Time.time + _timeBetweenEachSwitch;
            }
            if (axisInput < 0)
            {
                _nextTimeCanSwitch = Time.time + _timeBetweenEachSwitch;
            }
        }
    }
}
