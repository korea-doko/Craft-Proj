using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AlarmPanel : MonoBehaviour , ILoadable{

    [SerializeField] private RectTransform m_rect;
    [SerializeField] private bool m_isActive;

    [SerializeField] private Text m_nameText;
    [SerializeField] private Text m_descText;
    [SerializeField] private Button m_backBtn;

    public void Init()
    {
        m_rect = this.GetComponent<RectTransform>();

        m_backBtn.onClick.AddListener(() => Hide());
    }
    public bool Load()
    {
        m_rect.anchorMin = new Vector2(0.0f, 0.0f);
        m_rect.anchorMax = new Vector2(1.0f, 0.9f);

        m_rect.offsetMin = Vector2.zero;
        m_rect.offsetMax = Vector2.zero;

        Hide();

        return true;
    }
    public void Show(IAlarmTrigger _trigger)
    {
        m_nameText.text = _trigger.GetAlarmName;
        m_descText.text = _trigger.GetAlarmDesc;

        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
}
