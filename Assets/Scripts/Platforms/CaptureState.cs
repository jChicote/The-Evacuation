
namespace Evacuation.Level.TransportSystems
{
    public abstract class CaptureState
    {
        protected PlatformCapture platformCapture;

        /// <summary>
        /// This sets the capture context that holds reference to this state and essential for transitioning
        /// </summary>
        /// <param name="platformCapture"> The capture interface that maintains reference to this state </param>
        public void SetCaptureContext(PlatformCapture platformCapture)
        {
            this.platformCapture = platformCapture;
        }

        public abstract void InitState();
        public abstract void RunState();
    }

    public class TrackingState : CaptureState
    {
        public override void InitState()
        {
            platformCapture.captureRenderer.BeginRenderingCaptureVisual(platformCapture.shipPositionLocator);
            platformCapture.CaptureTimer.StartTimer();
        }

        public override void RunState()
        {
            platformCapture.captureRenderer.ShrinkCaptureCircle();
            platformCapture.captureRenderer.PositionCaptureLine();
        }
    }

    public class GuideState : CaptureState
    {
        public override void InitState()
        {
            platformCapture.CaptureTimer.ResetTimer();
            platformCapture.captureRenderer.StopRenderingCaptureVisual();
        }

        public override void RunState()
        {
            platformCapture.shipLandingManoeuvre.AutoLand(platformCapture.transform);

            if (platformCapture.shipLandingManoeuvre.CheckHasLanded())
            {
                platformCapture.TransitionState(new LandedState());
            }
        }
    }

    public class LandedState : CaptureState
    {
        public override void InitState()
        {
            platformCapture.shipTranslate.AttachToPlatform();
            platformCapture.platform.EnablePlatformTransport();
        }

        public override void RunState()
        {
            platformCapture.shipTranslate.StickToPlatform(platformCapture.transform.position);
        }
    }

    public class DepartedState : CaptureState
    {
        public override void InitState() { }

        public override void RunState() { }
    }
}

