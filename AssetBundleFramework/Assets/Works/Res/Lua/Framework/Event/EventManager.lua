--事件管理类--
EventManager = {}

_G.EventType = {};

local xpcall = xpcall;
local traceback = traceback;
local mAllListeners = {}
local mFuncPool = {};

require("Framework/Event/EventDefine");
require("Framework/Event/EventDefine_CSharp");

local CS_LuaEventInstance = CS.LuaEventUtility.Instance

local function AllocFuncData(func,obj)
    local funcData = mFuncPool[#mFuncPool];
    if funcData then
        mFuncPool[#mFuncPool] = nil;
    else
        funcData = {};
    end
    funcData.callback = func;
    funcData.caller = obj;
    return funcData;
end

local function FreeFuncData(funcData)
    funcData.callback = nil;
    funcData.caller = nil;
    mFuncPool[#mFuncPool + 1] = funcData;
end

--C#向Lua发送事件
function EventManager.OnCSharpEvent(eventName,paramTable)
    local eventId = EventDefine.EventNameToId(eventName)
    EventManager.Trigger(eventId,paramTable);
end

-- Event 系统初始化
function EventManager.InitModule()    
    _G.EventType = EventType;
    -- 事件加载
    -- EventDefine.InitEvent();
    -- EventDefine_CSharp.InitEvent()

    CS.LuaManager.Instance:LuaEventFunc('+', EventManager.OnCSharpEvent)
end



--[[
定义事件组
--]]
function EventManager.GEN_GROUP(sysName)
    if EventType[sysName] then LogManager.LogError("%s already has been registered!", sysName) end
    mEventSID = mEventSID + 1;
    EventType[sysName] = mEventSID;
end

--[[
定义事件
--]]
function EventManager.GEN_EVENT(evtName)
    if EventType[evtName] then LogManager.LogError("%s already has been registered!", evtName) end
    mEventEID = mEventEID + 1;
    EventType[evtName] = mEventEID;
end

--[[
注册事件监听函数
sysID   int      事件所属系统ID,使用 EventType.XX
eventType   int      事件ID,使用 EventType.XX
func    function 事件触发回调
obj     class    事件回调所属类对象
--]]
function EventManager.Reg(eventType,callback,caller)    
    if eventType == nil or callback == nil then
        LogManager.LogError("can't reg nil eventType = %s ,func = %s",eventType,callback);
        return
    end
    --某个系统的监听表
    -- local systemListeners = mAllListeners[sysID] or {};
    -- mAllListeners[sysID] = systemListeners;
    --某个系统某个事件的监听表
    if mAllListeners[eventType] == nil then
        mAllListeners[eventType] = {}
    end
    local listonItem = AllocFuncData(callback,caller)
    table.insert( mAllListeners[eventType],listonItem)
  
end

--[[
注销事件监听函数
sysID   int      事件所属系统ID,使用 EventType.XX
eventType   int      事件ID,使用        EventType.XX
func    function 事件触发回调
obj     class    事件回调所属类对象
--]]
function EventManager.UnReg(eventType,callback,caller)
    if eventType == nil or callback == nil then
        LogManager.LogError("can't unreg nil eventId %s %s",eventType,callback);
        return
    end
    --某个系统某个事件的监听表
    local eventListeners = mAllListeners[eventType] or {};
    --移除对应的监听对象
    for key,funcData in pairs(eventListeners) do
        if funcData.callback == callback then
            eventListeners[key] = nil;
        end
    end
end

--[[
触发某个事件
sysID   int      事件所属系统ID,使用 GameEvent.XX
eventType   int      事件ID,使用GameEvent.XX
...              事件额外参数
--]]
function EventManager.Trigger(eventType,paramTab)
    if eventType == nil or type(eventType) ~= "number" then
        LogManager.LogError("can't trigger nil eventId: %s",eventType);
        return
    end
    if LogManager.ShowEvent then
        local paramStr = ""
        if paramTab ~= nil then
            for key, value in pairs(paramTab) do
                paramStr = paramStr .. string.format("%s = %s",key,value)
            end
        end
        LogManager.LogEventInfo(string.format("evtId: {%s} ,param: {%s}",EventDefine.EventIdToName(eventType),paramStr))
    end
   
    local eventListeners = mAllListeners[eventType] or {};
    --执行事件回调
    local listenerCount = #eventListeners;
    for i = 1,listenerCount do
        local listener = eventListeners[i];
        if listener then
            if listener.caller then
                -- listener.func(listener.obj,...)
                local flag,msg = xpcall(listener.callback,debug.traceback,listener.caller,paramTab);
                if not flag then
                    LogManager.LogError(string.format("call func error-> %s",msg));
                end
            else
                -- listener.func(...)
                local flag,msg = xpcall(listener.callback,debug.traceback,paramTab);
                    if not flag then
                        LogManager.LogError(string.format("call func error  %s-> ", msg));
                end
            end
        end
    end

end

