--[[
    配表工具
]]
DataTableTool = {}

--[[
    检查配表字段是否存在
]]
function DataTableTool.CheckValueExist(value)
    if value == 0 then 
        return false
    end
    if value == nil then
        return false
    end
    return true
end

--[[
    从一行数据中读取对应语言的列
]]
function DataTableTool.GetColFromTableByLan(rowData)
    local cellData = rowData[LanguageTool.CurLanguage]
    return cellData
end

--[[
    根据人物Id读取人名
]]
function DataTableTool.GetRoleNameById(roleId)
    local key = string.format("name_%s_0",roleId)
    local rowData = TableManager.GetRowDataByKey(TableNameConfig.Table_RoleName,key)
    local name = DataTableTool.GetColFromTableByLan(rowData)
    return name
end

