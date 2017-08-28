using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RuneButtonClickArgs : EventArgs
{
    public RuneName m_clickRune;

    public RuneButtonClickArgs(RuneName _name)
    {
        m_clickRune = _name;
    }

}

public interface IRuneButton
{
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
}

[System.Serializable]
public class RuneButton : MonoBehaviour, IRuneButton
{

    [SerializeField] private LayoutElement m_layoutEle;
    [SerializeField] private float m_width;
    [SerializeField] private float m_height;
    [SerializeField] private Button m_btn;
    [SerializeField] private int m_id;
    [SerializeField] private RuneName m_runeName;
    [SerializeField] private Text m_text;

    public RuneName RuneName
    {
        get
        {
            return m_runeName;
        }
    }

    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;

    public void Init(int _id)
    {
        m_layoutEle = this.GetComponent<LayoutElement>();
        m_btn = this.GetComponent<Button>();
        m_text = this.GetComponentInChildren<Text>();


        m_id = _id;
        m_runeName = (RuneName)m_id;
        m_text.text = m_runeName.ToString();
            

        m_btn.onClick.AddListener(() => OnRuneButtonClicked(this, new RuneButtonClickArgs(m_runeName)));
    }

    public void SetCellSize(Vector2 _cellSize)
    {
        m_width = _cellSize.x;
        m_height = _cellSize.y;

        m_layoutEle.preferredHeight = m_height;
        m_layoutEle.preferredWidth = m_width;
    }
}
