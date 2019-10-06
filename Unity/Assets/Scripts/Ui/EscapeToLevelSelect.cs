using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EscapeToLevelSelect : MonoBehaviour
{
    [SerializeField]
    private string _levelSelectMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(_levelSelectMenu);
        }
    }
}
