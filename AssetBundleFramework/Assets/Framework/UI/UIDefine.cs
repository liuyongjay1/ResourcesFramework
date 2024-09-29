public enum UISoundEnum
{
    NO = -1, //没有音效类型
    COMMON_CLICK = 0,           //通用点击
    TOG1_CLICK = 1,             //通用二级页签
    TOG2_CLICK = 2,             //2级Toggle
    CLOSE_CLICK = 3,            //关闭按钮
    GAMESTART_CLICK = 4,        //游戏开始
    OPEN_CLICK = 5,             //打开按钮
    COMMON_BACK = 6,            //通用返回
    COMMON_CANNEL = 7,          //通用取消
    COMMON_CONFIRM = 8,         //通用确认
    COMMON_NON = 9,             //点击任意空白处音效   点击剧情对话框的时候也使用改音效
    CUSTOM_AUDIO = 10,          //其他类型  通过输入id播放
}
