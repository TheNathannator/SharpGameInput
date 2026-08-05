using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using SharpGameInput.Common;

namespace SharpGameInput.v3
{
    using bool_t = ByteBool;

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputKeyState
    {
        public uint32_t scanCode;
        public uint32_t codePoint;
        public uint8_t virtualKey;
        public bool_t isDeadKey;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMouseState
    {
        public GameInputMouseButtons buttons;
        public GameInputMousePositions positions;
        public int64_t positionX;
        public int64_t positionY;
        public int64_t absolutePositionX;
        public int64_t absolutePositionY;
        public int64_t wheelX;
        public int64_t wheelY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputVersion
    {
        public uint16_t major;
        public uint16_t minor;
        public uint16_t build;
        public uint16_t revision;

        public readonly override string ToString()
        {
            return $"{major}.{minor}.{build}.{revision}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputSensorsState
    {
        public float accelerationInGX;
        public float accelerationInGY;
        public float accelerationInGZ;

        public float angularVelocityInRadPerSecX;
        public float angularVelocityInRadPerSecY;
        public float angularVelocityInRadPerSecZ;

        public float headingInDegreesFromMagneticNorth;
        public GameInputSensorAccuracy headingAccuracy;

        public float orientationW;
        public float orientationX;
        public float orientationY;
        public float orientationZ;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputArcadeStickState
    {
        public GameInputArcadeStickButtons buttons;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputFlightStickState
    {
        public GameInputFlightStickButtons buttons;
        public GameInputSwitchPosition hatSwitch;
        public float roll;
        public float pitch;
        public float yaw;
        public float throttle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputGamepadState
    {
        public GameInputGamepadButtons buttons;
        public float leftTrigger;
        public float rightTrigger;
        public float leftThumbstickX;
        public float leftThumbstickY;
        public float rightThumbstickX;
        public float rightThumbstickY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRacingWheelState
    {
        public GameInputRacingWheelButtons buttons;
        public int32_t patternShifterGear;
        public float wheel;
        public float throttle;
        public float brake;
        public float clutch;
        public float handbrake;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputUsage
    {
        public uint16_t page;
        public uint16_t id;

        public readonly override string ToString()
        {
            return $"{page:X4}:{id:X4}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerSwitchInfo
    {
        public const int MaxSwitchStates = 8;

        // Can't use non-primitive types for fixed buffers
        internal fixed int _labels[8];
        public GameInputSwitchKind kind;

        public GameInputLabel GetLabel(int index)
        {
            ThrowHelper.CheckRange(index, MaxSwitchStates);
            fixed (int* ptr = _labels)
            {
                return (GameInputLabel)ptr[index];
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerInfo
    {
        public uint32_t controllerAxisCount;
        internal GameInputLabel* _controllerAxisLabels;

        public uint32_t controllerButtonCount;
        internal GameInputLabel* _controllerButtonLabels;

        public uint32_t controllerSwitchCount;
        internal GameInputControllerSwitchInfo* _controllerSwitchInfo;

        public readonly GameInputLabel GetControllerAxisLabel(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerAxisCount);
            return _controllerAxisLabels[index];
        }

        public readonly GameInputLabel GetControllerButtonLabel(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerButtonCount);
            return _controllerButtonLabels[index];
        }

        public readonly GameInputControllerSwitchInfo* GetControllerSwitchInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerSwitchCount);
            return _controllerSwitchInfo + index;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputKeyboardInfo
    {
        public GameInputKeyboardKind kind;
        public uint32_t layout;
        public uint32_t keyCount;
        public uint32_t functionKeyCount;
        public uint32_t maxSimultaneousKeys;
        public uint32_t platformType;
        public uint32_t platformSubtype;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMouseInfo
    {
        public GameInputMouseButtons supportedButtons;
        public uint32_t sampleRate;
        public bool_t hasWheelX;
        public bool_t hasWheelY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputSensorsInfo
    {
        public GameInputSensorsKind supportedSensors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputArcadeStickInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel stickUpLabel;
        public GameInputLabel stickDownLabel;
        public GameInputLabel stickLeftLabel;
        public GameInputLabel stickRightLabel;
        public GameInputLabel actionButton1Label;
        public GameInputLabel actionButton2Label;
        public GameInputLabel actionButton3Label;
        public GameInputLabel actionButton4Label;
        public GameInputLabel actionButton5Label;
        public GameInputLabel actionButton6Label;
        public GameInputLabel specialButton1Label;
        public GameInputLabel specialButton2Label;
        public uint32_t       extraButtonCount;
        public uint32_t       extraAxisCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputFlightStickInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel firePrimaryButtonLabel;
        public GameInputLabel fireSecondaryButtonLabel;
        public GameInputLabel hatSwitchUpLabel;
        public GameInputLabel hatSwitchDownLabel;
        public GameInputLabel hatSwitchLeftLabel;
        public GameInputLabel hatSwitchRightLabel;
        public GameInputLabel aButtonLabel;
        public GameInputLabel bButtonLabel;
        public GameInputLabel xButtonLabel;
        public GameInputLabel yButtonLabel;
        public GameInputLabel leftShoulderButtonLabel;
        public GameInputLabel rightShoulderButtonLabel;
        public uint32_t       extraButtonCount;
        public uint32_t       extraAxisCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputGamepadInfo
    {
        public GameInputGamepadButtons supportedLayout;
        public GameInputLabel          menuButtonLabel;
        public GameInputLabel          viewButtonLabel;
        public GameInputLabel          aButtonLabel;
        public GameInputLabel          bButtonLabel;
        public GameInputLabel          cButtonLabel;
        public GameInputLabel          xButtonLabel;
        public GameInputLabel          yButtonLabel;
        public GameInputLabel          zButtonLabel;
        public GameInputLabel          dpadUpLabel;
        public GameInputLabel          dpadDownLabel;
        public GameInputLabel          dpadLeftLabel;
        public GameInputLabel          dpadRightLabel;
        public GameInputLabel          leftShoulderButtonLabel;
        public GameInputLabel          rightShoulderButtonLabel;
        public GameInputLabel          leftThumbstickButtonLabel;
        public GameInputLabel          rightThumbstickButtonLabel;
        public uint32_t                extraButtonCount;
        public uint32_t                extraAxisCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRacingWheelInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel previousGearButtonLabel;
        public GameInputLabel nextGearButtonLabel;
        public GameInputLabel dpadUpLabel;
        public GameInputLabel dpadDownLabel;
        public GameInputLabel dpadLeftLabel;
        public GameInputLabel dpadRightLabel;
        public GameInputLabel aButtonLabel;
        public GameInputLabel bButtonLabel;
        public GameInputLabel xButtonLabel;
        public GameInputLabel yButtonLabel;
        public GameInputLabel leftThumbstickButtonLabel;
        public GameInputLabel rightThumbstickButtonLabel;
        public bool_t         hasClutch;
        public bool_t         hasHandbrake;
        public bool_t         hasPatternShifter;
        public int32_t        minPatternShifterGear;
        public int32_t        maxPatternShifterGear;
        public float          maxWheelAngle;
        public uint32_t       extraButtonCount;
        public uint32_t       extraAxisCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackMotorInfo
    {
        public GameInputFeedbackAxes supportedAxes;
        public bool_t isConstantEffectSupported;
        public bool_t isRampEffectSupported;
        public bool_t isSineWaveEffectSupported;
        public bool_t isSquareWaveEffectSupported;
        public bool_t isTriangleWaveEffectSupported;
        public bool_t isSawtoothUpWaveEffectSupported;
        public bool_t isSawtoothDownWaveEffectSupported;
        public bool_t isSpringEffectSupported;
        public bool_t isFrictionEffectSupported;
        public bool_t isDamperEffectSupported;
        public bool_t isInertiaEffectSupported;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRawDeviceReportInfo
    {
        public GameInputRawDeviceReportKind kind;
        public uint32_t id;
        public uint32_t size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputDeviceInfo
    {
        public uint16_t               vendorId;
        public uint16_t               productId;
        public uint16_t               revisionNumber;
        public GameInputUsage         usage;
        public GameInputVersion       hardwareVersion;
        public GameInputVersion       firmwareVersion;
        public APP_LOCAL_DEVICE_ID    deviceId;
        public APP_LOCAL_DEVICE_ID    deviceRootId;
        public GameInputDeviceFamily  deviceFamily;
        public GameInputKind          supportedInput;
        public GameInputRumbleMotors  supportedRumbleMotors;
        public GameInputSystemButtons supportedSystemButtons;
        public Guid                   containerId;
        public char_t*                displayName;
        public char_t*                pnpPath;

        public GameInputKeyboardInfo*    keyboardInfo;
        public GameInputMouseInfo*       mouseInfo;
        public GameInputSensorsInfo*     sensorsInfo;
        public GameInputControllerInfo*  controllerInfo;
        public GameInputArcadeStickInfo* arcadeStickInfo;
        public GameInputFlightStickInfo* flightStickInfo;
        public GameInputGamepadInfo*     gamepadInfo;
        public GameInputRacingWheelInfo* racingWheelInfo;

        public uint32_t forceFeedbackMotorCount;
        internal GameInputForceFeedbackMotorInfo* _forceFeedbackMotorInfo;

        public uint32_t inputReportCount;
        internal GameInputRawDeviceReportInfo* _inputReportInfo;

        public uint32_t outputReportCount;
        internal GameInputRawDeviceReportInfo* _outputReportInfo;

        public readonly GameInputForceFeedbackMotorInfo* GetForceFeedbackMotorInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)forceFeedbackMotorCount);
            return _forceFeedbackMotorInfo + index;
        }

        public readonly GameInputRawDeviceReportInfo* GetInputReportInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)inputReportCount);
            return _inputReportInfo + index;
        }

        public readonly GameInputRawDeviceReportInfo* GetOutputReportInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)outputReportCount);
            return _outputReportInfo + index;
        }

        public readonly string? GetDisplayName()
        {
            return StringHelper.FromUtf8(displayName);
        }

        public readonly string? GetPnpPath()
        {
            return StringHelper.FromUtf8(pnpPath);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputHapticInfo
    {
        public const uint32_t MaxLocations           = 8;
        public const uint32_t MaxAudioEndpointIdSize = 256;

        public static Guid LocationNone => Guid.Empty;

        public static readonly Guid LocationGripLeft     = new(0x08c707c2, 0x66bb, 0x406c, 0xa8, 0x4a, 0xdf, 0xe0, 0x85, 0x12, 0x0a, 0x92);
        public static readonly Guid LocationGripRight    = new(0x155a0b77, 0x8bb2, 0x40db, 0x86, 0x90, 0xb6, 0xd4, 0x11, 0x26, 0xdf, 0xc1);
        public static readonly Guid LocationTriggerLeft  = new(0x8de4d896, 0x5559, 0x4081, 0x86, 0xe5, 0x17, 0x24, 0xcc, 0x07, 0xc6, 0xbc);
        public static readonly Guid LocationTriggerRight = new(0xff0cb557, 0x3af5, 0x406b, 0x8b, 0x0f, 0x55, 0x5a, 0x2d, 0x92, 0xa2, 0x20);

        public fixed wchar_t audioEndpointId[(int)MaxAudioEndpointIdSize];
        public uint32_t locationCount;
        // Can't use non-primitive types for fixed buffers
        internal fixed byte _locations[(int)MaxLocations * 16 /*sizeof(Guid)*/];

        public Guid GetLocation(int index)
        {
            ThrowHelper.CheckRange(index, (int)locationCount);
            fixed (byte* ptr = _locations)
            {
                return ((Guid*)ptr)[index];
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackEnvelope
    {
        public uint64_t attackDuration;
        public uint64_t sustainDuration;
        public uint64_t releaseDuration;
        public float attackGain;
        public float sustainGain;
        public float releaseGain;
        public uint32_t playCount;
        public uint64_t repeatDelay;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackMagnitude
    {
        public float linearX;
        public float linearY;
        public float linearZ;
        public float angularX;
        public float angularY;
        public float angularZ;
        public float normal;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackConditionParams
    {
        public GameInputForceFeedbackMagnitude magnitude;
        public float positiveCoefficient;
        public float negativeCoefficient;
        public float maxPositiveMagnitude;
        public float maxNegativeMagnitude;
        public float deadZone;
        public float bias;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackConstantParams
    {
        public GameInputForceFeedbackEnvelope envelope;
        public GameInputForceFeedbackMagnitude magnitude;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackPeriodicParams
    {
        public GameInputForceFeedbackEnvelope envelope;
        public GameInputForceFeedbackMagnitude magnitude;
        public float frequency;
        public float phase;
        public float bias;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputForceFeedbackRampParams
    {
        public GameInputForceFeedbackEnvelope envelope;
        public GameInputForceFeedbackMagnitude startMagnitude;
        public GameInputForceFeedbackMagnitude endMagnitude;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GameInputForceFeedbackParams
    {
        [FieldOffset(0)]
        public GameInputForceFeedbackEffectKind kind;

        [FieldOffset(4)]
        internal GameInputForceFeedbackConstantParams _constant;

        [FieldOffset(4)]
        internal GameInputForceFeedbackRampParams _ramp;

        [FieldOffset(4)]
        internal GameInputForceFeedbackPeriodicParams _sineWave;

        [FieldOffset(4)]
        internal GameInputForceFeedbackPeriodicParams _squareWave;

        [FieldOffset(4)]
        internal GameInputForceFeedbackPeriodicParams _triangleWave;

        [FieldOffset(4)]
        internal GameInputForceFeedbackPeriodicParams _sawtoothUpWave;

        [FieldOffset(4)]
        internal GameInputForceFeedbackPeriodicParams _sawtoothDownWave;

        [FieldOffset(4)]
        internal GameInputForceFeedbackConditionParams _spring;

        [FieldOffset(4)]
        internal GameInputForceFeedbackConditionParams _friction;

        [FieldOffset(4)]
        internal GameInputForceFeedbackConditionParams _damper;

        [FieldOffset(4)]
        internal GameInputForceFeedbackConditionParams _inertia;

        private void CheckType(GameInputForceFeedbackEffectKind type)
        {
            if (kind != type)
                throw new InvalidOperationException("Attempted to access an incorrect force feedback type.");
        }

        private ref T CheckType<T>(GameInputForceFeedbackEffectKind type, ref T value)
            where T : unmanaged
        {
            CheckType(type);
            return ref value;
        }

        [UnscopedRef]
        public ref GameInputForceFeedbackConstantParams constant
            => ref CheckType(GameInputForceFeedbackEffectKind.Constant, ref _constant);

        [UnscopedRef]
        public ref GameInputForceFeedbackRampParams ramp
            => ref CheckType(GameInputForceFeedbackEffectKind.Ramp, ref _ramp);

        [UnscopedRef]
        public ref GameInputForceFeedbackPeriodicParams sineWave
            => ref CheckType(GameInputForceFeedbackEffectKind.SineWave, ref _sineWave);

        [UnscopedRef]
        public ref GameInputForceFeedbackPeriodicParams squareWave
            => ref CheckType(GameInputForceFeedbackEffectKind.SquareWave, ref _squareWave);

        [UnscopedRef]
        public ref GameInputForceFeedbackPeriodicParams triangleWave
            => ref CheckType(GameInputForceFeedbackEffectKind.TriangleWave, ref _triangleWave);

        [UnscopedRef]
        public ref GameInputForceFeedbackPeriodicParams sawtoothUpWave
            => ref CheckType(GameInputForceFeedbackEffectKind.SawtoothUpWave, ref _sawtoothUpWave);

        [UnscopedRef]
        public ref GameInputForceFeedbackPeriodicParams sawtoothDownWave
            => ref CheckType(GameInputForceFeedbackEffectKind.SawtoothDownWave, ref _sawtoothDownWave);

        [UnscopedRef]
        public ref GameInputForceFeedbackConditionParams spring
            => ref CheckType(GameInputForceFeedbackEffectKind.Spring, ref _spring);

        [UnscopedRef]
        public ref GameInputForceFeedbackConditionParams friction
            => ref CheckType(GameInputForceFeedbackEffectKind.Friction, ref _friction);

        [UnscopedRef]
        public ref GameInputForceFeedbackConditionParams damper
            => ref CheckType(GameInputForceFeedbackEffectKind.Damper, ref _damper);

        [UnscopedRef]
        public ref GameInputForceFeedbackConditionParams inertia
            => ref CheckType(GameInputForceFeedbackEffectKind.Inertia, ref _inertia);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputRumbleParams
    {
        public float lowFrequency;
        public float highFrequency;
        public float leftTrigger;
        public float rightTrigger;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputAxisMapping
    {
        public GameInputElementKind controllerElementKind;
        public uint32_t controllerIndex;

        public bool_t isInverted;

        public bool_t fromTwoButtons;
        public uint32_t buttonMinIndexValue;

        public GameInputSwitchPosition referenceDirection;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputButtonMapping
    {
        public GameInputElementKind controllerElementKind;
        public uint32_t controllerIndex;

        public bool_t isInverted;

        public GameInputSwitchPosition switchPosition;
    }
}
