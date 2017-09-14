using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


[System.Serializable]
public struct ModTypeSt
{
    public int m_id;
    public int m_givenID;
    public ModType m_modType;

    public ModTypeSt(int _id, int _givenId)
    {
        m_id = _id;
        m_givenID = _givenId;
        m_modType = (ModType)m_givenID;
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
    public ModType GetModType()
    {
        return m_modType;
    }
}
public interface IModManager : IManager
{

}
public class ModManager : MonoBehaviour, IModManager
{

    [SerializeField] List<Dictionary<string, string>> m_fullDic;
    [SerializeField] private List<ModTypeSt> m_modTypeList;
    [SerializeField] private int m_countOfModList;

    private static ModManager m_inst;
    public static ModManager Inst
    {
        get
        {
            return m_inst;
        }
    }
    public ModManager()
    {
        m_inst = this;
    }


    public void InitManager()
    {
        m_fullDic = new List<Dictionary<string, string>>();

        ReadModTypeFromXML();
        MakeModTypeList();

    }

    void ReadModTypeFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/ModType");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("ModType");

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
                    case "GivenID":
                        partialDic.Add("GivenID", content.InnerText);
                        break;
                    case "Type":
                        partialDic.Add("Type", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeModTypeList()
    {
        m_modTypeList = new List<ModTypeSt>();
        m_countOfModList = m_fullDic.Count;

        for (int i = 0; i < m_countOfModList; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];
            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);

            ModTypeSt st = new ModTypeSt(id, givenID);
            m_modTypeList.Add(st);
        }
        

        m_fullDic.Clear();
    }

    public ModType GetModTypeUsingID(int _id)
    {
        for(int i = 0; i < m_countOfModList;i++)
        {
            ModTypeSt st = m_modTypeList[i];

            int givenID = st.GetGivenID(_id);

            if (givenID == -1)
                continue;

            return (ModType)givenID;
        }

        Debug.Log("없는게 들어옴");

        return ModType.None;
    }


}
