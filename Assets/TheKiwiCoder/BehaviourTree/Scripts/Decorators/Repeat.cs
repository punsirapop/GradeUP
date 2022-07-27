using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public class Repeat : DecoratorNode {

        public bool restartOnSuccess = true;
        public bool restartOnFailure = false;

        public int roundNumber;
        private int roundCount = 1;

        protected override void OnStart() {

        }

        protected override void OnStop() {

        }

        protected override State OnUpdate() {
            switch (child.Update()) {
                case State.Running:
                    break;
                case State.Failure:
                    if (restartOnFailure) {
                        return State.Running;
                    } else {
                        return State.Failure;
                    }
                case State.Success:
                    if (restartOnSuccess) {
                        return State.Running;
                    }
                    if( roundCount < roundNumber)
                    {
                        roundCount += 1;
                        return State.Running;
                    }
                    else 
                    {
                        roundCount = 1;
                        return State.Success;
                    }
            }
            return State.Running;
        }
    }

    
}
