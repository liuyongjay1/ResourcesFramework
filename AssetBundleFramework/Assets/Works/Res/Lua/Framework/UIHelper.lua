--UI工具类--
UIHelper = {}

--获取子类Transform（如果没有找到不会报错）
function UIHelper.TryFindChild(name, root)
	local result = CS.UIHelper.FindChild(name, root)
	return result
end
--获取子类Transform--
function UIHelper.FindChild(name, root)
	--注意：如果这里报System.InvalidCastException异常，请检测root是否为Transform类型
	local result = CS.UIHelper.FindChild(name, root)
	if(result == nil) then
		LogManager.LogWarning("Not found child : ", name)
	end
	return result
end
--获取按钮--
function UIHelper.FindButton(name, root)
	return CS.UIHelper.FindComponent(name, root, "Button")
end
--获取文本--
function UIHelper.FindText(name, root)
	return CS.UIHelper.FindComponent(name, root, "Text")
end
function UIHelper.FindTMP(name, root)
	return CS.UIHelper.FindComponent(name, root, "TextMeshProUGUI")
end
--获取图片--
function UIHelper.FindImage(name, root)
	return CS.UIHelper.FindComponent(name, root, "Image")
end
--获取原图片--
function UIHelper.FindRawImage(name, root)
	return CS.UIHelper.FindComponent(name, root, "RawImage")
end
--获取输入按钮--
function UIHelper.FindInput(name, root)
	return CS.UIHelper.FindComponent(name, root, "InputField")
end

function UIHelper.FindTMPInput(name, root)
	return CS.UIHelper.FindComponent(name, root, "TMP_InputField")
end
--获取滑条--
function UIHelper.FindSlider(name, root)
	return CS.UIHelper.FindComponent(name, root, "Slider")
end
--获取下拉框--
function UIHelper.FindDropdown(name, root)
	return CS.UIHelper.FindComponent(name, root, "Dropdown")
end
--获取ScrollView
function UIHelper.FindScrollView(name, root)
	return CS.UIHelper.FindComponent(name, root, "ScrollRect")
end
--获取图集
function UIHelper.FindAtlas(name, root)
	return CS.UIHelper.FindComponent(name, root, "UIAtlas")
end

-- 获取TableView
function UIHelper.FindTableView( name, root )
	return CS.UIHelper.FindComponent(name, root, "UITableView")
end

-- 获取开关 Toggle
function UIHelper.FindToggle( name, root )
	return CS.UIHelper.FindComponent(name, root, "Toggle")
end

-- 获取开关 ToggleGroupSimple
function UIHelper.FindToggleGroupSimple( name, root )
	return CS.UIHelper.FindComponent(name, root, "UIToggleGroupSimple")
end

-- 获取粒子
function UIHelper.FindParticle( name, root )
	return CS.UIHelper.FindComponent(name, root, "ParticleSystem")
end

-- 获取 RectTransform
function UIHelper.FindRectTransform( name, root )
	return CS.UIHelper.FindComponent(name, root, "RectTransform")
end

-- 获取 Skeleton Graphic
function UIHelper.FindSkeletonGraphic(name, root)
	return CS.UIHelper.FindComponent(name ,root, "SkeletonGraphic")
end

--获取组件
function UIHelper.FindComponent(name, root, typeName)
	return CS.UIHelper.FindComponent(name, root, typeName)
end
--获取组件
function UIHelper.GetComponent(root, typeName)
	return CS.UIHelper.GetComponent(root, typeName)
end


function UIHelper.ChangeTMPFontAsset(text,fontAssetName)
	CS.UIHelper.ChangeTMPFontAsset(text,fontAssetName)
end

--解析颜色为RGB数值
function UIHelper.ParseColor(htmlString)
	local result, txtColor = UE.ColorUtility.TryParseHtmlString(htmlString)
	if(result == false) then
		LogManager.LogError("Falied parse color :", htmlString)
	end
	return txtColor
end


--OverTouch相关
function UIHelper.IsOverTouchUI()
	return CS.UIHelper.IsOverTouchUI()
end
function UIHelper.IsOverTouchElement(element, screenPoint, eventCamera)
	return CS.UIHelper.IsOverTouchElement(element, screenPoint, eventCamera)
end
function UIHelper.GetOverTouchElement()
	return CS.UIHelper.GetOverTouchElement()
