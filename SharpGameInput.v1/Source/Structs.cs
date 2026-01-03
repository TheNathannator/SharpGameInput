using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using SharpGameInput.Common;

namespace SharpGameInput.v1
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
    public struct GameInputUiNavigationState
    {
        public GameInputUiNavigationButtons buttons;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputUsage
    {
        public uint16_t page;
        public uint16_t id;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputControllerAxisInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputControllerButtonInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputControllerSwitchInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        public GameInputSwitchKind kind;
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
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputFlightStickInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel firePrimaryButtonLabel;
        public GameInputLabel fireSecondaryButtonLabel;
        public GameInputSwitchKind hatSwitchKind;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputGamepadInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel aButtonLabel;
        public GameInputLabel bButtonLabel;
        public GameInputLabel xButtonLabel;
        public GameInputLabel yButtonLabel;
        public GameInputLabel dpadUpLabel;
        public GameInputLabel dpadDownLabel;
        public GameInputLabel dpadLeftLabel;
        public GameInputLabel dpadRightLabel;
        public GameInputLabel leftShoulderButtonLabel;
        public GameInputLabel rightShoulderButtonLabel;
        public GameInputLabel leftThumbstickButtonLabel;
        public GameInputLabel rightThumbstickButtonLabel;
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
        public bool_t hasClutch;
        public bool_t hasHandbrake;
        public bool_t hasPatternShifter;
        public int32_t minPatternShifterGear;
        public int32_t maxPatternShifterGear;
        public float maxWheelAngle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputUiNavigationInfo
    {
        public GameInputLabel menuButtonLabel;
        public GameInputLabel viewButtonLabel;
        public GameInputLabel acceptButtonLabel;
        public GameInputLabel cancelButtonLabel;
        public GameInputLabel upButtonLabel;
        public GameInputLabel downButtonLabel;
        public GameInputLabel leftButtonLabel;
        public GameInputLabel rightButtonLabel;
        public GameInputLabel contextButton1Label;
        public GameInputLabel contextButton2Label;
        public GameInputLabel contextButton3Label;
        public GameInputLabel contextButton4Label;
        public GameInputLabel pageUpButtonLabel;
        public GameInputLabel pageDownButtonLabel;
        public GameInputLabel pageLeftButtonLabel;
        public GameInputLabel pageRightButtonLabel;
        public GameInputLabel scrollUpButtonLabel;
        public GameInputLabel scrollDownButtonLabel;
        public GameInputLabel scrollLeftButtonLabel;
        public GameInputLabel scrollRightButtonLabel;
        public GameInputLabel guideButtonLabel;
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
    public unsafe struct GameInputDeviceInfo
    {
        public uint16_t vendorId;
        public uint16_t productId;
        public GameInputUsage usage;
        public APP_LOCAL_DEVICE_ID deviceId;
        public APP_LOCAL_DEVICE_ID deviceRootId;
        public GameInputDeviceFamily deviceFamily;
        public GameInputKind supportedInput;
        public GameInputRumbleMotors supportedRumbleMotors;
        public GameInputSystemButtons supportedSystemButtons;
        public char_t* displayName;
        public char_t* pnpPath;

        public GameInputKeyboardInfo* keyboardInfo;
        public GameInputMouseInfo* mouseInfo;
        public GameInputArcadeStickInfo* arcadeStickInfo;
        public GameInputFlightStickInfo* flightStickInfo;
        public GameInputGamepadInfo* gamepadInfo;
        public GameInputRacingWheelInfo* racingWheelInfo;
        public GameInputUiNavigationInfo* uiNavigationInfo;

        public uint32_t controllerAxisCount;
        internal GameInputControllerAxisInfo* _controllerAxisInfo;

        public uint32_t controllerButtonCount;
        internal GameInputControllerButtonInfo* _controllerButtonInfo;

        public uint32_t controllerSwitchCount;
        internal GameInputControllerSwitchInfo* _controllerSwitchInfo;

        public uint32_t forceFeedbackMotorCount;
        internal GameInputForceFeedbackMotorInfo* _forceFeedbackMotorInfo;

        public GameInputControllerAxisInfo* GetControllerAxisInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerAxisCount);
            return _controllerAxisInfo + index;
        }

        public GameInputControllerButtonInfo* GetControllerButtonInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerButtonCount);
            return _controllerButtonInfo + index;
        }

        public GameInputControllerSwitchInfo* GetControllerSwitchInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerSwitchCount);
            return _controllerSwitchInfo + index;
        }

        public GameInputForceFeedbackMotorInfo* GetForceFeedbackMotorInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)forceFeedbackMotorCount);
            return _forceFeedbackMotorInfo + index;
        }

        public readonly string? GetDisplayName()
        {
            if (displayName == null)
            {
                return null;
            }

            int length = Utility.StringLength(displayName);
            return Encoding.UTF8.GetString(displayName, length);
        }

        public readonly string? GetPnpPath()
        {
            if (pnpPath == null)
            {
                return null;
            }

            int length = Utility.StringLength(pnpPath);
            return Encoding.UTF8.GetString(pnpPath, length);
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
}
