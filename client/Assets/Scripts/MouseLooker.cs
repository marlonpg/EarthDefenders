﻿
using UnityEngine;
using System.Collections;
namespace UnityStandardAssets.CrossPlatformInput
{
    public class MouseLooker : MonoBehaviour
    {

        // Use this for initialization
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;

        // internal private variables
        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private Transform character;
        private Transform cameraTransform;

        void Start()
        {
            // get a reference to the character's transform (which this script should be attached to)
            character = gameObject.transform;

            // get a reference to the main camera's transform
            cameraTransform = Camera.main.transform;

            // get the location rotation of the character and the camera
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = cameraTransform.localRotation;
        }

        void Update()
        {
            // rotate stuff based on the mouse
            LookRotation();
        }

        public void LookRotation()
        {
            //get the y and x rotation based on the Input manager
            float yRot = CrossPlatformInputManager.GetAxisRaw("HorizontalLook") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxisRaw("VerticalLook") * YSensitivity;


            // calculate the rotation
            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            // clamp the vertical rotation if specified
            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            // update the character and camera based on calculations
            if (smooth) // if smooth, then slerp over time
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                                                            smoothTime * Time.deltaTime);
                cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, m_CameraTargetRot,
                                                         smoothTime * Time.deltaTime);
            }
            else // not smooth, so just jump
            {
                character.localRotation = m_CharacterTargetRot;
                cameraTransform.localRotation = m_CameraTargetRot;
            }
        }

        // Some math ... eeck!
        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
    }
}
