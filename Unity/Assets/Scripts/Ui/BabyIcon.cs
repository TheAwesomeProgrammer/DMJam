using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BabyIcon : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite _deadBabyIcon;

    public bool IsAlive { get; private set; }

    private void Awake()
    {
        IsAlive = true;
    }

    public void BabyDied()
    {
        _image.sprite = _deadBabyIcon;
        IsAlive = false;
    }
}
