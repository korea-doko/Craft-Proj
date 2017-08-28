using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemInventoryPanel : MonoBehaviour {

    [SerializeField] private bool m_isActive;

    internal void Init()
    {
        Hide();
    }

    public void Show()
    {
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
}
