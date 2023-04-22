using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class SafeDic<Key,Value>
{
    [SerializeField]
    private string name;
    [SerializeField]
    private string keyName;
    [SerializeField]
    private string valueName;
    [SerializeField]
    private bool warning = true;



    public bool TryGetValue(Key key, out Value value)
    {
        return Dictionary.TryGetValue(key,out value);
    }

    public int Count => Dictionary.Count;
    
    

    public void Clear()
    {
  
        Dictionary.Clear();
    }
    public virtual Value this[Key key]
    {
        get 
        {
            if (key == null || !Dictionary.ContainsKey(key))
            {
                if (warning && key != null)
                {
                    GameTool.LogError($"{name}不存在{keyName}"+ key);
                }

                return default(Value);
            }
            return Dictionary[key];
        }
        set 
        {
            if (key == null || !Dictionary.ContainsKey(key))
            {
                if (warning && key != null)
                {
                    GameTool.LogError($"{name}不存在{keyName}"+key);
                }
   
                return;
            }
            Dictionary[key] = value;
        }
    }

  
    public Dictionary<Key, Value>.KeyCollection Keys => Dictionary.Keys;

    public SafeDic()
    {
        Dictionary = new Dictionary<Key, Value>();
    }

    public SafeDic(bool warning)
    {
        Dictionary = new Dictionary<Key, Value>();
        this.warning = warning;
    }

    public SafeDic(string name = null, string keyName = null, string valueName = null,string prefix = null,bool warning = true)
    {
        Dictionary = new Dictionary<Key, Value>();
   
        this.name = prefix +name;
        this.keyName = prefix +keyName;
        this.valueName = prefix +valueName;
        this.warning = warning;
    }

    [ShowInInspector]
    private Dictionary<Key, Value> dictionary;

    public Dictionary<Key, Value> Dictionary { get => dictionary; set => dictionary = value; }

    public bool ContainsKey(Key key)
    {
        return Dictionary.ContainsKey(key);
    }

    public virtual bool Add(Key key, Value value)
    {
        if (Dictionary.ContainsKey(key))
        {
            if (warning)
            {
                GameTool.LogError($"{name}在添加{valueName}时发现key重复"+ key);
            }
    
            return false;
        }
        Dictionary.Add(key, value);
        return true;
    }

    public virtual bool Remove(Key key)
    {
        if (!Dictionary.ContainsKey(key))
        {
            if (warning)
            {
                GameTool.LogError($"{name}在删除{keyName}时发现key不存在"+ key);
            }
        
            return false;
        }
        Dictionary.Remove(key);
        return true;
    }

    public virtual bool AddOrReplace(Key key, Value value)
    {
        if (Dictionary.ContainsKey(key))
        {
            Dictionary[key] = value;
            return false;
        }
        Dictionary.Add(key, value);
        return true;
    }

}
