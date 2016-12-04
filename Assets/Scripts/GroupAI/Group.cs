using UnityEngine;
using System.Collections.Generic;

public class Group : MonoBehaviour {

    public enum formationType
    {
        LINE,
        ELEMENTS
        
    }

    public formationType myFormation = formationType.ELEMENTS;
    public List<Enemy> members;
    public Enemy leader;
    public List<GameObject> shadows;
    public Node currentCommand;

    public void InitGroup(Enemy me)
    {
        members = new List<Enemy>();
        leader = me;
        AddMember(leader);
    }

    void Start()
    {
        leader = GetComponent<Enemy>();
        shadows = new List<GameObject>();
        CreateShadowClone();
    }

    void CreateShadowClone()
    {
        for (int i = 0; i < members.Count; i++)
        {
            GameObject a = new GameObject("ShadowClone");
            shadows.Add(a);
        }
    }

    public void SetCommand(string CommandName)
    {

        switch (CommandName)
        {
            case "Squadron":
                Formation myFormation = CreateFormation();
                List<Vector3> positions = myFormation.FormationPosition();

                foreach (Enemy a in members)
                {
                    for (int i = 1; i < members.Count; i++)
                    {
                        shadows[i].transform.position = positions[i];
                        Vector3 x = new Vector3(shadows[i].transform.position.x, members[i].transform.position.y, shadows[i].transform.position.z);
                        //members[i].GetComponent<GroupMemberAI>().GoToPosition(shadows[i]);
                        
                    }
                }


                break;
            case "MoveToTarget":

                break;
        }
    }

    void Update()
    {
       for(int i = 0; i < members.Count ; i++)
        {
            //Debug.Log(members[i].gameObject.name + " is " + i + " on the list");
        }
        //SetCommand("Squadron");
    }

    void AddMember(Enemy member)
    {
        
        member.GetComponent<Enemy>().myGroup = this;
    }

    Formation CreateFormation()
    {
        switch (myFormation)
        {
            case formationType.ELEMENTS:
                return new Elements(3, 2, 2, false, this.transform.position);
                
            default:
                return new Line(3, members.Count, false, transform.position);
        }
    }
}
