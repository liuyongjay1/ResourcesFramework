--WARNING: The script is created by TableCreater ,dont't edit it!
local protoc = require "Framework/protoc".new()
local pb = require "pb"

--[[
    程序读表用这个做TableName
]]
TableNameConfig = {
    	Table_Chapter1 = "Chapter1",
		Table_Chapter10 = "Chapter10",
		Table_Chapter10Dialog = "Chapter10Dialog",
		Table_Chapter11 = "Chapter11",
		Table_Chapter11Dialog = "Chapter11Dialog",
		Table_Chapter12 = "Chapter12",
		Table_Chapter12Dialog = "Chapter12Dialog",
		Table_Chapter1Dialog = "Chapter1Dialog",
		Table_Chapter2 = "Chapter2",
		Table_Chapter2Dialog = "Chapter2Dialog",
		Table_Chapter2Option = "Chapter2Option",
		Table_Chapter2OptionText = "Chapter2OptionText",
		Table_Chapter3 = "Chapter3",
		Table_Chapter3Dialog = "Chapter3Dialog",
		Table_Chapter3Option = "Chapter3Option",
		Table_Chapter3OptionText = "Chapter3OptionText",
		Table_Chapter4 = "Chapter4",
		Table_Chapter4Dialog = "Chapter4Dialog",
		Table_Chapter4Option = "Chapter4Option",
		Table_Chapter4OptionText = "Chapter4OptionText",
		Table_Chapter5 = "Chapter5",
		Table_Chapter5Dialog = "Chapter5Dialog",
		Table_Chapter5Option = "Chapter5Option",
		Table_Chapter5OptionText = "Chapter5OptionText",
		Table_Chapter6 = "Chapter6",
		Table_Chapter6Dialog = "Chapter6Dialog",
		Table_Chapter6Option = "Chapter6Option",
		Table_Chapter6OptionText = "Chapter6OptionText",
		Table_Chapter7 = "Chapter7",
		Table_Chapter7Dialog = "Chapter7Dialog",
		Table_Chapter7Option = "Chapter7Option",
		Table_Chapter7OptionText = "Chapter7OptionText",
		Table_Chapter8 = "Chapter8",
		Table_Chapter8Dialog = "Chapter8Dialog",
		Table_Chapter8Option = "Chapter8Option",
		Table_Chapter8OptionText = "Chapter8OptionText",
		Table_Chapter9 = "Chapter9",
		Table_Chapter9Dialog = "Chapter9Dialog",
		Table_LivelyRole = "LivelyRole",
		Table_MazeMap = "MazeMap",
		Table_MazeMapItem = "MazeMapItem",
		Table_ResData = "ResData",
		Table_RoleCall = "RoleCall",
		Table_RoleName = "RoleName",
		Table_SceneData = "SceneData",
		Table_ScreenEffect = "ScreenEffect",
		Table_UIConfig = "UIConfig",
		Table_WordData = "WordData",
	
}


_G.Table_Chapter1 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter1",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter1")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter1.proto"
        TableManager.AddLoadTableTask("Chapter1")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter1.LoadProtoCallback,Table_Chapter1,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter1.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter1.proto"))
        Table_Chapter1.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter1.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter1.LoadTableBytesCallBack,Table_Chapter1,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter1.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter1', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter1",AllRow.datas,Table_Chapter1)
    end,

}

_G.Table_Chapter10 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter10",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter10")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter10.proto"
        TableManager.AddLoadTableTask("Chapter10")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter10.LoadProtoCallback,Table_Chapter10,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter10.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter10.proto"))
        Table_Chapter10.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter10.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter10.LoadTableBytesCallBack,Table_Chapter10,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter10.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter10', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter10",AllRow.datas,Table_Chapter10)
    end,

}

_G.Table_Chapter10Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter10Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter10Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter10Dialog.proto"
        TableManager.AddLoadTableTask("Chapter10Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter10Dialog.LoadProtoCallback,Table_Chapter10Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter10Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter10Dialog.proto"))
        Table_Chapter10Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter10Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter10Dialog.LoadTableBytesCallBack,Table_Chapter10Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter10Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter10Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter10Dialog",AllRow.datas,Table_Chapter10Dialog)
    end,

}

