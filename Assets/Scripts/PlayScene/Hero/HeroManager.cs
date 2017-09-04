using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HeroDataWithLimitedTime
{
    public HeroData m_heroData;
    public int m_limitedTime;

    public HeroDataWithLimitedTime(HeroData _heroData, int _limitedTime)
    {
        m_heroData = _heroData;
        m_limitedTime = _limitedTime;
    }
}

public interface IHeroManager  : IManager, ILoadable,IUpdatable
{

}
public class HeroManager : MonoBehaviour,IHeroManager
{
    [SerializeField] private int m_regenTime;
    [SerializeField] private int m_currentTime;
    [SerializeField] private float m_passedTime;

    [SerializeField] private bool m_isActiveNow;
    [SerializeField] private int m_clickedID;
    [SerializeField] private List<HeroDataWithLimitedTime> m_availableHeroDataList;
    [SerializeField] private int m_maxNumOfAvailableHero;

    [SerializeField] private bool m_isLoaded;

    private HeroModel m_model;
    private HeroView m_view;

    private static HeroManager m_inst;
    public static HeroManager Inst
    {
        get
        {
            return m_inst;
        }        
    }
    public HeroManager()
    {
        m_inst = this;
    }

    public void InitManager()
    {
        m_regenTime = 10;
        m_currentTime = m_regenTime;
        m_passedTime = 0.0f;
        m_isActiveNow = false;        
        m_clickedID = -1;
        m_availableHeroDataList = new List<HeroDataWithLimitedTime>();
        m_maxNumOfAvailableHero = 8;
        m_isLoaded = false;

        m_model = Utils.MakeGameObjectWithComponent<HeroModel>(this.gameObject);
        m_model.InitModel();


        m_view = Utils.MakeGameObjectWithComponent<HeroView>(this.gameObject);
        m_view.InitView(m_model);
        m_view.OnHeroPanelClicked += M_view_OnHeroPanelClicked;
        m_view.OnHeroDetailPanelBuyBtnClicked += M_view_OnHeroDetailPanelBuyBtnClicked;


    }
    public bool Load()
    {
        
        if( !m_isLoaded )
        {

            for (int i = 0; i < 5; i++)
                MakeAvailableHeroData();

            m_view.ShowHeroNumCount(m_availableHeroDataList.Count, m_maxNumOfAvailableHero);

            m_isLoaded = true;
        }

        return m_view.Load();
    }   
    public void MenuButtonClicked(MenuName menuName)
    {
        if (menuName == MenuName.Hero)
        {
            m_isActiveNow = true;
            m_view.ShowHeroPanel(m_availableHeroDataList);
        }
        else
        {
            m_isActiveNow = false;
            m_view.Hide();        
        }
    }
    public void UpdateThis()
    {        
        if( CheckPassingASecond())
        {
            // 1초 지나갔음, 
            // 제한 시간이 다 되었나 확인한다.
            CheckTimeInAvailableList();

            // 히어로 리젠 시간이 되었는지 체크한다.
            CheckRegenTime();

         
        }
    }


    private void MakeAvailableHeroData(int _time = 0)
    {
        int time = _time == 0 ? UnityEngine.Random.Range(10, 22) : _time;

        BaseHeroData data = m_model.GetRandomBaseHeroData();
        string name = m_model.GetRandomHeroName();
        EHeroClass heroClass = data.GetHeroClass;
        Attribute baseAttr = data.GetBaseAttr;
        Attribute offsetAttr = data.GetOffsetAttr;

        HeroData heroData = new HeroData(name, heroClass, baseAttr, offsetAttr);


        Debug.Log("랜덤하게 아이템 일단 끼워준다.");

        ItemData armor = ItemManager.Inst.GenerateArmor();
        ItemData weapon = ItemManager.Inst.GenerateWeapon();
        ItemData misc = ItemManager.Inst.GenerateMisc();

        heroData.EquipItemWith(armor);
        heroData.EquipItemWith(weapon);
        heroData.EquipItemWith(misc);


        HeroDataWithLimitedTime limitedData = new HeroDataWithLimitedTime(heroData, time);

        m_availableHeroDataList.Add(limitedData);
    }
    private HeroData GetHeroData(int _id)
    {
        return m_availableHeroDataList[_id].m_heroData;
    }
    private void RemoveAvailableHeroData(int _id)
    {
        m_availableHeroDataList.RemoveAt(_id);
    }
    private bool CheckPassingASecond()
    {
        m_passedTime += Time.deltaTime;

        if( m_passedTime > 1.0f)
        {
            m_passedTime -= 1.0f;

            return true;
        }

        return false;
    }

    private void CheckRegenTime()
    {
        m_currentTime--;

        if (m_currentTime < 0)
        {
            m_currentTime = m_regenTime;

            if (m_availableHeroDataList.Count < m_maxNumOfAvailableHero)
            {
                MakeAvailableHeroData();
                m_view.ShowHeroPanel(m_availableHeroDataList);
            }
            else
                Debug.Log("갯수 꽉참");

            m_view.ShowHeroNumCount(m_availableHeroDataList.Count, m_maxNumOfAvailableHero);
        }

        //시간 지나간 것을 표시해준다.
        m_view.UpdateRegenTime(m_currentTime, m_regenTime);
    }
    private void CheckTimeInAvailableList()
    {
        // 리스트에 있는 애들 1초식 줄이자.

        for (int i = 0; i < m_availableHeroDataList.Count; i++)
        {
            HeroDataWithLimitedTime data = m_availableHeroDataList[i];
            data.m_limitedTime--;
        }

        // 남은 시간이 0 이면 없앤다.
        for(int i = m_availableHeroDataList.Count -1; i >= 0;i--)
        {
            HeroDataWithLimitedTime data = m_availableHeroDataList[i];

            if (data.m_limitedTime <= 0)
            {
                if (m_clickedID == i)
                    m_view.HideDetailPanel();

                m_availableHeroDataList.RemoveAt(i);
            }
        }

        m_view.ShowHeroPanel(m_availableHeroDataList);

        m_view.ShowHeroNumCount(m_availableHeroDataList.Count, m_maxNumOfAvailableHero);
    }
   
    /// 메시지 핸들러

    private void M_view_OnHeroDetailPanelBuyBtnClicked(object sender, EventArgs e)
    {
        HeroData heroData = GetHeroData(m_clickedID);
        RemoveAvailableHeroData(m_clickedID);

        m_view.ShowHeroPanel(m_availableHeroDataList);
        m_view.ShowHeroNumCount(m_availableHeroDataList.Count, m_maxNumOfAvailableHero);

        PlayerManager.Inst.HiredHero(heroData);
    }
    private void M_view_OnHeroPanelClicked(object sender, HeroPanelClickedArgs e)
    {
        m_clickedID = e.m_clickedID;

        HeroData heroData = GetHeroData(m_clickedID);

        m_view.ShowDetailPanel(heroData);
    }
}
