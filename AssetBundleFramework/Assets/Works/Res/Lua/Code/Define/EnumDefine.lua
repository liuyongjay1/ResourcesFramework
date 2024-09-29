
--[[
	索引到AllTables.lua -- TableNameConfig变量
	由于TableNameConfig变量存的是所有表的名字，有大量根据章节Id不同而重复的表名，例如Table_Chapter1、Table_Chapter2、Table_Chapte3等
	因此用Table_Chapter%s做代替，获取到当前的章节Id，做成通用接口
]]
TableNameStr = {
	Chapter = "Table_Chapter%s",
    ChapterDialog = "Table_Chapter%sDialog",
    ChapterOption = "Table_Chapter%sOption",
    ChapterOptionText = "Table_Chapter%sOptionText",
}



--计时器类型
ETimerType =
{
	Once = 1,
	Repeat = 2,
	Frame = 3,
}

--对话的UI类型
DialogUIType = {
    Common = 0,
    Message = 1,
    Scream = 2, --尖叫气泡
}

--角色ID定义，必须与配表一致
RoleIdDefine = {
	Role0 = 0,
	Role1 = 1,
	Role2 = 2,
	Role3 = 3,
	Role4 = 4,
	Role5 = 5,
	Role6 = 6,
	Role7 = 7,
	Role8 = 8,
}

