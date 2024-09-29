using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestData
{
    public string Name;
    public int index;
}

[CreateAssetMenu]
public class TestScriptableObject : ScriptableObject
{
    public List<TestData> dataList = new List<TestData>();
}
