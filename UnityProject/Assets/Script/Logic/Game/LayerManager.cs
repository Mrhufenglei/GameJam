//-----------------------------------------------------------------
//
//              Maggic @  2021-02-07 20:47:00
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class LayerManager
{
    //UI界面
    public static int UI;

    /// <summary>
    /// 英雄层
    /// </summary>
    public static int Player;

    /// <summary>
    /// 敌人
    /// </summary>
    public static int Enemy;

    /// <summary>
    /// 炸弹
    /// </summary>
    public static int Bomb;

    /// <summary>
    /// 地图
    /// </summary>
    public static int Map;

    /// <summary>
    ///地面
    /// </summary>
    public static int Background;

    public static int Layer_UI;
    public static int Layer_Map;
    public static int Layer_Player;
    public static int Layer_Enemy;
    public static int Layer_Bomb;
    public static int Layer_Background;

    static LayerManager()
    {
        //NameToLayer 是有消耗的 之后可以改成字典

        UI = LayerMask.NameToLayer("UI");
        Map = LayerMask.NameToLayer("Map");
        Player = LayerMask.NameToLayer("Player");
        Enemy = LayerMask.NameToLayer("Enemy");
        Bomb = LayerMask.NameToLayer("Bomb");
        Background = LayerMask.NameToLayer("Background");

        Layer_UI = 1 << UI;
        Layer_Map = 1 << Map;
        Layer_Player = 1 << Player;
        Layer_Enemy = 1 << Enemy;
        Layer_Bomb = 1 << Bomb;
        Layer_Background = 1 << Background;
    }
}