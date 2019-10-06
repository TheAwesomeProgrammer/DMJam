using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{
    [SerializeField]
    private string _sceneToLoad;

    public void OnMouseDown()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnMouseDown();
        }
    }

}
