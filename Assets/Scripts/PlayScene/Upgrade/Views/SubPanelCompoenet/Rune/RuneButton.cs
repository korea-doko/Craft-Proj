using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class RuneButtonClickArgs : EventArgs
{
    public RuneType m_clickRune;
    public RuneButtonClickArgs(RuneType _name)
    {
        m_clickRune = _name;
    }
}
public class RuneButtonLongPressedArgs:EventArgs
{
    public RuneType m_pressedRune;
    public RuneButtonLongPressedArgs(RuneType _type)
    {
        m_pressedRune = _type;
    }
}

public interface IRuneButton : IUpdatable,IPointerDownHandler,IPointerUpHandler,IAlarmTrigger
{
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;
}

[System.Serializable]
public class RuneButton : MonoBehaviour, IRuneButton
{

    [SerializeField] private LayoutElement m_layoutEle;
    [SerializeField] private float m_width;
    [SerializeField] private float m_height;
    [SerializeField] private Button m_btn;
    [SerializeField] private int m_id;
    [SerializeField] private RuneType m_runeType;
    [SerializeField] private Text m_imageText;
    [SerializeField] private Text m_numText;
    [SerializeField] private bool m_isPressed;
    [SerializeField] private float m_pressedTime;

    public RuneType RuneType
    {
        get
        {
            return m_runeType;
        }
    }

    public string GetAlarmName
    {
        get
        {
            return m_runeType.ToString();
        }
    }
    public string GetAlarmDesc
    {
        get { return m_runeType.ToString() + "설명, 룬 객체화 필요"; }
    }

    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    public event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;

    public void Init(int _id)
    {
        m_layoutEle = this.GetComponent<LayoutElement>();
        m_btn = this.GetComponent<Button>();
        m_id = _id;
        m_runeType = (RuneType)m_id;

        RuneData runeData = ItemManager.Inst.GetRuneData((RuneType)_id);
        m_imageText.text = runeData.GetTextImage;
    }

    public void UpdateThis()
    {
        if( m_isPressed )
        {
            m_pressedTime += Time.deltaTime;

            if( m_pressedTime > 0.5f)
            {
                m_isPressed = false;
                OnRuneButtonLongPressed(this, new RuneButtonLongPressedArgs(m_runeType));
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        m_pressedTime = 0.0f;
        m_isPressed = true;
    }

    public void Show(int _num)
    {
        m_numText.text = _num.ToString();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_pressedTime <= 0.5f)
            OnRuneButtonClicked(this, new RuneButtonClickArgs(m_runeType));

        m_isPressed = false;
    }
    public void SetSize(Vector2 _size)
    {
        m_width = _size.x;
        m_height = _size.y;

        m_layoutEle.preferredHeight = m_height;
        m_layoutEle.preferredWidth = m_width;
    }
   
}
