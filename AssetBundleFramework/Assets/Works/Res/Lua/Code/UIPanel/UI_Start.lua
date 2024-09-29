UI_Start = Class("UI_Start",UIPanel)

--初始化类--
function UI_Start:InitClass()
    
end

--创建--
function UI_Start:OnCreate(UIElement)
    self.UIElement = UIElement
    
    self:AddButtonListener(UIElement.startbtnButton,self.OnClickStartBtn)
    EventManager.Reg(EventType.Chapter_GMChangeRound, self.OnShow, self)

    self.DownBtn = UIElement.ingamedownloadButton
    self.DownText = UIElement.ingamedownloadtextTextMeshProUGUI
    self.DownText.text = CS.HotfixManager.Instance.InGameDownloadSize
    self:AddButtonListener(self.DownBtn,self.OnClickDownload)

    --LogManager.LogError("UI_Start:OnCreate")
end

--[[
    切换GM列表展开状态
]]
function UI_Start:OnClickDownload()
    CS.HotfixManager.Instance:StartInGameDownload() 
end

--[[
    切换GM列表展开状态
]]
function UI_Start:OnClickStartBtn()
    UIManager.CloseUI("UI_Start")
    --开始游戏逻辑
end

--销毁--
function UI_Start:OnDestroy()

end

--显示--
function UI_Start:OnShow()

end

--隐藏--
function UI_Start:OnClose()
end

--更新--
function UI_Start:OnUpdate()
end
