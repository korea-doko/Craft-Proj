using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroModel : IModel
{

}
public class HeroModel : MonoBehaviour, IHeroModel
{
    public void InitModel()
    {

    }
}
