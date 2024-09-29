--类声明--

UIPanel = Class("UIPanel")
--init--
function UIPanel:ctor()
	--成员变量--
	--记录所有UI事件
	self.EventListeners = {}
	--记录所有时间计时器
	self.AllTimers = {}
	--记录所有点击组件回调，清空闭包函数用
	self.bindedUnityEvents = {}
	--初始化继承类--
	self:InitClass()
end

--release--
function UIPanel:__release()
	self:OnRelease()
end

--[[
--初始化--
function UIPanel:InitClass() LogManager.LogError("UI not Override InitClass") end
--创建--


--销毁--

]]
function UIPanel:OnPanelCreate(uiElement)
	self:OnCreate(uiElement)
end

--显示--
function UIPanel:OnPanelShow() 
	self:OnShow()
end

--隐藏--
function UIPanel:OnPanelClose()
	self:OnClose()
	self:RemoveAllTimer()
end

--C#通知UI Obj销毁--
function UIPanel:OnPanelDestroy()
	LogManager.LogUIInfo("Lua UIPanel Destroy")
	self:RemoveAllEventListeners()
	self:RemoveBindUIAction()
	self:OnDestroy()
end

------------------------------------------------事件相关-开始-------------------------------------------------
--[[
	事件监听--
	如果继承自UIPanel，在面板销毁时会自动注销监听
	注意，如果继承自UIBase不会自动注销
]]
function UIPanel:AddEventListener(eventType, callback)
	EventManager.Reg(eventType, callback,self)

	if self.EventListeners == nil then
		self.EventListeners = {}
	end
	local newEvent = {}
	newEvent.evtType = eventType
	newEvent.callback = callback
	table.insert(self.EventListeners, newEvent)
	
end

--移除所有监听事件
function UIPanel:RemoveAllEventListeners()
	for index, event in ipairs(self.EventListeners) do
		EventManager.UnReg(event.evtType,event.callback)
	end
end
------------------------------------------------Timer相关-开始-------------------------------------------------
--[[
	
]]
function UIPanel:CreateTimer(timerType,delayTime,callback,timeOffset,count)
	local newTimer = TimerManager.Create(timerType,self, delayTime, callback,timeOffset,count)
	table.insert(self.AllTimers,newTimer)
	return newTimer
end

--
function UIPanel:StopTimerByType(timer,timerType)
	for index, value in ipairs(self.AllTimers) do
		if value == timer then
			if timerType == ETimerType.Once then
				TimerManager.StopOnceTimer( timer )
			elseif timerType == ETimerType.Repeat then
				TimerManager.StopRepeatTimer( timer )
			elseif timerType == ETimerType.Frame then
				TimerManager.StopFrameTimer( timer )
			end
			table.remove(self.AllTimers,index)
		end
	end
	
end


--移除所有时间事件
function UIPanel:RemoveAllTimer()
	self.AllTimers = {}
	TimerManager.StopTimerByCaller(self)
end
------------------------------------------------Timer相关-结束-------------------------------------------------

------------------------------------------------事件相关-结束-------------------------------------------------

-----------------------------------------------UI点击相关-开始-------------------------------------------------

--添加按钮事件
function UIPanel:AddButtonListener(btn, handle, param1)
	if(handle == nil) then
		LogManager.LogError("btn listener handle is nil.")
	end
	if btn == nil then
		LogManager.LogError("btn is nil.")
		return
	end
	--闭包函数
	local closure = function()
		handle(self, param1)
	end
	self:RecordBindUIAction(btn.onClick)
	btn.onClick:AddListener(closure)
end

--添加文本输入栏事件
function UIPanel:AddInputListener(input, handle)
	if handle == nil then
		LogManager.LogError.Error("input listener handle is nil.")
	end
	--闭包函数
	local closure = function (str)
		handle(self, str)
	end
	input.onValueChanged:AddListener(closure)
	self:RecordBindUIAction(input.onValueChanged)
	return input
end

-- 添加Toogle事件回调 (Toggle Event)
function UIPanel:AddToggleListener(toggle, handle, param)
	if handle == nil then
		LogManager.LogError.Error("toggle listener handle is nil.")
	end
	--闭包函数
	local closure = function (Toggle)
		handle(self, Toggle, param)
	end
	toggle.onValueChanged:AddListener(closure)
	self:RecordBindUIAction(toggle.onValueChanged)
	return toggle
end

--[[
	有点击回调的控件都记录下来，卸载时清空回调
	Params@
		eventHandler：Button.onClick/input.onValueChanged/toggle.onValueChanged
]]
function UIPanel:RecordBindUIAction(eventHandler)
	if not self.bindedUnityEvents then self.bindedUnityEvents = {} end
	table.insert(self.bindedUnityEvents, eventHandler)
end

--[[
	UI回调记录清空，清空闭包函数
	Button、Toggle、Input
]]
function UIPanel:RemoveBindUIAction()
	if self.bindedUnityEvents then
		for i,v in ipairs(self.bindedUnityEvents) do
			v:RemoveAllListeners()
			v = nil
		end
		self.bindedUnityEvents = nil
	end
end
-----------------------------------------------UI点击相关-结束-------------------------------------------------
