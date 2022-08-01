using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckCondition : ActionNode
{
    public int selectIndex;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (selectIndex == blackboard.nextSkillIndex )
        {
            blackboard.lastSkillIndex = selectIndex;
            blackboard.nextSkillIndex = -1;
            blackboard.index = 0;
            return State.Failure;
        }

        return State.Success;
    }
}