_G.Table_Chapter11 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter11",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter11")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter11.proto"
        TableManager.AddLoadTableTask("Chapter11")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter11.LoadProtoCallback,Table_Chapter11,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter11.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter11.proto"))
        Table_Chapter11.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter11.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter11.LoadTableBytesCallBack,Table_Chapter11,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter11.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter11', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter11",AllRow.datas,Table_Chapter11)
    end,

}

_G.Table_Chapter11Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter11Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter11Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter11Dialog.proto"
        TableManager.AddLoadTableTask("Chapter11Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter11Dialog.LoadProtoCallback,Table_Chapter11Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter11Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter11Dialog.proto"))
        Table_Chapter11Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter11Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter11Dialog.LoadTableBytesCallBack,Table_Chapter11Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter11Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter11Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter11Dialog",AllRow.datas,Table_Chapter11Dialog)
    end,

}

_G.Table_Chapter12 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter12",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter12")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter12.proto"
        TableManager.AddLoadTableTask("Chapter12")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter12.LoadProtoCallback,Table_Chapter12,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter12.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter12.proto"))
        Table_Chapter12.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter12.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter12.LoadTableBytesCallBack,Table_Chapter12,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter12.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter12', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter12",AllRow.datas,Table_Chapter12)
    end,

}

_G.Table_Chapter12Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter12Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter12Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter12Dialog.proto"
        TableManager.AddLoadTableTask("Chapter12Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter12Dialog.LoadProtoCallback,Table_Chapter12Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter12Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter12Dialog.proto"))
        Table_Chapter12Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter12Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter12Dialog.LoadTableBytesCallBack,Table_Chapter12Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter12Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter12Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter12Dialog",AllRow.datas,Table_Chapter12Dialog)
    end,

}

_G.Table_Chapter1Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter1Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter1Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter1Dialog.proto"
        TableManager.AddLoadTableTask("Chapter1Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter1Dialog.LoadProtoCallback,Table_Chapter1Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter1Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter1Dialog.proto"))
        Table_Chapter1Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter1Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter1Dialog.LoadTableBytesCallBack,Table_Chapter1Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter1Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter1Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter1Dialog",AllRow.datas,Table_Chapter1Dialog)
    end,

}

_G.Table_Chapter2 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter2",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter2")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter2.proto"
        TableManager.AddLoadTableTask("Chapter2")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2.LoadProtoCallback,Table_Chapter2,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter2.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter2.proto"))
        Table_Chapter2.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter2.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2.LoadTableBytesCallBack,Table_Chapter2,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter2.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter2', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter2",AllRow.datas,Table_Chapter2)
    end,

}

_G.Table_Chapter2Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter2Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter2Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter2Dialog.proto"
        TableManager.AddLoadTableTask("Chapter2Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2Dialog.LoadProtoCallback,Table_Chapter2Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter2Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter2Dialog.proto"))
        Table_Chapter2Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter2Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2Dialog.LoadTableBytesCallBack,Table_Chapter2Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter2Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter2Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter2Dialog",AllRow.datas,Table_Chapter2Dialog)
    end,

}

_G.Table_Chapter2Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter2Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter2Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter2Option.proto"
        TableManager.AddLoadTableTask("Chapter2Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2Option.LoadProtoCallback,Table_Chapter2Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter2Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter2Option.proto"))
        Table_Chapter2Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter2Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2Option.LoadTableBytesCallBack,Table_Chapter2Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter2Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter2Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter2Option",AllRow.datas,Table_Chapter2Option)
    end,

}

_G.Table_Chapter2OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter2OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter2OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter2OptionText.proto"
        TableManager.AddLoadTableTask("Chapter2OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2OptionText.LoadProtoCallback,Table_Chapter2OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter2OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter2OptionText.proto"))
        Table_Chapter2OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter2OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter2OptionText.LoadTableBytesCallBack,Table_Chapter2OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter2OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter2OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter2OptionText",AllRow.datas,Table_Chapter2OptionText)
    end,

}

