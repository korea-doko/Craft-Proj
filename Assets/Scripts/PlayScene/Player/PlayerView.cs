using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlayerView<T> : IView<T>
{

}
public class PlayerView : MonoBehaviour, IPlayerView<PlayerModel>
{
    public void InitView(PlayerModel _model)
    {

    }
}
