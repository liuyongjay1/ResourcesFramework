StringTool = {}

--[[
    分割字符串
]]
function StringTool.split(str, sep)
    local result = {}
    if str == nil or sep == nil or type(str) ~= "string" or type(sep) ~= "string" then
        return result
    end
 
    if string.len(sep) == 0 then
        return result
    end
    local pattern = string.format("([^%s]+)", sep)
    string.gsub(
        str,
        pattern,
        function(c)
            result[#result + 1] = c
        end
    )
    return result
end

--[[
    检查字符串是否为空
    配表数据没配的string也是nil
]]
function StringTool.IsEmpty(str)
    if str == nil then
        return true
    end
    local noSpaceStr = string.gsub(str, " ", "")
    if noSpaceStr == "" then
        return true
    end

    return false
end

function StringTool.StartsWith(str, substr)  
    if str == nil or substr == nil then  
        return false
    end  
    if string.find(str, substr) ~= 1 then  
        return false  
    else  
        return true  
    end  
end