_G.Table_Chapter3 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter3",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter3")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter3.proto"
        TableManager.AddLoadTableTask("Chapter3")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3.LoadProtoCallback,Table_Chapter3,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter3.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter3.proto"))
        Table_Chapter3.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter3.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3.LoadTableBytesCallBack,Table_Chapter3,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter3.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter3', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter3",AllRow.datas,Table_Chapter3)
    end,

}

_G.Table_Chapter3Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter3Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter3Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter3Dialog.proto"
        TableManager.AddLoadTableTask("Chapter3Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3Dialog.LoadProtoCallback,Table_Chapter3Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter3Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter3Dialog.proto"))
        Table_Chapter3Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter3Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3Dialog.LoadTableBytesCallBack,Table_Chapter3Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter3Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter3Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter3Dialog",AllRow.datas,Table_Chapter3Dialog)
    end,

}

_G.Table_Chapter3Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter3Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter3Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter3Option.proto"
        TableManager.AddLoadTableTask("Chapter3Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3Option.LoadProtoCallback,Table_Chapter3Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter3Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter3Option.proto"))
        Table_Chapter3Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter3Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3Option.LoadTableBytesCallBack,Table_Chapter3Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter3Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter3Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter3Option",AllRow.datas,Table_Chapter3Option)
    end,

}

_G.Table_Chapter3OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter3OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter3OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter3OptionText.proto"
        TableManager.AddLoadTableTask("Chapter3OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3OptionText.LoadProtoCallback,Table_Chapter3OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter3OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter3OptionText.proto"))
        Table_Chapter3OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter3OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter3OptionText.LoadTableBytesCallBack,Table_Chapter3OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter3OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter3OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter3OptionText",AllRow.datas,Table_Chapter3OptionText)
    end,

}

_G.Table_Chapter4 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter4",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter4")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter4.proto"
        TableManager.AddLoadTableTask("Chapter4")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4.LoadProtoCallback,Table_Chapter4,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter4.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter4.proto"))
        Table_Chapter4.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter4.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4.LoadTableBytesCallBack,Table_Chapter4,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter4.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter4', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter4",AllRow.datas,Table_Chapter4)
    end,

}

_G.Table_Chapter4Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter4Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter4Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter4Dialog.proto"
        TableManager.AddLoadTableTask("Chapter4Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4Dialog.LoadProtoCallback,Table_Chapter4Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter4Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter4Dialog.proto"))
        Table_Chapter4Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter4Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4Dialog.LoadTableBytesCallBack,Table_Chapter4Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter4Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter4Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter4Dialog",AllRow.datas,Table_Chapter4Dialog)
    end,

}

_G.Table_Chapter4Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter4Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter4Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter4Option.proto"
        TableManager.AddLoadTableTask("Chapter4Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4Option.LoadProtoCallback,Table_Chapter4Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter4Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter4Option.proto"))
        Table_Chapter4Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter4Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4Option.LoadTableBytesCallBack,Table_Chapter4Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter4Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter4Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter4Option",AllRow.datas,Table_Chapter4Option)
    end,

}

_G.Table_Chapter4OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter4OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter4OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter4OptionText.proto"
        TableManager.AddLoadTableTask("Chapter4OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4OptionText.LoadProtoCallback,Table_Chapter4OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter4OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter4OptionText.proto"))
        Table_Chapter4OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter4OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter4OptionText.LoadTableBytesCallBack,Table_Chapter4OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter4OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter4OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter4OptionText",AllRow.datas,Table_Chapter4OptionText)
    end,

}

_G.Table_Chapter5 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter5",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter5")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter5.proto"
        TableManager.AddLoadTableTask("Chapter5")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5.LoadProtoCallback,Table_Chapter5,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter5.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter5.proto"))
        Table_Chapter5.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter5.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5.LoadTableBytesCallBack,Table_Chapter5,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter5.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter5', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter5",AllRow.datas,Table_Chapter5)
    end,

}

_G.Table_Chapter5Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter5Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter5Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter5Dialog.proto"
        TableManager.AddLoadTableTask("Chapter5Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5Dialog.LoadProtoCallback,Table_Chapter5Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter5Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter5Dialog.proto"))
        Table_Chapter5Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter5Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5Dialog.LoadTableBytesCallBack,Table_Chapter5Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter5Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter5Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter5Dialog",AllRow.datas,Table_Chapter5Dialog)
    end,

}

