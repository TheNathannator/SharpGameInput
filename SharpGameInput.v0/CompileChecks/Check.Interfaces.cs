using System;
using SharpGameInput.v0;

public static partial class Checks
{
    public static unsafe void Check(IGameInput gameInput)
    {
        gameInput.CreateAggregateDevice(GameInputKind.Keyboard, out var device);
        gameInput.CreateDispatcher(out var dispatcher);
        gameInput.EnableOemDeviceSupport(0, 0, 0, 0);

        gameInput.SetFocusPolicy(GameInputFocusPolicy.DisableBackgroundInput);

        gameInput.FindDeviceFromId(new APP_LOCAL_DEVICE_ID(), out device);
        gameInput.FindDeviceFromObject(IntPtr.Zero, out device);
        gameInput.FindDeviceFromPlatformHandle(IntPtr.Zero, out device);
        gameInput.FindDeviceFromPlatformString(string.Empty, out device);
        char* str = stackalloc char[24];
        gameInput.FindDeviceFromPlatformString(str, out device);

        gameInput.GetCurrentTimestamp();
        gameInput.GetCurrentReading(GameInputKind.AnyKind, device, out var reading);
        gameInput.GetNextReading(reading, GameInputKind.AnyKind, device, out reading);
        gameInput.GetPreviousReading(reading, GameInputKind.AnyKind, device, out reading);
        gameInput.GetTemporalReading(0, device, out reading);

        gameInput.RegisterDeviceCallback(device, GameInputKind.AnyKind, GameInputDeviceStatus.AnyStatus,
            GameInputEnumerationKind.AsyncEnumeration, new object(), (a, b, c, d, e, f) => {}, out var token, out var result);
        gameInput.RegisterKeyboardLayoutCallback(device, new object(), (a, b, c, d, e, f) => {}, out token, out result);
        gameInput.RegisterReadingCallback(device, GameInputKind.AnyKind, 0, new object(), (a, b, c, d) => {}, out token, out result);
        gameInput.RegisterSystemButtonCallback(device, GameInputSystemButtons.Guide, new object(), (a, b, c, d, e, f) => {}, out token, out result);
    }

    public static void Check(IGameInputDispatcher dispatcher)
    {
        dispatcher.Dispatch(16000);
        dispatcher.OpenWaitHandle(out var handle);
    }

