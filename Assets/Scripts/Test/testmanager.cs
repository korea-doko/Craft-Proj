using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmanager : MonoBehaviour {

    public GameObject m_popupPanel;
    private static testmanager inst;

    public static testmanager Inst
    {
        get
        {
            return inst;
        }
    }
    private void Start()
    {
        inst = this;

        m_popupPanel.gameObject.SetActive(false);
    }

    public void Popup()
    {
        m_popupPanel.gameObject.SetActive(true);
    }
}
