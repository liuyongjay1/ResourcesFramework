
using System;
using System.Collections.Generic;
//using UnityEngine.Events;
public class TObjectPool<T> where T : new()
{
    private readonly List<T> m_UnusedList = new List<T>();
    private readonly List<T> m_UsingList = new List<T>();

    public T Pop()
    {
        T element;
        if (m_UnusedList.Count == 0)
        {
            element = new T();
        }
        else
        {
            element = m_UnusedList[0];
            m_UnusedList.RemoveAt(0);
        }

        m_UsingList.Add(element);
        return (T)element;
    }

    public void Push(T element)
    {
        if (m_UnusedList.Contains(element))
            LogManager.LogError("Object already in pool.cant not add twice,please Check Asset Unload API");
        if (m_UsingList.Contains(element))
            m_UsingList.Remove(element);
        m_UnusedList.Add(element);
    }
   
    public void ReleaseUnusedClass()
    {
        m_UnusedList.Clear();
    }

    public void Release()
    {
        m_UsingList.Clear();
        m_UnusedList.Clear();
    }

    public List<T> GetUnusedList()
    {
        return m_UnusedList;
    }

    public List<T> GetUsedList()
    {
        return m_UsingList;
    }
}
