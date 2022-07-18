using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        public Vector2 moveToPosition;
        public int index = 0;

        //In order to run first skill randomly
        public int nextSkillIndex = -1;
        public int lastSkillIndex = -1;

        public bool condition;
        public List<int> indexList;

        public Vector2 targetPosition;

        // [BOSSCHEM, LNG]
        public Vector2 centerPosition;
        public Vector2 scaleToFillMap;
    }
}