
--UI音频管理类--
AudioManager = {
	IsRootLoaded = false
}
local this = AudioManager

--C# AudioManager单例
local CS_AudioManagerInstance = CS.AudioManager.Instance

--Start--
function AudioManager.InitModule()
	
end

--[[
	
]]
function AudioManager.PlayAudio(audioPath)
	if CS_AudioManagerInstance then
		CS_AudioManagerInstance:PlayAudio(audioPath)
	end
end

--[[
	
]]
function AudioManager.StopAudio()
	if CS_AudioManagerInstance then
		CS_AudioManagerInstance:StopAudio()
	end

end

--[[
	
]]
function AudioManager.PlayAudioAtPoint(audioPath)
	if CS_AudioManagerInstance then
		CS_AudioManagerInstance:PlayAudio(audioPath)
	end
end
