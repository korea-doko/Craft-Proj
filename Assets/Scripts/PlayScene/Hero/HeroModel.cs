using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;



[System.Serializable]
public struct HeroClassSt
{
    public int m_id;
    public int m_givenID;
    public EHeroClass m_heroClass;

    public HeroClassSt(int _id, int _givenId)
    {
        m_id = _id;
        m_givenID = _givenId;
        m_heroClass = (EHeroClass)m_givenID;
    }

    public int GetID(int _givenId)
    {
        if (_givenId != m_givenID)
            return -1;

        return m_id;
    }
    public int GetGivenID(int _id)
    {
        if (_id != m_id)
            return -1;

        return m_givenID;
    }
    public EHeroClass GetModType()
    {
        return m_heroClass;
    }
}

public interface IHeroModel : IModel
{

}
public class HeroModel : MonoBehaviour, IHeroModel
{
    // XML에서 읽어온 것들

    [SerializeField] List<Dictionary<string, string>> m_fullDic;
    [SerializeField] private List<HeroClassSt> m_heroClassStList;
    [SerializeField] private List<BaseHeroData> m_baseHeroDataList;
    [SerializeField] private List<string> m_heroNameList;
    
    
    public void InitModel()
    {
        m_fullDic = new List<Dictionary<string, string>>();
                       
        ReadHeroClassFromXML();
        MakeHeroClassStList();
        // 히어로 클래스 ID, GivenID 가져와서 초기화

        ReadBaseHeroStatsFromXML();
        MakeBaseHeroStatsList();
        // 히어로 기본 스테이터스 초기화

        ReadHeroNameFromXML();
        MakeHeroNameList();
        // 히어로 이름 테이블 초기화      
    }

   

    public string GetRandomHeroName()
    {
        int count = m_heroNameList.Count;
        int rand = UnityEngine.Random.Range(0, count);
        return m_heroNameList[rand];
    }
    public BaseHeroData GetRandomBaseHeroData()
    {
        int rand = UnityEngine.Random.Range(0, m_baseHeroDataList.Count);

        BaseHeroData baseHeroData = m_baseHeroDataList[rand];

        return baseHeroData;
    }


    /// XML 로드 해오기

    private void ReadHeroClassFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/HeroClass");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("HeroClass");

        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "GivenID":
                        partialDic.Add("GivenID", content.InnerText);
                        break;
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
                    case "Class":
                        partialDic.Add("Class", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    private void MakeHeroClassStList()
    {
        m_heroClassStList = new List<HeroClassSt>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];
            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);

            HeroClassSt st = new HeroClassSt(id, givenID);
            m_heroClassStList.Add(st);
        }

        m_fullDic.Clear();

      
    }

    private void ReadBaseHeroStatsFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/HeroStats");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("HeroStats");

        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "Class":
                        partialDic.Add("Class", content.InnerText);
                        break;
                    case "BaseMinStr":
                        partialDic.Add("BaseMinStr", content.InnerText);
                        break;
                    case "BaseMaxStr":
                        partialDic.Add("BaseMaxStr", content.InnerText);
                        break;
                    case "BaseMinDex":
                        partialDic.Add("BaseMinDex", content.InnerText);
                        break;
                    case "BaseMaxDex":
                        partialDic.Add("BaseMaxDex", content.InnerText);
                        break;
                    case "BaseMinInt":
                        partialDic.Add("BaseMinInt", content.InnerText);
                        break;
                    case "BaseMaxInt":
                        partialDic.Add("BaseMaxInt", content.InnerText);
                        break;
                    case "OffsetMinStr":
                        partialDic.Add("OffsetMinStr", content.InnerText);
                        break;
                    case "OffsetMaxStr":
                        partialDic.Add("OffsetMaxStr", content.InnerText);
                        break;
                    case "OffsetMinDex":
                        partialDic.Add("OffsetMinDex", content.InnerText);
                        break;
                    case "OffsetMaxDex":
                        partialDic.Add("OffsetMaxDex", content.InnerText);
                        break;
                    case "OffsetMinInt":
                        partialDic.Add("OffsetMinInt", content.InnerText);
                        break;
                    case "OffsetMaxInt":
                        partialDic.Add("OffsetMaxInt", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    private void MakeBaseHeroStatsList()
    {
        m_baseHeroDataList = new List<BaseHeroData>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int classID = int.Parse(dic["Class"]);
            EHeroClass heroClass = EHeroClass.None;

            for (int j = 0; j < m_heroClassStList.Count; j++)
            {
                HeroClassSt st = m_heroClassStList[j];
                int classGivenID = st.GetGivenID(classID);

                if (classGivenID == -1)
                    continue;

                heroClass = (EHeroClass)classGivenID;
                break;
            }
            int baseMinStr = int.Parse(dic["BaseMinStr"]);
            int baseMaxStr = int.Parse(dic["BaseMaxStr"]);
            int baseMinDex = int.Parse(dic["BaseMinDex"]);
            int baseMaxDex = int.Parse(dic["BaseMaxDex"]);
            int baseMinInt = int.Parse(dic["BaseMinInt"]);
            int baseMaxInt = int.Parse(dic["BaseMaxInt"]);

            int offsetMinStr = int.Parse(dic["OffsetMinStr"]);
            int offsetMaxStr = int.Parse(dic["OffsetMaxStr"]);
            int offsetMinDex = int.Parse(dic["OffsetMinDex"]);
            int offsetMaxDex = int.Parse(dic["OffsetMaxDex"]);
            int offsetMinInt = int.Parse(dic["OffsetMinInt"]);
            int offsetMaxInt = int.Parse(dic["OffsetMaxInt"]);

            Attribute baseMinAttr = new Attribute(baseMinStr, baseMinDex, baseMinInt);
            Attribute baseMaxAttr = new Attribute(baseMaxStr, baseMaxDex, baseMaxInt);

            Attribute offsetMinAttr = new Attribute(offsetMinStr, offsetMinDex, offsetMinInt);
            Attribute offsetMaxAttr = new Attribute(offsetMaxStr, offsetMaxDex, offsetMaxInt);

            BaseHeroData baseHeroData = new BaseHeroData(heroClass, baseMinAttr, baseMaxAttr, offsetMinAttr, offsetMaxAttr);

            m_baseHeroDataList.Add(baseHeroData);
        }

        m_fullDic.Clear();
    }

    private void ReadHeroNameFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/HeroName");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("HeroName");

        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
                    case "HeroNames":
                        partialDic.Add("HeroNames", content.InnerText);
                        break;
                }
            }

            m_fullDic.Add(partialDic);
        }
    }
    private void MakeHeroNameList()
    {
        m_heroNameList = new List<string>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];
            string name = dic["HeroNames"];
            m_heroNameList.Add(name);
        }

        m_fullDic.Clear();
    }

  
}

