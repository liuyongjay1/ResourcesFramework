
--InitClass--
FsmStateStart = Class("FsmStateStart", FsmStateBase)

function FsmStateStart:ctor()
	self.Type = EFsmType.Start
end

--Enter--
function FsmStateStart:Enter()
	UIManager.ShowUI("UI_Start")
end

--Update--
function FsmStateStart:Update()

end

--Exit--
function FsmStateStart:Exit()
  
end
