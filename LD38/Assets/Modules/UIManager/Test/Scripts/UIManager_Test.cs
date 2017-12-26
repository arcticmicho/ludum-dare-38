using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameModules;

public class UIManager_Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(UITest());
    }

    public IEnumerator UITest()
    {
        yield return new WaitForSeconds(1.0f);

        Debug.LogWarning("Showing Test Popup");
        var popup = UIManager.Instance.RequestPopup<UIPanel_Test>(UIPanelPriority.Medium, UIPanelFlags.None);
        popup.Show();

        yield return new WaitForSeconds(2.0f);

        UIManager.Instance.ClosePopup(popup);
    }
}

