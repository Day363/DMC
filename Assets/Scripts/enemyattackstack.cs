using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattackstack : MonoBehaviour
{
    [System.Serializable]
    public class StackCell
    {
        public Stack stack;
        public bool random;
        public int minstack;
        public int maxstack;
        public int fixstack;
    }

    public List<StackCell> stackcells = new List<StackCell> { };
}
