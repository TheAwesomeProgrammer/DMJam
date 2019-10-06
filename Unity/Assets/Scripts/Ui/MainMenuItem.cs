using UnityEngine;
using System.Collections;

public class MainMenuItem : MonoBehaviour
{
    [SerializeField]
    private MainMenuUi _mainMenuUi;

    [SerializeField]
    private Type _type;

    private void OnMouseDown()
    {
        _mainMenuUi.OnClick(_type);
    }

    public enum Type
    {
        Play,
        Tutorial,
        Credits,
        Quit
    }
}


