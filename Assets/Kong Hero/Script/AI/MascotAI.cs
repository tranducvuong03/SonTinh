using TMPro;
using UnityEngine;
using System.Collections;

public class MascotAI : MonoBehaviour
{
    public static bool isInteracting = false; // Biến tĩnh để kiểm tra hội thoại

    public GameObject mascot; // Linh vật
    public TextMeshProUGUI messageText; // Nội dung hội thoại
    public TextMeshProUGUI speakerNameText; // Tên người nói
    public string[] dialogues; // Danh sách câu thoại
    public string[] speakers; // Danh sách người nói (Player hoặc Mascot)

    private int currentDialogueIndex = 0;
    private bool hasInteracted = false;
    private bool isTyping = false; // Kiểm soát hiệu ứng gõ chữ

    public Controller2D controller2D; // Điều khiển nhân vật

    private void Start()
    {
        mascot.SetActive(false);
        messageText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInteracting && !hasInteracted)
        {
            hasInteracted = true; // Chỉ kích hoạt một lần
            StartCoroutine(StartDialogue());
        }
    }

    private IEnumerator StartDialogue()
    {
        isInteracting = true;
        mascot.SetActive(true);
        messageText.gameObject.SetActive(true);
        speakerNameText.gameObject.SetActive(true);
        controller2D.enabled = false; // Ngăn nhân vật di chuyển

        while (currentDialogueIndex < dialogues.Length)
        {
            // Hiện từng chữ, chờ hoàn thành trước khi tiếp tục
            yield return StartCoroutine(TypeDialogue(speakers[currentDialogueIndex], dialogues[currentDialogueIndex]));

            // Chờ bấm Space trước khi chuyển câu tiếp theo
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.K) && !isTyping);
            currentDialogueIndex++; // Chỉ tăng khi đã bấm Space và hoàn thành gõ chữ
        }

        EndDialogue();
    }

    private IEnumerator TypeDialogue(string speaker, string dialogue)
    {
        speakerNameText.text = "";
        messageText.text = ""; // Xóa nội dung cũ

        isTyping = true;

        // Hiệu ứng gõ từng chữ của tên người nói
        foreach (char letter in speaker.ToCharArray())
        {
            speakerNameText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        speakerNameText.text = "";
        yield return new WaitForSeconds(0.2f); // Dừng nhẹ sau khi hiển thị tên

        // Hiệu ứng gõ từng chữ của hội thoại
        foreach (char letter in dialogue.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.05f);

            if (Input.GetKeyDown(KeyCode.L)) // Nếu bấm Space, hiện toàn bộ ngay lập tức
            {
                messageText.text = dialogue;
                speakerNameText.text = speaker;
                break;
            }
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        mascot.SetActive(false);
        messageText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);
        controller2D.enabled = true; // Cho phép nhân vật di chuyển lại
        isInteracting = false;
    }
}
