/// <summary>
/// 消息统计
/// </summary>
public enum LocalMessageName
{
    None,
    /// <summary>
    /// 多语言
    /// </summary>
    CC_REFRESH_LANGUAGE,
    /// <summary>
    /// 关闭loading界面 回调
    /// </summary>
    CC_UI_LOADINGVIEW_CLOSE,
    
    /// <summary>
    /// 游戏开始
    /// </summary>
    CC_GAME_Start,
    
    /// <summary>
    /// 失败 -GameController.FailType
    /// </summary>
    CC_GAME_FAIL,
    /// <summary>
    /// 成功
    /// </summary>
    CC_GAME_WIN,
    
    /// <summary>
    /// 创建Hp   -BaseMember
    /// </summary>
    CC_GAME_CREATEHP,
    /// <summary>
    /// 删除Hp   -BaseMember
    /// </summary>
    CC_GAME_DESTROYHP,
    
    /// <summary>
    /// 检查是否游戏结束
    /// </summary>
    CC_GAME_CHECKISOVERFORMEMBERS,
    /// <summary>
    /// 玩家受伤
    /// </summary>
    CC_GAME_PlayerHit,

}


