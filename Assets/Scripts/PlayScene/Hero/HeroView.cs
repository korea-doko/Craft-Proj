using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroView<T> : IView<T>
{

}
public class HeroView : MonoBehaviour , IHeroView<HeroModel>{

    public void InitView(HeroModel _model)
    {

    }
}
