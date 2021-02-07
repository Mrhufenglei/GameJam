using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机名称统计
/// </summary>
public enum StateName
{
    /// <summary>
    /// 登录状态
    /// </summary>
    LoginState,
    /// <summary>
    /// 选择界面
    /// </summary>
    SelectState,
    /// <summary>
    /// 加载  选择状态到游戏状态
    /// </summary>
    LoadingSelectToGameState,
    /// <summary>
    /// 加载  游戏状态到选择状态
    /// </summary>
    LoadingGameToSelectState,
    /// <summary>
    /// 游戏状态
    /// </summary>
    GameState,
}
