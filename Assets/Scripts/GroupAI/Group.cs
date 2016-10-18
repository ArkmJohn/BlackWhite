using UnityEngine;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    List<Character> members;
    Character leader;
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
