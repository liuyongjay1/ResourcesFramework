
--类声明--
UITemplate = class("UITemplate", UIBase) 

--init--
function UITemplate:ctor()
	--成员变量--
	self.AttachPanel = nil
	self.PanelName = nil
	self.PanelParent = nil
	self.Listeners = {}
	self.bindedUnityEvents = {}

	--成员变量--
	self.Transform = nil
	self.Manifest = nil
	self.IsPrepare = false
	self.ChildUITweenControls = nil
	self.ChildTweenControlsForExit = nil
	self.ExitTime = 0
	self.Canvas = nil
	self.Raycaster = nil
	self.ChildCanvas = nil
	self.ChildRaycaster = nil
	self.MaxDepth = 0
	--初始化继承类--
	self:InitClass()
end

--release--
function UITemplate:__release()
	if self.PanelName then
		TimerManager.StopTimerByPanel(self.PanelName)
	end
	self.AttachPanel = nil
	self.PanelParent = nil
	self.PanelName = nil
	self.ChildUITweenControls = nil
	self.ChildTweenControlsForExit = nil
	self.ExitTime = 0

	if not IsNil(self.Transform) then
		UE.GameObject.Destroy(self.Transform.gameObject)
	end
	self.Transform = nil
	self.Manifest = nil
	self.IsPrepare = false
end


--初始化类--
function UIPanel:InitClass()
end
--创建--
function UITemplate:OnCreate()
end
--销毁--
function UITemplate:OnDestroy()
end
--更新--
function UITemplate:OnUpdate()
end


--设置面板显隐
function UITemplate:SetVisible(visible)
	if(self.Transform == nil) then
		return
	end

	--优化显示
	if(visible) then
		self.Transform.gameObject.layer = UIBase.ShowLayer
		--for i=1, self.ChildCanvas.Length-1, 1 do
		--	self.ChildCanvas[i].gameObject.layer = UIBase.ShowLayer
		--end
		for _, canvasObj in ipairs(self.ChildCanvas) do
			canvasObj.canvas.gameObject.layer = UIBase.ShowLayer
		end
		local adjustOrders = self.Transform:GetComponentsInChildren(typeof(CS.AdjustUIParticleOrder), true)
		for i=0,adjustOrders.Length-1 do
			CS.UIHelper.SetLayer(UIBase.ShowLayer, adjustOrders[i].gameObject)
		end
	else
		self.Transform.gameObject.layer = UIBase.HideLayer
		--for i=1, self.ChildCanvas.Length-1, 1 do
		--	self.ChildCanvas[i].gameObject.layer = UIBase.HideLayer
		--end
		for _, canvasObj in ipairs(self.ChildCanvas) do
			canvasObj.canvas.gameObject.layer = UIBase.HideLayer
		end
		local adjustOrders = self.Transform:GetComponentsInChildren(typeof(CS.AdjustUIParticleOrder), true)
		for i=0,adjustOrders.Length-1 do
			CS.UIHelper.SetLayer(UIBase.HideLayer, adjustOrders[i].gameObject)
		end
	end

	--优化交互
	if self.Raycaster then
		self.Raycaster.enabled = visible
	end
	self.Canvas.enabled = visible
	for i=0, self.ChildRaycaster.Length-1, 1 do
		self.ChildRaycaster[i].enabled = visible
	end

	for i, v in ipairs(self.ChildUITweenControls) do
		v.enabled = visible
	end

	for i, v in ipairs(self.ChildTweenControlsForExit) do
		if v.isShared==false then
			v.enabled = not visible
		end
	end
end
function UITemplate:IsVisible()
	return self.Transform.gameObject.layer == UIBase.ShowLayer
end
--设置面板激活状态
function UITemplate:SetActive(active)
	self.Transform.gameObject:SetActive(active)
end

--设置新的父物体
function UITemplate:SetNewParent(parent)
	self.PanelParent = parent
	self.Transform:SetParent(parent, false)
