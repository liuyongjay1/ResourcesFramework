--事件管理类--
ScreenTouchMgr = {
	
}

local CS_TouchMgrInstance = CS.TouchMgr.Instance

--[[
    CS_TouchMgrInstance:SwipeEvent('+', ScreenTouchMgr.OnSwipeEvent)
    CS_TouchMgrInstance:SwipeEvent('-', ScreenTouchMgr.OnSwipeEvent)
]]

-- Event 系统初始化
function ScreenTouchMgr.InitModule()    
end

--[[
    注册单击事件
]]
function ScreenTouchMgr.RegTouchStart(callback)
    CS_TouchMgrInstance:TouchStartEvent('+', callback)
end

--[[
    注销单击事件
]]
function ScreenTouchMgr.UnRegTouchStart(callback)
    CS_TouchMgrInstance:TouchStartEvent('-', callback)
end

--[[
    注册拖拽事件
]]
function ScreenTouchMgr.RegSwipeEvent(callback)
    CS_TouchMgrInstance:SwipeEvent('+', callback)
end

--[[
    注销拖拽事件
]]
function ScreenTouchMgr.UnRegSwipeEvent(callback)
    CS_TouchMgrInstance:SwipeEvent('-', callback)
end
