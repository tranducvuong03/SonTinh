using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string introSceneName = "Dialogue"; // Tên scene dẫn truyện

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(introSceneName);
    }
}