_G.Table_Chapter5Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter5Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter5Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter5Option.proto"
        TableManager.AddLoadTableTask("Chapter5Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5Option.LoadProtoCallback,Table_Chapter5Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter5Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter5Option.proto"))
        Table_Chapter5Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter5Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5Option.LoadTableBytesCallBack,Table_Chapter5Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter5Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter5Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter5Option",AllRow.datas,Table_Chapter5Option)
    end,

}

_G.Table_Chapter5OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter5OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter5OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter5OptionText.proto"
        TableManager.AddLoadTableTask("Chapter5OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5OptionText.LoadProtoCallback,Table_Chapter5OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter5OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter5OptionText.proto"))
        Table_Chapter5OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter5OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter5OptionText.LoadTableBytesCallBack,Table_Chapter5OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter5OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter5OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter5OptionText",AllRow.datas,Table_Chapter5OptionText)
    end,

}

_G.Table_Chapter6 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter6",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter6")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter6.proto"
        TableManager.AddLoadTableTask("Chapter6")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6.LoadProtoCallback,Table_Chapter6,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter6.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter6.proto"))
        Table_Chapter6.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter6.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6.LoadTableBytesCallBack,Table_Chapter6,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter6.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter6', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter6",AllRow.datas,Table_Chapter6)
    end,

}

_G.Table_Chapter6Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter6Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter6Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter6Dialog.proto"
        TableManager.AddLoadTableTask("Chapter6Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6Dialog.LoadProtoCallback,Table_Chapter6Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter6Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter6Dialog.proto"))
        Table_Chapter6Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter6Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6Dialog.LoadTableBytesCallBack,Table_Chapter6Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter6Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter6Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter6Dialog",AllRow.datas,Table_Chapter6Dialog)
    end,

}

_G.Table_Chapter6Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter6Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter6Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter6Option.proto"
        TableManager.AddLoadTableTask("Chapter6Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6Option.LoadProtoCallback,Table_Chapter6Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter6Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter6Option.proto"))
        Table_Chapter6Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter6Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6Option.LoadTableBytesCallBack,Table_Chapter6Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter6Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter6Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter6Option",AllRow.datas,Table_Chapter6Option)
    end,

}

_G.Table_Chapter6OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter6OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter6OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter6OptionText.proto"
        TableManager.AddLoadTableTask("Chapter6OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6OptionText.LoadProtoCallback,Table_Chapter6OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter6OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter6OptionText.proto"))
        Table_Chapter6OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter6OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter6OptionText.LoadTableBytesCallBack,Table_Chapter6OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter6OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter6OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter6OptionText",AllRow.datas,Table_Chapter6OptionText)
    end,

}

_G.Table_Chapter7 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter7",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter7")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter7.proto"
        TableManager.AddLoadTableTask("Chapter7")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7.LoadProtoCallback,Table_Chapter7,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter7.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter7.proto"))
        Table_Chapter7.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter7.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7.LoadTableBytesCallBack,Table_Chapter7,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter7.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter7', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter7",AllRow.datas,Table_Chapter7)
    end,

}

_G.Table_Chapter7Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter7Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter7Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter7Dialog.proto"
        TableManager.AddLoadTableTask("Chapter7Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7Dialog.LoadProtoCallback,Table_Chapter7Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter7Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter7Dialog.proto"))
        Table_Chapter7Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter7Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7Dialog.LoadTableBytesCallBack,Table_Chapter7Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter7Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter7Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter7Dialog",AllRow.datas,Table_Chapter7Dialog)
    end,

}

_G.Table_Chapter7Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter7Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter7Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter7Option.proto"
        TableManager.AddLoadTableTask("Chapter7Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7Option.LoadProtoCallback,Table_Chapter7Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter7Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter7Option.proto"))
        Table_Chapter7Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter7Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7Option.LoadTableBytesCallBack,Table_Chapter7Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter7Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter7Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter7Option",AllRow.datas,Table_Chapter7Option)
    end,

}

