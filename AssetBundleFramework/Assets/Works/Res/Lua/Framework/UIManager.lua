
--UI管理类--
UIManager = {
	IsRootLoaded = false
}
local this = UIManager

--C# UIManager单例
local CS_UIManagerInstance = CS.UIManager.Instance

--Start--
function UIManager.InitModule(LoadCanvasRootCallback)
	this.LoadCanvasRootCallback = LoadCanvasRootCallback
	require "Framework/UIPanel"
	UIManager.CreateUIRoot()
end

--创建Root--
function UIManager.CreateUIRoot()
	CS_UIManagerInstance:LoadCanvasRoot(this)
end

--C#通知加载成功
function UIManager.OnRootLoaded()
	this.IsRootLoaded = true
	--Canvas加载成功，通知框架继续下一步
	this.LoadCanvasRootCallback()

end

--加载常驻图集--
function UIManager.LoadCacheAtlas(atlasName)
	if this.AssetAtlas[atlasName] == nil then
		local asset = CS.MoAssetUIAtlas()
		local resName = "UIAtlas/" .. atlasName
		asset:AsyncLoad(resName, nil)
		this.AssetAtlas[atlasName] = asset
	end
end

--卸载指定图集--
function UIManager.UnLoadCacheAtlas(atlasName)
	local asset = this.AssetAtlas[atlasName]
	if asset then
		asset:UnLoad()
		this.AssetAtlas[atlasName] = nil
	end
end

--卸载所有图集
function UIManager.UnLoadAllAtlas()
	for name, v in pairs(this.AssetAtlas) do
		local asset = v
		asset:UnLoad()
		this.AssetAtlas[name] = nil
	end
end

--检测Root是否准备完毕--
function UIManager.CheckRootPrepare()
	return this.IsRootLoaded
end


-----------------------------------------------------Get接口--开始----------------------------------------------------------
--获取UI桌面
function UIManager.GetUIRoot()
	return this.UIRoot
end

--获取UIAtlas--
--返回CS.UnityEngine.U2D.SpriteAtlas
function UIManager.GetUIAtlas(name)
	local asset = this.AssetAtlas[name]
	if(asset ~= nil) then
		return asset.Atlas
	else
		LogManager.LogError("Not found SpriteAtlas : ", name)
	end
end

--获取面板--
function UIManager.GetPanel(panelName)
	for k,v in pairs(this.Stacks) do
		if(v.PanelName == panelName) then
			return v
		end
	end
end

-----------------------------------------------------Get接口--结束----------------------------------------------------------

-----------------------------------------------------业务层接口-开始---------------------------------------------------------
--[[
	打开UI接口
	Params@
		name:UI名
		paramTab需要向该UI传的参数，以Table的形式传过来,代码示例：UIManager.ShowUI("xx",{a = 1}),在xx中使用：self.paramTab.a
]]
function UIManager.ShowUI(uiName, paramTab)
	if paramTab ~= nil and type(paramTab) ~= "table" then
		UE.Debug.LogError("UIManager.ShowUI,paramTab should be table type")
	end
	if CS_UIManagerInstance then
		CS_UIManagerInstance:ShowUI(uiName,paramTab)
	end
end

--隐藏UI
function UIManager.CloseUI(uiName)
	if CS_UIManagerInstance then
		CS_UIManagerInstance:CloseUI(uiName)
	end
end

--隐藏UI
function UIManager.CloseAllUI()
	if CS_UIManagerInstance then
		CS_UIManagerInstance:CloseAllUI()
	end
end

--获取UI当前显示状态
--true显示，false隐藏
function UIManager.GetUIShowState(uiName)
	if CS_UIManagerInstance then
		return CS_UIManagerInstance:GetUIShowState(uiName)
	end
	return false
end

--[[
	锁定所有按钮，只能点击这一个，用于新手引导等功能
	提示：锁定会自动记录当前按钮状态，取消锁定后还原
]]
function UIManager.FocusButton(uiName,btnID)
	if CS_UIManagerInstance then
		CS_UIManagerInstance:FocusButton(uiName,btnID)
	end
end
--[[
	取消锁定所有按钮，恢复到锁定前状态
]]
function UIManager.CancleFocusButton()
	if CS_UIManagerInstance then
		CS_UIManagerInstance:FocusButton()
	end
end
--[[
	置按钮可点击状态
	Params@
		uiName:UI名称
		btnID:预设上填的按钮ID
		isActive:true开启点击
]]
function UIManager.SetBtnActive(uiName,btnID,isActive)
	if CS_UIManagerInstance then
		CS_UIManagerInstance:SetBtnActive(uiName,btnID,isActive)
	end
end

--[[
	获取按钮是否可点击
]]
function UIManager.GetBtnActiveState(uiName,btnID)
	if CS_UIManagerInstance then
		return CS_UIManagerInstance:GetBtnActiveState(uiName,btnID)
	end
	return false
end

-----------------------------------------------------业务层接口-结束---------------------------------------------------------

--[[
	为何会有这个接口？
	有的Lua脚本按功能文件夹分类，PanelPathMap就是文件夹路径
]]
function UIManager.GetPanelPath(name)
	if not PanelPathMap[name] then
		return "Panel/UIPanel/"..name
	else
		return "Panel/UIPanel/"..PanelPathMap[name].."/"..name
	end
end


