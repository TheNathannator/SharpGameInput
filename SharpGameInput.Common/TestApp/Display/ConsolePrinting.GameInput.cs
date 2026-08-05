#if GAMEINPUT_V1_OR_GREATER
#define HAS_ABS_MOUSE
#endif

#if GAMEINPUT_V0
#define HAS_TOUCH_REPORTS
#endif

#if GAMEINPUT_V0
#define HAS_MOTION_REPORTS
#endif

#if GAMEINPUT_V2_OR_LATER
#define HAS_SENSOR_REPORTS
#endif

#if GAMEINPUT_V0 || GAMEINPUT_V3_OR_LATER
#define HAS_RAW_REPORTS
#endif

using System;
using System.Buffers;
using System.Text;
using SharpGameInput.TestApp.Utility;

namespace SharpGameInput.TestApp.Display
{
    internal static partial class ConsolePrinting
    {
        private static ulong _timestampBase;

        public static void SetTimestampBase(ulong timestamp)
        {
            _timestampBase = timestamp;
        }

        public static TimeSpan TimestampToTimeSpan(ulong timestamp)
            => TimeSpan.FromSeconds(timestamp / 1000000.0);

        public static TimeSpan TimestampToTimeSpanFromStartup(ulong timestamp)
            => TimestampToTimeSpan(timestamp - _timestampBase);

        public static void WriteTimestamp(ulong timestamp)
        {
            var time = TimestampToTimeSpan(timestamp);
            var timeSinceStartup = TimestampToTimeSpanFromStartup(timestamp);
            Console.Write($"{time} ({timeSinceStartup})");
        }

        public static void Print(LightIGameInputReading reading, GameInputKind filterKinds, StateBuffer? lastReport)
        {
            if (filterKinds == GameInputKind.AnyKind)
            {
                // Filter out reading kinds that can overlap with other kinds
                // They will still be considered in the default case, but should not interfere
                // with printing a more specific report kind where possible
                filterKinds &= GameInputKind.Controller | GameInputKind.UiNavigation;
            }

            var inputs = reading.GetInputKind();
            switch (inputs & filterKinds)
            {
#if HAS_RAW_REPORTS
                case GameInputKind.RawDeviceReport:
                {
                    PrintRawReport(reading, lastReport);
                    break;
                }
#endif
                case GameInputKind.ControllerAxis:
                case GameInputKind.ControllerButton:
                case GameInputKind.ControllerSwitch:
                case GameInputKind.ControllerAxis | GameInputKind.ControllerButton:
                case GameInputKind.ControllerAxis | GameInputKind.ControllerSwitch:
                case GameInputKind.ControllerButton | GameInputKind.ControllerSwitch:
                case GameInputKind.ControllerAxis | GameInputKind.ControllerButton | GameInputKind.ControllerSwitch:
                {
                    PrintControllerReading(reading, lastReport);
                    break;
                }
                case GameInputKind.Keyboard:
                {
                    PrintKeyboardReading(reading, lastReport);
                    break;
                }
                case GameInputKind.Mouse:
                {
                    PrintMouseReading(reading, lastReport);
                    break;
                }
#if HAS_TOUCH_REPORTS
                case GameInputKind.Touch:
                {
                    PrintTouchReading(reading, lastReport);
                    break;
                }
#endif
#if HAS_MOTION_REPORTS
                case GameInputKind.Motion:
                {
                    PrintMotionReading(reading, lastReport);
                    break;
                }
#endif
#if HAS_SENSOR_REPORTS
                case GameInputKind.Sensors:
                {
                    PrintSensorsReading(reading, lastReport);
                    break;
                }
#endif
                case GameInputKind.ArcadeStick:
                {
                    PrintArcadeStickReading(reading, lastReport);
                    break;
                }
                case GameInputKind.FlightStick:
                {
                    PrintFlightStickReading(reading, lastReport);
                    break;
                }
                case GameInputKind.Gamepad:
                {
                    PrintGamepadReading(reading, lastReport);
                    break;
                }
                case GameInputKind.RacingWheel:
                {
                    PrintRacingWheelReading(reading, lastReport);
                    break;
                }
                case GameInputKind.UiNavigation:
                {
                    PrintUiNavigationReading(reading, lastReport);
                    break;
                }
                default:
                {
                    if ((inputs & GameInputKind.Controller) != 0)
                    {
                        PrintControllerReading(reading, lastReport);
                        break;
                    }

                    if ((inputs & GameInputKind.UiNavigation) != 0)
                    {
                        PrintUiNavigationReading(reading, lastReport);
                        break;
                    }

                    ulong timestamp = reading.GetTimestamp();
                    if (lastReport == null || lastReport.Write(timestamp))
                    {
                        WriteTimestamp(timestamp);
                        Console.WriteLine($": {inputs} (0x{(int)inputs})");
                    }
                    break;
                }
            }
        }

#if HAS_RAW_REPORTS
        public static bool PrintRawReport(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetRawReport(out var rawReport))
            {
                return false;
            }