end


--计算UI满屏分辨率
function UIHelper.CalculateFullScreenSize()
	local screenWidth = UE.Screen.width;
	local screenHeight = UE.Screen.height;
	local standardWidth = 2048;
	local standardHeight = 1150;
	
	local standardRate = standardWidth / standardHeight;
	local screenRate = screenWidth / screenHeight;
	local width = standardWidth;
	local height = standardHeight;

	if (screenRate > standardRate) then
		--以宽度进行适配的界面
		width = UE.Mathf.FloorToInt(standardHeight * screenRate);
		height = standardHeight * (width / standardWidth);	
	elseif(screenRate < standardRate) then	
		--以高度进行适配的界面
		height = UE.Mathf.FloorToInt(standardWidth / screenRate);
		width = standardWidth * (height / standardHeight);	
	end

	return width, height
end

--计算文字所占大小--
function UIHelper.GetTextPreferredSize(str, text, width, isEmoji)
	if not IsNil(text.font) then
		return CS.UIHelper.GetTextPreferredSize(text, str, width, isEmoji)
	else
		--执行UILocalizedFont的Awake
		local go = UE.GameObject.Instantiate(text.gameObject)
		local size = CS.UIHelper.GetTextPreferredSize(go:GetComponent("Text"), str, width, isEmoji)
		UE.GameObject.Destroy(go)
		return size
	end
end

--计算文字所占大小--
function UIHelper.GetTMPTextPreferredSize(str, text, width,height)
	if text.font == nil then
		LogManager.LogWarning(string.format("%s has null fontAsset", text.gameObject.name))
		text.font = CS.TMPro.TMP_Settings.defaultFontAsset
	end
	if text.fontSharedMaterial == nil then
		LogManager.LogWarning(string.format("%s has null material", text.gameObject.name))
		text.fontSharedMaterial = CS.TMPro.TMP_Settings.defaultFontAsset.material
	end
	return CS.UIHelper.GetTMPTextPreferredSize(text, str, width, height)	
end

--获取文字的lineCount,调用过此方法后CachedTextGenerator将被刷新
function UIHelper.GetTextLineCount(str, text, isEmoji)
	local width = text.rectTransform.rect.width
	return CS.UIHelper.GetTextLineCount(text, str, width, isEmoji)
end

--获取文字的lineCount
function UIHelper.GetTMPTextLineCount(str, text)
	if not str or str == "" then
		return 0
	end
	local width = text.rectTransform.rect.width
	return CS.UIHelper.GetTMPTextLineCount(text, str, width)
end

function UIHelper.SetFullScreen(monoObj)
	local rectTrans = monoObj:GetComponent("RectTransform")
	if rectTrans then
		local w = rectTrans.rect.width
		local h = rectTrans.rect.height
		local scale = math.max( UIManager.ScreenSize.width / w, UIManager.ScreenSize.height / h )
		rectTrans.sizeDelta = UE.Vector2(w*scale, h*scale)
		return scale
	end
end

function UIHelper.SetFullScreenFixHeight(monoObj)
	local rectTrans = monoObj:GetComponent("RectTransform")
	if rectTrans then
		local w = rectTrans.rect.width
		local h = rectTrans.rect.height
		local scale = UIManager.ScreenSize.height / h
		rectTrans.sizeDelta = UE.Vector2(w*scale, h*scale)
		return scale
	end
end

function UIHelper.SetObjScale(monoObj,scale)
	local rectTrans = monoObj:GetComponent("RectTransform")
	if rectTrans then
		local w = rectTrans.rect.width
		local h = rectTrans.rect.height
		rectTrans.sizeDelta = UE.Vector2(w*scale, h*scale)
	end
end

function UIHelper.SetBlurMask(mask, delay)
	UIHelper.SetFullScreen(mask)
	UIManager.FetchScreenMask(mask, delay)
end

function UIHelper.SetTextureSheetAnimationStartFrame(ps, frame)
	CS.UIHelper.SetTextureSheetAnimationStartFrame(ps, frame)
end

function UIHelper.RemoveMissingChars(inputField, content)
	if CS.TMProHelper.RemoveMissingChars then
		return CS.TMProHelper.RemoveMissingChars(inputField.textComponent.font, content)
	else
		local found = inputField.textComponent.font:HasCharacters(content)
		return not found
	end
end