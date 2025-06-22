using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "NewCommentData", menuName = "DarkWeb/CommentData", order = 0)]
public class CommentData : ScriptableObject
{
    public LocalizedString Customer, Comment, Data;
    public int Star; 
}
