using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinConditionTrigger : MonoBehaviour
{
    [SerializeField]
    private float _delayBeforeSwitchingToNextLevel;

    [SerializeField]
    private Object _sceneToLoad;

    [SerializeField]
    private GameObject _triggerGo;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite _deathSprite;

    private void Awake()
    {
        TriggerNotifier triggerNotifier = _triggerGo.AddComponent<TriggerNotifier>();
        triggerNotifier.Init(new System.Collections.Generic.List<UnitType>()
        {
            UnitType.Player
        });
        triggerNotifier.UnitEntered += UnitEntered;
    }

    private void UnitEntered(UnitType unitType, Unit unit)
    {
        if(_sceneToLoad != null)
        {
            _spriteRenderer.sprite = _deathSprite;
            LeanTween.delayedCall(_delayBeforeSwitchingToNextLevel, MoveToNextLevel);
        }
        else
        {
            Debug.LogError("Please select correct scene object");
        }        
    }

    private void MoveToNextLevel()
    {
        LevelManager.Instance.CompletedCurrentLevel();
        LevelManager.Instance.LoadHighestUnlockedLevel();
    }
}