            using (rawReport)
            {
                uint reportId = rawReport.GetReportInfo().id;
                int reportSize = (int)rawReport.GetRawDataSize();

                using var _buffer = new StackOrPoolArray<byte>(reportSize, stackalloc byte[64]);
                var buffer = _buffer.Array;

                if (buffer.Length > 0) unsafe
                {
                    fixed (byte* ptr = buffer)
                    {
                        int readSize = (int)rawReport.GetRawData((UIntPtr)buffer.Length, ptr);
                        buffer = buffer.Slice(0, Math.Min(readSize, buffer.Length));
                    }
                }

                if (lastReport == null || lastReport.Write(buffer))
                {
                    WriteTimestamp(reading.GetTimestamp());
                    Console.Write($": [{buffer.Length:D3}] {reportId:X2}: ");
                    WriteBuffer(buffer);
                    Console.WriteLine();
                }
            }

            return true;
        }
#endif

        public static bool PrintControllerReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            lastReport?.ResetAppend();

            int buttonCount = (int)reading.GetControllerButtonCount();
            if (buttonCount > 0)
            {
                using var _buffer = new StackOrPoolArray<ByteBool>(
                    buttonCount,
                    stackalloc ByteBool[32]
                );
                var buffer = _buffer.Array;

                unsafe
                {
                    fixed (ByteBool* ptr = buffer)
                    {
                        int readSize = (int)reading.GetControllerButtonState((uint)buffer.Length, ptr);
                        buffer = buffer.Slice(0, Math.Min(readSize, buffer.Length));
                    }
                }

                if (lastReport == null || lastReport.Append(buffer))
                {
                    WriteTimestamp(reading.GetTimestamp());
                    Console.Write($": buttons ");

                    if (buffer.Length < 1)
                    {
                        Console.Write(" <none>");
                    }
                    else
                    {
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            bool pressed = buffer[i];
                            Console.Write(pressed ? '1' : '0');
                        }
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                lastReport?.Append([]);
            }

            int switchCount = (int)reading.GetControllerSwitchCount();
            if (switchCount > 0)
            {
                using var _buffer = new StackOrPoolArray<GameInputSwitchPosition>(
                    buttonCount,
                    stackalloc GameInputSwitchPosition[16]
                );
                var buffer = _buffer.Array;

                unsafe
                {
                    fixed (GameInputSwitchPosition* ptr = buffer)
                    {
                        int readSize = (int)reading.GetControllerSwitchState((uint)buffer.Length, ptr);
                        buffer = buffer.Slice(0, Math.Min(readSize, buffer.Length));
                    }
                }

                if (lastReport == null || lastReport.Append(buffer))
                {
                    WriteTimestamp(reading.GetTimestamp());
                    Console.Write($": switches");

                    if (buffer.Length < 1)
                    {
                        Console.Write(" <none>");
                    }
                    else
                    {
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            var position = buffer[i];
                            Console.Write($" {position}");
                        }
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                lastReport?.Append([]);
            }

            int axisCount = (int)reading.GetControllerAxisCount();
            if (axisCount > 0)
            {
                using var _buffer = new StackOrPoolArray<float>(
                    buttonCount,
                    stackalloc float[16]
                );
                var buffer = _buffer.Array;

                unsafe
                {
                    fixed (float* ptr = buffer)
                    {
                        int readSize = (int)reading.GetControllerAxisState((uint)buffer.Length, ptr);
                        buffer = buffer.Slice(0, Math.Min(readSize, buffer.Length));
                    }
                }

                if (lastReport == null || lastReport.Append(buffer))
                {
                    WriteTimestamp(reading.GetTimestamp());
                    Console.Write($": axes");

                    if (buffer.Length < 1)
                    {
                        Console.Write(" <none>");
                    }
                    else
                    {
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            float value = buffer[i];
                            Console.Write($" {value:F3}");
                        }
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                lastReport?.Append([]);
            }

            return true;
        }

        public static bool PrintKeyboardReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            int keyCount = (int)reading.GetKeyCount();
            if (keyCount < 1 && (reading.GetInputKind() & GameInputKind.Keyboard) == 0)
            {
                return false;
            }

            using var _keyBuffer = new StackOrPoolArray<GameInputKeyState>(keyCount, stackalloc GameInputKeyState[16]);
            var keyBuffer = _keyBuffer.Array;

            if (keyBuffer.Length > 0) unsafe
            {
                fixed (GameInputKeyState* ptr = keyBuffer)
                {
                    int readKeys = (int)reading.GetKeyState((uint)keyBuffer.Length, ptr);
                    keyBuffer = keyBuffer.Slice(0, Math.Min(readKeys, keyBuffer.Length));
                }
            }

            if (lastReport == null || lastReport.Write(keyBuffer))
            {
                WriteTimestamp(reading.GetTimestamp());
                if (keyBuffer.Length == 0)
                {
                    Console.Write(": <none>");
                }
                else
                {
                    Span<char> charBuffer = stackalloc char[4];

                    Console.Write(": ");
                    for (int i = 0; i < keyBuffer.Length; i++)
                    {
                        ref readonly var key = ref keyBuffer[i];
                        if (key.virtualKey == 0 && key.scanCode == 0)
                        {
                            break;
                        }

                        if (i != 0)
                        {
                            Console.Write(", ");
                        }

                        if (key.codePoint == 0 || !Rune.TryCreate(key.codePoint, out var rune))
                        {
                            Console.Write($"<{key.scanCode:X8}>");
                            continue;
                        }

                        int written = rune.EncodeToUtf16(charBuffer);
                        for (int c = 0; c < written; c++)
                        {
                            Console.Write(charBuffer[c]);
                        }
                    }
                }

                Console.WriteLine();
            }

            return true;
        }

        public static bool PrintMouseReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetMouseState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {state.buttons}");
                Console.Write($"  X {state.positionX} Y {state.positionY}");
#if HAS_ABS_MOUSE
                Console.Write($"  abs X {state.absolutePositionX} Y {state.absolutePositionY}");
#endif
                Console.Write($"  wheel X {state.wheelX} Y {state.wheelY}");
                Console.WriteLine();
            }

