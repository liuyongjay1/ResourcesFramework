EventDefine_CSharp = {}

local GEN_GROUP = EventManager.GEN_GROUP;
local GEN_EVENT = EventManager.GEN_EVENT;

function EventDefine_CSharp.InitEvent()
    EventManager.GEN_GROUP("CSharp_EVENT");
    --键盘鼠标事件
    EventType.Battle_UpdateTroopUI  = 1; --战斗布阵UI更新
    EventType.Battle_CreateMoveCommond  = 2; --创建战斗指令
    EventType.TouchEvent  = 3; --创建战斗指令  

end
