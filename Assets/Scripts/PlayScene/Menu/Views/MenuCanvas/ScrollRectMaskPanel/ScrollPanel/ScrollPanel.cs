using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanel : MonoBehaviour, IUpdatable {

    [SerializeField] private MenuPanel[] m_menuPanelAry;
    [SerializeField] private RectTransform m_rect;
    [SerializeField] private float m_width;
    [SerializeField] private float m_height;
    [SerializeField] private Vector3 m_startPos;
    [SerializeField] private Vector3 m_destPos;
    [SerializeField] private Vector3 m_dir;
    [SerializeField] private bool m_isMoving;
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_moveTime;
    [SerializeField] private float m_passedTime;
    [SerializeField] private int m_currentOrder;
    [SerializeField] private float m_timeCorrectionValue;
    


    public void Init(float _width, float _height)
    {
        m_width = _width;
        m_height = _height;

        m_rect = this.GetComponent<RectTransform>();
        m_rect.pivot = new Vector2(0.0f, 0.5f);

        int numOfMenu = System.Enum.GetNames(typeof(MenuName)).Length;
        m_menuPanelAry = new MenuPanel[numOfMenu];

        MenuPanel[] inchild = this.GetComponentsInChildren<MenuPanel>();

        for (int i = 0; i < numOfMenu; i++)
        {
            m_menuPanelAry[i] = inchild[i];
            m_menuPanelAry[i].Init((MenuName)i);
            m_menuPanelAry[i].ChangeSize(m_width, m_height);
        }

        m_isMoving = false;
        m_dir = Vector3.zero;
        m_moveSpeed = 100.0f;
        m_startPos = Vector3.zero;
        m_destPos = Vector3.zero;
        m_moveTime = 0.25f;
        m_timeCorrectionValue = 1.0f / m_moveTime;
        m_passedTime = 0.0f;
        m_currentOrder = 2;
        
        m_rect.transform.position = new Vector3(- m_currentOrder* m_width, 0.0f, 0.0f);
    }
    public void MovePanelToCenter(MenuName _name)
    {
        
        int inputOrder = (int)_name;

        if (inputOrder == m_currentOrder)
        {
            Debug.Log(_name);                   
            return;
        }
        m_currentOrder = inputOrder;

        float order = (float)(inputOrder);
               
        m_startPos = m_rect.transform.localPosition;
        m_destPos = new Vector3(-order * m_width, 0.0f, 0.0f);
        
        m_dir = m_destPos - m_startPos;
        m_dir.Normalize();

        m_isMoving = true;
        m_passedTime = 0.0f;
    }
    public void UpdateThis()
    {
        if (m_isMoving)
            MovePanel();
    }

   

    void MovePanel()
    {
        m_passedTime += Time.deltaTime;

        if (m_passedTime > m_moveTime)
        {
            m_rect.transform.position = m_destPos;
            m_isMoving = false;
        }
        else
        {
            Vector3 localPos = Vector3.Lerp(m_startPos, m_destPos, m_passedTime * m_timeCorrectionValue);
            m_rect.transform.position = localPos;
        }
    }

    


}
