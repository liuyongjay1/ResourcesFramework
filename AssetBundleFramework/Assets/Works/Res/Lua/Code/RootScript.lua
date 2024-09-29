require("LuaPanda").start("127.0.0.1", 8818)

require 'Framework/Functions'
require 'Framework/UIManager'
require 'Framework/AudioManager'
require 'Framework/Event/EventManager'
require 'Framework/ScreenTouchMgr'
require 'Framework/KeyboardMgr'
require 'Framework/Class'

require 'Framework/Table/TableManager'
require 'Framework/Tool/ToolManager'
require 'Framework/TimerManager'
require 'Framework/FSM/FsmManager'

_G.UE = CS.UnityEngine
_G.SYS = CS.System
_G.LogManager = CS.LogManager

--[[
	Lua工程启动入口
]]
function LuaStartUp()
	LogManager.LogProcedure("LuaStartUp")
	--配表模块
	TableManager.InitModule(LoadTableFinish) 
end

--[[
	加载Lua配表数据
]]
function LoadTableFinish()
	LogManager.LogProcedure("LoadTableFinish")
	EventManager.InitModule()
	FsmManager.InitModule()
	ScreenTouchMgr.InitModule()

	--加载UIRoot
	UIManager.InitModule(LoadCanvasRootCallback)
end

--加载Canvas回调
function LoadCanvasRootCallback()
	LogManager.LogProcedure("LoadCanvasRootCallback")

    FsmManager.Start(EFsmType.Start)
end

function Update()
	TimerManager.Update()
end