    public static unsafe void Check(IGameInputDevice left, IGameInputDevice right,
        LightIGameInputDevice lightLeft, LightIGameInputDevice lightRight)
    {
        ref readonly var info = ref left.GetDeviceInfo();

        left.GetDeviceStatus();
        left.GetBatteryState(out var batteryState);

        left.PowerOff();

        left.SendInputSynchronizationHint();
        left.SetInputSynchronizationState(false);

        left.CreateForceFeedbackEffect(0, new GameInputForceFeedbackParams(), out var effect);
        left.IsForceFeedbackMotorPoweredOn(0);
        left.SetForceFeedbackMotorGain(0, 0);
        left.SetHapticMotorState(0, new GameInputHapticFeedbackParams());
        left.SetRumbleState(new GameInputRumbleParams());

        left.CreateRawDeviceReport(0, GameInputRawDeviceReportKind.Output, out var rawReport);
        left.GetRawDeviceFeature(0, out rawReport);
        left.SetRawDeviceFeature(rawReport);
        left.SendRawDeviceOutput(rawReport);
        left.SendRawDeviceOutputWithResponse(rawReport, out rawReport);

        left.AcquireExclusiveRawDeviceAccess(500);
        left.ReleaseExclusiveRawDeviceAccess();

        left.ExecuteRawDeviceIoControl(0, new byte[4], new byte[4], out var bytesReturned);
#if NETSTANDARD2_1_OR_GREATER
        Span<byte> inSpan = stackalloc byte[4];
        Span<byte> outSpan = stackalloc byte[4];
        left.ExecuteRawDeviceIoControl(0, inSpan, outSpan, out bytesReturned);
#endif
        byte* inBuffer = stackalloc byte[4];
        byte* outBuffer = stackalloc byte[4];
        left.ExecuteRawDeviceIoControl(0, 4, inBuffer, 4, outBuffer, out bytesReturned);
        left.ExecuteRawDeviceIoControl(0, new GameInputBatteryState(), out GameInputGamepadState output, out bytesReturned);
        left.ExecuteRawDeviceIoControl(0, new GameInputBatteryState());
        left.ExecuteRawDeviceIoControl(0, out output, out bytesReturned);

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

    public static unsafe void Check(IGameInputReading left, IGameInputReading right,
        LightIGameInputReading lightLeft, LightIGameInputReading lightRight)
    {
        left.GetDevice(out var device);
        left.GetInputKind();
        left.GetSequenceNumber(GameInputKind.ControllerButton);
        left.GetTimestamp();

        left.GetRawReport(out var rawReport);

        left.GetControllerAxisCount();
        left.GetControllerAxisState(new float[4]);
#if NETSTANDARD2_1_OR_GREATER
        Span<float> axisSpan = stackalloc float[4];
        left.GetControllerAxisState(axisSpan);
#endif
        float* axisStates = stackalloc float[4];
        left.GetControllerAxisState(4, axisStates);

        left.GetControllerButtonCount();
        left.GetControllerButtonState(new bool[4]);
#if NETSTANDARD2_1_OR_GREATER
        Span<bool> buttonSpan = stackalloc bool[4];
        left.GetControllerButtonState(buttonSpan);
#endif
        bool* buttonStates = stackalloc bool[4];
        left.GetControllerButtonState(4, buttonStates);

        left.GetControllerSwitchCount();
        left.GetControllerSwitchState(new GameInputSwitchPosition[4]);
#if NETSTANDARD2_1_OR_GREATER
        Span<GameInputSwitchPosition> switchSpan = stackalloc GameInputSwitchPosition[4];
        left.GetControllerSwitchState(switchSpan);
#endif
        var switchStates = stackalloc GameInputSwitchPosition[4];
        left.GetControllerSwitchState(4, switchStates);

        left.GetKeyCount();
        left.GetKeyState(new GameInputKeyState[4]);
#if NETSTANDARD2_1_OR_GREATER
        Span<GameInputKeyState> keySpan = stackalloc GameInputKeyState[4];
        left.GetKeyState(keySpan);
#endif
        var keyStates = stackalloc GameInputKeyState[4];
        left.GetKeyState(4, keyStates);

        left.GetTouchCount();
        left.GetTouchState(new GameInputTouchState[4]);
#if NETSTANDARD2_1_OR_GREATER
        Span<GameInputTouchState> touchSpan = stackalloc GameInputTouchState[4];
        left.GetTouchState(touchSpan);
#endif
        var touchStates = stackalloc GameInputTouchState[4];
        left.GetTouchState(4, touchStates);

        left.GetArcadeStickState(out var arcadeState);
        left.GetFlightStickState(out var flightState);
        left.GetGamepadState(out var gamepadState);
        left.GetMotionState(out var motionState);
        left.GetMouseState(out var mouseState);
        left.GetRacingWheelState(out var wheelState);
        left.GetUiNavigationState(out var navState);

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

    public static unsafe void Check(IGameInputRawDeviceReport left, IGameInputRawDeviceReport right,
        LightIGameInputRawDeviceReport lightLeft, LightIGameInputRawDeviceReport lightRight)
    {
        ref readonly var info = ref left.GetReportInfo();

        left.GetDevice(out var device);

        left.GetItemValue(0, out long value);
        left.SetItemValue(0, value);
        left.ResetAllItems();
        left.ResetItemValue(0);

        left.GetRawDataSize();
        left.GetRawData(new byte[4]);
#if NETSTANDARD2_1_OR_GREATER
        Span<byte> span = stackalloc byte[4];
        left.GetRawData(span);
#endif
        byte* buffer = stackalloc byte[4];
        left.GetRawData(4, buffer);
        left.GetRawData(out GameInputGamepadState data);

        left.SetRawData(new byte[4]);
#if NETSTANDARD2_1_OR_GREATER
        left.SetRawData(span);
#endif
        left.SetRawData(4, buffer);
        left.SetRawData(data);

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

    public static void Check(IGameInputForceFeedbackEffect left, IGameInputForceFeedbackEffect right,
        LightIGameInputForceFeedbackEffect lightLeft, LightIGameInputForceFeedbackEffect lightRight)
    {
        left.GetDevice(out var device);
        left.GetMotorIndex();

        left.GetGain();
        left.GetParams(out var ffbParams);
        left.GetState();

        left.SetGain(0);
        left.SetParams(ffbParams);
        left.SetState(GameInputFeedbackEffectState.Running);

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
}