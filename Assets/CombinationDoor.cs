using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
public class CombinationDoor : TriggerParent
{
    [SerializeField] List<TriggerParent> triggers = new List<TriggerParent>();
    public List<bool> Combination = new List<bool>() { false, false, false, false, false, false, false, false };
    public List<bool> Answer = new List<bool>() {false, true, false, true,true,false,true,true };
    [SerializeField] bool Correct = false;

    private void Awake()
    {
        
        for (int i = 0; i < triggers.Count; i++)
        {
            int index = i;
            triggers[i].OnTrigger += () =>
            {
                Combination[index] = !Combination[index];
                print(Combination.SequenceEqual(Answer));
                if(Combination.SequenceEqual(Answer)) Trigger(true);
            };
        }
    }

    [Button]
    public void Test()
    {
        print(Combination.SequenceEqual(Answer));
    }
    [Button]
    public void set()
    {
        Combination = new List<bool>(Answer);
    }
}
