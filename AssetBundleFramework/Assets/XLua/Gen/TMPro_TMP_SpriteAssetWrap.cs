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
    public class TMProTMP_SpriteAssetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TMPro.TMP_SpriteAsset);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 8, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UpdateLookupTables", _m_UpdateLookupTables);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpriteIndexFromHashcode", _m_GetSpriteIndexFromHashcode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpriteIndexFromUnicode", _m_GetSpriteIndexFromUnicode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetSpriteIndexFromName", _m_GetSpriteIndexFromName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SortGlyphTable", _m_SortGlyphTable);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "version", _g_get_version);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "faceInfo", _g_get_faceInfo);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spriteCharacterTable", _g_get_spriteCharacterTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spriteCharacterLookupTable", _g_get_spriteCharacterLookupTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spriteGlyphTable", _g_get_spriteGlyphTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spriteSheet", _g_get_spriteSheet);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "spriteInfoList", _g_get_spriteInfoList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fallbackSpriteAssets", _g_get_fallbackSpriteAssets);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "spriteSheet", _s_set_spriteSheet);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "spriteInfoList", _s_set_spriteInfoList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fallbackSpriteAssets", _s_set_fallbackSpriteAssets);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SearchForSpriteByUnicode", _m_SearchForSpriteByUnicode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SearchForSpriteByHashCode", _m_SearchForSpriteByHashCode_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new TMPro.TMP_SpriteAsset();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TMPro.TMP_SpriteAsset constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UpdateLookupTables(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UpdateLookupTables(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpriteIndexFromHashcode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _hashCode = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetSpriteIndexFromHashcode( _hashCode );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpriteIndexFromUnicode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    uint _unicode = LuaAPI.xlua_touint(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetSpriteIndexFromUnicode( _unicode );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSpriteIndexFromName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetSpriteIndexFromName( _name );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SearchForSpriteByUnicode_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_SpriteAsset _spriteAsset = (TMPro.TMP_SpriteAsset)translator.GetObject(L, 1, typeof(TMPro.TMP_SpriteAsset));
                    uint _unicode = LuaAPI.xlua_touint(L, 2);
                    bool _includeFallbacks = LuaAPI.lua_toboolean(L, 3);
                    int _spriteIndex;
                    
                        var gen_ret = TMPro.TMP_SpriteAsset.SearchForSpriteByUnicode( _spriteAsset, _unicode, _includeFallbacks, out _spriteIndex );
                        translator.Push(L, gen_ret);
                    LuaAPI.xlua_pushinteger(L, _spriteIndex);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SearchForSpriteByHashCode_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    TMPro.TMP_SpriteAsset _spriteAsset = (TMPro.TMP_SpriteAsset)translator.GetObject(L, 1, typeof(TMPro.TMP_SpriteAsset));
                    int _hashCode = LuaAPI.xlua_tointeger(L, 2);
                    bool _includeFallbacks = LuaAPI.lua_toboolean(L, 3);
                    int _spriteIndex;
                    
                        var gen_ret = TMPro.TMP_SpriteAsset.SearchForSpriteByHashCode( _spriteAsset, _hashCode, _includeFallbacks, out _spriteIndex );
                        translator.Push(L, gen_ret);
                    LuaAPI.xlua_pushinteger(L, _spriteIndex);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SortGlyphTable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SortGlyphTable(  );
                    
                    
                    
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
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.version);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_faceInfo(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.faceInfo);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spriteCharacterTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.spriteCharacterTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spriteCharacterLookupTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.spriteCharacterLookupTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spriteGlyphTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.spriteGlyphTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spriteSheet(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.spriteSheet);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_spriteInfoList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.spriteInfoList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fallbackSpriteAssets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fallbackSpriteAssets);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spriteSheet(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spriteSheet = (UnityEngine.Texture)translator.GetObject(L, 2, typeof(UnityEngine.Texture));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_spriteInfoList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.spriteInfoList = (System.Collections.Generic.List<TMPro.TMP_Sprite>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<TMPro.TMP_Sprite>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fallbackSpriteAssets(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TMPro.TMP_SpriteAsset gen_to_be_invoked = (TMPro.TMP_SpriteAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fallbackSpriteAssets = (System.Collections.Generic.List<TMPro.TMP_SpriteAsset>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<TMPro.TMP_SpriteAsset>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
