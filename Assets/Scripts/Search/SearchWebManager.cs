using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SearchWebManager : MonoBehaviour
{
    [HideInInspector] public SearchData searchData;
    [HideInInspector] public PageManager pageManager;
    public TMP_Text linkText;
    public Image bookmarkImage;
    public Sprite normalBookmark, selectBookmark;
    public void CreateBookmarks()
    {
        if (SearchManager.Instance.CreateBookmarksFunction(searchData, pageManager))
        {
            BookmarksSpriteChange(true);
            pageManager.visitBookmarksTransition.BookmarksSpriteChange(true);
        }
        else
        {
            BookmarksSpriteChange(false);
            pageManager.visitBookmarksTransition.BookmarksSpriteChange(false);
        }
    }
    public void BookmarksSpriteChange(bool Bookmarks)
    {
        if (Bookmarks)
        {
            bookmarkImage.sprite = selectBookmark;
        }
        else bookmarkImage.sprite = normalBookmark;
    }
}
