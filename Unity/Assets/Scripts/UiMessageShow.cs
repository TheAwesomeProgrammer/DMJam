using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UiMessageShow : MonoBehaviour
{
    private float MOVE_IN_TIME = 0.5f;
    private float SHOW_TIME = 1;

    [SerializeField]
    private TextMeshProUGUI _uiLabel;

    public void ShowMessage(string message)
    {   
        _uiLabel.text = message;
        _uiLabel.gameObject.LeanMoveLocalX(0, MOVE_IN_TIME);
        LeanTween.delayedCall(MOVE_IN_TIME + SHOW_TIME,
            MoveOutToRight);
        LeanTween.delayedCall(MOVE_IN_TIME + SHOW_TIME + MOVE_IN_TIME, MoveBackToLeft);
    }

    private void MoveOutToRight()
    {
        if(_uiLabel != null)
        {
            _uiLabel?.gameObject.LeanMoveLocalX(_uiLabel.rectTransform.rect.width, MOVE_IN_TIME);
        }
             
    }

    private void MoveBackToLeft()
    {
        if (_uiLabel != null)
        {
            _uiLabel?.gameObject.LeanMoveLocalX(-_uiLabel.rectTransform.rect.width, 0);
        }
    }
}
