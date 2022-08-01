using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public class Wait : ActionNode {
        public float duration = 1;
        public float addMinRandomDuration;
        public float addMaxRandomDuration;
        float startTime;
        float randomDuration;

        protected override void OnStart() {
            randomDuration = Random.Range(addMinRandomDuration, addMaxRandomDuration);
            startTime = Time.time;
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            if (Time.time - startTime > duration + randomDuration) {
                return State.Success;
            }
            return State.Running;
        }
    }
}
