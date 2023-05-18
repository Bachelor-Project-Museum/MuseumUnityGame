using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {

        protected Node _root = null;
        private GameObject _playerObject = null;

        protected void Start()
        {
            _playerObject = GameObject.FindGameObjectWithTag("Player");
            StartBehaviorTree();
        }

        protected virtual void Update()
        {
            if (_root != null)
            {
                _root.SetData("player", _playerObject.transform);
                _root.Evaluate();
            }
        }

        protected abstract void StartBehaviorTree();

    }
}