            return true;
        }

#if HAS_TOUCH_REPORTS
        public static bool PrintTouchReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            int touchCount = (int)reading.GetTouchCount();
            if (touchCount < 1 && (reading.GetInputKind() & GameInputKind.Touch) == 0)
            {
                return false;
            }

            using var _touchBuffer = new StackOrPoolArray<GameInputTouchState>(touchCount, stackalloc GameInputTouchState[8]);
            var touchBuffer = _touchBuffer.Array;

            if (touchBuffer.Length > 0) unsafe
            {
                fixed (GameInputTouchState* ptr = touchBuffer)
                {
                    int readKeys = (int)reading.GetTouchState((uint)touchBuffer.Length, ptr);
                    touchBuffer = touchBuffer.Slice(0, Math.Min(readKeys, touchBuffer.Length));
                }
            }

            if (lastReport == null || lastReport.Write(touchBuffer))
            {
                WriteTimestamp(reading.GetTimestamp());
                if (touchBuffer.Length == 0)
                {
                    Console.Write(": <none>");
                }
                else
                {
                    Span<char> charBuffer = stackalloc char[4];

                    Console.WriteLine(":");
                    for (int i = 0; i < touchBuffer.Length; i++)
                    {
                        ref readonly var touch = ref touchBuffer[i];
                        Console.Write($"  ID {touch.touchId} sensor {touch.sensorIndex}");
                        Console.Write($"  position X {touch.positionX:F3} Y {touch.positionY:F3}");
                        Console.Write($"  pressure {touch.positionX:F3}");
                        Console.Write($"  proximity {touch.positionX:F3}");
                        Console.Write($"  contact {touch.positionX:F3}");
                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
            }

            return true;
        }
#endif

#if HAS_MOTION_REPORTS
        public static bool PrintMotionReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetMotionState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.WriteLine(
                    "\n" +
                    $"  acceleration X {state.accelerationX:F3,-7} Y {state.accelerationY:F3,-7} Z {state.accelerationZ:F3,-7}\n" +
                    $"  rotation     X {state.angularVelocityX:F3,-7} Y {state.angularVelocityY:F3,-7} Z {state.angularVelocityZ:F3,-7}\n" +
                    $"  compass      X {state.magneticFieldX:F3,-7} Y {state.magneticFieldY:F3,-7} Z {state.magneticFieldZ:F3,-7}\n" +
                    $"  orientation  W {state.orientationW:F3,-7} X {state.orientationX:F3,-7} Y {state.orientationY:F3,-7} Z {state.orientationZ:F3,-7}"
                );
            }

            return true;
        }
