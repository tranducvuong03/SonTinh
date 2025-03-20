using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Message[] messages;
	public Actor[] actors;

	void Start()
	{
		// Khởi tạo danh sách nhân vật
		actors = new Actor[1];
		actors[0] = new Actor { name = "Vua Hùng" };

		// Khởi tạo danh sách lời thoại
		messages = new Message[6];
		messages[0] = new Message { actorId = 0, message = "Hỡi các vị anh hùng khắp bốn phương!", audioFileName = "voice-1" };
		messages[1] = new Message { actorId = 0, message = "Ta có một ái nữ tên là Mị Nương, dung nhan tuyệt sắc, đức hạnh vẹn toàn.", audioFileName = "voice-2" };
		messages[2] = new Message { actorId = 0, message = "Nay, Ta muốn tìm một người tài giỏi để kết duyên cùng con gái ta.", audioFileName = "voice-3" };
		messages[3] = new Message { actorId = 0, message = "Ai có thể chứng tỏ được tài năng hơn người, ta sẽ chọn làm phò mã!", audioFileName = "voice-4" };
		messages[4] = new Message { actorId = 0, message = "Sính lễ bao gồm: Một trăm ván cơm nếp, hai trăm nệp bánh chưng, voi chín ngà,\\ngà chín cựa, ngựa chín hồng mao.", audioFileName = "voice-5" };
		messages[5] = new Message { actorId = 0, message = "Các khanh, hãy lên đường ngay!", audioFileName = "voice-6" };

		// Tự động kích hoạt hội thoại
		FindAnyObjectByType<DialogueManager>().InitializeDialogue(messages, actors);
	}

	public void SkipDialogue()
	{
		FindAnyObjectByType<DialogueManager>().SkipDialogue();
	}
}

[System.Serializable]
public class Message
{
	public int actorId;
	public string message;
	public string audioFileName;
}

[System.Serializable]
public class Actor
{
	public string name;
	public Sprite sprite;
}
