using UnityEngine;
using System.Collections;

public class MainMenuItem : MonoBehaviour
{
    private float _startSize;

    [SerializeField]
    private float _scaleSizeIncrease;

    [SerializeField]
    private float _scaleTime;

    [SerializeField]
    private MainMenuUi _mainMenuUi;

    [SerializeField]
    private Type _type;

    private void Start()
    {
        _startSize = 1;
    }

    public void OnMouseDown()
    {
        _mainMenuUi.OnClick(_type);
    }

    public void OnMouseEnter()
    {        
        LeanTween.scale(gameObject, Vector3.one * (_startSize + _scaleSizeIncrease), _scaleTime);
    }

    public void OnMouseExit()
    {
        LeanTween.scale(gameObject, Vector3.one * (_startSize), _scaleTime);
    }

    public enum Type
    {
        Play,
        Tutorial,
        Credits,
        Quit
    }
}


