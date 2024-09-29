
-- 类声明
TimerManager = {
	OnceList = {},
	RepeatList = {},
	FrameList = {},
	OnceRecycledList = {},
	RepeatRecycledList = {},
	FrameRecycledList = {},
}
local this = TimerManager

--[[
	usage:
		OnceTimer:
			-- timeValue 定时器时长
			-- PanelName 面板名称，没在面板中填nil
			TimerManager.Create(ETimerType.Once, PanelName, timeValue, function ( ... )
				XXX()
			end)

		RepeatTimer:
			-- timeValue 定时器时长
			-- PanelName 面板名称，没在面板中填nil
			-- timeDelay 首次计时的延时，默认是0
			local timer = TimerManager.Create(ETimerType.Repeat, PanelName, timeValue, function ( ... )
				XXX()
			end, timeDelay)
			...
			TimerManager.StopRepeatTimer(timer)

		DelayFrame:
			-- frameCnt 等待帧数
			-- PanelName 面板名称，没在面板中填nil
			TimerManager.Create(ETimerType.Frame, PanelName, frameCnt, function ( ... )
				XXX()
			end)
]]--

--Start--
function TimerManager.Start()
	Debug.Log("TimerManager.Start")
end

local function ClearData( data )
	--data.Timer = nil
	data.Callback = nil
	--data = nil
end

function TimerManager.Cleanup()
	for _, v in pairs(this.OnceList) do
		ClearData(v)
	end
	for _, v in pairs(this.RepeatList) do
		ClearData(v)
	end
	for _, v in pairs(this.FrameList) do
		ClearData(v)
	end
	this.OnceList = {}
	this.RepeatList = {}
	this.FrameList = {}
	for _, v in pairs(this.OnceRecycledList) do
		ClearData(v)
	end
	for _, v in pairs(this.RepeatRecycledList) do
		ClearData(v)
	end
	for _, v in pairs(this.FrameRecycledList) do
		ClearData(v)
	end
	this.OnceRecycledList = {}
	this.RepeatRecycledList = {}
	this.FrameRecycledList = {}
end

--Update--
function TimerManager.Update()
	local deltaTime = UE.Time.unscaledDeltaTime
	
	for i = #this.OnceList, 1, -1 do
		local once = this.OnceList[i]
		if once and once.Timer:Update(deltaTime) then
			local callback = once.Callback
			local caller = once.Caller
			table.remove(this.OnceList, i)
			ClearData(once)
			table.insert(this.OnceRecycledList, once)
			if caller then
				callback(caller)
			else
				callback()
			end
		end
	end

	for i = #this.RepeatList, 1, -1 do
		local repeatData = this.RepeatList[i]
		if repeatData.Timer:Update(deltaTime) then
			local callback = repeatData.Callback
			local caller = repeatData.Caller
			if caller then
				callback(caller,repeatData.Timer.DeltaTime,repeatData.Timer.DoTimes,repeatData.Timer.Times)
			else
				callback(repeatData.Timer.DeltaTime,repeatData.Timer.DoTimes,repeatData.Timer.Times)
			end
			
		elseif repeatData.Timer.IsOver then
			table.remove(this.RepeatList, i)
			ClearData(repeatData)
			table.insert(this.RepeatRecycledList, repeatData)
		end
	end

	for i = #this.FrameList, 1, -1 do
		local frame = this.FrameList[i]
		if frame.Timer:Update() then
			local callback = frame.Callback
			local caller = frame.Caller
			table.remove(this.FrameList, i)
			ClearData(frame)
			table.insert(this.FrameRecycledList, frame)
			if caller then
				callback(caller)
			else
				callback()
			end
		end
	end
end

--Create--
function TimerManager.Create( timerType, caller, delay, callback, timeOffset,count )
	local timer = nil
	if timerType == ETimerType.Once then
		if #this.OnceRecycledList > 0 then
			local once = table.remove(this.OnceRecycledList)
			timer = once.Timer
			timer.Delay = delay
			timer:Reset()
			once.Callback = callback
			once.Caller = caller
			table.insert(this.OnceList, once)
		else
			timer = OnceTimer.new(delay)
			table.insert(this.OnceList, {Timer = timer, Callback = callback, Caller = caller})
		end
	elseif timerType == ETimerType.Repeat then
		if #this.RepeatRecycledList > 0 then
			local repeatData = table.remove(this.RepeatRecycledList)
			timer = repeatData.Timer
			timer.Delay = delay or 0
			timer.Repeat = timeOffset or 0
			timer.Times = count or -1
			timer:Reset()
			repeatData.Callback = callback
			repeatData.Caller = caller
			table.insert(this.RepeatList, repeatData)
		else
			timer = RepeatTimer.new(delay or 0,timeOffset,count)
			table.insert(this.RepeatList, {Timer = timer, Callback = callback, Caller = caller})
		end
	elseif timerType == ETimerType.Frame then
		if #this.FrameRecycledList > 0 then
			local frame = table.remove(this.FrameRecycledList)
			timer = frame.Timer
			timer.Delay = delay
			timer:Reset()
			frame.Callback = callback
			frame.Caller = caller
			table.insert(this.FrameList, frame)
		else
			timer = DelayFrame.new(delay)
			table.insert(this.FrameList, {Timer = timer, Callback = callback, Caller = caller})
		end
	end

	return timer
