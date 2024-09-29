
--类声明--
FsmStateBase = Class("FsmStateBase")

--init--
function FsmStateBase:ctor()
	self.Type = nil
	self.Params = nil
end

--Enter--
function FsmStateBase:Enter()
end

--Update--
function FsmStateBase:Update()
end

--Set--
function FsmStateBase:setParams( params )
	self.Params = params
end

--Exit--
function FsmStateBase:Exit()
	self.Params = nil
end