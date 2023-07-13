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
            _playerObject = GameManager.Instance.PlayerObject;
            StartBehaviorTree();
        }

        protected virtual void Update()
        {
            if (_root != null)
            {
                _root.SetData((string)GameManager.Instance.PlayerObject.name, _playerObject.transform);
                _root.Evaluate();
            }
        }

        protected abstract void StartBehaviorTree();

    }
}
