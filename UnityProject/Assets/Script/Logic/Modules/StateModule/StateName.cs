using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机名称统计
/// </summary>
public enum StateName
{
    /// <summary>
    /// 检查资源
    /// </summary>
    CheckAssetsState = 1,
    /// <summary>
    /// 登录状态
    /// </summary>
    LoginState,

    LoadingToSelectLevelState,
    SelectLevelState,
}
