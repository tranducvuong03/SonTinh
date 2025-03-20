using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    public AudioSource audioSource;
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    void Start()
    {
        backgroundBox.gameObject.SetActive(true);
        isActive = true;
        DisplayMessage();
    }

    public void InitializeDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Start conversation! Load messages" + messages.Length);
        DisplayMessage();
    }

    public void SkipDialogue()
    {
        SceneManager.LoadScene("World 1-1");
    }

    void DisplayMessage()
    {
        if (currentMessages == null || currentMessages.Length == 0) return;

        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        if (!string.IsNullOrEmpty(messageToDisplay.audioFileName))
        {
            AudioClip clip = Resources.Load<AudioClip>("Audio/Dialogues/" + messageToDisplay.audioFileName);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Không tìm thấy âm thanh: " + messageToDisplay.audioFileName);
            }
        }
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended!");
            isActive = false;
            backgroundBox.gameObject.SetActive(false);
            SceneManager.LoadScene("World 1-1");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive)
        {
            NextMessage();
        }

        if (Input.GetMouseButtonDown(0) && isActive) // Click chuột trái để hiển thị lời thoại tiếp theo
        {
            NextMessage();
        }
    }
}
