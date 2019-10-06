using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelUi : MonoBehaviour, IDeselectHandler, ISelectHandler
{
    [SerializeField]
    private Image _normalLevel;

    [SerializeField]
    private Image _hardLevel;

    [SerializeField]
    private GameObject _marker;

    private void Awake()
    {
        _marker.SetActive(false);
    }

    public void UpdateUi(Level level)
    {
        UpdateStatus(_normalLevel, level.IsUnlocked);
        UpdateStatus(_hardLevel, level.IsHardModeUnlocked);
    }  

    private void UpdateStatus(Image levelImage, bool unlocked)
    {
        if (unlocked)
        {
            Unlock(levelImage);
        }
        else
        {
            Lock(levelImage);
        }
    }

    private void Lock(Image image)
    {
        image.color = Color.black;
    }

    private void Unlock(Image image)
    {
        image.color = Color.white;
    }

    public void OnSelect(BaseEventData eventData)
    {
        _marker.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _marker.SetActive(false);
    }
}
