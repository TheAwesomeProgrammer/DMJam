using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinConditionTrigger : MonoBehaviour
{
    [SerializeField]
    private float _delayBeforeSwitchingToNextLevel;

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
        _spriteRenderer.sprite = _deathSprite;
        LeanTween.delayedCall(_delayBeforeSwitchingToNextLevel, MoveToNextLevel);    
    }

    private void MoveToNextLevel()
    {
        LevelManager.Instance.CompletedCurrentLevel();
        LevelManager.Instance.LoadHighestUnlockedLevel();
    }
}
