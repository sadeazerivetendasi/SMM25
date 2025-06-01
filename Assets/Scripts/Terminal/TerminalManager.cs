using UnityEngine;

public class TerminalManager : MonoBehaviour
{
    public static TerminalManager Instance;
    public Transform commandTransform;
    public GameObject commandPrefab;
    void Awake()
    {
        Instance = this;
    }
    public void NewCommand(string command)
    {
        TerminalCommandScript terminalCommandScript = Instantiate(commandPrefab, commandTransform).GetComponent<TerminalCommandScript>();
        terminalCommandScript.SetText(command,"et");
    }
}
