--事件管理类--
TableManager = {}

local xpcall = xpcall;
local traceback = traceback;

--加载中列表
local LoadingList = {}

--表数据缓存
local TableCache = {}
-- 系统初始化
function TableManager.InitModule(loadFinishCallback)    
	TableManager.loadFinishCallback = loadFinishCallback
	local allTable = require 'Framework/Table/AutoGen/AllTables'
	allTable.StartLoadAllTable()
end



-----------------------------------------------------Get接口--开始----------------------------------------------------------
function TableManager.GetRowDataById(tableName,id)
	if TableCache[tableName] == nil then
		LogManager.LogError("table not exist,tableName: " ..tableName)
		return nil
	end
	if TableCache[tableName].tableConfig.KeyName ~= "id" then
		LogManager.LogError("table first col not id" .. tableName)
		return nil
	end
	return TableCache[tableName].allRow[id]
end

function TableManager.GetRowDataByKey(tableName,key)
	if TableCache[tableName] == nil then
		LogManager.LogError("table not exist,tableName: " ..tostring(tableName))
		return
	end
	if TableCache[tableName].tableConfig.KeyName ~= "key" then
		LogManager.LogError("table first col not key" .. tostring(tableName))
		return nil
	end
	return TableCache[tableName].allRow[key]
end
--[[
	Params@：
		tableName：表名
		restrictKey：限制条件，例如{layer = 7},代表 列名：layer 行值：7的一整行数据加入返回列表
		sortFunc:排序方法，由开发者传入，举例：
											local function sortFunction(a,b)
												return a.layer < b.layer --layer列从小到大排序
											end
]]
function TableManager.GetTable(tableName,restrictKey,sortFunc)
	local targetTable = TableCache[tableName]
	if targetTable == nil then
		LogManager.LogError("table not exist,tableName: " ..tableName)
		return
	end
	--返回列表
	local tab = {}
	--如果没有限制条件，直接返回原数据
	if restrictKey == nil then
		for index, rowData in pairs(targetTable.allRow) do
			table.insert(tab, rowData)
		end
	else
		local checkRestrictKey = false
		for restrictKey,value in pairs(restrictKey) do
			for index, rowData in pairs(targetTable.allRow) do
				--先检验一下表有没有该列，restrictKey是开发者传入的
				if checkRestrictKey == false then
					if rowData[restrictKey] == nil then
						LogManager.LogError("table not exist col,colName: " .. restrictKey)
						return nil
					end
					checkRestrictKey = true
				end
				if rowData[restrictKey] == value then
					table.insert(tab, rowData)
				end
			end
		end
	end
	--执行排序方法
	if sortFunc ~= nil then
		table.sort(tab,sortFunc)
	end
	return tab
end
-----------------------------------------------------Get接口--结束----------------------------------------------------------

-----------------------------------------------------配表加载流程--开始----------------------------------------------------------
function TableManager.AddLoadTableTask(tableName)
	table.insert(LoadingList,tableName)
end

--[[
	表数据解析成功回调
	首列是id的，以id排序保存
	首列是key的，不排序
]]
function TableManager.LoadTableSuccess(tableName,allRow,tableConfig)
	LogManager.LogProcedure("Load table success,tableName: ".. tableName)
	TableCache[tableName] = {}
	TableCache[tableName].allRow = {}
	if tableConfig.KeyName == "id" then--以INT类型id为第一列
		for index, rowData in ipairs(allRow) do
			TableCache[tableName].allRow[rowData.id] = rowData
		end
		--默认按id排序
		local function sortFunction(a,b)
			return a.id < b.id
		end
		table.sort(TableCache[tableName].allRow, sortFunction)
	else --以STRING类型key为第一列
		for index, rowData in ipairs(allRow) do
			TableCache[tableName].allRow[rowData.key] = rowData
		end
	end

	TableCache[tableName].tableConfig = tableConfig

	for index = 1, #LoadingList do
		if LoadingList[index] == tableName then
			table.remove(LoadingList, index)
			break
		end
	end
	--全部加载成功，通知框架继续下一步
	if #LoadingList == 0 then
		TableManager.loadFinishCallback()
	end
end
-----------------------------------------------------配表加载流程--结束----------------------------------------------------------