#endif

#if HAS_SENSOR_REPORTS
        public static bool PrintSensorsReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetSensorsState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.WriteLine(
                    "\n" +
                    $"  acceleration X {state.accelerationInGX:F3,-7} Y {state.accelerationInGY:F3,-7} Z {state.accelerationInGZ:F3,-7}\n" +
                    $"  rotation     X {state.angularVelocityInRadPerSecX:F3,-7} Y {state.angularVelocityInRadPerSecY:F3,-7} Z {state.angularVelocityInRadPerSecZ:F3,-7}\n" +
                    $"  compass      ° {state.headingInDegreesFromMagneticNorth:F3}\n" +
                    $"  orientation  W {state.orientationW:F3,-7} X {state.orientationX:F3,-7} Y {state.orientationY:F3,-7} Z {state.orientationZ:F3,-7}"
                );
            }

            return true;
        }
#endif

        public static bool PrintArcadeStickReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetArcadeStickState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {state.buttons}");
                Console.WriteLine();
            }

            return true;
        }

        public static bool PrintFlightStickReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetFlightStickState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {state.buttons}");
                Console.Write($"  hat {state.hatSwitch}");
                Console.Write($"  roll {state.roll:F3}");
                Console.Write($"  pitch {state.pitch:F3}");
                Console.Write($"  yaw {state.yaw:F3}");
                Console.Write($"  throttle {state.throttle:F3}");
                Console.WriteLine();
            }

            return true;
        }

        public static bool PrintGamepadReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetGamepadState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {state.buttons}");
                Console.Write($"  LT {state.leftTrigger:F3} RT {state.rightTrigger:F3}");
                Console.Write($"  LX {state.leftThumbstickX:F3} LY {state.leftThumbstickY:F3}");
                Console.Write($"  RX {state.rightThumbstickX:F3} RY {state.rightThumbstickY:F3}");
                Console.WriteLine();
            }

            return true;
        }

        public static bool PrintRacingWheelReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetRacingWheelState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {state.buttons}");
                Console.Write($"  gear {state.patternShifterGear}");
                Console.Write($"  wheel {state.wheel:F3}");
                Console.Write($"  throttle {state.throttle:F3}");
                Console.Write($"  brake {state.brake:F3}");
                Console.Write($"  clutch {state.clutch:F3}");
                Console.Write($"  handbrake {state.handbrake:F3}");
                Console.WriteLine();
            }

            return true;
        }

        public static bool PrintUiNavigationReading(LightIGameInputReading reading, StateBuffer? lastReport)
        {
            if (!reading.GetUiNavigationState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {state.buttons}");
                Console.WriteLine();
            }

            return true;
        }
    }
}