end
--设置显示排序位置
function UITemplate:SetSortingOrder( depth, alreadyCalcParent )
	if self.PanelParent then
		depth = depth or 5
		local bhvCanvas = self.PanelParent:GetComponentInParent(typeof(UE.Canvas))
		if not alreadyCalcParent then
			depth = bhvCanvas.sortingOrder + depth
		end
		self.Canvas.overrideSorting = true
		self.Canvas.sortingOrder = depth
		self.MaxDepth = depth

		--不含根节点Canvas的list
		for _, canvasObj in ipairs(self.ChildCanvas) do
			--depth = depth + 5
			--local canvas = self.ChildCanvas[i]
			--canvas.sortingOrder = depth
			canvasObj.canvas.sortingOrder = depth + canvasObj.sortingOrder
			self.MaxDepth = math.max(self.MaxDepth, canvasObj.canvas.sortingOrder)
		end

		self:OnSortDepth()
	end
end

function UITemplate:OnSortDepth()

end

function UITemplate:GetDepth()
	if(self.Canvas == nil) then
		return 0
	else
		return self.Canvas.sortingOrder
	end
end

--内部方法--
function UITemplate:InternalLoad()
	local cloneObj = self.AttachPanel:CloneAttachPrefab(self.PanelName)
	if(cloneObj == nil) then
		return
	end

	--设置父类对象
	self.Transform = cloneObj.transform
	self.Transform:SetParent(self.PanelParent, false)

	--获取组件
	self.Canvas = self.Transform:GetComponent("Canvas")
	self.Raycaster = self.Transform:GetComponent("GraphicRaycaster")
	self.Manifest = self.Transform:GetComponent("UIManifest")
	if(self.Manifest == nil) then
		LogManager.LogError("Not found UIManifest component. Prefab is ", self.PanelName)
		return
	end

	self:ResetChildCanvas()
	self.ChildRaycaster = self.Transform:GetComponentsInChildren(typeof(UE.UI.GraphicRaycaster), true)

	--退出动画
	local maxExitTime = 0
	self.ChildUITweenControls = {}
	self.ChildTweenControlsForExit = {}
	local tmpTweenControls = self.Transform:GetComponentsInChildren(typeof(CS.UITweenControl), true)
	local tmpTweenControl = nil
	for i=0,tmpTweenControls.Length-1 do
		local isShared = false
		tmpTweenControl = tmpTweenControls[i]
		if tmpTweenControl.enabled and tmpTweenControl.isAuto then
			table.insert(self.ChildUITweenControls, tmpTweenControl)
			isShared = true
		end
		if tmpTweenControl.isAuto and tmpTweenControl.forExit then
			local tweenCtrlWrap = {
				isShared = isShared,
				com = tmpTweenControl
			}
			table.insert(self.ChildTweenControlsForExit, tweenCtrlWrap)
			local total = tmpTweenControl.delay + tmpTweenControl.duration
			if maxExitTime < total then
				maxExitTime = total
			end	
		end
	end
	self.ExitTime = maxExitTime
	--回调接口
	self:OnCreate()

	self.IsPrepare = true	
end


--内部方法--
function UITemplate:InternalUnLoad()
	self:OnDestroy()
	self:InternalCleanup()
	self:_PlayExitAndRelease()
end

function UITemplate:InternalCleanup()
	--移除所有监听
	for k, v in pairs(self.Listeners) do
		for priority, closure in pairs(v) do
			EventSystem.RemoveListener(k, closure, priority)
		end
	end
	self.Listeners = nil
end

function UITemplate:ResetChildCanvas()
	--self.ChildCanvas = self.Transform:GetComponentsInChildren(typeof(UE.Canvas), true)
	self.ChildCanvas = {}
	local canvasArray = self.Transform:GetComponentsInChildren(typeof(UE.Canvas), true)
	--注意：这是C#数组，索引从1开始，跳过父类的Canvas
	for i=1,canvasArray.Length-1 do
		table.insert(self.ChildCanvas, {canvas = canvasArray[i], sortingOrder = canvasArray[i].sortingOrder})
	end
end

function UITemplate:_PlayExitAndRelease()
	for i, v in ipairs(self.ChildTweenControlsForExit) do
		v.enabled = true 
		if v.isShared then
			v.com:PlayReverse()
		end	
	end
	if self.ExitTime>0 then
		TimerManager.Create(ETimerType.Once, self.PanelName, self.ExitTime, function ( ... )
			self:__release()
		end)
	else
		self:__release()	
	end
end

--内部方法--
function UITemplate:InternalUpdate()
	self:OnUpdate()
end