using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
