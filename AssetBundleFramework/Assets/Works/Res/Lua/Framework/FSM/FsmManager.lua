require 'Framework/FSM/FsmDefine'
require("Framework/FSM/FsmStateBase")
--状态机管理器--
FsmManager = 
{
    RunState = nil, --当前状态
    PreState = nil, --之前状态
    States = {}, --状态集合
    ChangeDone = true, -- 状态切换结束
    RunType = nil,
    NextType = nil,
}
local this = FsmManager

--自动初始化状态机--
function FsmManager.InitModule()
	for k,v in pairs(EFsmType) do
		local name = ("FsmState" .. v)
		require("Framework/FSM/State/".. name)
		local state =_G[name].new()
		FsmManager.AddState(v, state)
	end
	
end

--运行--
function FsmManager.Start(runType)
    print("FsmManager.Start")
    local state = this.GetState(runType)
    this.RunState = state
    this.PreState = state
    this.RunState:Enter()
end

--更新--
function FsmManager.Update()
    if(this.RunState ~= nil) then
        this.RunState:Update()
    end
end

--添加状态--
function FsmManager.AddState(type, state)
    this.States[type] = state
end

--转换状态--
function FsmManager.ChangeState(type, params)
    local changeState = this.GetState(type)
    if(changeState == nil) then
        Debug.Error("Can not change state ", type)
        return
    end

    if this.RunType == type then
        Debug.Warning("The state is running ", type)
        return
    end

    Debug.Log("Change fsm state to ", type)

    this.ChangeDone = false
    this.PreType = this.RunState.Type
    this.NextType = type
    this.PreState = this.RunState
    this.PreState:Exit()
    this.RunState = changeState
    this.RunState:setParams(params)
    this.RunState:Enter()
    this.ChangeDone = true
    this.RunType = type
end


-- 检查当前状态是否为指定状态
function FsmManager.CheckCurState( state )
    return this.RunState == state
end

--回到之前状态--
function FsmManager.RevertToPreState()
    this.ChangeState(this.PreState)
end

--获取一个状态--
--枚举名作Key
function FsmManager.GetState(type)
    return this.States[type]
end