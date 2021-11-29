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

        // Start is called before the first frame update
        private void Start()
        {
            timer = new TimeUtility.SimpleCountDown(12, Time.deltaTime);
            captureCircle.gameObject.SetActive(false);
            captureLine.gameObject.SetActive(false);
        }

        // Update is called once per frame
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
                    captureCircle.gameObject.SetActive(true);
                    captureLine.gameObject.SetActive(true);
                    captureCircleScaleValue = 1;
                    captureState = CaptureState.Running;
                    break;
                case CaptureState.Running:
                    RenderAnimatingCaptureCircle();
                    RenderCaptureLine();
                    break;
                case CaptureState.End:
                    StopRenderingCaptureCircle();
                    StopCaptureLine();
                    isCaptured = true;
                    captureState = CaptureState.InActive;
                    timer.ResetTimer();
                    break;
                default:
                    break;
            }
        }

        public virtual void ResetCaptureSystem()
        {
            captureCircle.gameObject.SetActive(false);
            captureLine.gameObject.SetActive(false);
            captureCircle.localScale = new Vector2(1, 1);
            captureCircleScaleValue = 1;
            isCaptured = false;
            captureState = CaptureState.InActive;
            timer.ResetTimer();
        }

        private void RenderAnimatingCaptureCircle()
        {
            captureCircleScaleValue = Mathf.Lerp(0.33528f, 0, timer.InterpolateValue);
            captureCircle.localScale = new Vector2(captureCircleScaleValue, captureCircleScaleValue);

            print(timer.InterpolateValue);
            if (timer.CheckTimeIsUp())
            {
                print("Time is Up");
                captureState = CaptureState.End;
                return;
            }

            timer.TickTimer();
        }

        private void RenderCaptureLine()
        {
            captureLine.SetPosition(0, new Vector2(0, 0));
            captureLine.SetPosition(1, transform.InverseTransformPoint(capturedCharacter.transform.position));
        }

        private void StopRenderingCaptureCircle()
        {
            captureCircle.gameObject.SetActive(false);
            captureCircle.localScale = new Vector2(1, 1);
            captureCircleScaleValue = 1;
        }

        private void StopCaptureLine()
        {
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
            isCaptured = false;
            capturedCharacter = null;
            ResetCaptureSystem();
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
