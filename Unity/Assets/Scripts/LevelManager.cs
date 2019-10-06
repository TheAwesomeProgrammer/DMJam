using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    private int _currentLevelIndex;
    private Level _currentLevel;

    private static LevelManager _instance;

    [SerializeField]
    private List<Level> _levels;

    public List<Level> Levels => _levels;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }

            return _instance;
        }
    }

    public void CompletedCurrentLevel()
    {
        _currentLevel.UnlockHardMode();
        Level nextLevel = GetNextLevel();
        nextLevel.Unlock();
    }

    public void LoadLevel(Level level, bool loadHardMode)
    {
        if (loadHardMode)
        {
            level.LoadHardLevel();
        }
        else
        {
            level.Load();
        }
    }

    public void LoadHighestUnlockedLevel()
    {
        Level highUnlockedLevel = _levels.FindLast(item => item.IsUnlocked);
        highUnlockedLevel.Load();
    }

    private Level GetNextLevel()
    {
        return _levels[_currentLevelIndex + 1];
    }
    
}
