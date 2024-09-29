----------------------------------------------
-- FileName: UIPanelHelper.lua
-- Date: Thu, 23 Apr 2020 20:52:25 GMT+0800
-- Module: Framework-UI
-- Description: Panel子界面基类
--  
----------------------------------------------
--类声明--
UIPanelHelper = class("UIPanelHelper",UIBase)

function UIPanelHelper:ctor()
	self.Panel = nil
	self.RootTrans = nil
	self.IsShowing = false
	self.Listeners = {}
	self.NoPlayPopAudio = false		-- 不播放弹板的弹出和关闭音效
	self.Templates = {}
	self.bindedUnityEvents = {}
	--初始化继承类--
	self:InitClass()
end

function UIPanelHelper:Init( panel, root )
	self.Panel = panel
	self.RootTrans = root

	self:ResetChildCanvas()
	self.Canvas = self.RootTrans.transform:GetComponent("Canvas")
	self.Manifest = self.RootTrans.transform:GetComponent("UIManifest")
	self:OnCreate()
end

function UIPanelHelper:PlayPopAudio()
	return not self.NoPlayPopAudio
end 

function UIPanelHelper:Show( ... )
	self.RootTrans.gameObject:SetActive(true)
	if self.IsShowing then
		return
	end

	if self:PlayPopAudio() then
		AudioManagerHelper.PlaySound("PanelPop")
	end

	if not self.Panel.KeepSortingOrder then
		UITools.SetNodeSortingOrder(self.RootTrans, self.Panel:GetCoverDepth())
	end
	
	if self.Templates then
		for i=1, #self.Templates, 1 do
			self.Templates[i]:SetVisible(true)
		end
	end
	self:OnShow(...)
	self.IsShowing = true
end

function UIPanelHelper:UnShow( ... )
	self.RootTrans.gameObject:SetActive(false)
	if not self.IsShowing then
		return
	end

	if self:PlayPopAudio() then
		AudioManagerHelper.PlaySound("PanelShut")
	end
	if not self.Panel.KeepSortingOrder then
		self.Panel:RemoveCover()
	end
	self:Clear(...)

	if self.Templates then
		for i=1, #self.Templates, 1 do
			self.Templates[i]:SetVisible(false)
		end
	end
	
	self.IsShowing = false
end

function UIPanelHelper:OnDestroy()
	self:Clear()
	--移除所有监听
	for k,v in pairs(self.Listeners) do
		EventSystem.RemoveListener(k, v)
	end
	self.Listeners = nil

	--移除所有模板
	for i, v in ipairs(self.Templates) do
		v:InternalUnLoad()
	end
	self.Templates = nil
end

function UIPanelHelper:ResetChildCanvas()
	--self.ChildCanvas = self.RootTrans:GetComponentsInChildren(typeof(UE.Canvas), true)
	self.ChildCanvas = {}
	local canvasArray = self.RootTrans:GetComponentsInChildren(typeof(UE.Canvas), true)
	--注意：这是C#数组，索引从1开始，跳过父类的Canvas
	for i=1,canvasArray.Length-1 do
		table.insert(self.ChildCanvas, canvasArray[i])
	end
end


function UIPanelHelper:SetDepth(depth)

	if (IsNil(self.Canvas)) then
		return
	end

	--设置父类
	self.Canvas.sortingOrder = depth
    self:RefreshDepth()
end


function UIPanelHelper:RefreshDepth()
    if (self.Canvas == nil) then
        return
    end

    local depth = self.Canvas.sortingOrder
    --设置子类
    ----注意：这是C#数组，索引从1开始，跳过父类的Canvas
    --for i=1, self.ChildCanvas.Length-1, 1 do
    --不含根节点Canvas的list
    for _, canvas in ipairs(self.ChildCanvas) do
        depth = depth + 5
        --local canvas = self.ChildCanvas[i]
        canvas.sortingOrder = depth
    end

    if (self.topBar) then
        depth = depth + 5
    end
    self.MaxDepth = depth
    self:UpdateParticlesOrder()
end

function UIPanelHelper:UpdateParticlesOrder()
	local adjustOrders = self.RootTrans:GetComponentsInChildren(typeof(CS.AdjustUIParticleOrder), true)
	for i=0,adjustOrders.Length-1 do
		adjustOrders[i]:UpdateOrder(true)
	end
end

--事件监听--
--在面板销毁时会自动注销监听
function UIPanelHelper:AddEventListener(eventType, handle)
	--闭包函数
	local closure = function (etype, param)
		handle(self, etype, param)
	end

	self.Listeners[eventType] = closure
	EventSystem.AddListener(eventType, closure)
end

--初始化类--
function UIPanelHelper:InitClass()

end

--创建--
function UIPanelHelper:OnCreate()

end

--显示--
function UIPanelHelper:OnShow( ... )

end

--更新--
function UIPanelHelper:OnUpdate( ... )
	if self.Templates then
		for i=1, #self.Templates, 1 do
			if(self.Templates[i].IsPrepare) then
				self.Templates[i]:InternalUpdate()
			end
		end
	end
end

--清除--
function UIPanelHelper:Clear( ... )
	
end

-- 重置高度
function UIPanelHelper:OnSortDepth( ... )
	if not self.Panel.KeepSortingOrder then
		if self.IsShowing then
			UITools.SetNodeSortingOrder(self.RootTrans, self.Panel:GetCoverDepth())
		end
	end
end

--模板相关
function UIPanelHelper:CreateTemplate(name,parent,AttachPanel)
	--if(parent == nil) then
	--LogManager.LogError("CreateTemplate parent is nil.")
	--end

	--如果已经存在
	local panel = self:GetTemplate(name)
	if(panel ~= nil) then
		return panel
	end

	require ("Panel/UITemplate/"..name) --动态生成模板类
	panel =_G[name].new()
	panel.AttachPanel = AttachPanel
	panel.PanelName = name
	panel.PanelParent = parent
	panel:InternalLoad()
	table.insert(self.Templates, panel)
	return panel
end

function UIPanelHelper:GetTemplate(name)
	if self.Templates then
		for i=1, #self.Templates, 1 do
			if(self.Templates[i].PanelName == name) then
				return self.Templates[i]
			end
		end
	end
end

function UIPanelHelper:ShowTemplate(name)
	if self.Templates then
		for i=1, #self.Templates, 1 do
			if(self.Templates[i].PanelName == name) then
				self.Templates[i]:SetActive(true)
			end
		end
	end
end

function UIPanelHelper:HideTemplate(name)
	if self.Templates then
		for i=1, #self.Templates, 1 do
			if(self.Templates[i].PanelName == name) then
				self.Templates[i]:SetActive(false)
			end
		end
	end
end