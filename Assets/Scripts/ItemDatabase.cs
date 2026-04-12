using System.Collections.Generic;
using UnityEngine;

public static class ItemDatabase
{
    private static Dictionary<string,ItemDefinition> dictionary=new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void Init()
    {
        var defs = Resources.LoadAll<ItemDefinition>("ItemDefinitions");
        foreach(var def in defs)
        {
            dictionary.Add(def.Id,def);
        }
    }

    public static Item Create(string id)
    {
        if(dictionary.ContainsKey(id))
        {
            return dictionary[id].CreateInstance();
        }

        return null;
    }
}