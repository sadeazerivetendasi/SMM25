using UnityEngine;

public class ChatDialogManager : MonoBehaviour
{
    public Transform messageContainer;
    public GameObject messagePrefabMe,messagePrefabYou;
    ChatData chatData;
    int activeIndex;
    void Start()
    {
        for (int i = 0; i < activeIndex; i++)
        {
            if (chatData.dialogData[i].chooseCharacter == DialogData.ChooseCharacter.Me)
            {
                Instantiate(messagePrefabMe, messageContainer);
            }
            else
            {
                Instantiate(messagePrefabYou, messageContainer);
            }
        }
    }

    void Update()
    {
        
    }
}
