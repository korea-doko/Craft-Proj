using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public interface IItemSelectButton
{
    event EventHandler OnItemSelectButtonClicked;
}

public class ItemSelectButton : MonoBehaviour,IItemSelectButton {

    [SerializeField] private Button m_button;

    public event EventHandler OnItemSelectButtonClicked;

    public void Init()
    {
        m_button = this.GetComponent<Button>();
        m_button.onClick.AddListener(() => OnItemSelectButtonClicked(this, EventArgs.Empty));
    }
}
