using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{
    [SerializeField]
    private Object _sceneToLoad;

    public void OnMouseDown()
    {
        SceneManager.LoadScene(_sceneToLoad.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnMouseDown();
        }
    }

}
