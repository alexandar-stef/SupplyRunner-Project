using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{

    public class WeaponSway : MonoBehaviour
    {

        public PlayerMovement mover;

        [Header("Sway")]
        public float step = 0.01f;
        public float maxStepDistance = 0.06f;
        Vector3 swayPos;

        [Header("Sway Rotation")]
        public float rotationStep = 4f;
        public float maxRotationStep = 5f;
        Vector3 swayEulerRot;

        [SerializeField] private float smooth;
        float smoothRot = 12f;

        [Header("Bobbing")]
        public float speedCurve;
        float curveSin { get => Mathf.Sin(speedCurve); }
        float curveCos { get => Mathf.Cos(speedCurve); }

        public Vector3 travelLimit = Vector3.one * 0.025f;
        public Vector3 bobLimit = Vector3.one * 0.01f;
        Vector3 bobPosition;

        public float bobExaggeration;

        [Header("Bob Rotation")]
        public Vector3 multi;
        Vector3 bobEulerRotation;


        private void Update()
        {

            GetInput();

            Sway();
            SwayRotation();
            BobOffset();
            BobRotation();

            CompositePositionRotation();

        }

        Vector2 walkInput;
        Vector2 lookInput;

        private void GetInput()
        {
            //get player movement
            walkInput.x = Input.GetAxisRaw("Horizontal");
            walkInput.y = Input.GetAxisRaw("Vertical");
            walkInput = walkInput.normalized;


            lookInput.x = Input.GetAxisRaw("Mouse X");
            lookInput.y = Input.GetAxisRaw("Mouse Y");

        }

        private void Sway()
        {
            Vector3 invertLook = lookInput * -step;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);

            swayPos = invertLook;
        }

        private void SwayRotation()
        {
            Vector2 invertLook = lookInput * -rotationStep;
            invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

            swayEulerRot = new Vector3(invertLook.y, invertLook.x, invertLook.x);
        }

        private void CompositePositionRotation()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition, Time.deltaTime * smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(swayEulerRot) * Quaternion.Euler(bobEulerRotation), Time.deltaTime * smoothRot);

        }

        private void BobOffset()
        {

            speedCurve += Time.deltaTime * (mover.GetGround() ? (Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical")) * bobExaggeration : 1f) + 0.01f;

            bobPosition.x = (curveCos * bobLimit.x * (mover.GetGround() ? 1 : 0)) - (walkInput.x * travelLimit.x);
            bobPosition.y = (curveSin * bobLimit.y) - (Input.GetAxisRaw("Vertical") * travelLimit.y);
            bobPosition.z = -(walkInput.y * travelLimit.z);
        }

        private void BobRotation()
        {
            bobEulerRotation.x = (walkInput != Vector2.zero ? multi.x * (Mathf.Sin(2 * speedCurve)) : multi.x * (Mathf.Sin(2 * speedCurve) / 2));
            bobEulerRotation.y = (walkInput != Vector2.zero ? multi.y * curveCos : 0);
            bobEulerRotation.z = (walkInput != Vector2.zero ? multi.z * curveCos * walkInput.x : 0);
        }


    }
}
