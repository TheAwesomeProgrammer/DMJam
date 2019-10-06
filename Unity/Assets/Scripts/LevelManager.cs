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

    private void Awake()
    {
        if(Instance != this && Instance == null)
        {
            Destroy(gameObject);

        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _currentLevel = _levels[_currentLevelIndex];
            _currentLevel.Unlock();
        }
        
    }

    public bool HasLoadedLevel(string name)
    {
        return GetLevelFromName(name).HasBeenLoaded;
    }

    public void LevelWasLoaded(string name)
    {
        GetLevelFromName(name).LoadedLevel();
    }

    private Level GetLevelFromName(string name)
    {
        return _levels.Find(item => item.Name == name);
    }

    public void CompletedCurrentLevel()
    {
        _currentLevel.Unlock();
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
        _currentLevelIndex = _levels.FindIndex(item => item.Name == highUnlockedLevel.Name);
        highUnlockedLevel.Load();       
    }

    private Level GetNextLevel()
    {
        return _levels[_currentLevelIndex + 1];
    }
    
}
