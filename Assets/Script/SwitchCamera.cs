using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera dialogueCamera; // Camera hội thoại
    public Camera gameCamera;     // Camera game chính (Parallax)

    public void SwitchToGameCamera()
    {
        if (dialogueCamera != null)
        {
            dialogueCamera.gameObject.SetActive(false); // Tắt camera hội thoại
        }

        if (gameCamera != null)
        {
            gameCamera.gameObject.SetActive(true);  // Bật camera game
            Camera.main.transform.SetParent(gameCamera.transform); // Gán Main Camera vào Game Camera
            Camera.main.transform.position = gameCamera.transform.position; // Đặt lại vị trí
            Camera.main.transform.rotation = gameCamera.transform.rotation; // Đặt lại góc quay
        }
    }
}
