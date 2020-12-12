using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public static bool isSelectionClosed = true;
    [SerializeField]
    GameObject SelectionMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && isSelectionClosed)
        {
            isSelectionClosed = false;
            SelectionMenu.SetActive(true);
            Time.timeScale = 0.1f;
        }
        else if (Input.GetKeyUp(KeyCode.Tab) && !isSelectionClosed)
        {
            isSelectionClosed = true;
            SelectionMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
