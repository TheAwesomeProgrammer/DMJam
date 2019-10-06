using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoadOnPlay;

    [SerializeField]
    private string _sceneToLoadOnTutorial;

    [SerializeField]
    private string _sceneToLoadOnCredits;

    public void OnClick(MainMenuItem.Type type)
    {
        switch (type)
        {
            case MainMenuItem.Type.Play:
                SceneManager.LoadScene(_sceneToLoadOnPlay);
                break;

            case MainMenuItem.Type.Tutorial:
                SceneManager.LoadScene(_sceneToLoadOnTutorial);
                break;

            case MainMenuItem.Type.Credits:
                SceneManager.LoadScene(_sceneToLoadOnCredits);
                break;

            case MainMenuItem.Type.Quit:
                Application.Quit();
                break;
        }
    }    
}
