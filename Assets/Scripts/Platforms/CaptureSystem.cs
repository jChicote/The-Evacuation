using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheEvacuation.TimeUtility;

namespace TheEvacuation.Platforms
{
    public class CaptureSystem : BaseInteractiveObject
    {
        // Fields
        protected TimeUtility.SimpleCountDown timer;
        protected CaptureState captureState;
        protected Transform capturedCharacter;

        [SerializeField] private Transform captureCircle;
        [SerializeField] private LineRenderer captureLine;

        protected bool isActive = false;
        protected bool isCaptured = false;
        protected float captureCircleScaleValue;

        // Properties
        public bool IsCaptured { get => isCaptured; set => isCaptured = value; }
        public bool IsActive { get => isActive; set => isActive = value; }

        private void Start()
        {
            timer = new TimeUtility.SimpleCountDown(12, Time.deltaTime);
            captureCircle.gameObject.SetActive(false);
            captureLine.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isPaused) return;
            if (IsActive) RunCapture();
        }

        public virtual void RunCapture()
        {
            if (capturedCharacter == null) return;

            switch (captureState)
            {
                case CaptureState.Enter:
                    BeginRenderingCaptureSystem();
                    break;
                case CaptureState.Running:
                    RenderAnimatingCaptureCircle();
                    RenderCaptureLine();
                    break;
                case CaptureState.End:
                    StopRederingCaptureSystem();
                    ResetCaptureSystem();
                    break;
                default:
                    break;
            }
        }

        public virtual void BeginRenderingCaptureSystem()
        {
            captureCircle.gameObject.SetActive(true);
            captureLine.gameObject.SetActive(true);

            captureCircleScaleValue = 1;
            captureState = CaptureState.Running;
        }

        public virtual void ResetCaptureSystem()
        {
            captureCircle.localScale = new Vector2(0.33528f, 0.33528f);
            captureCircleScaleValue = 1;
            isCaptured = false;
            captureState = CaptureState.InActive;
            timer.ResetTimer();
        }

        protected void RenderAnimatingCaptureCircle()
        {
            captureCircleScaleValue = Mathf.Lerp(0.33528f, 0, timer.InterpolateValue);
            captureCircle.localScale = new Vector2(captureCircleScaleValue, captureCircleScaleValue);

            if (timer.CheckTimeIsUp())
            {
                print("Time is Up");
                captureState = CaptureState.End;
                return;
            }

            timer.TickTimer();
        }

        protected void RenderCaptureLine()
        {
            captureLine.SetPosition(0, new Vector2(0, 0));
            captureLine.SetPosition(1, transform.InverseTransformPoint(capturedCharacter.transform.position));
        }

        protected void StopRederingCaptureSystem()
        {
            captureCircle.gameObject.SetActive(false);
            captureLine.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (collision.GetComponent<IProjectile>() != null) return;

            capturedCharacter = collision.transform;
            captureState = CaptureState.Enter;
            IsActive = true;
            print("Has Captured");
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (collision.GetComponent<IProjectile>() != null) return;

            IsActive = false;
            capturedCharacter = null;
            StopRederingCaptureSystem();
            ResetCaptureSystem();
            print("Has Released Character");
        }
    }

    public enum CaptureState
    {
        InActive,
        Enter,
        Running,
        End
    }
}