_G.Table_Chapter7OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter7OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter7OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter7OptionText.proto"
        TableManager.AddLoadTableTask("Chapter7OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7OptionText.LoadProtoCallback,Table_Chapter7OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter7OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter7OptionText.proto"))
        Table_Chapter7OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter7OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter7OptionText.LoadTableBytesCallBack,Table_Chapter7OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter7OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter7OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter7OptionText",AllRow.datas,Table_Chapter7OptionText)
    end,

}

_G.Table_Chapter8 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter8",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter8")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter8.proto"
        TableManager.AddLoadTableTask("Chapter8")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8.LoadProtoCallback,Table_Chapter8,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter8.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter8.proto"))
        Table_Chapter8.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter8.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8.LoadTableBytesCallBack,Table_Chapter8,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter8.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter8', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter8",AllRow.datas,Table_Chapter8)
    end,

}

_G.Table_Chapter8Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter8Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter8Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter8Dialog.proto"
        TableManager.AddLoadTableTask("Chapter8Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8Dialog.LoadProtoCallback,Table_Chapter8Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter8Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter8Dialog.proto"))
        Table_Chapter8Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter8Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8Dialog.LoadTableBytesCallBack,Table_Chapter8Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter8Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter8Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter8Dialog",AllRow.datas,Table_Chapter8Dialog)
    end,

}

_G.Table_Chapter8Option = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter8Option",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter8Option")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter8Option.proto"
        TableManager.AddLoadTableTask("Chapter8Option")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8Option.LoadProtoCallback,Table_Chapter8Option,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter8Option.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter8Option.proto"))
        Table_Chapter8Option.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter8Option.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8Option.LoadTableBytesCallBack,Table_Chapter8Option,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter8Option.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter8Option', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter8Option",AllRow.datas,Table_Chapter8Option)
    end,

}

_G.Table_Chapter8OptionText = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter8OptionText",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter8OptionText")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter8OptionText.proto"
        TableManager.AddLoadTableTask("Chapter8OptionText")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8OptionText.LoadProtoCallback,Table_Chapter8OptionText,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter8OptionText.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter8OptionText.proto"))
        Table_Chapter8OptionText.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter8OptionText.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter8OptionText.LoadTableBytesCallBack,Table_Chapter8OptionText,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter8OptionText.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter8OptionText', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter8OptionText",AllRow.datas,Table_Chapter8OptionText)
    end,

}

_G.Table_Chapter9 = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter9",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter9")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter9.proto"
        TableManager.AddLoadTableTask("Chapter9")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter9.LoadProtoCallback,Table_Chapter9,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter9.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter9.proto"))
        Table_Chapter9.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter9.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter9.LoadTableBytesCallBack,Table_Chapter9,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter9.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter9', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter9",AllRow.datas,Table_Chapter9)
    end,

}

_G.Table_Chapter9Dialog = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("Chapter9Dialog",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("Chapter9Dialog")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_Chapter9Dialog.proto"
        TableManager.AddLoadTableTask("Chapter9Dialog")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter9Dialog.LoadProtoCallback,Table_Chapter9Dialog,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: Chapter9Dialog.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_Chapter9Dialog.proto"))
        Table_Chapter9Dialog.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/Chapter9Dialog.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_Chapter9Dialog.LoadTableBytesCallBack,Table_Chapter9Dialog,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: Chapter9Dialog.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_Chapter9Dialog', loader:GetBytes()))
        TableManager.LoadTableSuccess("Chapter9Dialog",AllRow.datas,Table_Chapter9Dialog)
    end,

}

_G.Table_LivelyRole = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("LivelyRole",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("LivelyRole")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_LivelyRole.proto"
        TableManager.AddLoadTableTask("LivelyRole")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_LivelyRole.LoadProtoCallback,Table_LivelyRole,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: LivelyRole.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_LivelyRole.proto"))
        Table_LivelyRole.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/LivelyRole.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_LivelyRole.LoadTableBytesCallBack,Table_LivelyRole,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: LivelyRole.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_LivelyRole', loader:GetBytes()))
        TableManager.LoadTableSuccess("LivelyRole",AllRow.datas,Table_LivelyRole)
    end,

}

