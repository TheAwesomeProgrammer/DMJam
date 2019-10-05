using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private const string LOST_TEXT = "You lost.";
    private const float RESTART_TIME = 5;

    [SerializeField]
    private UiMessageShow _uiMessageShow;

    private void Awake()
    {
        _uiMessageShow.ShowMessage(LOST_TEXT);
        Invoke("LoadSceneAfterTime", RESTART_TIME);
    }

    private void LoadSceneAfterTime()
    {
        Game.Instance.LoadLevel(Game.Instance.CurrentLevelReached);
    }
}
