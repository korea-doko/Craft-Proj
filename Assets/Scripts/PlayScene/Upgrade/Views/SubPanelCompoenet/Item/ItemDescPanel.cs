using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescPanel : MonoBehaviour {

    [SerializeField] private Text m_descText;

	public void Init()
    {
        m_descText = this.GetComponentInChildren<Text>();
        m_descText.text = "Initialized";
    }

    internal void ShowSelectedItem(ItemData itemData)
    {
        m_descText.text = itemData.GetItemInfo();
    }
}
