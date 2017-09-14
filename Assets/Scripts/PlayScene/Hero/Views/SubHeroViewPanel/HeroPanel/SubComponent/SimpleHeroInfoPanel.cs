using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimpleHeroInfoPanel : MonoBehaviour {

    [SerializeField] private Text m_text;
	
    internal void Init()
    {
        m_text.text = "내용 없음";
    }

    internal void Show(HeroData m_heroData)
    {
        m_text.text = "간단한 정보 \n 이름 : " + m_heroData.GetName;
    }

}
