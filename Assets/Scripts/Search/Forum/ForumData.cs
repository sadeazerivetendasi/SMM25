using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "NewForumData", menuName = "Search/ForumData", order = 0)]
public class ForumData : ScriptableObject {
    public Sprite personLogo;
    public enum ForumUser
    {
        CyberNomad99,
        DeepWebVet,
        ParanoidPete,
        BakuLocal,
        VendorWatcher,
        NewbieQ,
        OpSecMaster,
        DarkTales,
        SkepticalSam,
        TechInsider,
        GhostProtocol
    }
    public ForumUser personName;
    public LocalizedString personDescription, personInfo, messageData, messageBox;
}
