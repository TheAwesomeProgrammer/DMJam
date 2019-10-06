using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField]
    private Object _sceneToLoadOnPlay;

    [SerializeField]
    private Object _sceneToLoadOnTutorial;

    [SerializeField]
    private Object _sceneToLoadOnCredits;

    public void OnClick(MainMenuItem.Type type)
    {
        switch (type)
        {
            case MainMenuItem.Type.Play:
                SceneManager.LoadScene(_sceneToLoadOnPlay.name);
                break;

            case MainMenuItem.Type.Tutorial:
                SceneManager.LoadScene(_sceneToLoadOnTutorial.name);
                break;

            case MainMenuItem.Type.Credits:
                SceneManager.LoadScene(_sceneToLoadOnCredits.name);
                break;

            case MainMenuItem.Type.Quit:
                Application.Quit();
                break;
        }
    }    
}
