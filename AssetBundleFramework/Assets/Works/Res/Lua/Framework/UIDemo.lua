--[[
	Lua 接口使用demo
]]
local UI_Demo = class("UI_Demo",UIPanel)

--require使用方式
local XXX = require("XXX")
--使用class
local node = XXX.new()
--调用class方法
node:FuncAAFuncAA()

--初始化类--
function UI_Demo:InitClass()

end

--创建--
function UI_Demo:OnCreate(UIElement)
	--保存组件
    self.Input_IP = UIElement.inputfield_ip
    self.Input_IP.text = "xxxx"

	--使用class
    local node = XXX.new()
	--调用class方法
    node:FuncAA()

	--UI传参,详见UIManager.ShowUI
	local paramTab = self.paramTab

    --复制物体并获取组件
    local obj = UE.GameObject.Instantiate("xxx")
    local TroopUIElement = {}
    obj:GetComponent("UIElement"):ApplyElementToLua(TroopUIElement)
    
    --注册按钮点击事件
    self:AddButtonListener(UIElement.confirmbtn,self.OnClickConfirm,nil)

    --注册Lua事件
    EventManager.Reg(EventType.XXX,EventType.XXX, self.OnLuaEvent, self)

    --注册Action
    CS.InputManager.LuaBindAction("selectOption1",self,"selectOptionCallBack")
    CS.InputManager.LuaBindAction("selectOption2",self,"selectOptionCallBack")
    CS.InputManager.LuaBindAction("selectOption3",self,"selectOptionCallBack")

end

function UI_Demo:selectOptionCallBack(actionName,type, time)
    LogManager.LogInfo("actionName: " ..tostring(actionName))
end 

--Lua事件触发回调
function UI_Demo:OnLuaEvent()
   
end


--隐藏--
function UI_Demo:OnClose()
end

--显示--
function UI_Demo:OnShow()
end

--更新--
function UI_Demo:OnUpdate()

end

return UI_Demo