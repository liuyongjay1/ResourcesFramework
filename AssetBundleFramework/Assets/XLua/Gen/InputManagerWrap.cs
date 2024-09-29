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
    public class InputManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(InputManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Tick", _m_Tick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExcuteAction", _m_ExcuteAction);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AxisDrag", _m_AxisDrag);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Enable", _g_get_Enable);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Enable", _s_set_Enable);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 13, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "LuaBindAction", _m_LuaBindAction_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LuaRemoveAction", _m_LuaRemoveAction_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "BindAction", _m_BindAction_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveAction", _m_RemoveAction_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LuaBindTouch", _m_LuaBindTouch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "BindTouch", _m_BindTouch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LuaRemoveTouch", _m_LuaRemoveTouch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveTouch", _m_RemoveTouch_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LuaBindAxis", _m_LuaBindAxis_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "BindAxis", _m_BindAxis_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LuaRemoveAxis", _m_LuaRemoveAxis_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveAxis", _m_RemoveAxis_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new InputManager();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to InputManager constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                InputManager gen_to_be_invoked = (InputManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Tick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                InputManager gen_to_be_invoked = (InputManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    float _fFrameTime = (float)LuaAPI.lua_tonumber(L, 2);
                    
                    gen_to_be_invoked.Tick( _fFrameTime );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaBindAction_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _actionName = LuaAPI.lua_tostring(L, 1);
                    XLua.LuaTable _tab = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
                    string _func = LuaAPI.lua_tostring(L, 3);
                    
                    InputManager.LuaBindAction( _actionName, _tab, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaRemoveAction_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _actionName = LuaAPI.lua_tostring(L, 1);
                    XLua.LuaTable _tab = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
                    
                    InputManager.LuaRemoveAction( _actionName, _tab );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BindAction_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _actionName = LuaAPI.lua_tostring(L, 1);
                    InputManager.KeyCallback _func = translator.GetDelegate<InputManager.KeyCallback>(L, 2);
                    
                    InputManager.BindAction( _actionName, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAction_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _actionName = LuaAPI.lua_tostring(L, 1);
                    InputManager.KeyCallback _func = translator.GetDelegate<InputManager.KeyCallback>(L, 2);
                    
                    InputManager.RemoveAction( _actionName, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExcuteAction(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                InputManager gen_to_be_invoked = (InputManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _actionName = LuaAPI.lua_tostring(L, 2);
                    int _type = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.ExcuteAction( _actionName, _type );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaBindTouch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    XLua.LuaTable _tab = (XLua.LuaTable)translator.GetObject(L, 1, typeof(XLua.LuaTable));
                    string _func = LuaAPI.lua_tostring(L, 2);
                    
                    InputManager.LuaBindTouch( _tab, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BindTouch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    InputManager.TouchCallback _func = translator.GetDelegate<InputManager.TouchCallback>(L, 1);
                    
                    InputManager.BindTouch( _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaRemoveTouch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    XLua.LuaTable _tab = (XLua.LuaTable)translator.GetObject(L, 1, typeof(XLua.LuaTable));
                    
                    InputManager.LuaRemoveTouch( _tab );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveTouch_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    InputManager.TouchCallback _func = translator.GetDelegate<InputManager.TouchCallback>(L, 1);
                    
                    InputManager.RemoveTouch( _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaBindAxis_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _joyIndex = LuaAPI.xlua_tointeger(L, 1);
                    XLua.LuaTable _tab = (XLua.LuaTable)translator.GetObject(L, 2, typeof(XLua.LuaTable));
                    string _func = LuaAPI.lua_tostring(L, 3);
                    
                    InputManager.LuaBindAxis( _joyIndex, _tab, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BindAxis_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _joyIndex = LuaAPI.xlua_tointeger(L, 1);
                    InputManager.AxisCallback _func = translator.GetDelegate<InputManager.AxisCallback>(L, 2);
                    
                    InputManager.BindAxis( _joyIndex, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LuaRemoveAxis_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    XLua.LuaTable _tab = (XLua.LuaTable)translator.GetObject(L, 1, typeof(XLua.LuaTable));
                    
                    InputManager.LuaRemoveAxis( _tab );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAxis_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    int _joyIndex = LuaAPI.xlua_tointeger(L, 1);
                    InputManager.AxisCallback _func = translator.GetDelegate<InputManager.AxisCallback>(L, 2);
                    
                    InputManager.RemoveAxis( _joyIndex, _func );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AxisDrag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                InputManager gen_to_be_invoked = (InputManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    float _x = (float)LuaAPI.lua_tonumber(L, 3);
                    float _y = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.AxisDrag( _index, _x, _y );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Enable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                InputManager gen_to_be_invoked = (InputManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Enable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Enable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                InputManager gen_to_be_invoked = (InputManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Enable = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
