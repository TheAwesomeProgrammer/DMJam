using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscapeToLevelSelect : MonoBehaviour
{
    [SerializeField]
    private Object _levelSelectMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(_levelSelectMenu.name);
        }
    }
}
