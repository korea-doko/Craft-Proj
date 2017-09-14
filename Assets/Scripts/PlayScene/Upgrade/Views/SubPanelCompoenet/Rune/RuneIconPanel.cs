using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IRuneIconPanel:IUpdatable
{
    event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;
}

[System.Serializable]
public class RuneIconPanel : MonoBehaviour, IRuneIconPanel
{
    [SerializeField] private List<RuneButton> m_runeButtonList;
    [SerializeField] private RectTransform m_rect;
    
    public event EventHandler<RuneButtonClickArgs> OnRuneButtonClicked;
    public event EventHandler<RuneButtonLongPressedArgs> OnRuneButtonLongPressed;

    public void Init()
    {
        m_rect = this.GetComponent<RectTransform>();
        
        for(int i = 0; i < m_runeButtonList.Count;i++)
        {
            RuneButton btn = m_runeButtonList[i];
            btn.Init(i);

            btn.OnRuneButtonClicked += Btn_OnRuneButtonClicked;
            btn.OnRuneButtonLongPressed += Btn_OnRuneButtonLongPressed;
        }       
    }

    

    public void Show(int[] _playerOwnedRunes)
    {
        for (int i = 0; i < _playerOwnedRunes.Length; i++)
        {
            RuneButton runeBtn = m_runeButtonList[i];
            runeBtn.Show(_playerOwnedRunes[i]);
        }

    }
    public void UpdateThis()
    {
        foreach (RuneButton btn in m_runeButtonList)
            btn.UpdateThis();
    }

   
    // 이벤트 핸들러
    private void Btn_OnRuneButtonClicked(object sender, RuneButtonClickArgs e)
    {
        OnRuneButtonClicked(this, e);
    }
    private void Btn_OnRuneButtonLongPressed(object sender, RuneButtonLongPressedArgs e)
    {
        OnRuneButtonLongPressed(this, e);
    }



}
