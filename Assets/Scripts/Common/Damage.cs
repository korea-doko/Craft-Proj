using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamage
{

}

[System.Serializable]
public class Damage : IDamage
{
    int m_minDamage;                      //최소 데미지
    int m_maxDamage;                      //최대 데미지

    int m_penetrateRating;                //관통력
    int m_accuracyRating;                 //적중력

    int m_addedMinDamage;                 //추가된 최소 데미지
    int m_addedMaxDamage;                 //추가된 최대 데미지

    float m_increasedMinDamage;           //추가된 최소 데미지 증가률
    float m_increasedMaxDamage;           //추가된 최대 데미지 증가률

    int m_addedPenetrateRating;           // 추가된 관통력
    float m_increasedPenetrateRating;     // 추가된 관통력 증가율

    int m_addedAccuracyRating;            // 추가된 적중력
    float m_increasedAccuracyRating;      // 추가된 적중력 증가율

    public Damage()
    {

    }
}
