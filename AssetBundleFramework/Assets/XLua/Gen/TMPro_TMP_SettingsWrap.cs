#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class TMProTMP_SettingsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TMPro.TMP_Settings);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 7, 33, 5);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadDefaultSettings", _m_LoadDefaultSettings_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSettings", _m_GetSettings_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFontAsset", _m_GetFontAsset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSpriteAsset", _m_GetSpriteAsset_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetStyleSheet", _m_GetStyleSheet_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LoadLinebreakingRules", _m_LoadLinebreakingRules_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "version", _g_get_version);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableWordWrapping", _g_get_enableWordWrapping);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableKerning", _g_get_enableKerning);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableExtraPadding", _g_get_enableExtraPadding);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableTintAllSprites", _g_get_enableTintAllSprites);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableParseEscapeCharacters", _g_get_enableParseEscapeCharacters);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableRaycastTarget", _g_get_enableRaycastTarget);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "getFontFeaturesAtRuntime", _g_get_getFontFeaturesAtRuntime);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "missingGlyphCharacter", _g_get_missingGlyphCharacter);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "warningsDisabled", _g_get_warningsDisabled);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultFontAsset", _g_get_defaultFontAsset);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultFontAssetPath", _g_get_defaultFontAssetPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultFontSize", _g_get_defaultFontSize);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultTextAutoSizingMinRatio", _g_get_defaultTextAutoSizingMinRatio);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultTextAutoSizingMaxRatio", _g_get_defaultTextAutoSizingMaxRatio);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultTextMeshProTextContainerSize", _g_get_defaultTextMeshProTextContainerSize);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultTextMeshProUITextContainerSize", _g_get_defaultTextMeshProUITextContainerSize);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "autoSizeTextContainer", _g_get_autoSizeTextContainer);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "isTextObjectScaleStatic", _g_get_isTextObjectScaleStatic);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "fallbackFontAssets", _g_get_fallbackFontAssets);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "matchMaterialPreset", _g_get_matchMaterialPreset);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultSpriteAsset", _g_get_defaultSpriteAsset);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultSpriteAssetPath", _g_get_defaultSpriteAssetPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "enableEmojiSupport", _g_get_enableEmojiSupport);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "missingCharacterSpriteUnicode", _g_get_missingCharacterSpriteUnicode);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultColorGradientPresetsPath", _g_get_defaultColorGradientPresetsPath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "defaultStyleSheet", _g_get_defaultStyleSheet);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "styleSheetsResourcePath", _g_get_styleSheetsResourcePath);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "leadingCharacters", _g_get_leadingCharacters);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "followingCharacters", _g_get_followingCharacters);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "linebreakingRules", _g_get_linebreakingRules);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "useModernHangulLineBreakingRules", _g_get_useModernHangulLineBreakingRules);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "instance", _g_get_instance);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "missingGlyphCharacter", _s_set_missingGlyphCharacter);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "isTextObjectScaleStatic", _s_set_isTextObjectScaleStatic);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "enableEmojiSupport", _s_set_enableEmojiSupport);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "missingCharacterSpriteUnicode", _s_set_missingCharacterSpriteUnicode);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "useModernHangulLineBreakingRules", _s_set_useModernHangulLineBreakingRules);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new TMPro.TMP_Settings();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_Settings constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadDefaultSettings_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        var gen_ret = TMPro.TMP_Settings.LoadDefaultSettings(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSettings_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        var gen_ret = TMPro.TMP_Settings.GetSettings(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFontAsset_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        var gen_ret = TMPro.TMP_Settings.GetFontAsset(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpriteAsset_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        var gen_ret = TMPro.TMP_Settings.GetSpriteAsset(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetStyleSheet_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        var gen_ret = TMPro.TMP_Settings.GetStyleSheet(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadLinebreakingRules_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    TMPro.TMP_Settings.LoadLinebreakingRules(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_version(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, TMPro.TMP_Settings.version);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableWordWrapping(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableWordWrapping);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableKerning(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableKerning);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableExtraPadding(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableExtraPadding);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableTintAllSprites(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableTintAllSprites);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableParseEscapeCharacters(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableParseEscapeCharacters);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableRaycastTarget(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableRaycastTarget);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_getFontFeaturesAtRuntime(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.getFontFeaturesAtRuntime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_missingGlyphCharacter(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushinteger(L, TMPro.TMP_Settings.missingGlyphCharacter);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_warningsDisabled(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.warningsDisabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultFontAsset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.defaultFontAsset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultFontAssetPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, TMPro.TMP_Settings.defaultFontAssetPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultFontSize(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, TMPro.TMP_Settings.defaultFontSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultTextAutoSizingMinRatio(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, TMPro.TMP_Settings.defaultTextAutoSizingMinRatio);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultTextAutoSizingMaxRatio(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, TMPro.TMP_Settings.defaultTextAutoSizingMaxRatio);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultTextMeshProTextContainerSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushUnityEngineVector2(L, TMPro.TMP_Settings.defaultTextMeshProTextContainerSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultTextMeshProUITextContainerSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushUnityEngineVector2(L, TMPro.TMP_Settings.defaultTextMeshProUITextContainerSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_autoSizeTextContainer(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.autoSizeTextContainer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isTextObjectScaleStatic(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.isTextObjectScaleStatic);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fallbackFontAssets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.fallbackFontAssets);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_matchMaterialPreset(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.matchMaterialPreset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultSpriteAsset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.defaultSpriteAsset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultSpriteAssetPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, TMPro.TMP_Settings.defaultSpriteAssetPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enableEmojiSupport(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.enableEmojiSupport);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_missingCharacterSpriteUnicode(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.xlua_pushuint(L, TMPro.TMP_Settings.missingCharacterSpriteUnicode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultColorGradientPresetsPath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, TMPro.TMP_Settings.defaultColorGradientPresetsPath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_defaultStyleSheet(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.defaultStyleSheet);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_styleSheetsResourcePath(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushstring(L, TMPro.TMP_Settings.styleSheetsResourcePath);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_leadingCharacters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.leadingCharacters);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_followingCharacters(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.followingCharacters);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_linebreakingRules(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.linebreakingRules);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_useModernHangulLineBreakingRules(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, TMPro.TMP_Settings.useModernHangulLineBreakingRules);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, TMPro.TMP_Settings.instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_missingGlyphCharacter(RealStatePtr L)
        {
		    try {
                
			    TMPro.TMP_Settings.missingGlyphCharacter = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_isTextObjectScaleStatic(RealStatePtr L)
        {
		    try {
                
			    TMPro.TMP_Settings.isTextObjectScaleStatic = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_enableEmojiSupport(RealStatePtr L)
        {
		    try {
                
			    TMPro.TMP_Settings.enableEmojiSupport = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_missingCharacterSpriteUnicode(RealStatePtr L)
        {
		    try {
                
			    TMPro.TMP_Settings.missingCharacterSpriteUnicode = LuaAPI.xlua_touint(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_useModernHangulLineBreakingRules(RealStatePtr L)
        {
		    try {
                
			    TMPro.TMP_Settings.useModernHangulLineBreakingRules = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
