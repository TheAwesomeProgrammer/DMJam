using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Object = UnityEngine.Object;

[Serializable]
public class Level
{
    [SerializeField]
    private string _levelName;

    [SerializeField]
    private string _hardModeLevelName;

    public bool IsUnlocked { get; private set; }
    public bool IsHardModeUnlocked { get; private set; }
    public bool HasBeenLoaded { get; private set; }
    public string Name => _levelName;

    public void Load()
    {
        SceneManager.LoadScene(_levelName);
    }

    public void LoadHardLevel()
    {
        SceneManager.LoadScene(_hardModeLevelName);
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
