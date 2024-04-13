using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lvl_0
{
    public class SummonerController : MonoBehaviour
    {

        [SerializeField]
        private List<JointTargets> m_jointTargets;

        [SerializeField]
        private Vector3 m_jointForce;

        private InputActions m_inputActions;
        private Dictionary<Joint, Rigidbody> m_joints;

        private void Awake()
        {
            m_inputActions = new InputActions();
            m_joints = new Dictionary<Joint, Rigidbody>();
            foreach(var jointTarget in m_jointTargets)
            {
                m_joints.Add(jointTarget.joint, jointTarget.jointTransform);
            }
        }

        private void OnEnable()
        {
            m_inputActions.Summoning.Enable();
        }

        private void Update()
        {
            if (m_inputActions.Summoning.Q.IsPressed())
            {
                m_joints[Joint.LeftHand].AddForce(m_jointForce);
            }
            
            if (m_inputActions.Summoning.W.IsPressed())
            {
                m_joints[Joint.LeftElbow].AddForce(m_jointForce);
            }

            if (m_inputActions.Summoning.O.IsPressed())
            {
                m_joints[Joint.RightElbow].AddForce(m_jointForce);
            }

            if (m_inputActions.Summoning.P.IsPressed())
            {
                m_joints[Joint.RightHand].AddForce(m_jointForce);
            }
        }
    }

    [Serializable]
    public struct JointTargets
    {
        public Joint joint;
        public Rigidbody jointTransform;
    }

    [Serializable]
    public enum Joint
    {
        LeftElbow,
        RightElbow,
        LeftHand,
        RightHand
    }
}
