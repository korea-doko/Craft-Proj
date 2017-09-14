using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class btntest : MonoBehaviour,IPointerDownHandler, IPointerUpHandler {

    public Button m_btn;

    public bool m_isBtnDowned;
    public float m_downedTime;

    private void Start()
    {
        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener(() => ListenerTest());

        m_isBtnDowned = false;
        m_downedTime = 0.0f;        
    }
    public void Update()
    {
        if (m_isBtnDowned)
        {
            m_downedTime += Time.deltaTime;

            if (m_downedTime >= 1.5f)
            {
                m_isBtnDowned = false;
                PopUp();
            }
        }
    }

    private void PopUp()
    {
        testmanager.Inst.Popup();
    }

    public void ListenerTest()
    {
        if (m_downedTime >= 1.5f)
            Debug.Log("안나와");
        else
            Debug.Log("리스너");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_isBtnDowned = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_isBtnDowned = false;
        m_downedTime = 0.0f;
    }
}
