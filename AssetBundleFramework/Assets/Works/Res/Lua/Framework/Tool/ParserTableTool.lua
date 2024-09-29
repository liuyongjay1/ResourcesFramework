--[[
    解析配表文字，支持编程文本
    ParserTableTool.GetCellString(str)
    规则：
    使用@符号把配表字符串分段，分三段去读表
    tableName,rowName,colName
]]

ParserTableTool = {}


ParserTableTool.template = [[
    local tableName = TableNameConfig.Table_%s
    local keyName = "%s"
    local rowData = TableManager.GetRowDataByKey(tableName,keyName)
    if rowData == nil then
        LogManager.LogError("TableManager.GetRowDataByKey is nil,tableName:".. tostring(tableName).."  keyName: " .. tostring(keyName))
    end
    local cellData = rowData["%s"]
    local dataTable = {}
    ParserTableTool.GetSectionTab(dataTable,cellData)
    local result = ""
    for index, info in ipairs(dataTable) do
        if info.type == "text" then 
            result = result .. info.content
        elseif info.type == "code" then  
            local loadFuncStr = string.format(ParserTableTool.template,info.tableName,info.rowName,info.colName)
            local returnValue = assert(load(loadFuncStr))()
            result = result .. returnValue
        end
    end
    return result
    --local tableName,rowName,colName = ParserTableTool.SeperateCode(cellData)
    --local loadFuncStr = string.format(ParserTableTool.template,tableName,rowName,colName)
    --local returnValue = assert(load(loadFuncStr))()
    --return returnValue

]]


-- function ParserTableTool.CheckHasReplace(str)
--     --i是开始，j是结束
--     local i = string.find(str,"@")
--     local result =  string.sub(str, i)
--     local strArr_AT = StringTool.split(str,"@")

--     local codeString = ""
--     --正文内容
--     -- i==1证明编程字段在开头
--     if i == 1 then
--         codeString = strArr_AT[1]
--         if #strArr_AT > 1 then
--             result.text = "%s"..strArr_AT[2]
--         end

--     else -- i~=1证明编程字段在中间，把正文拆成了两段
--         codeString = strArr_AT[2]
--         if #strArr_AT == 2 then
--             result.text = strArr_AT[1] .."%s"
--         elseif #strArr_AT == 3 then
--             result.text = strArr_AT[1] .."%s".. strArr_AT[3]

--         end
--     end
--     local strArr_Equal = StringTool.split(codeString,"=")
    
--     result.tableName = strArr_Equal[1]
--     result.rowName = strArr_Equal[2]
  
--     if strArr_Equal[3] == "&" then
--         result.colName = LanguageTool.CurLanguage
--     else
--         result.colName = strArr_Equal[3]
--     end
--     return false,result
-- end

function ParserTableTool.SeperateCode(code)
    local dataTableInfo = {}
    local strArr_Equal = StringTool.split(code,"=")
    local tableName = strArr_Equal[1]
    local rowName = strArr_Equal[2]
    local colName = ""
    --默认如果&代表直接按当前语言取列，不是&则写入列明
    if strArr_Equal[3] == "&" then
        colName = LanguageTool.CurLanguage
    else
        colName = strArr_Equal[3]
    end
    return tableName,rowName,colName
end

--[[
    这个方法把string按@符号裁切成对应的段落
]]
function ParserTableTool.GetSectionTab(result,str)
	local content = {}
	local i = string.find(str,"@")
	if i == nil then
		local section = {}
		section.type = "text"
		section.content = str
		table.insert(result, section)
        return
	end
	if i == 1 then--符号在第一位
		local section = {}
		section.type = "code"
		str = string.sub(str,2)
		local j = string.find(str,"@")
		section.content = string.sub(str,1,j - 1)--截取到第二个@符号
        local tableName,rowName,colName = ParserTableTool.SeperateCode(section.content)
        section.tableName = tableName
        section.rowName = rowName
        section.colName = colName

		str = string.sub(str,j + 1)
      
		table.insert(result, section)
		ParserTableTool.GetSectionTab(result,str)
	else	--@符号不在开头
		local section = {}
		section.type = "text"
		section.content = string.sub(str,1, i - 1)
		str = string.sub(str,i)
		table.insert(result, section)
		ParserTableTool.GetSectionTab(result,str)
	end
end

--[[
    解析配表文字，支持编程文本
]]
function ParserTableTool.GetCellString(str)
    local dataTable = {}
    ParserTableTool.GetSectionTab(dataTable,str)
    local result = ""
    for index, info in ipairs(dataTable) do
        if info.type == "text" then 
            result = result .. info.content

        elseif info.type == "code" then  
            local loadFuncStr = string.format(ParserTableTool.template,info.tableName,info.rowName,info.colName)
            local returnValue = assert(load(loadFuncStr))()
            result = result .. returnValue
        end
    end
    return result
end
