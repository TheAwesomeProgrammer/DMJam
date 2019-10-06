using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelsUi : MonoBehaviour
{
    private const string HORIZONTAL_AXIS_INPUT = "Horizontal";

    private bool _isFirstLevelSelection = true;
    private float _nextTimeCanSwitch;
    private int _currentLevelSectionIndex;
    private List<LevelSection> _levelSections;

    [SerializeField]
    private float _timeBetweenEachSwitch;

    [SerializeField]
    private int _levelsInASection;

    [SerializeField]
    private LevelSection _levelSectionPrefab;

    [SerializeField]
    private Transform _levelSectionParent;

    private void Start()
    {
        _levelSections = new List<LevelSection>();
        List<Level> levelsForSection = new List<Level>();
        foreach (var level in LevelManager.Instance.Levels)
        {
            levelsForSection.Add(level);
            if(levelsForSection.Count >= _levelsInASection)
            {
                LevelSection levelSection = Instantiate(_levelSectionPrefab, _levelSectionParent);
                _levelSections.Add(levelSection);
                levelSection.Init(levelsForSection);
                levelsForSection.Clear();
                if (_isFirstLevelSelection)
                {
                    levelSection.Select();
                }
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
        HandleLevelSectionSelection();
    }

    private void HandleLevelSectionSelection()
    {
        float axisInput = Input.GetAxis(HORIZONTAL_AXIS_INPUT);
        if (_nextTimeCanSwitch < Time.time && _levelSections[_currentLevelSectionIndex].IsBottomMarkerActive)
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
        if (_currentLevelSectionIndex > 0)
        {
            _currentLevelSectionIndex--;
        }

        Select();
    }

    private void MoveRight()
    {
        if (_currentLevelSectionIndex < _levelSections.Count && _levelSections[_currentLevelSectionIndex + 1].IsUnlocked)
        {
            _currentLevelSectionIndex++;
        }

        Select();
    }

    private void Select()
    {
        foreach(var levelSection in _levelSections)
        {
            levelSection.DeSelect();
        }

        _levelSections[_currentLevelSectionIndex].Select();
    }
}
