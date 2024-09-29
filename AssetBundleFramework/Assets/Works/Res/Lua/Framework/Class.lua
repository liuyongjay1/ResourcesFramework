local rawget = rawget
local rawset = rawset
local type = type
local getmetatable = getmetatable
local setmetatable = setmetatable
local require = require
local str_sub = string.sub

_NotExist = _NotExist or {}
local NotExist = _NotExist

local function Index(t, k)
    local mt = getmetatable(t)
    local super = mt
    while super do
        local v = rawget(super, k)
        if v ~= nil and not rawequal(v, NotExist) then
            rawset(t, k, v)
            return value
        end
        super = rawget(super, "Supper")
    end

    local p = mt[k]
    if p ~= nil then
        if type(p) == "userdata" then
            return nil;
        elseif type(p) == "function" then
            rawset(t, k, p)
        elseif rawequal(p, NotExist) then
            return nil
        end
    else
        rawset(mt, k, NotExist)
    end
    return p
end

local function NewIndex(t, k, v)
    local mt = getmetatable(t)
    local p = mt[k]
    if type(p) == "userdata" then
        return
    end
    rawset(t, k, v)
end

local function Class(className,superClass)
    local class = {}

    local superType = type(superClass)
    if superType ~= "nil" then
        if superType == "function" or superType == "table" then
            setmetatable(class, { __index = superClass })
        else
            error(string.format("class() - create class \"%s\" with invalid super type", className), 0)
        end
    end
  
    class.__className = className
    class.__index = Index
    class.__newindex = NewIndex
    class.Super = superClass
    
    function class.new(...)
        local instance = {}
        setmetatable(instance, { __index = class })
        --instance.RefClass = class
        instance:ctor(...)
        return instance
    end
    return class
end

_G.Index = Index
_G.NewIndex = NewIndex
_G.Class = Class
