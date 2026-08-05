using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using SharpGameInput.Common;

namespace SharpGameInput.v0
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
        public int64_t positionX;
        public int64_t positionY;
        public int64_t wheelX;
        public int64_t wheelY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputTouchState
    {
        public uint64_t touchId;
        public uint32_t sensorIndex;
        public float positionX;
        public float positionY;
        public float pressure;
        public float proximity;
        public float contactRectTop;
        public float contactRectLeft;
        public float contactRectRight;
        public float contactRectBottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMotionState
    {
        public float accelerationX;
        public float accelerationY;
        public float accelerationZ;
        public float angularVelocityX;
        public float angularVelocityY;
        public float angularVelocityZ;
        public float magneticFieldX;
        public float magneticFieldY;
        public float magneticFieldZ;
        public float orientationW;
        public float orientationX;
        public float orientationY;
        public float orientationZ;
        public GameInputMotionAccuracy accelerometerAccuracy;
        public GameInputMotionAccuracy gyroscopeAccuracy;
        public GameInputMotionAccuracy magnetometerAccuracy;
        public GameInputMotionAccuracy orientationAccuracy;
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
    public struct GameInputBatteryState
    {
        public float chargeRate;
        public float maxChargeRate;
        public float remainingCapacity;
        public float fullChargeCapacity;
        public GameInputBatteryStatus status;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputString
    {
        public uint32_t sizeInBytes;
        public uint32_t codePointCount;
        public char_t* data;

        public readonly override string ToString()
        {
            return Encoding.UTF8.GetString(data, (int)sizeInBytes);
        }

        public static string? ToString(GameInputString* str)
        {
            if (str == null)
            {
                return null;
            }

            return str->ToString();
        }
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
    public unsafe struct GameInputRawDeviceItemCollectionInfo
    {
        public GameInputRawDeviceItemCollectionKind kind;
        public uint32_t childCount;
        public uint32_t siblingCount;
        public uint32_t usageCount;
        public GameInputUsage* usages;
        public GameInputRawDeviceItemCollectionInfo* parent;
        public GameInputRawDeviceItemCollectionInfo* firstSibling;
        public GameInputRawDeviceItemCollectionInfo* previousSibling;
        public GameInputRawDeviceItemCollectionInfo* nextSibling;
        public GameInputRawDeviceItemCollectionInfo* lastSibling;
        public GameInputRawDeviceItemCollectionInfo* firstChild;
        public GameInputRawDeviceItemCollectionInfo* lastChild;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputRawDeviceReportItemInfo
    {
        public uint32_t bitOffset;
        public uint32_t bitSize;
        public int64_t logicalMin;
        public int64_t logicalMax;
        public double physicalMin;
        public double physicalMax;
        public GameInputRawDevicePhysicalUnitKind physicalUnits;
        public uint32_t rawPhysicalUnits;
        public int32_t rawPhysicalUnitsExponent;
        public GameInputRawDeviceReportItemFlags flags;
        public uint32_t usageCount;
        public GameInputUsage* usages;
        public GameInputRawDeviceItemCollectionInfo* collection;
        public GameInputString* itemString;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputRawDeviceReportInfo
    {
        public GameInputRawDeviceReportKind kind;
        public uint32_t id;
        public uint32_t size;
        public uint32_t itemCount;
        public GameInputRawDeviceReportItemInfo* items;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerAxisInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        public bool_t isContinuous;
        public bool_t isNonlinear;
        public bool_t isQuantized;
        public bool_t hasRestValue;
        public float restValue;
        public uint64_t resolution;
        public uint16_t legacyDInputIndex;
        public uint16_t legacyHidIndex;
        public uint32_t rawReportIndex;
        public GameInputRawDeviceReportInfo* inputReport;
        public GameInputRawDeviceReportItemInfo* inputReportItem;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerButtonInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        public uint16_t legacyDInputIndex;
        public uint16_t legacyHidIndex;
        public uint32_t rawReportIndex;
        public GameInputRawDeviceReportInfo* inputReport;
        public GameInputRawDeviceReportItemInfo* inputReportItem;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputControllerSwitchInfo
    {
        public const int PositionLabelCount = 9;

        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        // public fixed GameInputLabel positionLabels[PositionLabelCount];
        internal fixed int _positionLabels[PositionLabelCount];
        public GameInputSwitchKind kind;
        public uint16_t legacyDInputIndex;
        public uint16_t legacyHidIndex;
        public uint32_t rawReportIndex;
        public GameInputRawDeviceReportInfo* inputReport;
        public GameInputRawDeviceReportItemInfo* inputReportItem;

        public readonly GameInputLabel GetPositionLabel(int index)
        {
            ThrowHelper.CheckRange(index, PositionLabelCount);
            return (GameInputLabel)_positionLabels[index];
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputKeyboardInfo
    {
        public GameInputKeyboardKind kind;
        public uint32_t layout;
        public uint32_t keyCount;
        public uint32_t functionKeyCount;
        public uint32_t maxSimultaneousKeys;
        public uint32_t platformType;
        public uint32_t platformSubtype;
        public GameInputString* nativeLanguage;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMouseInfo
    {
        public GameInputMouseButtons supportedButtons;
        public uint32_t sampleRate;
        public uint32_t sensorDpi;
        public bool_t hasWheelX;
        public bool_t hasWheelY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputTouchSensorInfo
    {
        public GameInputKind mappedInputKinds;
        public GameInputLabel label;
        public GameInputLocation location;
        public uint32_t locationId;
        public uint64_t resolutionX;
        public uint64_t resolutionY;
        public GameInputTouchShape shape;
        public float aspectRatio;
        public float orientation;
        public float physicalWidth;
        public float physicalHeight;
        public float maxPressure;
        public float maxProximity;
        public uint32_t maxTouchPoints;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GameInputMotionInfo
    {
        public float maxAcceleration;
        public float maxAngularVelocity;
        public float maxMagneticFieldStrength;
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
        public GameInputLocation location;
        public uint32_t locationId;
        public uint32_t maxSimultaneousEffects;
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
    public struct GameInputHapticWaveformInfo
    {
        public GameInputUsage usage;
        public bool_t isDurationSupported;
        public bool_t isIntensitySupported;
        public bool_t isRepeatSupported;
        public bool_t isRepeatDelaySupported;
        public uint64_t defaultDuration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputHapticFeedbackMotorInfo
    {
        public GameInputRumbleMotors mappedRumbleMotors;
        public GameInputLocation location;
        public uint32_t locationId;
        public uint32_t waveformCount;
        public GameInputHapticWaveformInfo* waveformInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GameInputDeviceInfo
    {
        public uint32_t infoSize;
        public uint16_t vendorId;
        public uint16_t productId;
        public uint16_t revisionNumber;
        public uint8_t interfaceNumber;
        public uint8_t collectionNumber;
        public GameInputUsage usage;
        public GameInputVersion hardwareVersion;
        public GameInputVersion firmwareVersion;
        public APP_LOCAL_DEVICE_ID deviceId;
        public APP_LOCAL_DEVICE_ID deviceRootId;
        public GameInputDeviceFamily deviceFamily;
        public GameInputDeviceCapabilities capabilities;
        public GameInputKind supportedInput;
        public GameInputRumbleMotors supportedRumbleMotors;
        public uint32_t inputReportCount;
        public uint32_t outputReportCount;
        public uint32_t featureReportCount;
        public uint32_t controllerAxisCount;
        public uint32_t controllerButtonCount;
        public uint32_t controllerSwitchCount;
        public uint32_t touchPointCount;
        public uint32_t touchSensorCount;
        public uint32_t forceFeedbackMotorCount;
        public uint32_t hapticFeedbackMotorCount;
        public uint32_t deviceStringCount;
        public uint32_t deviceDescriptorSize;
        public GameInputRawDeviceReportInfo* inputReportInfo;
        public GameInputRawDeviceReportInfo* outputReportInfo;
        public GameInputRawDeviceReportInfo* featureReportInfo;
        internal GameInputControllerAxisInfo* _controllerAxisInfo;
        internal GameInputControllerButtonInfo* _controllerButtonInfo;
        internal GameInputControllerSwitchInfo* _controllerSwitchInfo;
        public GameInputKeyboardInfo* keyboardInfo;
        public GameInputMouseInfo* mouseInfo;
        public GameInputTouchSensorInfo* touchSensorInfo;
        public GameInputMotionInfo* motionInfo;
        public GameInputArcadeStickInfo* arcadeStickInfo;
        public GameInputFlightStickInfo* flightStickInfo;
        public GameInputGamepadInfo* gamepadInfo;
        public GameInputRacingWheelInfo* racingWheelInfo;
        public GameInputUiNavigationInfo* uiNavigationInfo;
        internal GameInputForceFeedbackMotorInfo* _forceFeedbackMotorInfo;
        internal GameInputHapticFeedbackMotorInfo* _hapticFeedbackMotorInfo;
        public GameInputString* displayName;
        public GameInputString* deviceStrings;
        public void* deviceDescriptorData;

        public readonly GameInputControllerAxisInfo* GetControllerAxisInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerAxisCount);
            return _controllerAxisInfo + index;
        }

        public readonly GameInputControllerButtonInfo* GetControllerButtonInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerButtonCount);
            return _controllerButtonInfo + index;
        }

        public readonly GameInputControllerSwitchInfo* GetControllerSwitchInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)controllerSwitchCount);
            return _controllerSwitchInfo + index;
        }

        public readonly GameInputForceFeedbackMotorInfo* GetForceFeedbackMotorInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)forceFeedbackMotorCount);
            return _forceFeedbackMotorInfo + index;
        }

        public readonly GameInputHapticFeedbackMotorInfo* GetHapticFeedbackMotorInfo(int index)
        {
            ThrowHelper.CheckRange(index, (int)hapticFeedbackMotorCount);
            return _hapticFeedbackMotorInfo + index;
        }

        public readonly string? GetDisplayName()
        {
            return GameInputString.ToString(displayName);
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
    public struct GameInputHapticFeedbackParams
    {
        public uint32_t waveformIndex;
        public uint64_t duration;
        public float intensity;
        public uint32_t playCount;
        public uint64_t repeatDelay;
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
