using UnityEngine;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    public List<Enemy> members;
    public Enemy leader;
    public Node currentCommand;

    public void InitGroup(Enemy me)
    {
        members = new List<Enemy>();
        leader = me;
        AddMember(leader);
    }

    public void Members(List<Enemy> newMembers)
    {
        // Compare each member to each new member and if not there remove from group
        // set the newmembers as the member
        // Sets each member to this group

    }

    void AddMember(Enemy member)
    {
        
        member.GetComponent<Enemy>().myGroup = this;
    }
    
}
