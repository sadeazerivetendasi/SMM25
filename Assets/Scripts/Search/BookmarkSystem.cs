using System.Collections.Generic;
using UnityEngine;

public class BookmarkSystem : MonoBehaviour
{
    [SerializeField] private Transform bookmarkContainer;
    [SerializeField] private GameObject bookmarkPrefab;
    [SerializeField] private List<SearchData> activeBookmarks;
    [SerializeField] private List<BookmarkRedirect> activeBookmarksList;
    private void CreateBookmark(SearchData searchData)
    {
        BookmarkRedirect newBookmark = Instantiate(bookmarkPrefab, bookmarkContainer).GetComponent<BookmarkRedirect>();
        newBookmark.SetRedirectText(searchData.Title);
        newBookmark.searchData = searchData;
        searchData.IsBookmark = true;
        activeBookmarks.Add(searchData);
        activeBookmarksList.Add(newBookmark);
    }
    void DelBookmark(SearchData searchData)
    {
        int b = activeBookmarks.IndexOf(searchData);
        if (b != -1 && b < activeBookmarksList.Count)
        {
            activeBookmarks.RemoveAt(b);

            Destroy(activeBookmarksList[b].gameObject);
            activeBookmarksList.RemoveAt(b);
        }
    }
    public bool CreateBookmarkFunc(SearchData searchData)
    {
        if (activeBookmarks.Count != 0)
        {
            if (!activeBookmarks.Contains(searchData))
            {
                CreateBookmark(searchData);
                return true;
            }
            else
            {
                DelBookmark(searchData);
                return false;
            }
        }
        else
        {
            CreateBookmark(searchData);
            return true;
        }
    }
}
