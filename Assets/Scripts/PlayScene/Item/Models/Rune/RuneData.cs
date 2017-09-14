using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRuneData 
{
    RuneType GetRuneType { get; }
    string GetName { get; }
    string GetDesc { get; }
    string GetTextImage { get; }
}

[System.Serializable]
public class RuneData : IRuneData , IAlarmTrigger
{
    [SerializeField] private string m_name;
    [SerializeField] private string m_desc;
    [SerializeField] private RuneType m_type;
    [SerializeField] private string m_textImage;
    
    public RuneData(string _name,RuneType _type )
    {
        m_name = _name;
        m_type = _type;

        switch (_type)
        {
            case RuneType.Reinforcement:
                m_desc = "강화의 룬은 <color=blue>매직</color>등급의 아이템의 모드를 1개 추가합니다.";
                break;
            case RuneType.MagicPower:
                m_desc = "마력의 룬은 <color=blue>매직</color>등급의 아이템을 <color=yellow>레어</color>등급의 아이템으로 바꿔줍니다.";
                break;
            case RuneType.Unholy:
                m_desc = "부정의 룬은 <color=yellow>레어</color>등급의 아이템의 모드를 1개 추가합니다.";
                break;
            case RuneType.BlackSmith:
                m_desc = "대장장이의 룬은 <color=white>노말</color>등급의 아이템을 <color=blue>매직</color>등급의 아이템으로 바꿔줍니다.";
                break;
            case RuneType.Luck:
                m_desc = "행운의 룬은 <color=white>노말</color>등급의 아이템을 <color=blue>매직</color> 또는 <color=yellow>레어</color> 또는 <color=orange>유니크</color>등급의 아이템으로 바꿔줍니다.";
                break;
            case RuneType.Wizard:
                m_desc = "마법사의 룬은 <color=white>노말</color>등급의 아이템을 <color=yellow>레어</color>등급의 아이템으로 바꿔줍니다.";
                break;
            case RuneType.Alteration:
                m_desc = "변화의 룬은 <color=blue>매직</color>등급의 아이템에 있는 모든 모드를 무작위로 바꿔줍니다.";
                break;
            case RuneType.Chaos:
                m_desc = "혼돈의 룬은 <color=yellow>레어</color>등급의 아이템에 있는 모든 모드를 무작위로 바꿔줍니다.";
                break;
            case RuneType.Purification:
                m_desc = "정화의 룬은 <color=white>노말</color>등급으로 아이템을 바꿔줍니다.";
                break;
            case RuneType.Void:
                m_desc = "공허의 룬은 <color=blue>매직</color> 또는 <color=yellow>레어</color> 등급의 아이템에 있는 모드 중 하나를 제거합니다.";
                break;
            case RuneType.Divine:
                m_desc = "신성의 룬은 <color=blue>매직</color> 또는 <color=yellow>레어</color> 또는 <color=orange>유니크</color> 등급의 아이템에 있는 모든 모드의 옵션을 무작위로 바꿔줍니다.";
                break;
            case RuneType.Curruption:
                m_desc = "타락의 룬은 아이템의 고유 옵션을 추가합니다. 그리고 더 이상의 아이템의 변경이 불가능합니다.";
                break;
            default:
                m_desc = "안나와";
                break;
        }
    }

    public string GetName { get { return m_name; } }    
    public string GetDesc { get { return m_desc; } }
    public string GetTextImage
    {
        get
        {
            switch (m_type)
            {
                case RuneType.Reinforcement:
                    return " <color=white>[</color> <color=blue>+ @</color> <color=white>]</color>";
                case RuneType.MagicPower:
                    return " <color=white>[</color> <color=blue>+</color><color=yellow> @</color> <color=white>]</color> ";
                case RuneType.Unholy:
                    return " <color=white>[</color> <color=yellow>+ @</color> <color=white>]</color> ";
                case RuneType.BlackSmith:
                    return " <color=white>[</color> <color=white>+</color><color=blue> !</color> <color=white>]</color>";
                case RuneType.Luck:
                    return " <color=white>[</color> <color=white>+</color><color=blue>!</color><color=yellow>!</color><color=orange>!</color> <color=white>]</color>";
                case RuneType.Wizard:
                    return " <color=white>[</color> <color=white>+</color><color=yellow> !</color> <color=white>]</color>";
                case RuneType.Alteration:
                    return " <color=white>[</color> <color=blue>%</color> <color=white>]</color>";
                case RuneType.Chaos:
                    return " <color=white>[</color> <color=yellow>%</color> <color=white>]</color>";
                case RuneType.Purification:
                    return " <color=white>[</color> <color=white>+</color> <color=white>]</color>";
                case RuneType.Void:
                    return " <color=white>[</color> <color=white>-</color> <color=white>]</color>";
                case RuneType.Divine:
                    return " <color=white>[</color><color=blue>%</color><color=yellow>%</color><color=orange>%</color><color=white>]</color>";
                case RuneType.Curruption:
                    return " <color=white>[</color> <color=red>+</color> <color=white>]</color>";
                default:
                    break;
            }

            return "안나와";
        }
    }
    public string GetAlarmName { get { return m_name; } }
    public string GetAlarmDesc { get { return m_desc; } }
    RuneType IRuneData.GetRuneType { get { return m_type; } }

}
