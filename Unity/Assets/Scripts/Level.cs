using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Object = UnityEngine.Object;

[Serializable]
public class Level
{
    [SerializeField]
    private Object _level;

    [SerializeField]
    private Object _hardModeLevel;

    public bool IsUnlocked { get; private set; }
    public bool IsHardModeUnlocked { get; private set; }
    public bool HasBeenLoaded { get; private set; }
    public string Name => _level.name;

    public void Load()
    {
        SceneManager.LoadScene(_level.name);
    }

    public void LoadHardLevel()
    {
        SceneManager.LoadScene(_hardModeLevel.name);
    }

    public void Unlock()
    {
        IsUnlocked = true;
    }

    public void UnlockHardMode()
    {
        IsHardModeUnlocked = true;
    }

    public void LoadedLevel()
    {
        HasBeenLoaded = true;
    }
}
