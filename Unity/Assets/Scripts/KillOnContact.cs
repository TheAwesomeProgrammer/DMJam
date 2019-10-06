using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KillOnContact : MonoBehaviour
{
    [SerializeField]
    private GameObject _collisionGo;
    
    void Start()
    {
        TriggerNotifier triggerNotifier = _collisionGo.AddComponent<TriggerNotifier>();
        triggerNotifier.Init(new System.Collections.Generic.List<UnitType>() { UnitType.Player });
        triggerNotifier.UnitEntered += UnitEntered;
    }

    private void UnitEntered(UnitType unitType, Unit unit)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
