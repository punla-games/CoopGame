using System.Collections.Generic;
using UnityEngine;

public class TableInteractable:Interactable
{
    public bool IsDirty { get; private set; }

    public GameObject CupPrefab;
    public Transform[] CupSpawns;
    public Transform CupHolder;

    private List<GameObject> Cups = new();

    private float timer = 0;

    public void Start()
    {
        SetDirty();
    }
    public void Update()
    {
        timer=Mathf.Max(timer-Time.deltaTime,0f);
        if(!IsDirty&&timer==0f)
            SetDirty();
    }

    public void SetDirty()
    {
        IsDirty=true;

        var spawns = new List<Transform>();
        spawns.AddRange(CupSpawns);
        spawns.Shuffle();

        int n = Random.Range(1,spawns.Count);
        for(int i = 0; i < n; i++)
        {
            var pos = CupSpawns[i].position;
            var rot = Quaternion.Euler(0f,Random.value*360f,0f);
            var cup = Instantiate(CupPrefab,pos,rot,CupHolder);
            Cups.Add(cup);
        }
    }
    public void ResetDirty()
    {
        IsDirty=false;

        foreach(var cup in Cups)
            Destroy(cup);

        Cups.Clear();
    }

    public override float GetInteractDuration(Player player)
    {
        if(IsDirty)
            return 0.5f;

        return 0f;
    }
    public override string GetInteractText(Player player)
    {
        if(IsDirty)
            return $"Press \"F\" to clean.";

        return "";
    }
    public override void OnInteract(Player player)
    {
        if(IsDirty)
        {
            PlayerHUD.Get.ShowTipEarned();
            ResetDirty();
            timer=Random.value*10f;
        }
    }
}
