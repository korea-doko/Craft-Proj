using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerManager  :  IManager
{

}
public class PlayerManager : MonoBehaviour, IPlayerManager
{

    private PlayerModel m_model;
    private PlayerView m_view;
    private static PlayerManager m_inst;
    public static PlayerManager Inst
    {
        get
        {
            return m_inst;
        }
    }
  
    public void InitManager()
    {
        m_inst = this;

        m_model = Utils.MakeGameObjectWithComponent<PlayerModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<PlayerView>(this.gameObject);
        m_view.InitView(m_model);
    }
    
    public void HiredHero(HeroData _data)
    {
        m_model.AddHeroData(_data);
    }
    public List<HeroData> GetOwnedHeroDataList()
    {
        return m_model.HeroList;
    }
    public HeroData GetHeroData(int _id)
    {
        return m_model.HeroList[_id];
    }

    internal void EquipItemWith()
    {
        throw new NotImplementedException();
    }
}
