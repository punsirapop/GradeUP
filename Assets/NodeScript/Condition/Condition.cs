using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class Condition : ActionNode
{
    public int normalAttackNumber;
    
    // 3 skill in this game
    public int numberOfSkill;

    protected override void OnStart() {
        blackboard.index += 1;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        if(blackboard.index >= normalAttackNumber)
        {
            Debug.Log("Use Skill!");
            //Set next skill
            SetNextSkill();
        }
        
        return State.Success;
    }

    private void SetNextSkill()
    {
        //next skill will not use same skill
        int nextSkillIndex = Random.Range(0, numberOfSkill);
        int offsetForSameSkill = Random.Range(1, numberOfSkill);

        if(nextSkillIndex == blackboard.lastSkillIndex)
        {
            nextSkillIndex = (nextSkillIndex + offsetForSameSkill) % numberOfSkill;
        }
        Debug.Log("next skill index = " + nextSkillIndex);
        blackboard.nextSkillIndex = nextSkillIndex;

    }
}
