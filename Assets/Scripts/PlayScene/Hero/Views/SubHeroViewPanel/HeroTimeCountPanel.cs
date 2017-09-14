using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroTimeCountPanel : MonoBehaviour {

    [SerializeField] private Text m_text;
	
    public void Init()
    {
        m_text.text = "time";
    }

    internal void Show(HeroModel model)
    {
        Debug.Log("업데이트 계속 시켜서 바꿔야 한다...");
    }
    
    public void UpdateRegenTime(int currentTime, int regenTime)
    {
        m_text.text = currentTime.ToString() + " / " + regenTime.ToString();             
    }
}
