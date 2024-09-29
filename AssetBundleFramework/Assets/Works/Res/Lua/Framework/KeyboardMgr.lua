--事件管理类--
KeyboardMgr = {
	
}

local CS_KeyboardInstance = CS.Input_Keyboard.Instance

--[[
    CS_TouchMgrInstance:SwipeEvent('+', KeyboardMgr.OnSwipeEvent)
    CS_TouchMgrInstance:SwipeEvent('-', KeyboardMgr.OnSwipeEvent)
]]

-- Event 系统初始化
function KeyboardMgr.InitModule()    
end

--[[
    注册单击事件
]]
function KeyboardMgr.RegMouseScroll(callback)
    CS_KeyboardInstance:MouseScrollEvent('+', callback)
end

--[[
    注销单击事件
]]
function KeyboardMgr.UnRegMouseScroll(callback)
    CS_KeyboardInstance:MouseScrollEvent('-', callback)
end

