-- 基本功能与功能扩展 Lua5.3
-- deve.huang 2014.8

Utils = Utils or {}

funcFree = function (...) end

-- function argsToString(...)
-- 	local str = ""
-- 	for i,v in ipairs({...}) do
-- 		str = str .. tostring(v) .. " "
-- 	end
-- 	return str
-- end

local log = LogManager.LogInfo
local logTrace = Debug.Trace
local logWarnning = LogManager.LogWarning
local logError = LogManager.LogError

---------------------------------------------------
-- 适用于调试时
local function tostring_do( obj, mode, split, depth, depthMax, sj, sn, sl, sv, sc, sp, bLv0 )
	if (depth == nil) then depth=0; end

	local m0="" --move space
	for n=0,depth-1,1 do m0=m0 .. sp; end
	local isArr = mode>2 and obj[1] and true or false
	local str = (isArr and "[" or "{") .. sj
	
	local mov=""
	for n=0,depth-0,1 do mov=mov .. sp; end
	local s = ""
	local t = ""  --vtype

	local ai = 0
	for i,f in pairs(obj) do

		if type(i) == "number" then
			s = '['..i..']'.. sc
		elseif type(i) == "string" then
			s = sl..i..sl.. sc
		else
			s = '<>'..tostring(i).. sc
		end

		t = type(f) --field
		if (t == "table" and (not bLv0)) then
			if showIdx then
				str = str..mov..i.." = {"..type(obj).."}".. sn
			end
			if depthMax and depth > depthMax then
				str = str..mov..s.."<{"..type(f).."}>".. sn
			else
				if not depthMax and f.__cname then
					str = str..mov..s.."[".. f.__cname.."]"

					local t = f.super
					if t then
						for i = 1, 5 do
							if t and t.__cname then
								str = str.."[".. t.__cname.."]"
								t = t.super
							else
								break
							end
						end
					end
					str = str .. sn
				else
					str = str..mov..s..tostring_do(f, mode, split, depth+1, depthMax, sj, sn, sl, sv, sc, sp, bLv0 )
				end
			end

		elseif (t == "number") then
			str = str..mov..s.. tostring(f) .. sn
		elseif (t == "string") then
			if mode == 2 then
				f = string.gsub( f, '\'', '\\\'' )
				f = string.gsub( f, '\"', '\\\"' )
			end
			str = str..mov..s.. sv..f..sv .. sn
		elseif (t == "boolean") then
			str = str..mov..s.. tostring(f) .. sn

		elseif (t == "function") then
			str = str..mov..i.. "()" .. sn
		elseif (t == "nil") then
			str = str..mov..s.. "nil" .. sn
		elseif f.ToString then
			-- str = str..mov..i.. "<userdata=[" ..tolua.type(f) .."]>".. sn
			-- str = str..mov..i.. "[" ..tolua.type(f) .."]".. sn
			-- str = str..mov..i.. "[" .. typeof(f) .."]".. sn
			str = str..mov..i.. "[<" .. f:ToString() ..">]".. sn
		else
			str = str..mov..i.. "<userdata=[" ..f .."]>".. sn
		end
	end
	if mode and mode>1 then str = string.sub(str, 1, #str-1) ..sj end
	str = str..m0.. (isArr and "]" or "}") .. (depth==0 and sj or sn)
	return str
end

-- mode nil/log  1/log 2/json-lua 3/json-nor
function ToString(obj, mode, split, depth, depthMax)
	if (obj == nil) then return "nil" end
	if (type(obj) ~= "table") then return ToStringC(obj) end
	if depth == nil then depth=0 end
	if depthMax and depth > depthMax then return obj end

	if not mode then mode = 0 end
	if not split then split = "\n" end
	local showIdx=false
	local sj=nil  --Obj分割符 split
	local sn=nil  --行分割符
	local sl=nil  --key引号
	local sv=nil  --值引号
	local sc=nil  --键值连接符
	local sp=nil  --行前的空格
	local bLv0=nil
	if     mode == 0  then showIdx=false; sj=split; sn=split; bLv0=nil;	 sl="";    sv="\'";  sc=" = ";  sp="    "  --@
	elseif mode == 1  then showIdx=true;  sj=split; sn=split; bLv0=true; sl="\'";  sv="\'";  sc=" = ";  sp="    "
	elseif mode == 2  then showIdx=false; sj=split; sn=","..split;		 sl="";    sv="\'";  sc=" = ";  sp="\t"
	elseif mode == 3  then showIdx=false; sj=split; sn=","..split;		 sl="\"";  sv="\"";  sc=" : ";  sp="\t"
	end
	return tostring_do( obj, mode, split, depth, depthMax, sj, sn, sl, sv, sc, sp, bLv0 )
end


function ToStringB(obj)
	return ToString(obj, 1)
end

function ToStringC(obj)
	if type(obj) == "boolean" then
		return tostring(obj)
	elseif type(obj) == "number" then
		return obj
	elseif type(obj) == "string" then
		return string.format("'%s'", obj)
	else
		return tostring(obj)
	end
end

------------------Algorithms-------------------
-- 冒泡排序
-- list:  排序对象
-- order: 排序方式： -1(<从小到大 升序)  1(>从大到小 降序)
algs = {}
function algs.BubbleSort( list, order )
	local needSweep = function ( a, b )
		if order == -1 then
			return a > b
		end

		return a < b
	end
	
	for i = #list - 1, 1, -1 do
		for j = 1, i do
			if needSweep(list[j], list[j + 1]) then
				list[j], list[j + 1] = list[j + 1], list[j]
			end
		end
	end
end

-----------------------------------------------
--多属性排序
--sortFields: 属性列表  如:{"type","lvl"}
--sortOrder : 排序方式： -1(<从小到大 升序)  1(>从大到小 降序)
-- 例 table.sortOn( tmp, {"num", "race"}, 1)
-- 例 table.sortOn( ret, {"level", "quality", "cardId"}, {1,1,-1} )
function table.sortOn(tab, sortFields, sortOrder)
	if type(tab)~="table" then
		logError("table.sortOn: tab abnormity")
		return
	end
	if type(sortFields)=="string" then sortFields={sortFields} end
	if not sortOrder then sortOrder = -1 end

	local L = #sortFields
	local k = nil
	local order = sortOrder
	local function sortCondition(a,b)
		for i=1,L do
			if type(sortOrder) == "table" then
				order = sortOrder[i] or -1
			end
			k=sortFields[i]
			if a[k] == b[k] then
			else
				if nil == a[k] or nil == b[k] then
					if EnabledLuaDebug then
						logTrace("[table.sortOn] 缺少字段值 k/i:", tostring(k), i)
					end
				elseif order==-1 then
					return a[k] < b[k]
				else
					return a[k] > b[k]
				end
			end
		end
		if order==-1 then
			return a[k] > b[k]
		else
			return a[k] < b[k]
		end
	end
	table.sort( tab, sortCondition )
end
--数值列表排序
--order     : 排序方式： -1(<从小到大 升序)  1(>从大到小 降序)
function table.sortNum(tab, order)
	if not order then order = -1 end

	if order == -1 then
		table.sort( tab, function(a,b) return a<b end )
	else
		table.sort( tab, function(a,b) return a>b end )
	end
	return tab
end

function table.splitListByNum( ls, splitNum )
	local ret = {}
	local tmp = {}
	for i,v in ipairs( ls ) do
		table.insert( tmp, v )
		if #tmp >= splitNum then
			table.insert( ret, tmp )
			tmp = {}
		end
	end
	if #tmp > 0 then
		table.insert( ret, tmp )
	end
	return ret
end

function table.mergeList( a, b )
	for i,v in ipairs(b) do
		table.insert( a, v )
	end
end
function table.clone(tab, full)

	if not tab then
		return
	end

	if tab.cloneCfg then
		return tab.cloneCfg(tab)
	end

	if type(full)~="boolean" then full = false end
	local ret = {}
	for i,v in pairs(tab) do
		if full and "table" == type(v) then
			ret[i] = table.clone(v, full)
		else
			ret[i] = v
		end
	end
	return ret
end

function table.cloneByPairs(tab, full)
	return table.clone(tab, full)
end

function table.deepClone(tab, full)

	if not tab then
		return
	end

	if tab.cloneCfg then
		return tab.cloneCfg(tab)
	end

	if type(full)~="boolean" then full = false end
	local ret = {}
	for i,v in pairs(tab) do
		if full and "table" == type(v) then
			ret[i] = table.clone(v, full)
		else
			ret[i] = v
		end
	end	
	return setmetatable(ret, getmetatable(tab))
end

function table.cloneByIndex(tab, full)
	if type(full)~="boolean" then full = false end
	local ret = {}
	for i,v in ipairs(tab) do
		if full and "table" == type(v) then
			ret[i] = table.clone(v, full)
		else
			ret[i] = v
		end
	end
	return ret
end

function table.cutting( ls, startIdx, endIdx )
	startIdx = startIdx or 1
	endIdx = endIdx or #ls
	local ret = {}
	for i,v in ipairs(ls) do
		if startIdx <= i and i <= endIdx then
			table.insert( ret, v )
		elseif i > endIdx then
			break
		end
	end
	return ret
end
function table.removeRang( ls, iStart, iEnd )
	iStart = iStart or 1
	iEnd = iEnd or #ls
	local ret = {}
	for i,v in ipairs(ls) do
		if i < iStart or iEnd < i then
			table.insert( ret, v )
		end
	end
	return ret
end
function table.removeRepeat( tab, k )
	local key = {}
	local idx = 1
	for i = 1, #tab do
		local v = tab[ idx ]
		if v then
			if not key[ v[k] ] then
				key[ v[k] ] = true
				idx = idx+1
			else
				table.remove( tab, idx )
			end
		end
	end
end
function table.getUnRepeat( tab, k )
	local ret = {}
	local key = {}
	for i = 1, #tab do
		local v = tab[ i ]
		if v then
			if not key[ v[k] ] then
				key[ v[k] ] = true
				table.insert( ret, v )
			end
		end
	end
	return ret
end

function table.indexOf(tab, ele)
	for i,v in ipairs(tab) do
		if v == ele then
			return i
		end
	end
	return -1
end

function table.removeElement(tab, ele)
	for i,v in ipairs(tab) do
		if v == ele then
			return table.remove( tab, i )
		end
	end
end

function table.reverse(tab)
	local ret = {}
	local L = #tab
	for i,v in ipairs(tab) do
		ret[L-i+1] = v
	end
	return ret
end

function table.arrayToString(arr, sep)
	return table.concat( arr, sep  or "")
end

function table.arrayToMap(arr, defValue)
	local useIdx = false
	if type(defValue)=="nil" then 
		defValue = true
	elseif type(defValue)=="string" and defValue=="INDEX" then
		useIdx = true
	end
	
	local ret = {}
	for i,v in ipairs(arr) do
		ret[v] = useIdx and i or defValue
	end
	return ret
end
function table.mapKeyToList(map)
	local ret = {}
	for k,v in pairs(map) do
		table.insert(ret, k)
	end
	return ret
end
function table.mapValueToList(map)
	local ret = {}
	for k,v in pairs(map) do
		table.insert(ret, v)
	end
	return ret
end

function table.size( t )
	local len = 0
	local tb = t or {}
	for _, v in pairs(tb) do
		if v then
			len = len + 1
		end
	end

	return len
end


function table.containsValue( t,val )
	for _, value in pairs(t) do
		if value == val then
			return true
		end
	end
	return false
end

function table.swapKeyAndValue(t)
	local ret = {}
	for k, v in pairs(t) do
		ret[v] = k
	end
	return ret
end

-- Map合并
-- 将b的内容整合入a
function table.mergeMap(a, b)
	for key, value in pairs(b) do
		a[key] = value
	end
end

function table.isSame(t1, t2)
	if t1 == t2 then
		return true
	elseif (t1 == nil and t2 ~= nil) or (t1 ~= nil and t2 == nil) then
		return false
	end
	for k, v in pairs(t1) do
		if not t2[k] or t2[k] ~= v then
			return false
		end
	end
	for k, v in pairs(t2) do
		if not t1[k] or t1[k] ~= v then
			return false
		end
	end
	return true
end

-- idx/value最接近tab元素项
function Utils.arrayNear(tab, ri, rv)
	if (not ri) and (not rv) then return nil, nil end

	local ti = 0
	local td = 100000000000
	local d = 0
	for i,v in pairs(tab) do
		if     ri then d = math.abs(i-ri)
		elseif rv then d = math.abs(v-rv) end
		if d < td then td=d; ti=i end
	end
	return ti, tab[ti]
end

function Utils.getMD5String( str )
	-- return cus.XUtils:getInstance():getStringMD5Hash( str )
	return ME.Utility.HashUtility.StringSHA1(str) -- SHA1
end

function Utils.getDeviceID()
	-- return "51BC5960-D9F1-4E01-B7CA-D05462A41367"
	return CS.DeviceHelper.Instance.UUID
end

-----------------------------------------------
function getFormat( ... )
	return string.format( ... )
end

function string.rfind( s, pattern )
	local idx = nil
	local i = 0
	while true do
		i = string.find(s, pattern, i+1)
		if i == nil then
			break
		else
			idx = i
		end
	end
	return idx
end

-- --字符串分割为数组
-- -- sep:分割符     点 : "%p"
-- --       txt文件分行 : "\r\n"
-- function string.split(str, sep, fix, ftype)
-- 	if not sep then sep = "," end
-- 	if not fix then fix = 0 end

-- 	local trans = function (v)
-- 		if not ftype then
-- 		elseif ftype == "number" then
-- 			v = tonumber(v)
-- 		end
-- 		return v
-- 	end
-- 	local arr = {}
-- 	local p0,p1 = 0,0
-- 	while (#str > 0) do
-- 		p0,p1 = string.find(str,sep)
-- 		if not p0 then
-- 			break
-- 		else
-- 			table.insert( arr, trans(string.sub(str, 1,p0-1-fix)))
-- 			str = string.sub(str, p0+(p1-p0)+1,#str)
-- 		end
-- 	end
-- 	table.insert( arr, trans(str))
	
-- 	return arr
-- end

-- for ProjTank
-- function Utils.getSplitTab(str)
-- 	local list = string.split(string.sub(str, 2,#str), "|", nil, "number")
-- 	return list
-- end
-- function Utils.getSplitEle(str, i)
-- 	local list = string.split(string.sub(str, 2,#str), "|")
-- 	local n = list[i]
-- 	return n and tonumber(n) or nil
-- end
function Utils.getTabEleChg(tab)
	for i,v in ipairs(tab) do
		tab[i] = tonumber(v)
	end
	return tab
end
function Utils.getTabChgEle(tab, i)
	return tonumber(tab[i])
end


-- 无法用于嵌套结构
local _keyTypes = 
{
	boolean = 1,
	number = 2,
}
function Utils.tableToStr(tab)
	local ret = ""
	for k,v in pairs(tab) do
		ret = k .."=".. tostring(v).."&".. (_keyTypes[type(v)] or "0") .."||".. ret
	end
	return ret
end
function Utils.strToTable(str)
	local ret = {}
	local t = string.split(str, "||")
	for i,v in ipairs(t) do
		if v == "" then

		else
			local l,r = string.find(v,"=")
			if l then
				local k = string.sub(v, 1, l-1)
				local cx = string.sub(v, r+1, -1)
				local x,y = string.find(cx,"&")
				local c = string.sub(cx, 1, x-1)
				local t = string.sub(cx, y+1, -1)
				if t == '1' then
					if c == "true" then c = true end
					if c == "false" then c = false end
				elseif t == '2' then
					c = tonumber( c )
				end
				ret[k] = c
			else
				table.insert( ret, v )
			end
		end
	end
	return ret
end

-- 在str中以d的方向 按r的步长加sep
function string.join(str, sep, r, d)
	if not sep then sep = "" end
	if not r then r = 1 end
	if not d then d = 0 end --dir

	local ret = ""
	local l = #str
	local i = 1
	local n = 0
	for t=1,l do
		if i+r > l then sep = "" end
		if d < 0 then
			n = l-i-r+2
			ret = string.format("%s%s%s", sep, string.sub(str, n<1 and 1 or n, l-i+1), ret)
		else
			ret = string.format("%s%s%s", ret, string.sub(str, i, i+r-1), sep)
		end
		i = i+r
		if i > l then break end
	end
	return ret
end

function string.TrimStart( str, trimStr )
	if string.sub(str, 1, #trimStr) == trimStr then
		return string.sub(str, #trimStr+1)
	end
	return str
end
function string.TrimEnd( str, trimStr )
	if string.sub(str, #str-#trimStr+1, #str) == trimStr then
		return string.sub(str, 1, #str-#trimStr)
	end
	return str
end

-- hex: 0xff1199 lua版bit操作太慢
-- function string.numToRGB( hex )
-- 	return  bit._and( bit._rshift(hex,16), 0xff ),
-- 			bit._and( bit._rshift(hex,8), 0xff ),
-- 			bit._and( hex, 0xff )
-- end
function string.hexToRGB(hexstr)
	if (not hexstr) or type(hexstr)~="string" or #hexstr<6 then
		logError("ERROR [string.hexToRGB] not param")
		return 255,255,255
	end
	if string.find(hexstr,"#") == 1 then
		hexstr = string.sub(hexstr,2)
	end
	if string.find(hexstr,"0x") == 1 then
		hexstr = string.sub(hexstr,3)
	end

	if (#hexstr < 6) then
		-- for i=#hexstr , 6-1 do hexstr = '0' .. hexstr end
		for i=#hexstr , 6-1 do hexstr = hexstr .. '0' end
	end
	return  tonumber(string.sub(hexstr, 1, 2),16),
			tonumber(string.sub(hexstr, 3, 4),16),
			tonumber(string.sub(hexstr, 5, 6),16)
end
function string.hexToRGBA(hexstr)
	if string.find(hexstr,"#") == 1 then
		hexstr = string.sub(hexstr,2)
	end
	if string.find(hexstr,"0x") == 1 then
		hexstr = string.sub(hexstr,3)
	end

	if (#hexstr < 8) then
		for i=#hexstr , 8-1 do hexstr = hexstr .. '0' end
	end
	return  tonumber(string.sub(hexstr, 1, 2),16),
			tonumber(string.sub(hexstr, 3, 4),16),
			tonumber(string.sub(hexstr, 5, 6),16),
			tonumber(string.sub(hexstr, 7, 8),16)
end

function string.hexToRGB_F(hexstr)
	local r,g,b = string.hexToRGB(hexstr)
	return r/255,g/255,b/255
end
function string.hexToRGBA_F(hexstr)
	local r,g,b,a = string.hexToRGBA(hexstr)
	return r/255,g/255,b/255,a/255
end

function string.hexToC3B(hexstr)
	local r,g,b = string.hexToRGB(hexstr)
	return cc.c3b(r,g,b)
end
function string.hexToC4F(hexstr)
	local r,g,b,a = string.hexToRGBA_F(hexstr)
	return cc.c4f(r,g,b,a)
end

function string.fixZero( s )
	return string.len(s) < 2 and '0'..s or s
end
function string.c3bToHex( c )
	local s = string.fixZero( string.format("%X", c.r ) )
			..string.fixZero( string.format("%X", c.g ) )
			..string.fixZero( string.format("%X", c.b ) )
	return s
end
function string.c3fToHex( c )
	local s = string.fixZero( string.format("%X", math.floor(c.r*255 ) ))
			..string.fixZero( string.format("%X", math.floor(c.g*255 ) ))
			..string.fixZero( string.format("%X", math.floor(c.b*255 ) ))
	return s
end

-- 十进制转到十六进制
function string.hexToDec( s )
	-- return tonumber( s, 16 )
	return string.format("%d", s)
end
-- 十进制转到十六进制
function string.decToHex( s )
	return string.format("%#x", s)
end



local _fmtNum = 
{
	[3]="K",
	[6]="M",
	-- [9]="G",
	-- [12]="T",
	-- [15]="P",
}
-- s _fmtNum
-- sep 分段符
-- width 返回的最大字符长度
function string.formatNum(n, sep, width)
	if not sep then sep = "," end
	if not width then width = 8 end

	local s = tostring(n)
	if #s > width then
		local l = Utils.arrayNear(_fmtNum, #s-width)
		if l and _fmtNum[l] then
			s = string.sub(s, 1,#s-l)
			-- logWarnning("formatNum", s)
			if sep and #sep>0 then s = string.join(s, sep, 3, -1) end
			return s .. _fmtNum[l]
		end
	end
	if sep and #sep>0 then s = string.join(s, sep, 3, -1) end
	return s
end
-- 小数点前后一共5位,小数点后面是前面的用完之后剩下的; 超过99999k再用m，
-- width 返回位数
-- round 是否执行4舍5入
function string.formatMoney(num, width, round)
	if type(num) ~= "number" then
		if type(num) == "string" then
			local tmpNum = tonumber(num)
			if type(tmpNum) == "number" then
				num = tmpNum
			else
				return num
			end
		else
			return num
		end
	end
	if not width then width = 5 end
	if num >= 10000000 then
		local i = math.floor( num / 1000000 )
		local f = num / 1000000
		if i >= 10000 or i == f then
			return i .. "M"
		end
		if round then
			if width <= #tostring(i) then
				return math.round(f).."M"
			else
				return math.round(f, width - #tostring(i)).."M"
			end
		end
		if width <= 0 then
			return math.floor(f) .. "M"
		else
			return string.sub(tostring(f), 1, width + 1) .. "M"
		end

	elseif num >= 1000000 then
		local i = math.floor( num / 1000 )
		local f = num / 1000
		if i >= 10000 or i == f then
			return i .. "K"
		end
		if round then
			if width <= #tostring(i) then
				return math.round(f).."K"
			else
				return math.round(f, width - #tostring(i)).."K"
			end
		end
		if width <= 0 then
			return math.floor(f) .. "K"
		else
			return string.sub(tostring(f), 1, width + 1) .. "K"
		end
	end
	return num
end


-- 系统字不支持自动换行
function wrapByCount( s, max )
	if type(s) ~= "string" then return s end
	max = max or 100
	local n1,n2 = 0,0
	for i=1, #s do
		local x,y = string.find(s, "\n", n1+1)
		if not x and i > n1 + max then
			x = i
		end
		if x then
			if x > n1 + max then x = n1 + max end
			n2 = x + 1
			if n1 > 0 then
				if n2 - n1 > max then
					local s1 = string.sub(s, 1, n2)
					local s2 = string.sub(s, n2+1, #s)
					s = s1 .. "\n" .. s2
				end
			end
			n1 = n2
			i = n2
			-- print(0, "wrapByCount", i, n1,n2)
		end
	end
	return s
end

-- 检查字是不是汉字
-- 一个汉字符len为3, 
-- 一个英文字符len为1
function string.checkWord(s)
	local ret = {};
	local f = '[%z\1-\127\194-\244][\128-\191]*';
	local line, lastLine, isBreak = '', false, false;
	local nCN, nEN = 0,0
	for v in string.gmatch(s, f) do
		if #v~=1 then nCN=nCN+1
		else nEN=nEN+1 end
		table.insert(ret, {c=v, isChinese=(#v~=1)});
	end
	return ret, nCN, nEN, nCN+nEN
end
-- function string.lenWord(s)
-- 	local f = '[%z\1-\127\194-\244][\128-\191]*';
-- 	local line, lastLine, isBreak = '', false, false;
-- 	local nCN, nEN = 0,0
-- 	for v in string.gmatch(s, f) do  -- string.gsub 和 string.gmatch 会产生大量的子串
-- 		if #v~=1 then nCN=nCN+1
-- 		else nEN=nEN+1 end
-- 	end
-- 	return nCN+nEN, nCN, nEN
-- end

-- 快25%
function string.lenWord(str)
	local byteLen = #str
	local strLen = 0
	local enLen = 0
	local i = 1
	local curByte
	local count = 1
	while i <= byteLen do
		curByte = string.byte(str,i)
		count = 1
		if 0 < curByte and curByte <= 127 then
			count = 1
			enLen = enLen + 1
		elseif 192 <= curByte and curByte < 223 then
			count = 2
		elseif 224 <= curByte and curByte < 239 then
			count = 3
		elseif 240 <= curByte and curByte < 247 then
			count = 4
		end
		i = i + count
		strLen = strLen + 1
	end
	return strLen, enLen
end
--[[

]]
function string.checkWordAlignment(s, max)
	local ret, nCN, nEN, nA = string.checkWord(s)
	if nCN*2 + nEN > max then
		return 0
	end
	return 1
end

--[[
	
]]
function string.hasChinese(s)
	local ret, nCN, nEN, nA = string.checkWord(s)
	return nCN ~= nEN
end


--获取路径
function string.getPath(u)  
	return string.match(u, "(.+)/[^/]*%.%w+$") or u --*nix system  
	--return string.match(u, “(.+)\\[^\\]*%.%w+$”) — windows  
end  
--获取文件名
function string.getFilename(u)  
	return string.match(u, ".+/([^/]*%.%w+)$") or u -- *nix system  
	--return string.match(u, “.+\\([^\\]*%.%w+)$”) — *nix system  
end  
--去除扩展名  
function string.stripExtensionName(u)  
	local idx = u:match(".+()%.%w+$")  
	return idx and u:sub(1, idx-1) or u
end 
--获取扩展名
function string.getExtensionName(u)  
	return u:match(".+%.(%w+)$")  
end  
-----------------------------------------------
math.LongMax = 9223372036854775807

local DOUBLE_ERR_CEIL  = 0.00000001
local DOUBLE_ERR_FLOOR = 0.99999999
math._floor = math.floor
math._ceil = math.ceil
function math.ceil( v )
	local i, d = math.modf(math.abs( v ))
	if d < DOUBLE_ERR_CEIL or d > DOUBLE_ERR_FLOOR then
		return math._ceil( v - DOUBLE_ERR_FLOOR )
	end
	return math._ceil( v )
end
function math.floor( v )
	local i, d = math.modf(math.abs( v ))
	if d < DOUBLE_ERR_CEIL or d > DOUBLE_ERR_FLOOR then
		return math._floor( v + DOUBLE_ERR_CEIL )
	end
	return math._floor( v )
end

-- lua5.3 浮点数时 小数部分即使没值也会显示为.0
function math.clearZero( n )
	local i = math.floor( n )
	if i == n then
		return i
	end
	return n
end

function math.pow( x, y)
	return x^y
end
-- 截取小数部分长度。四舍五入
function math.round(v, l)
	if l then
		local d = math.pow(10, l)
		local t = math.floor(v * d + 0.5)
		return (t % d ~= 0) and t / d or math.floor(t / d)
	end
	return math.floor(v + 0.5)
end

-- 限制取值范围
function math.clamp(v, min,max)
	if not min then min = 0 end
	if not max then max = 1 end
	if min > max then
		local tmp = min
		min = max
		max = tmp
	end
	v = math.max(v, min)
	v = math.min(v, max)
	return v
end

-- 另 cc.rectContainsPoint( rect, point )
-- rect : [x,y, width,height]
function math.rectContainsPoint(r, x,y)
	return x > r[1] and y > r[2] and x < r[1]+r[3] and y < r[2]+r[4]
end
-- rect : [x0,y0, x1,y1]
function math.pointInnerDots(r, x,y)
	return x > r[1] and y > r[2] and x < r[3] and y < r[4]
end


function math.distance(x1,y1,x2,y2)
	local dx = x2-x1
	local dy = y2-y1
	return math.sqrt(dx*dx+dy*dy)
end
function math.distanceByPoint(p1, p2)
	return math.distance(p1.x,p1.y, p2.x,p2.y)
end

function math.xrad(x1,y1,x2,y2)
	local dx = x2-x1
	local dy = y2-y1
	return math.atan2(dy,dx)
end
function math.xradByPoint(p1, p2)
	return math.xrad(p1.x,p1.y, p2.x,p2.y)
end

-----------------------------------------------

-- fix (n^2)
local _eqs=nil
function Utils.getNumMask(N)
	if not _eqs then
		_eqs = {}
		local n=1
		for i=1,16 do table.insert(_eqs,n); n=2*n; end
	end
	local ret = {}
	local M=N
	local v=0
	while (M>0) do
		for i=#_eqs,1,-1 do
			v=_eqs[i]
			if v <= M then
				table.insert( ret, v)
				M=M-v
				break
			end
		end
	end
	return ret
end

function __G__TRACKBACK__(msg)
    LogManager.LogWarning("==========   __G__TRACKBACK__   ===========")
    LogManager.LogWarning("LUA ERROR: " .. tostring(msg) .. "\n")
    LogManager.LogWarning(debug.traceback())
	LogManager.LogWarning("==========   __G__TRACKBACK__ END    ===========")
	
    return msg
end

function __LUA__ERROR__(msg)
    LogManager.LogWarning("==========   LUA ERROR START   ===========")
    LogManager.LogWarning("LUA ERROR: " .. tostring(msg) .. "\n")
    LogManager.LogWarning(debug.traceback())
	LogManager.LogWarning("==========   LUA ERROR  END    ===========")
	
    return msg
end

function Utils.resumeCoroutine(corou)
	if corou and coroutine.status (corou) == "suspended" then
		local ok, res = coroutine.resume(corou, true)
		if not ok then
	        __LUA__ERROR__( "[CUSTOM CORNUTINE] " .. res)
			LogManager.LogError("resumeCoroutine failed!")
		end
	end
end

function Utils.stopCoroutine(corou)
	if corou and coroutine.status (corou) == "suspended" then
		local ok, res = coroutine.resume(corou, false)
		if not ok then
	        __LUA__ERROR__( "[CUSTOM CORNUTINE] " .. res)
			LogManager.LogError("stopCoroutine failed!")
		end
	end
end

-----------------------------------------------
--Base64编码
function Utils.Base64Encode( str )
	return CS.Base64.Encode(str)
end

--Base64解码
function Utils.Base64Decode( str )
	return CS.Base64.Decode(str)
end

--Url Encode
function Utils.urlEncode(s)
	return CS.HTTPProxy.URLEncode(s)
end

--RSA Encode
function Utils.RSAEncode( str, publicKey )
	return CS.RSAUtil.Encrypt(str, publicKey)
end
-----------------------------------------------

-----------------------------------------------
-- 功能版本判断
function Utils.CheckVersion( compareVersion, ShowTips )
	-- 为传入compareVersion参数，说明没有版本限制
	if compareVersion == nil then
		return true
	end

	-- 版本限制判断
    local Version = string.split(compareVersion, ".")
	local ApplicationVer = string.split(UE.Application.version, "_")[1]
    local AppVersion = string.split(ApplicationVer, ".")
	for i, value in ipairs(AppVersion) do
		local ver = Version[i]
        if value ~= ver then
            local isValid = tonumber(value) > tonumber(ver)
            if not isValid and ShowTips then
                UITools.ShowTips("Version_NotMatch")
            end
            return isValid
        end
    end

    return true
end
-----------------------------------------------

-----------------------------------------------
-- 打印执行时间间隔
local socket = require "socket"
function Utils.PrintTime(  )
	if lastStamp == nil then
		curStamp = socket.gettime()
		LogManager.LogInfo("starmstamp is " .. curStamp)
		lastStamp = curStamp
	else
		curStamp = socket.gettime()
		LogManager.LogInfo("timeGap is " .. curStamp - lastStamp)
		lastStamp = curStamp
	end
end
-----------------------------------------------

-----------------------------------------------
-- 安全释放资源
function Utils.ReleaseAsset( asset )
	if asset then
        asset:UnLoad()
        asset = nil
    end
end

-- 安全销毁对象
function Utils.DestroyObj( Object )
	if Object then
		Object:Destroy()
		Object = nil
	end
end

-- 安全销毁定时器
function Utils.ReleaseTimer( timer )
	if timer then
        timer:Kill()
        timer = nil
    end
end
-----------------------------------------------

Time = {}
Time.timeZone = 0 -- 服务器的时区
Time.ServerUTCSecret = ""
Time.LastRealTimeSecret = ""
Time.RandomOffset = 0

function Time.SyncServerTime(timestamp, timeZone)
	local _lastServerUTCMilliSecTime = timestamp
	CS.XYServerTime.SyncServerTime(timestamp, timeZone)
	xlua.private_accessible(CS.XYServerTime)
	local _lastGameRealTime = CS.XYServerTime._lastGameRealTime
	Time.RandomOffset = math.random(-100, 100) 
	Time.ServerUTCSecret = Utils.getMD5String(_lastServerUTCMilliSecTime + Time.RandomOffset)
	Time.LastRealTimeSecret = Utils.getMD5String(_lastGameRealTime - Time.RandomOffset)
end

function Time.CheckServerTime()
	if Time.ServerUTCSecret == "" or Time.LastRealTimeSecret == "" then
		return
	end
	xlua.private_accessible(CS.XYServerTime)
	local _lastServerUTCMilliSecTime = CS.XYServerTime._lastServerUTCMilliSecTime
	local _lastGameRealTime = CS.XYServerTime._lastGameRealTime
	local curUTCSecret = Utils.getMD5String(_lastServerUTCMilliSecTime + Time.RandomOffset)
	local curRealTimeSecret = Utils.getMD5String(_lastGameRealTime - Time.RandomOffset)
	local nowTs = CS.XYServerTime.GetCurrentServerUTCTime()
	local isOverflow = (nowTs - (_lastServerUTCMilliSecTime / 1000)) > ONE_DAY
	if isOverflow or curUTCSecret ~= Time.ServerUTCSecret or curRealTimeSecret ~= Time.LastRealTimeSecret then
		LogManager.LogWarning("Time.CheckServerTime: Login time is too long, login again")
		FsmSystem.ChangeState(EFsmType.Logout)
	end
end

-- 返回本地时区
function Time.GetLocalTimeZone()
	local now = os.time()
	local locZone = os.difftime(now, os.time(os.date("!*t", now)))/3600
	return locZone
end

-- 返回服务器当前时区的时间
function Time.GetTime()
	return CS.XYServerTime.GetCurrentServerLocalTime()
	-- return os.time()
end
-- 返回服务器不带时区的时间(UTC0)
function Time.GetUTCTime()
	return CS.XYServerTime.GetCurrentServerUTCTime()
end

-- 返回格式化后的服务器当前时区的时间 
-- 等同于 CS.XYServerTime.TimeToString(math.floor(CS.XYServerTime.GetCurrentServerLocalTime()), "hh\\:mm\\:ss")
function Time.FormatServerTime( utc0_sec )
	local timestamp = math.floor(utc0_sec) -- 服务器传来的UTC0的时间(不带时区的时间)
	timestamp = timestamp - (Time.GetLocalTimeZone() - Time.timeZone) * 60 * 60
	-- os.date()中已带本地时区计算 所以先去掉前服务器时区差
	return os.date("%Y-%m-%d %H:%M:%S", timestamp)
			-- .." UTC"..(Time.timeZone>0 and "+" or "") ..Time.timeZone 
end

function Time.FormatServerTimeNew( utc0_sec )
	local timestamp = math.floor(utc0_sec) -- 服务器传来的UTC0的时间(不带时区的时间)
	timestamp = timestamp - (Time.GetLocalTimeZone() - Time.timeZone) * 60 * 60
	-- os.date()中已带本地时区计算 所以先去掉前服务器时区差
	return os.date("%Y-%m-%d %H:%M:%S", timestamp), os.date("*t",timestamp)
			-- .." UTC"..(Time.timeZone>0 and "+" or "") ..Time.timeZone 
end

function Time.FormatServerTime_Now()
	return Time.FormatServerTime( Time.GetUTCTime() )
end

-- 时间戳转年月日
function Time.FormatTime( timestamp )
	return os.date("%Y-%m-%d %H:%M:%S", timestamp)
end

-- return {year, month, day, hour, min, sec, wday(7六 1日), yday, isdst}
-- function Time.getDate()
-- 	return os.date("*t", Time.GetTime())
-- end
-- function Time.getWeekday()
-- 	return tonumber(os.date("%w", Time.GetTime())) --0~6 周日~周六
-- end

--绝对时间转秒 date(string): "2014-08-07 19:45:33"
--别超过2038年 lua5.1
-- function Time.dateToTime(date)
-- 	if not string.find(date, "-") then
-- 		date = string.format("%s %s", os.date("%Y-%m-%d", Time.getTime()), date)
-- 	end
-- 	local y, m, d, hh, mm, ss = string.match(date, "(%d+)-(%d+)-(%d+) (%d+):(%d+):(%d+)")
-- 	if not y then
-- 		y, m, d, hh, mm, ss = string.match(date, "(%d+)-(%d+)-(%d+)-(%d+):(%d+):(%d+)")
-- 	end
-- 	if not y then 
-- 		logWarnning("WARNNING DateToTime 参数非法")
-- 		return 0 
-- 	end
-- 	local t = os.time({year=y, month=m, day=d, hour=hh, min=mm, sec=ss})
-- 	-- logWarnning("convertedTimestamp", tostring(t), os.time())
-- 	return t
-- end

function Time.today( timestamp )
	local date = os.date("*t", math.floor(timestamp))
	return date.day
end


local _strD = nil
local _strH = nil
local _strM = nil
local _strS = nil
local function chkTimeStr()
	if not _strD then
		_strD = UITools.GetLang("UICommon_time5")
		_strH = UITools.GetLang("UICommon_time6")
		_strM = UITools.GetLang("UICommon_time7")
		_strS = UITools.GetLang("UICommon_time8")
	end
end
local function _getD( v ) return string.format( _strD, v ) end
local function _getH( v ) return string.format( _strH, v ) end
local function _getM( v ) return string.format( _strM, v ) end
local function _getS( v ) return string.format( _strS, v ) end

local function doubleDigi(sec)
	if sec < 10 then
		return string.format("%02d", sec)
	end
	return sec
end
Time.doubleDigi = doubleDigi
local function getSecGap(s,g)
	local v = 0
	if (s >= g) then
		v = math.floor(s / g)
		s = s - (v * g)
	end
	return s,v
end
--格式化时间显示
-- @formatMode 
--      0 简略时间(最多两位)
--      1 完整时间(最多天时分秒)
-- @unitMode
--      0 无单位 仅“天” 其它使用冒号“:”
---     1 完整单位 完全都有  “天时分秒”
-- @maxIsHour
---     1 “时分秒”
function Time.gapValue(sec)
	local nD=0
	local hh=0
	local mm=0
	local ss=math.floor(sec)
	if (ss < 0) then ss = 0 end
	ss,nD = getSecGap(ss, 86400)
	ss,hh = getSecGap(ss, 3600)
	ss,mm = getSecGap(ss, 60)
	return nD, hh, mm, ss
end

-- 剩余时间格式化为任何时候都显示“时分秒”
-- fullUnit=false  00:00:00
-- fullUnit=True  00时00分00秒
function Time.Format_HMS(sec, fullUnit)
	chkTimeStr()
	local nD, hh, mm, ss = Time.gapValue(sec)
	hh = hh+nD*24
	if fullUnit then
		return string.format("%02d%s%02d%s%02d%s",hh,_strH,mm,_strM,ss,_strS)
	end
	return string.format("%02d:%02d:%02d",hh,mm,ss)
end
-- 剩余时间格式化为“分秒”
function Time.Format_MS(sec)
	chkTimeStr()
	local nD, hh, mm, ss = Time.gapValue(sec)
	mm = nD*24*60*60 + hh*60*60 + mm
	return string.format("%02d:%02d",mm,ss)
end

-- 最多只显示两位 如: 天时  时分 分秒
function Time.Format_Max2(sec, fullUnit)
	chkTimeStr()
	local nD, hh, mm, ss = Time.gapValue(sec)
	if nD > 0 then
		if fullUnit then
			return _getD(nD).._getH(hh)
		end
		return string.format("%dday%dh", nD, hh)
	end
	return Time.Format_MaxHM(sec, fullUnit)
end
-- 最多只显示一位 如: 天  时  分  秒
function Time.Format_Max1(sec)
	chkTimeStr()
	local nD, hh, mm, ss = Time.gapValue(sec)
	if nD > 0 then return _getD(nD) end
	if hh > 0 then return _getH(hh) end
	if mm > 0 then return _getM(mm) end
	return _getS(ss)
end
function Time.Format_MaxHM(sec, fullUnit)
	chkTimeStr()
	local nD, hh, mm, ss = Time.gapValue(sec)
	hh = hh + nD * 24
	nD = 0
	if hh > 0 then
		if fullUnit then
			return _getH(hh) .. _getM(mm)
		end
		return string.format("%02d:%02d", hh, mm)
	end
	if fullUnit then
		return _getM(mm).._getS(ss)
	end
	return string.format("%02d:%02d", mm, ss)
end

function Time.Format(sec)
	local nD, hh, mm, ss = Time.gapValue(sec)
	if nD >= 30 then
		return UITools.GetLang("UIGuildMember_time5")
	elseif nD >= 1 then
		return UITools.GetLang("UIGuildMember_time4", nD)
	elseif hh >= 1 then
		return UITools.GetLang("UIGuildMember_time3", hh)
	else
		return UITools.GetLang("UIGuildMember_time2", mm > 0 and mm or 1)
	end
end

function Time.FormatPassTime(sec)
	local nD, hh, mm, ss = Time.gapValue(sec)
	if nD >= 30 then
		return UITools.GetLang("UIGuildMember_time5") -- 30天以上
	elseif nD >= 1 then
		return UITools.GetLang("UIGuildMember_time4", nD) -- n天之前
	elseif hh >= 1 then
		return UITools.GetLang("UIGuildMember_time3", hh) -- n小时之前
	elseif mm >= 1 then
		return UITools.GetLang("UIGuildMember_time2", mm) -- n分钟之前
	end
	return UITools.GetLang( "UIGuildMember_time1") -- 在线
end
-----------------------------------------------

MathGeom = {}
local Deg2Rad = math.pi / 180
local Rad2Deg = 180 / math.pi
function MathGeom.GetRad(pS, pT)
	-- local atan2 = math.atan2
	local atan2 = CS.UnityEngine.Mathf.Atan2
	local r = atan2( pT.y - pS.y, pT.x - pS.x )
	return r
end
-- 返回 +/-180
function MathGeom.GetAngle(S, T)
	return MathGeom.GetRad( S, T ) * Rad2Deg
end

function MathGeom.GetMoveVector2( p0, speed, a )
	local x = speed * math.cos( a * Deg2Rad )
	local y = speed * math.sin( a * Deg2Rad )
	return {x = p0.x + x, y = p0.y + y }
end

function MathGeom.MoveToward( p0, p1, distance )
	local a = MathGeom.GetAngle(p0, p1)
	return MathGeom.GetMoveVector2( p0, distance, a )
end

-- Vector3.Distance(S, T);
function MathGeom.Distance( S, T)
	local d = math.sqrt( (T.x - S.x) * (T.x - S.x)
						+ (T.y - S.y) * (T.y - S.y)
						+ (T.z - S.z) * (T.z - S.z) )
	return d
end

function MathGeom.GetVector2( vec )
	return CS.UnityEngine.Vector2( vec.x, vec.y )
end

-- 获取最大公约数(greatest common divisor)
-- 辗转相除法
-- list: 求取结果的列表
function MathGeom.GetGCD( list )
	local pool = table.cloneByIndex(list)
	local cnt = #pool
	while pool[1] ~= pool[cnt] do
		table.sortNum(pool, 1)
		for i = 1, cnt - 1 do
			local remainder = pool[i] % pool[i + 1]
			pool[i] = (remainder == 0) and pool[i + 1] or remainder
		end
	end

	return pool[1]
end

-----------------------------------------------
---Profile采样---
local profileSampleVer = "1.0.36"
function Utils.ProfilerBeginSample( name )
	if not Debug.isDevBuild then return end
	if Utils.CheckVersion(profileSampleVer) then
		ME.Utility.ProfilerSample.BeginSample(name)
	end
end

function Utils.ProfilerEndSample(  )
	if not Debug.isDevBuild then return end
	if Utils.CheckVersion(profileSampleVer) then
		ME.Utility.ProfilerSample.EndSample()
	end
end

-----------------------------------------------

CsToLuaData = {}
function CsToLuaData.ListToTab( csList )
	local ret = {}
	local n = csList.Count
	if n and n > 0 then
		n = math.max( n-1, 0 )
		for i = 0, n do
			table.insert( ret, csList[ i ] )
		end
	end
	return ret
end

function CsToLuaData.ArrayToTab( csArray )
	local ret = {}
	local n = csArray.Length
	if n and n > 0 then
		n = math.max( n-1, 0 )
		for i = 0, n do
			table.insert( ret, csArray[ i ] )
		end
	end
	return ret
end

function math.lerp(from, to, p)
	p = math.clamp(p, 0, 1)
	return from*(1-p) + to*p
end

--判断GameObject为null(destroy)
local Object_Extention_IsDestroyed = UE.UnityEngine_Object_Extention.IsDestroyed
function IsNil(uobj)
	--return uobj == nil or uobj:Equals(nil)
	return uobj == nil or (type(uobj) == 'userdata' and (type(uobj.IsDestroyed) == 'function' and {uobj:IsDestroyed()} or (Object_Extention_IsDestroyed and {Object_Extention_IsDestroyed(uobj)} or {uobj:Equals(nil)})[1])[1])
end

function GetEnumKey(tbl, val)
	if not getmetatable(tbl) then
		local metatbl = {}
		for k, v in pairs(tbl) do
			metatbl[v] = k
		end
		setmetatable(tbl, {__index=metatbl})
	end
	local key = tbl[val]
	if not key then
		for k, v in pairs(tbl) do
			if v == val then
				key = k
				break
			end
		end
	end
	return key
end

function HasCSType(fullName)
	--assume type from Assembly-CSharp.dll
	local ass = SYS.Reflection.Assembly.Load("Assembly-CSharp")
	return ass:GetType(fullName) ~= nil
end

-- ABNF from RFC 3629
--
-- UTF8-octets = *( UTF8-char )
-- UTF8-char = UTF8-1 / UTF8-2 / UTF8-3 / UTF8-4
-- UTF8-1 = %x00-7F
-- UTF8-2 = %xC2-DF UTF8-tail
-- UTF8-3 = %xE0 %xA0-BF UTF8-tail / %xE1-EC 2( UTF8-tail ) /
-- %xED %x80-9F UTF8-tail / %xEE-EF 2( UTF8-tail )
-- UTF8-4 = %xF0 %x90-BF 2( UTF8-tail ) / %xF1-F3 3( UTF8-tail ) /
-- %xF4 %x80-8F 2( UTF8-tail )
-- UTF8-tail = %x80-BF

-- 0xxxxxxx                            | 007F   (127)
-- 110xxxxx	10xxxxxx                   | 07FF   (2047)
-- 1110xxxx	10xxxxxx 10xxxxxx          | FFFF   (65535)
-- 11110xxx	10xxxxxx 10xxxxxx 10xxxxxx | 10FFFF (1114111)

local pattern = '[%z\1-\127\194-\244][\128-\191]*'

-- helper function
local posrelat =
	function (pos, len)
		if pos < 0 then
			pos = len + pos + 1
		end

		return pos
	end

utf8 = utf8 or {}

-- THE MEAT

-- maps f over s's utf8 characters f can accept args: (visual_index, utf8_character, byte_index)
utf8.map =
	function (s, f, no_subs)
		local i = 0

		if no_subs then
			for b, e in s:gmatch('()' .. pattern .. '()') do
				i = i + 1
				local c = e - b
				f(i, c, b)
			end
		else
			for b, c in s:gmatch('()(' .. pattern .. ')') do
				i = i + 1
				f(i, c, b)
			end
		end
	end

-- THE REST

-- generator for the above -- to iterate over all utf8 chars
utf8.chars =
	function (s, no_subs)
		return coroutine.wrap(function () return utf8.map(s, coroutine.yield, no_subs) end)
	end

-- returns the number of characters in a UTF-8 string
utf8.len =
	function (s)
		-- count the number of non-continuing bytes
		return select(2, s:gsub('[^\128-\193]', ''))
	end

-- replace all utf8 chars with mapping
utf8.replace =
	function (s, map)
		return s:gsub(pattern, map)
	end

-- reverse a utf8 string
utf8.reverse =
	function (s)
		-- reverse the individual greater-than-single-byte characters
		s = s:gsub(pattern, function (c) return #c > 1 and c:reverse() end)

		return s:reverse()
	end

-- strip non-ascii characters from a utf8 string
utf8.strip =
	function (s)
		return s:gsub(pattern, function (c) return #c > 1 and '' end)
	end

-- like string.sub() but i, j are utf8 strings
-- a utf8-safe string.sub()
utf8.sub =
	function (s, i, j)
		local l = utf8.len(s)

		i =       posrelat(i, l)
		j = j and posrelat(j, l) or l

		if i < 1 then i = 1 end
		if j > l then j = l end

		if i > j then return '' end

		local diff = j - i
		local iter = utf8.chars(s, true)

		-- advance up to i
		for _ = 1, i - 1 do iter() end

		local c, b = select(2, iter())

		-- i and j are the same, single-charaacter sub
		if diff == 0 then
			return string.sub(s, b, b + c - 1)
		end

		i = b

		-- advance up to j
		for _ = 1, diff - 1 do iter() end

		c, b = select(2, iter())

		return string.sub(s, i, b + c - 1)
	end