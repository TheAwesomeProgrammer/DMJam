using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinConditionTrigger : MonoBehaviour
{
    [SerializeField]
    private Object _sceneToLoad;

    [SerializeField]
    private GameObject _triggerGo;


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
            Game.Instance.CurrentLevelReached++;
            SceneManager.LoadScene(_sceneToLoad.name);
        }
        else
        {
            Debug.LogError("Please select correct scene object");
        }
        
    }
}
