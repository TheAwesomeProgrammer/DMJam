using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game _instance;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Object _sceneToLoadOnLose;

    [SerializeField]
    private Object[] _levels;

    public static Game Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<Game>();
            }

            return _instance;
        }
    }

    public int CurrentLevelReached
    {
        get
        {
            return PlayerPrefs.GetInt(nameof(CurrentLevelReached));
        }
        set
        {
            PlayerPrefs.SetInt(nameof(CurrentLevelReached), value);
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(_levels[level].name);
    }

    private void Awake()
    {
        _player.MetKilledBabyQuota += OnMetKilledBabyQuota;
        DontDestroyOnLoad(gameObject);
    }

    private void OnMetKilledBabyQuota()
    {
        SceneManager.LoadScene(_sceneToLoadOnLose.name);
    }
}
