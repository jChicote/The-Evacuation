
namespace Evacuation.Level.TransportSystems
{
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public class TrackingState : CaptureState
    {
        public override void InitState()
        {
            platformCapture.captureRenderer.BeginRenderingCaptureVisual(platformCapture.autoLandingSystem);
            platformCapture.CaptureTimer.StartTimer();
        }

        public override void RunState()
        {
            platformCapture.captureRenderer.ShrinkCaptureCircle();
            platformCapture.captureRenderer.PositionCaptureLine();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GuideState : CaptureState
    {
        public override void InitState()
        {
            platformCapture.CaptureTimer.ResetTimer();
            platformCapture.captureRenderer.StopRenderingCaptureVisual();
        }

        public override void RunState()
        {
            platformCapture.autoLandingSystem.AutoLand(platformCapture.transform);

            if (platformCapture.autoLandingSystem.CheckHasLanded())
            {
                platformCapture.TransitionState(new LandedState());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LandedState : CaptureState
    {
        public override void InitState()
        {
            platformCapture.shipLandingSystem.AttachToPlatform();
            platformCapture.platform.EnablePlatformTransport();
        }

        public override void RunState()
        {
            platformCapture.shipLandingSystem.StickToPlatform(platformCapture.transform.position);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DepartedState : CaptureState
    {
        public override void InitState() { }

        public override void RunState() { }
    }
}

