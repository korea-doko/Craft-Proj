using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IRuneIconPanel
{
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
}

[System.Serializable]
public class RuneIconPanel : MonoBehaviour, IRuneIconPanel
{
    [SerializeField] private List<RuneButton> m_runeButtonList;
    [SerializeField] private GridLayoutGroup m_gridLayout;
    [SerializeField] private RectTransform m_rect;
    [SerializeField] private float m_cellWidth;
    [SerializeField] private float m_cellHeight;
    [SerializeField] private bool m_isCellSizeInit;

    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;

    public void Init()
    {
        
        m_gridLayout = this.GetComponent<GridLayoutGroup>();

        m_rect = this.GetComponent<RectTransform>();

        m_rect.anchorMax = new Vector2(1.0f, 1.0f);
        m_rect.anchorMin = new Vector2(0.0f, 0.8f);

        m_rect.offsetMax = Vector2.zero;
        m_rect.offsetMin = Vector2.zero;

        m_runeButtonList = new List<RuneButton>();

        GameObject runeButtonPrefab = Resources.Load("PlayScene/Upgrade/RuneButton") as GameObject;

        int numOfRune = System.Enum.GetNames(typeof(RuneName)).Length;

        for (int i = 0; i < numOfRune; i++)
        {
            RuneButton btn = ((GameObject)Instantiate(runeButtonPrefab)).GetComponent<RuneButton>();
            btn.Init(i);
            btn.transform.SetParent(this.transform);
            btn.OnRuneButtonClicked += Btn_OnRuneButtonClicked;
            m_runeButtonList.Add(btn);
        }

        m_cellWidth = -1;
        m_cellHeight = -1;


        InitCellSize();

    }

    private void Btn_OnRuneButtonClicked(object sender, RuneButtonClickArgs e)
    {
        OnRuneButtonClicked(this, e);
    }

    void InitCellSize()
    {
        m_cellWidth = m_rect.rect.width / 6;
        m_cellHeight = m_rect.rect.height / 2;

        m_gridLayout.cellSize = new Vector2(m_cellWidth, m_cellHeight);

        for (int i = 0; i < m_runeButtonList.Count; i++)
            m_runeButtonList[i].SetCellSize(m_gridLayout.cellSize);

        if (m_cellWidth != 0.0f && m_cellHeight != 0.0f)
            m_isCellSizeInit = true;
        else
            m_isCellSizeInit = false;
    }

    internal void Show()
    {
        if (!m_isCellSizeInit)
            InitCellSize();

    }
}