end

function TimerManager.StopRepeatTimer( timer )
	for i, v in ipairs(this.RepeatList) do
		if v.Timer == timer then
			timer:Kill()
			table.remove(this.RepeatList, i)
			ClearData(v)
			table.insert(this.RepeatRecycledList, v)
			break
		end
	end
end

function TimerManager.StopOnceTimer( timer )
	for i, v in ipairs(this.OnceList) do
		if v.Timer == timer then
			timer:Kill()
			table.remove(this.OnceList, i)
			ClearData(v)
			table.insert(this.OnceRecycledList, v)
			break
		end
	end
end

function TimerManager.StopFrameTimer( timer )
	for i, v in ipairs(this.FrameList) do
		if v.Timer == timer then
			timer:Kill()
			table.remove(this.FrameList, i)
			ClearData(v)
			table.insert(this.FrameRecycledList, v)
			break
		end
	end
end

function TimerManager.StopTimerByCaller( caller )
	for i = #this.OnceList, 1, -1 do
		local once = this.OnceList[i]
		if once.Caller == caller then
			table.remove(this.OnceList, i)
			ClearData(once)
			table.insert(this.OnceRecycledList, once)
		end
	end

	for i = #this.RepeatList, 1, -1 do
		local repeatData = this.RepeatList[i]
		if repeatData.Caller == caller then
			table.remove(this.RepeatList, i)
			ClearData(repeatData)
			table.insert(this.RepeatRecycledList, repeatData)
		end
	end

	for i = #this.FrameList, 1, -1 do
		local frame = this.FrameList[i]
		if frame.Caller == caller then
			table.remove(this.FrameList, i)
			ClearData(frame)
			table.insert(this.FrameRecycledList, frame)
		end
	end
end

-------------------------------------------------------------------------------------------------------------
--类声明--
DelayFrame = Class("DelayFrame")

function DelayFrame:ctor(delayFrame)
	self.Delay = delayFrame
end

--init--
function DelayFrame:__init()
	self.DelayFramer = 0
	self.IsOver = false
	self.IsPause = false
end

--Update--
function DelayFrame:Update()
	if(self.IsOver or self.IsPause) then
		return false
	end

	self.DelayFramer = self.DelayFramer + 1
	if(self.DelayFramer > self.Delay) then
		self:Kill()
		return true
	end
	
	return false
end

--重置计时器--
function DelayFrame:Reset()
	self.DelayFramer = 0
	self.IsOver = false
	self.IsPause = false
end

--结束计时器--
function DelayFrame:Kill()
	self.IsOver = true
end

-------------------------------------------------------------------------------------------------------------
--类声明--
OnceTimer = Class("OnceTimer")

function OnceTimer:ctor(delayTime)
	self.Delay = delayTime
end

--init--
function OnceTimer:__init()
	self.DelayTimer = 0
	self.IsOver = false
	self.IsPause = false
end

--Update--
function OnceTimer:Update(deltaTime)
	if(self.IsOver or self.IsPause) then
		return false
	end
	
	self.DelayTimer = self.DelayTimer + deltaTime
	if(self.DelayTimer >= self.Delay) then
		self:Kill()
		return true
	end
	
	return false
end

--重置计时器--
function OnceTimer:Reset()
	self.DelayTimer = 0
	self.IsOver = false
	self.IsPause = false
end

--结束计时器--
function OnceTimer:Kill()
	self.IsOver = true
end

-- 获取剩余时间 --
function OnceTimer:GetLastTime(  )
	return math.ceil(self.Delay - self.DelayTimer)
end

-------------------------------------------------------------------------------------------------------------
--类声明--
RepeatTimer = Class("RepeatTimer")

function RepeatTimer:ctor(delayTime, repeatTime, times)
	self:Reset()
	self.Delay = delayTime
	self.Repeat = repeatTime
	self.Times = times or -1
end

--Update--
function RepeatTimer:Update(deltaTime)
	if(self.IsOver or self.IsPause or self.Times == 0) then
		return false
	end

	self.DelayTimer = self.DelayTimer + deltaTime
	if(self.DelayTimer > self.Delay) then
		self.RepeatTimer = self.RepeatTimer + deltaTime
		if(self.RepeatTimer >= self.Repeat) then
			self.DeltaTime = self.RepeatTimer
			self.RepeatTimer = self.RepeatTimer - self.Repeat
			self.DoTimes = self.DoTimes + 1
			if self.Times > 0 then
				if self.DoTimes >= self.Times then
					self.IsOver = true
				end
			end
			return true
		end
	end

	return false
end

--重置计时器--
function RepeatTimer:Reset()
	self.DelayTimer = 0
	self.RepeatTimer = 0
	self.DeltaTime = 0
	self.DoTimes = 0
	self.IsOver = false
	self.IsPause = false
end

--结束计时器--
function RepeatTimer:Kill()
	self.IsOver = true
end

--跳过计时等待直接运行一次
function RepeatTimer:SkipWait()
	self.RepeatTimer = self.Repeat
end