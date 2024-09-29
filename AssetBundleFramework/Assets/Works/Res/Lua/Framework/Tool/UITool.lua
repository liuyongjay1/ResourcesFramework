UITool = {}

function UITool.RunStringCode(strCode)
    return assert(load(strCode))()
end

--[[
    获取图集相对路径
]]
function UITool.GetAtlasFullPath(atlasName)
    return "UIAtlas/"..atlasName ..".spriteatlas"
end

--[[
    获取图集相对路径
]]
function UITool.GetScreenEffectFullPath(effectName)
    return "Prefabs/ScreenEffectPrefab/" .. effectName .. ".prefab"
end

--[[
    获取图集相对路径
]]
function UITool.GetUIBackgroundPath(localPath)
    return "Texture/BG/" .. localPath
end

--[[
    根据章节Id获取表名
]]
function UITool.GetTableNameByChapterIndex(typeStr)
    local tableNameStr = string.format(typeStr,ChapterManager.GetCurChapterIndex())
    local codeStr = string.format("local tableName = TableNameConfig.%s;return tableName",tableNameStr)
    local tableName = UITool.RunStringCode(codeStr)
    return tableName
end

