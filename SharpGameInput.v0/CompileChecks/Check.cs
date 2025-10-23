using SharpGameInput.Common;
using SharpGameInput.v0;

public static partial class Checks
{
    public static void Check()
    {
        GameInput.Create(out var gameInput);
        GameInput.Create(out gameInput, out var result);
    }

    public static void Check(APP_LOCAL_DEVICE_ID left, APP_LOCAL_DEVICE_ID right)
    {
        left.Equals(right);
        left.GetHashCode();
        left.ToString();

        bool b;
        b = left == right;
        b = left != right;
    }

    public static void Check(CallbackTokenDisposer disposer)
    {
        disposer.Dispose();
    }

    public static void Check(GameInputCallbackToken left, GameInputCallbackToken right,
        LightGameInputCallbackToken lightLeft, LightGameInputCallbackToken lightRight)
    {
        left.Unregister(5000);
        left.TryUnregister(5000);
        left.UnregisterAsync(5000).Wait();
        left.TryUnregisterAsync(5000).Wait();

        left.Stop();
        lightLeft.Stop();

        left.GetHashCode();
        lightLeft.GetHashCode();

        left.Equals(right);
        left.Equals(lightLeft);
        lightLeft.Equals(right);
        lightLeft.Equals(lightRight);

        bool b;
        b = left == right;
        b = left == lightRight;
        b = lightLeft == right;
        b = lightLeft == lightRight;
    }

    public static void Check(GameInputForceFeedbackParams ffbParams)
    {
        ref var constant = ref ffbParams.constant;
        ref var ramp = ref ffbParams.ramp;
        ref var sineWave = ref ffbParams.sineWave;
        ref var squareWave = ref ffbParams.squareWave;
        ref var triangleWave = ref ffbParams.triangleWave;
        ref var sawtoothUpWave = ref ffbParams.sawtoothUpWave;
        ref var sawtoothDownWave = ref ffbParams.sawtoothDownWave;
        ref var spring = ref ffbParams.spring;
        ref var friction = ref ffbParams.friction;
        ref var damper = ref ffbParams.damper;
        ref var inertia = ref ffbParams.inertia;
    }

    public static void Check(GameInputComPtr<IGameInput> left, GameInputComPtr<IGameInput> right)
    {
        left.DangerousGetHandle();

        bool b = left.IsInvalid;

        left.GetHashCode();
        left.Equals(right);

        b = left == right;
        b = left != right;

        left.Duplicate();
        left.Dispose();
    }
}