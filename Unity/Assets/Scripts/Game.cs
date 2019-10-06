using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private const string MINI_MAP_INPUT = "MiniMap";

    private static Game _instance;

    [SerializeField]
    private GameObject _miniMapGo;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Object _sceneToLoadOnLose;

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

    private void Awake()
    {
        _player.MetKilledBabyQuota += OnMetKilledBabyQuota;
        DontDestroyOnLoad(gameObject);
    }

    private void OnMetKilledBabyQuota()
    {
        Lost();
    }

    private void Update()
    {
        if (Input.GetButtonDown(MINI_MAP_INPUT))
        {
            _miniMapGo.SetActive(!_miniMapGo.activeSelf);
        }
    }

    public void Lost()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
