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
    [SerializeField] private Text m_text;

    public event EventHandler OnItemSelectButtonClicked;

    public void Init()
    {
        m_button.onClick.AddListener(() => OnItemSelectButtonClicked(this, EventArgs.Empty));
        m_text.text = "강화할 아이템을 선택하세요.";
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
