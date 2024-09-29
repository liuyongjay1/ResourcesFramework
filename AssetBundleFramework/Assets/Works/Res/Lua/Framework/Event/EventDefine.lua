EventDefine = {}

local ID_START = 0
local function getNext()
	ID_START = ID_START + 1
	return ID_START
end

--事件类型--
--格式说明：服务器的请求事件用Request
EventType = 
{
	--C#通知lua事件
	UIEvent_TypeWriterFinish = getNext(),

    --UI事件
    UIEvent_OnCreate = getNext(),
    UIEvent_OnShow = getNext(),
	UIEvent_OnClose = getNext(),
	UIEvent_DialogFinishCountdown = getNext(),		--开始倒计时
	UIEvent_DelayCountdown = getNext(),		--开始倒计时

	--章节事件
	Chapter_EnterCurRound = getNext(),	--进入下一回合
	Chapter_RoundFinish = getNext(),	--当前回合结束

	Chapter_GMChangeRound = getNext(),	--当前回合结束
	--
}

--转换--
function EventDefine.EventIdToName(eventId)
	for k,v in pairs(EventType) do
		if(v == eventId) then
			return k
		end
	end
	LogManager.LogError("Not found eventType ", eventId)
end

--转换--
function EventDefine.EventNameToId(eventName)
	for k,v in pairs(EventType) do
		if(k == eventName) then
			return v
		end
	end
	LogManager.LogError("Not found eventId, eventName", eventName)
end
return EventDefine
