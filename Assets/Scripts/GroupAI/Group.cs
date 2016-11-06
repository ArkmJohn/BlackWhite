using UnityEngine;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    public List<Character> members;
    public Character leader;
    public Node currentCommand;

    public void InitGroup(Character me)
    {
        members = new List<Character>();
        leader = me;
        members.Add(leader);
    }

    void AddMember(Character member)
    {
        members.Add(member);
    }
    
}