_G.Table_MazeMap = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("MazeMap",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("MazeMap")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_MazeMap.proto"
        TableManager.AddLoadTableTask("MazeMap")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_MazeMap.LoadProtoCallback,Table_MazeMap,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: MazeMap.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_MazeMap.proto"))
        Table_MazeMap.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/MazeMap.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_MazeMap.LoadTableBytesCallBack,Table_MazeMap,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: MazeMap.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_MazeMap', loader:GetBytes()))
        TableManager.LoadTableSuccess("MazeMap",AllRow.datas,Table_MazeMap)
    end,

}

_G.Table_MazeMapItem = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("MazeMapItem",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("MazeMapItem")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_MazeMapItem.proto"
        TableManager.AddLoadTableTask("MazeMapItem")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_MazeMapItem.LoadProtoCallback,Table_MazeMapItem,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: MazeMapItem.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_MazeMapItem.proto"))
        Table_MazeMapItem.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/MazeMapItem.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_MazeMapItem.LoadTableBytesCallBack,Table_MazeMapItem,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: MazeMapItem.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_MazeMapItem', loader:GetBytes()))
        TableManager.LoadTableSuccess("MazeMapItem",AllRow.datas,Table_MazeMapItem)
    end,

}

_G.Table_ResData = 
{
    Belong = "client",
    KeyName = "id",
    GetRowData = function (id)
        return TableManager.GetRowData("ResData",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("ResData")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_ResData.proto"
        TableManager.AddLoadTableTask("ResData")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_ResData.LoadProtoCallback,Table_ResData,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: ResData.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_ResData.proto"))
        Table_ResData.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/ResData.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_ResData.LoadTableBytesCallBack,Table_ResData,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: ResData.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_ResData', loader:GetBytes()))
        TableManager.LoadTableSuccess("ResData",AllRow.datas,Table_ResData)
    end,

}

_G.Table_RoleCall = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("RoleCall",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("RoleCall")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_RoleCall.proto"
        TableManager.AddLoadTableTask("RoleCall")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_RoleCall.LoadProtoCallback,Table_RoleCall,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: RoleCall.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_RoleCall.proto"))
        Table_RoleCall.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/RoleCall.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_RoleCall.LoadTableBytesCallBack,Table_RoleCall,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: RoleCall.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_RoleCall', loader:GetBytes()))
        TableManager.LoadTableSuccess("RoleCall",AllRow.datas,Table_RoleCall)
    end,

}

_G.Table_RoleName = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("RoleName",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("RoleName")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_RoleName.proto"
        TableManager.AddLoadTableTask("RoleName")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_RoleName.LoadProtoCallback,Table_RoleName,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: RoleName.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_RoleName.proto"))
        Table_RoleName.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/RoleName.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_RoleName.LoadTableBytesCallBack,Table_RoleName,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: RoleName.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_RoleName', loader:GetBytes()))
        TableManager.LoadTableSuccess("RoleName",AllRow.datas,Table_RoleName)
    end,

}

_G.Table_SceneData = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("SceneData",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("SceneData")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_SceneData.proto"
        TableManager.AddLoadTableTask("SceneData")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_SceneData.LoadProtoCallback,Table_SceneData,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: SceneData.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_SceneData.proto"))
        Table_SceneData.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/SceneData.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_SceneData.LoadTableBytesCallBack,Table_SceneData,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: SceneData.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_SceneData', loader:GetBytes()))
        TableManager.LoadTableSuccess("SceneData",AllRow.datas,Table_SceneData)
    end,

}

_G.Table_ScreenEffect = 
{
    Belong = "client",
    KeyName = "key",
    GetRowData = function (id)
        return TableManager.GetRowData("ScreenEffect",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("ScreenEffect")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_ScreenEffect.proto"
        TableManager.AddLoadTableTask("ScreenEffect")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_ScreenEffect.LoadProtoCallback,Table_ScreenEffect,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: ScreenEffect.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_ScreenEffect.proto"))
        Table_ScreenEffect.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/ScreenEffect.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_ScreenEffect.LoadTableBytesCallBack,Table_ScreenEffect,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: ScreenEffect.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_ScreenEffect', loader:GetBytes()))
        TableManager.LoadTableSuccess("ScreenEffect",AllRow.datas,Table_ScreenEffect)
    end,

}

_G.Table_UIConfig = 
{
    Belong = "client",
    KeyName = "id",
    GetRowData = function (id)
        return TableManager.GetRowData("UIConfig",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("UIConfig")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_UIConfig.proto"
        TableManager.AddLoadTableTask("UIConfig")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_UIConfig.LoadProtoCallback,Table_UIConfig,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: UIConfig.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_UIConfig.proto"))
        Table_UIConfig.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/UIConfig.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_UIConfig.LoadTableBytesCallBack,Table_UIConfig,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: UIConfig.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_UIConfig', loader:GetBytes()))
        TableManager.LoadTableSuccess("UIConfig",AllRow.datas,Table_UIConfig)
    end,

}

_G.Table_WordData = 
{
    Belong = "client",
    KeyName = "id",
    GetRowData = function (id)
        return TableManager.GetRowData("WordData",id)
    end,
    GetAllRowData = function ()
        return TableManager.GetAllRowData("WordData")
    end,
    
    LoadProto = function ()
        local loadPath = "Lua_Proto/Table_WordData.proto"
        TableManager.AddLoadTableTask("WordData")
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_WordData.LoadProtoCallback,Table_WordData,true)
    end,
    LoadProtoCallback = function(caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,protoName: WordData.proto")
            return
        end

        assert(protoc:load(loader:GetText(),"Table_WordData.proto"))
        Table_WordData.LoadTableBytes()
    end,

    LoadTableBytes = function()
        local loadPath = "Lua_Bytes/WordData.bytes"
        CS.LoadAssetUtility.LoadTextAsset(loadPath,Table_WordData.LoadTableBytesCallBack,Table_WordData,true)
    end,

    LoadTableBytesCallBack = function (caller,loader,result)
        if(result == false) then
            LogManager.LogError("Lua load Table Proto Failed,bytes Name: WordData.bytes")
            return
        end
        local AllRow = assert(pb.decode('Table_WordData', loader:GetBytes()))
        TableManager.LoadTableSuccess("WordData",AllRow.datas,Table_WordData)
    end,

}

local AllTables = 
{ 
    StartLoadAllTable = function()
    	Table_Chapter1.LoadProto();
		Table_Chapter10.LoadProto();
		Table_Chapter10Dialog.LoadProto();
		Table_Chapter11.LoadProto();
		Table_Chapter11Dialog.LoadProto();
		Table_Chapter12.LoadProto();
		Table_Chapter12Dialog.LoadProto();
		Table_Chapter1Dialog.LoadProto();
		Table_Chapter2.LoadProto();
		Table_Chapter2Dialog.LoadProto();
		Table_Chapter2Option.LoadProto();
		Table_Chapter2OptionText.LoadProto();
		Table_Chapter3.LoadProto();
		Table_Chapter3Dialog.LoadProto();
		Table_Chapter3Option.LoadProto();
		Table_Chapter3OptionText.LoadProto();
		Table_Chapter4.LoadProto();
		Table_Chapter4Dialog.LoadProto();
		Table_Chapter4Option.LoadProto();
		Table_Chapter4OptionText.LoadProto();
		Table_Chapter5.LoadProto();
		Table_Chapter5Dialog.LoadProto();
		Table_Chapter5Option.LoadProto();
		Table_Chapter5OptionText.LoadProto();
		Table_Chapter6.LoadProto();
		Table_Chapter6Dialog.LoadProto();
		Table_Chapter6Option.LoadProto();
		Table_Chapter6OptionText.LoadProto();
		Table_Chapter7.LoadProto();
		Table_Chapter7Dialog.LoadProto();
		Table_Chapter7Option.LoadProto();
		Table_Chapter7OptionText.LoadProto();
		Table_Chapter8.LoadProto();
		Table_Chapter8Dialog.LoadProto();
		Table_Chapter8Option.LoadProto();
		Table_Chapter8OptionText.LoadProto();
		Table_Chapter9.LoadProto();
		Table_Chapter9Dialog.LoadProto();
		Table_LivelyRole.LoadProto();
		Table_MazeMap.LoadProto();
		Table_MazeMapItem.LoadProto();
		Table_ResData.LoadProto();
		Table_RoleCall.LoadProto();
		Table_RoleName.LoadProto();
		Table_SceneData.LoadProto();
		Table_ScreenEffect.LoadProto();
		Table_UIConfig.LoadProto();
		Table_WordData.LoadProto();
	        --...
    end
}
return AllTables
