using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpGameInput.v0.Tests;

public class SizeChecks
{
    private static unsafe void AssertSize<T>(int expected, bool checkMarshal = true)
        where T : unmanaged
    {
        Assert.Multiple(() =>
        {
            Assert.That(sizeof(T), Is.EqualTo(expected), $"{typeof(T).Name} is the wrong size with sizeof(T)");
            Assert.That(Unsafe.SizeOf<T>(), Is.EqualTo(expected), $"{typeof(T).Name} is the wrong size with Unsafe.SizeOf<T>()");
            if (checkMarshal)
                Assert.That(Marshal.SizeOf<T>(), Is.EqualTo(expected), $"{typeof(T).Name} is the wrong size with Marshal.SizeOf<T>()");
        });
    }

    private static unsafe void AssertSize<T>(ref T _, int expected, bool checkMarshal = true)
        where T : unmanaged
    {
        AssertSize<T>(expected, checkMarshal);
    }

    private static unsafe void AssertSizeEnum<T>(int expected)
        where T : unmanaged, System.Enum
    {
        AssertSize<T>(expected, checkMarshal: false);
    }

    private static unsafe void AssertOffset<T, TField>(ref T instance, ref TField field, string fieldName, nint expected, bool checkMarshal = true)
        where T : unmanaged
        where TField : unmanaged
    {
        Assert.That(OffsetOf(ref instance, ref field), Is.EqualTo(expected), $"{typeof(T).Name}.{fieldName} is the wrong offset with OffsetOf()");
        if (checkMarshal)
            Assert.That(Marshal.OffsetOf<T>(fieldName), Is.EqualTo(expected), $"{typeof(T).Name}.{fieldName} is the wrong offset with Marshal.OffsetOf<T>()");
    }

    private static unsafe void AssertOffset<T>(T* instance, void* field, string fieldName, nint expected, bool checkMarshal = true)
        where T : unmanaged
    {
        Assert.That(OffsetOf(instance, field), Is.EqualTo(expected), $"{typeof(T).Name}.{fieldName} is the wrong offset with OffsetOf()");
        if (checkMarshal)
            Assert.That(Marshal.OffsetOf<T>(fieldName), Is.EqualTo(expected), $"{typeof(T).Name}.{fieldName} is the wrong offset with Marshal.OffsetOf<T>()");
    }

    private static unsafe nint OffsetOf(void* instance, void* field)
    {
        return (nint)field - (nint)instance;
    }

    private static unsafe nint OffsetOf<T, TField>(ref T instance, ref TField field)
        where T : unmanaged
        where TField : unmanaged
    {
        return Unsafe.ByteOffset(ref instance, ref Unsafe.As<TField, T>(ref field));
    }

    [Test]
    public void Enum() => Assert.Multiple(() =>
    {
        AssertSizeEnum<GameInputKind>(4);
        AssertSizeEnum<GameInputEnumerationKind>(4);
        AssertSizeEnum<GameInputFocusPolicy>(4);
        AssertSizeEnum<GameInputSwitchKind>(4);
        AssertSizeEnum<GameInputSwitchPosition>(4);
        AssertSizeEnum<GameInputKeyboardKind>(4);
        AssertSizeEnum<GameInputMouseButtons>(4);
        AssertSizeEnum<GameInputTouchShape>(4);
        AssertSizeEnum<GameInputMotionAccuracy>(4);
        AssertSizeEnum<GameInputArcadeStickButtons>(4);
        AssertSizeEnum<GameInputFlightStickButtons>(4);
        AssertSizeEnum<GameInputGamepadButtons>(4);
        AssertSizeEnum<GameInputRacingWheelButtons>(4);
        AssertSizeEnum<GameInputUiNavigationButtons>(4);
        AssertSizeEnum<GameInputDeviceStatus>(4);
        AssertSizeEnum<GameInputBatteryStatus>(4);
        AssertSizeEnum<GameInputDeviceFamily>(4);
        AssertSizeEnum<GameInputDeviceCapabilities>(4);
        AssertSizeEnum<GameInputRawDeviceReportKind>(4);
        AssertSizeEnum<GameInputRawDeviceReportItemFlags>(4);
        AssertSizeEnum<GameInputRawDeviceItemCollectionKind>(4);
        AssertSizeEnum<GameInputRawDevicePhysicalUnitKind>(4);
        AssertSizeEnum<GameInputLabel>(4);
        AssertSizeEnum<GameInputLocation>(4);
        AssertSizeEnum<GameInputFeedbackAxes>(4);
        AssertSizeEnum<GameInputFeedbackEffectState>(4);
        AssertSizeEnum<GameInputForceFeedbackEffectKind>(4);
        AssertSizeEnum<GameInputRumbleMotors>(4);
    });

    [Test]
    public unsafe void Struct() => Assert.Multiple(() =>
    {
        {
            APP_LOCAL_DEVICE_ID instance = default;
            AssertSize(ref instance, 32);
            AssertOffset(&instance, instance.value, nameof(instance.value), 0);
        }

        {
            GameInputKeyState instance = default;
            AssertSize(ref instance, 12);
            AssertOffset(ref instance, ref instance.scanCode,   nameof(instance.scanCode),   0);
            AssertOffset(ref instance, ref instance.codePoint,  nameof(instance.codePoint),  4);
            AssertOffset(ref instance, ref instance.virtualKey, nameof(instance.virtualKey), 8);
            AssertOffset(ref instance, ref instance.isDeadKey,  nameof(instance.isDeadKey),  9);
        }

        {
            GameInputMouseState instance = default;
            AssertSize(ref instance, 40);
            AssertOffset(ref instance, ref instance.buttons,   nameof(instance.buttons),   0);
            AssertOffset(ref instance, ref instance.positionX, nameof(instance.positionX), 8);
            AssertOffset(ref instance, ref instance.positionY, nameof(instance.positionY), 16);
            AssertOffset(ref instance, ref instance.wheelX,    nameof(instance.wheelX),    24);
            AssertOffset(ref instance, ref instance.wheelY,    nameof(instance.wheelY),    32);
        }

        {
            GameInputTouchState instance = default;
            AssertSize(ref instance, 48);
            AssertOffset(ref instance, ref instance.touchId,           nameof(instance.touchId),           0);
            AssertOffset(ref instance, ref instance.sensorIndex,       nameof(instance.sensorIndex),       8);
            AssertOffset(ref instance, ref instance.positionX,         nameof(instance.positionX),         12);
            AssertOffset(ref instance, ref instance.positionY,         nameof(instance.positionY),         16);
            AssertOffset(ref instance, ref instance.pressure,          nameof(instance.pressure),          20);
            AssertOffset(ref instance, ref instance.proximity,         nameof(instance.proximity),         24);
            AssertOffset(ref instance, ref instance.contactRectTop,    nameof(instance.contactRectTop),    28);
            AssertOffset(ref instance, ref instance.contactRectLeft,   nameof(instance.contactRectLeft),   32);
            AssertOffset(ref instance, ref instance.contactRectRight,  nameof(instance.contactRectRight),  36);
            AssertOffset(ref instance, ref instance.contactRectBottom, nameof(instance.contactRectBottom), 40);
        }

        {
            GameInputMotionState instance = default;
            AssertSize(ref instance, 68);
            AssertOffset(ref instance, ref instance.accelerationX,         nameof(instance.accelerationX),         0);
            AssertOffset(ref instance, ref instance.accelerationY,         nameof(instance.accelerationY),         4);
            AssertOffset(ref instance, ref instance.accelerationZ,         nameof(instance.accelerationZ),         8);
            AssertOffset(ref instance, ref instance.angularVelocityX,      nameof(instance.angularVelocityX),      12);
            AssertOffset(ref instance, ref instance.angularVelocityY,      nameof(instance.angularVelocityY),      16);
            AssertOffset(ref instance, ref instance.angularVelocityZ,      nameof(instance.angularVelocityZ),      20);
            AssertOffset(ref instance, ref instance.magneticFieldX,        nameof(instance.magneticFieldX),        24);
            AssertOffset(ref instance, ref instance.magneticFieldY,        nameof(instance.magneticFieldY),        28);
            AssertOffset(ref instance, ref instance.magneticFieldZ,        nameof(instance.magneticFieldZ),        32);
            AssertOffset(ref instance, ref instance.orientationW,          nameof(instance.orientationW),          36);
            AssertOffset(ref instance, ref instance.orientationX,          nameof(instance.orientationX),          40);
            AssertOffset(ref instance, ref instance.orientationY,          nameof(instance.orientationY),          44);
            AssertOffset(ref instance, ref instance.orientationZ,          nameof(instance.orientationZ),          48);
            AssertOffset(ref instance, ref instance.accelerometerAccuracy, nameof(instance.accelerometerAccuracy), 52);
            AssertOffset(ref instance, ref instance.gyroscopeAccuracy,     nameof(instance.gyroscopeAccuracy),     56);
            AssertOffset(ref instance, ref instance.magnetometerAccuracy,  nameof(instance.magnetometerAccuracy),  60);
            AssertOffset(ref instance, ref instance.orientationAccuracy,   nameof(instance.orientationAccuracy),   64);
        }

        {
            GameInputArcadeStickState instance = default;
            AssertSize(ref instance, 4);
            AssertOffset(ref instance, ref instance.buttons, nameof(instance.buttons), 0);
        }

        {
            GameInputFlightStickState instance = default;
            AssertSize(ref instance, 24);
            AssertOffset(ref instance, ref instance.buttons,   nameof(instance.buttons),   0);
            AssertOffset(ref instance, ref instance.hatSwitch, nameof(instance.hatSwitch), 4);
            AssertOffset(ref instance, ref instance.roll,      nameof(instance.roll),      8);
            AssertOffset(ref instance, ref instance.pitch,     nameof(instance.pitch),     12);
            AssertOffset(ref instance, ref instance.yaw,       nameof(instance.yaw),       16);
            AssertOffset(ref instance, ref instance.throttle,  nameof(instance.throttle),  20);
        }

        {
            GameInputGamepadState instance = default;
            AssertSize(ref instance, 28);
            AssertOffset(ref instance, ref instance.buttons,          nameof(instance.buttons),          0);
            AssertOffset(ref instance, ref instance.leftTrigger,      nameof(instance.leftTrigger),      4);
            AssertOffset(ref instance, ref instance.rightTrigger,     nameof(instance.rightTrigger),     8);
            AssertOffset(ref instance, ref instance.leftThumbstickX,  nameof(instance.leftThumbstickX),  12);
            AssertOffset(ref instance, ref instance.leftThumbstickY,  nameof(instance.leftThumbstickY),  16);
            AssertOffset(ref instance, ref instance.rightThumbstickX, nameof(instance.rightThumbstickX), 20);
            AssertOffset(ref instance, ref instance.rightThumbstickY, nameof(instance.rightThumbstickY), 24);
        }

        {
            GameInputRacingWheelState instance = default;
            AssertSize(ref instance, 28);
            AssertOffset(ref instance, ref instance.buttons,            nameof(instance.buttons),            0);
            AssertOffset(ref instance, ref instance.patternShifterGear, nameof(instance.patternShifterGear), 4);
            AssertOffset(ref instance, ref instance.wheel,              nameof(instance.wheel),              8);
            AssertOffset(ref instance, ref instance.throttle,           nameof(instance.throttle),           12);
            AssertOffset(ref instance, ref instance.brake,              nameof(instance.brake),              16);
            AssertOffset(ref instance, ref instance.clutch,             nameof(instance.clutch),             20);
            AssertOffset(ref instance, ref instance.handbrake,          nameof(instance.handbrake),          24);
        }

        {
            GameInputUiNavigationState instance = default;
            AssertSize(ref instance, 4);
            AssertOffset(ref instance, ref instance.buttons, nameof(instance.buttons), 0);
        }

        {
            GameInputBatteryState instance = default;
            AssertSize(ref instance, 20);
            AssertOffset(ref instance, ref instance.chargeRate,         nameof(instance.chargeRate),         0);
            AssertOffset(ref instance, ref instance.maxChargeRate,      nameof(instance.maxChargeRate),      4);
            AssertOffset(ref instance, ref instance.remainingCapacity,  nameof(instance.remainingCapacity),  8);
            AssertOffset(ref instance, ref instance.fullChargeCapacity, nameof(instance.fullChargeCapacity), 12);
            AssertOffset(ref instance, ref instance.status,             nameof(instance.status),             16);
        }

        {
            GameInputString instance = default;
            AssertSize(ref instance, 16);
            AssertOffset(ref instance, ref instance.sizeInBytes,    nameof(instance.sizeInBytes),    0);
            AssertOffset(ref instance, ref instance.codePointCount, nameof(instance.codePointCount), 4);
            AssertOffset(&instance,    &instance.data,              nameof(instance.data),           8);
        }

        {
            GameInputUsage instance = default;
            AssertSize(ref instance, 4);
            AssertOffset(ref instance, ref instance.page, nameof(instance.page), 0);
            AssertOffset(ref instance, ref instance.id,   nameof(instance.id),   2);
        }

        {
            GameInputVersion instance = default;
            AssertSize(ref instance, 8);
            AssertOffset(ref instance, ref instance.major,    nameof(instance.major),    0);
            AssertOffset(ref instance, ref instance.minor,    nameof(instance.minor),    2);
            AssertOffset(ref instance, ref instance.build,    nameof(instance.build),    4);
            AssertOffset(ref instance, ref instance.revision, nameof(instance.revision), 6);
        }

        {
            GameInputRawDeviceItemCollectionInfo instance = default;
            AssertSize(ref instance, 80);
            AssertOffset(ref instance, ref instance.kind,            nameof(instance.kind),            0);
            AssertOffset(ref instance, ref instance.childCount,      nameof(instance.childCount),      4);
            AssertOffset(ref instance, ref instance.siblingCount,    nameof(instance.siblingCount),    8);
            AssertOffset(ref instance, ref instance.usageCount,      nameof(instance.usageCount),      12);
            AssertOffset(&instance,    &instance.usages,             nameof(instance.usages),          16);
            AssertOffset(&instance,    &instance.parent,             nameof(instance.parent),          24);
            AssertOffset(&instance,    &instance.firstSibling,       nameof(instance.firstSibling),    32);
            AssertOffset(&instance,    &instance.previousSibling,    nameof(instance.previousSibling), 40);
            AssertOffset(&instance,    &instance.nextSibling,        nameof(instance.nextSibling),     48);
            AssertOffset(&instance,    &instance.lastSibling,        nameof(instance.lastSibling),     56);
            AssertOffset(&instance,    &instance.firstChild,         nameof(instance.firstChild),      64);
            AssertOffset(&instance,    &instance.lastChild,          nameof(instance.lastChild),       72);
        }

        {
            GameInputRawDeviceReportItemInfo instance = default;
            AssertSize(ref instance, 88);
            AssertOffset(ref instance, ref instance.bitOffset,                nameof(instance.bitOffset),                0);
            AssertOffset(ref instance, ref instance.bitSize,                  nameof(instance.bitSize),                  4);
            AssertOffset(ref instance, ref instance.logicalMin,               nameof(instance.logicalMin),               8);
            AssertOffset(ref instance, ref instance.logicalMax,               nameof(instance.logicalMax),               16);
            AssertOffset(ref instance, ref instance.physicalMin,              nameof(instance.physicalMin),              24);
            AssertOffset(ref instance, ref instance.physicalMax,              nameof(instance.physicalMax),              32);
            AssertOffset(ref instance, ref instance.physicalUnits,            nameof(instance.physicalUnits),            40);
            AssertOffset(ref instance, ref instance.rawPhysicalUnits,         nameof(instance.rawPhysicalUnits),         44);
            AssertOffset(ref instance, ref instance.rawPhysicalUnitsExponent, nameof(instance.rawPhysicalUnitsExponent), 48);
            AssertOffset(ref instance, ref instance.flags,                    nameof(instance.flags),                    52);
            AssertOffset(ref instance, ref instance.usageCount,               nameof(instance.usageCount),               56);
            AssertOffset(&instance,    &instance.usages,                      nameof(instance.usages),                   64);
            AssertOffset(&instance,    &instance.collection,                  nameof(instance.collection),               72);
            AssertOffset(&instance,    &instance.itemString,                  nameof(instance.itemString),               80);
        }

        {
            GameInputRawDeviceReportInfo instance = default;
            AssertSize(ref instance, 24);
            AssertOffset(ref instance, ref instance.kind,      nameof(instance.kind),      0);
            AssertOffset(ref instance, ref instance.id,        nameof(instance.id),        4);
            AssertOffset(ref instance, ref instance.size,      nameof(instance.size),      8);
            AssertOffset(ref instance, ref instance.itemCount, nameof(instance.itemCount), 12);
            AssertOffset(&instance,    &instance.items,        nameof(instance.items),     16);
        }

        {
            GameInputControllerAxisInfo instance = default;
            AssertSize(ref instance, 48);
            AssertOffset(ref instance, ref instance.mappedInputKinds,  nameof(instance.mappedInputKinds),  0);
            AssertOffset(ref instance, ref instance.label,             nameof(instance.label),             4);
            AssertOffset(ref instance, ref instance.isContinuous,      nameof(instance.isContinuous),      8);
            AssertOffset(ref instance, ref instance.isNonlinear,       nameof(instance.isNonlinear),       9);
            AssertOffset(ref instance, ref instance.isQuantized,       nameof(instance.isQuantized),       10);
            AssertOffset(ref instance, ref instance.hasRestValue,      nameof(instance.hasRestValue),      11);
            AssertOffset(ref instance, ref instance.restValue,         nameof(instance.restValue),         12);
            AssertOffset(ref instance, ref instance.resolution,        nameof(instance.resolution),        16);
            AssertOffset(ref instance, ref instance.legacyDInputIndex, nameof(instance.legacyDInputIndex), 24);
            AssertOffset(ref instance, ref instance.legacyHidIndex,    nameof(instance.legacyHidIndex),    26);
            AssertOffset(ref instance, ref instance.rawReportIndex,    nameof(instance.rawReportIndex),    28);
            AssertOffset(&instance,    &instance.inputReport,          nameof(instance.inputReport),       32);
            AssertOffset(&instance,    &instance.inputReportItem,      nameof(instance.inputReportItem),   40);
        }

        {
            GameInputControllerButtonInfo instance = default;
            AssertSize(ref instance, 32);
            AssertOffset(ref instance, ref instance.mappedInputKinds,   nameof(instance.mappedInputKinds),   0);
            AssertOffset(ref instance, ref instance.label,              nameof(instance.label),              4);
            AssertOffset(ref instance, ref instance.legacyDInputIndex,  nameof(instance.legacyDInputIndex),  8);
            AssertOffset(ref instance, ref instance.legacyHidIndex,     nameof(instance.legacyHidIndex),     10);
            AssertOffset(ref instance, ref instance.rawReportIndex,     nameof(instance.rawReportIndex),     12);
            AssertOffset(&instance, &instance.inputReport,              nameof(instance.inputReport),        16);
            AssertOffset(&instance, &instance.inputReportItem,          nameof(instance.inputReportItem),    24);
        }

        {
            GameInputControllerSwitchInfo instance = default;
            AssertSize(ref instance, 72);
            AssertOffset(ref instance, ref instance.mappedInputKinds,  nameof(instance.mappedInputKinds),  0);
            AssertOffset(ref instance, ref instance.label,             nameof(instance.label),             4);
            AssertOffset(&instance,    instance._positionLabels,       nameof(instance._positionLabels),   8);
            AssertOffset(ref instance, ref instance.kind,              nameof(instance.kind),              44);
            AssertOffset(ref instance, ref instance.legacyDInputIndex, nameof(instance.legacyDInputIndex), 48);
            AssertOffset(ref instance, ref instance.legacyHidIndex,    nameof(instance.legacyHidIndex),    50);
            AssertOffset(ref instance, ref instance.rawReportIndex,    nameof(instance.rawReportIndex),    52);
            AssertOffset(&instance,    &instance.inputReport,          nameof(instance.inputReport),       56);
            AssertOffset(&instance,    &instance.inputReportItem,      nameof(instance.inputReportItem),   64);
        }

        {
            GameInputKeyboardInfo instance = default;
            AssertSize(ref instance, 40);
            AssertOffset(ref instance, ref instance.kind,                nameof(instance.kind),                0);
            AssertOffset(ref instance, ref instance.layout,              nameof(instance.layout),              4);
            AssertOffset(ref instance, ref instance.keyCount,            nameof(instance.keyCount),            8);
            AssertOffset(ref instance, ref instance.functionKeyCount,    nameof(instance.functionKeyCount),    12);
            AssertOffset(ref instance, ref instance.maxSimultaneousKeys, nameof(instance.maxSimultaneousKeys), 16);
            AssertOffset(ref instance, ref instance.platformType,        nameof(instance.platformType),        20);
            AssertOffset(ref instance, ref instance.platformSubtype,     nameof(instance.platformSubtype),     24);
            AssertOffset(&instance,    &instance.nativeLanguage,         nameof(instance.nativeLanguage),      32);
        }

        {
            GameInputMouseInfo instance = default;
            AssertSize(ref instance, 16);
            AssertOffset(ref instance, ref instance.supportedButtons, nameof(instance.supportedButtons), 0);
            AssertOffset(ref instance, ref instance.sampleRate,       nameof(instance.sampleRate),       4);
            AssertOffset(ref instance, ref instance.sensorDpi,        nameof(instance.sensorDpi),        8);
            AssertOffset(ref instance, ref instance.hasWheelX,        nameof(instance.hasWheelX),        12);
            AssertOffset(ref instance, ref instance.hasWheelY,        nameof(instance.hasWheelY),        13);
        }

        {
            GameInputTouchSensorInfo instance = default;
            AssertSize(ref instance, 64);
            AssertOffset(ref instance, ref instance.mappedInputKinds, nameof(instance.mappedInputKinds), 0);
            AssertOffset(ref instance, ref instance.label,            nameof(instance.label),            4);
            AssertOffset(ref instance, ref instance.location,         nameof(instance.location),         8);
            AssertOffset(ref instance, ref instance.locationId,       nameof(instance.locationId),       12);
            AssertOffset(ref instance, ref instance.resolutionX,      nameof(instance.resolutionX),      16);
            AssertOffset(ref instance, ref instance.resolutionY,      nameof(instance.resolutionY),      24);
            AssertOffset(ref instance, ref instance.shape,            nameof(instance.shape),            32);
            AssertOffset(ref instance, ref instance.aspectRatio,      nameof(instance.aspectRatio),      36);
            AssertOffset(ref instance, ref instance.orientation,      nameof(instance.orientation),      40);
            AssertOffset(ref instance, ref instance.physicalWidth,    nameof(instance.physicalWidth),    44);
            AssertOffset(ref instance, ref instance.physicalHeight,   nameof(instance.physicalHeight),   48);
            AssertOffset(ref instance, ref instance.maxPressure,      nameof(instance.maxPressure),      52);
            AssertOffset(ref instance, ref instance.maxProximity,     nameof(instance.maxProximity),     56);
            AssertOffset(ref instance, ref instance.maxTouchPoints,   nameof(instance.maxTouchPoints),   60);
        }

        {
            GameInputMotionInfo instance = default;
            AssertSize(ref instance, 12);
            AssertOffset(ref instance, ref instance.maxAcceleration,          nameof(instance.maxAcceleration),          0);
            AssertOffset(ref instance, ref instance.maxAngularVelocity,       nameof(instance.maxAngularVelocity),       4);
            AssertOffset(ref instance, ref instance.maxMagneticFieldStrength, nameof(instance.maxMagneticFieldStrength), 8);
        }

        {
            GameInputArcadeStickInfo instance = default;
            AssertSize(ref instance, 56);
            AssertOffset(ref instance, ref instance.menuButtonLabel,     nameof(instance.menuButtonLabel),     0);
            AssertOffset(ref instance, ref instance.viewButtonLabel,     nameof(instance.viewButtonLabel),     4);
            AssertOffset(ref instance, ref instance.stickUpLabel,        nameof(instance.stickUpLabel),        8);
            AssertOffset(ref instance, ref instance.stickDownLabel,      nameof(instance.stickDownLabel),      12);
            AssertOffset(ref instance, ref instance.stickLeftLabel,      nameof(instance.stickLeftLabel),      16);
            AssertOffset(ref instance, ref instance.stickRightLabel,     nameof(instance.stickRightLabel),     20);
            AssertOffset(ref instance, ref instance.actionButton1Label,  nameof(instance.actionButton1Label),  24);
            AssertOffset(ref instance, ref instance.actionButton2Label,  nameof(instance.actionButton2Label),  28);
            AssertOffset(ref instance, ref instance.actionButton3Label,  nameof(instance.actionButton3Label),  32);
            AssertOffset(ref instance, ref instance.actionButton4Label,  nameof(instance.actionButton4Label),  36);
            AssertOffset(ref instance, ref instance.actionButton5Label,  nameof(instance.actionButton5Label),  40);
            AssertOffset(ref instance, ref instance.actionButton6Label,  nameof(instance.actionButton6Label),  44);
            AssertOffset(ref instance, ref instance.specialButton1Label, nameof(instance.specialButton1Label), 48);
            AssertOffset(ref instance, ref instance.specialButton2Label, nameof(instance.specialButton2Label), 52);
        }

        {
            GameInputFlightStickInfo instance = default;
            AssertSize(ref instance, 20);
            AssertOffset(ref instance, ref instance.menuButtonLabel,          nameof(instance.menuButtonLabel),          0);
            AssertOffset(ref instance, ref instance.viewButtonLabel,          nameof(instance.viewButtonLabel),          4);
            AssertOffset(ref instance, ref instance.firePrimaryButtonLabel,   nameof(instance.firePrimaryButtonLabel),   8);
            AssertOffset(ref instance, ref instance.fireSecondaryButtonLabel, nameof(instance.fireSecondaryButtonLabel), 12);
            AssertOffset(ref instance, ref instance.hatSwitchKind,            nameof(instance.hatSwitchKind),            16);
        }

        {
            GameInputGamepadInfo instance = default;
            AssertSize(ref instance, 56);
            AssertOffset(ref instance, ref instance.menuButtonLabel,            nameof(instance.menuButtonLabel),            0);
            AssertOffset(ref instance, ref instance.viewButtonLabel,            nameof(instance.viewButtonLabel),            4);
            AssertOffset(ref instance, ref instance.aButtonLabel,               nameof(instance.aButtonLabel),               8);
            AssertOffset(ref instance, ref instance.bButtonLabel,               nameof(instance.bButtonLabel),               12);
            AssertOffset(ref instance, ref instance.xButtonLabel,               nameof(instance.xButtonLabel),               16);
            AssertOffset(ref instance, ref instance.yButtonLabel,               nameof(instance.yButtonLabel),               20);
            AssertOffset(ref instance, ref instance.dpadUpLabel,                nameof(instance.dpadUpLabel),                24);
            AssertOffset(ref instance, ref instance.dpadDownLabel,              nameof(instance.dpadDownLabel),              28);
            AssertOffset(ref instance, ref instance.dpadLeftLabel,              nameof(instance.dpadLeftLabel),              32);
            AssertOffset(ref instance, ref instance.dpadRightLabel,             nameof(instance.dpadRightLabel),             36);
            AssertOffset(ref instance, ref instance.leftShoulderButtonLabel,    nameof(instance.leftShoulderButtonLabel),    40);
            AssertOffset(ref instance, ref instance.rightShoulderButtonLabel,   nameof(instance.rightShoulderButtonLabel),   44);
            AssertOffset(ref instance, ref instance.leftThumbstickButtonLabel,  nameof(instance.leftThumbstickButtonLabel),  48);
            AssertOffset(ref instance, ref instance.rightThumbstickButtonLabel, nameof(instance.rightThumbstickButtonLabel), 52);
        }

        {
            GameInputRacingWheelInfo instance = default;
            AssertSize(ref instance, 48);
            AssertOffset(ref instance, ref instance.menuButtonLabel,         nameof(instance.menuButtonLabel),         0);
            AssertOffset(ref instance, ref instance.viewButtonLabel,         nameof(instance.viewButtonLabel),         4);
            AssertOffset(ref instance, ref instance.previousGearButtonLabel, nameof(instance.previousGearButtonLabel), 8);
            AssertOffset(ref instance, ref instance.nextGearButtonLabel,     nameof(instance.nextGearButtonLabel),     12);
            AssertOffset(ref instance, ref instance.dpadUpLabel,             nameof(instance.dpadUpLabel),             16);
            AssertOffset(ref instance, ref instance.dpadDownLabel,           nameof(instance.dpadDownLabel),           20);
            AssertOffset(ref instance, ref instance.dpadLeftLabel,           nameof(instance.dpadLeftLabel),           24);
            AssertOffset(ref instance, ref instance.dpadRightLabel,          nameof(instance.dpadRightLabel),          28);
            AssertOffset(ref instance, ref instance.hasClutch,               nameof(instance.hasClutch),               32);
            AssertOffset(ref instance, ref instance.hasHandbrake,            nameof(instance.hasHandbrake),            33);
            AssertOffset(ref instance, ref instance.hasPatternShifter,       nameof(instance.hasPatternShifter),       34);
            AssertOffset(ref instance, ref instance.minPatternShifterGear,   nameof(instance.minPatternShifterGear),   36);
            AssertOffset(ref instance, ref instance.maxPatternShifterGear,   nameof(instance.maxPatternShifterGear),   40);
            AssertOffset(ref instance, ref instance.maxWheelAngle,           nameof(instance.maxWheelAngle),           44);
        }

        {
            GameInputUiNavigationInfo instance = default;
            AssertSize(ref instance, 84);
            AssertOffset(ref instance, ref instance.menuButtonLabel,        nameof(instance.menuButtonLabel),        0);
            AssertOffset(ref instance, ref instance.viewButtonLabel,        nameof(instance.viewButtonLabel),        4);
            AssertOffset(ref instance, ref instance.acceptButtonLabel,      nameof(instance.acceptButtonLabel),      8);
            AssertOffset(ref instance, ref instance.cancelButtonLabel,      nameof(instance.cancelButtonLabel),      12);
            AssertOffset(ref instance, ref instance.upButtonLabel,          nameof(instance.upButtonLabel),          16);
            AssertOffset(ref instance, ref instance.downButtonLabel,        nameof(instance.downButtonLabel),        20);
            AssertOffset(ref instance, ref instance.leftButtonLabel,        nameof(instance.leftButtonLabel),        24);
            AssertOffset(ref instance, ref instance.rightButtonLabel,       nameof(instance.rightButtonLabel),       28);
            AssertOffset(ref instance, ref instance.contextButton1Label,    nameof(instance.contextButton1Label),    32);
            AssertOffset(ref instance, ref instance.contextButton2Label,    nameof(instance.contextButton2Label),    36);
            AssertOffset(ref instance, ref instance.contextButton3Label,    nameof(instance.contextButton3Label),    40);
            AssertOffset(ref instance, ref instance.contextButton4Label,    nameof(instance.contextButton4Label),    44);
            AssertOffset(ref instance, ref instance.pageUpButtonLabel,      nameof(instance.pageUpButtonLabel),      48);
            AssertOffset(ref instance, ref instance.pageDownButtonLabel,    nameof(instance.pageDownButtonLabel),    52);
            AssertOffset(ref instance, ref instance.pageLeftButtonLabel,    nameof(instance.pageLeftButtonLabel),    56);
            AssertOffset(ref instance, ref instance.pageRightButtonLabel,   nameof(instance.pageRightButtonLabel),   60);
            AssertOffset(ref instance, ref instance.scrollUpButtonLabel,    nameof(instance.scrollUpButtonLabel),    64);
            AssertOffset(ref instance, ref instance.scrollDownButtonLabel,  nameof(instance.scrollDownButtonLabel),  68);
            AssertOffset(ref instance, ref instance.scrollLeftButtonLabel,  nameof(instance.scrollLeftButtonLabel),  72);
            AssertOffset(ref instance, ref instance.scrollRightButtonLabel, nameof(instance.scrollRightButtonLabel), 76);
            AssertOffset(ref instance, ref instance.guideButtonLabel,       nameof(instance.guideButtonLabel),       80);
        }

        {
            GameInputForceFeedbackMotorInfo instance = default;
            AssertSize(ref instance, 28);
            AssertOffset(ref instance, ref instance.supportedAxes,                     nameof(instance.supportedAxes),                     0);
            AssertOffset(ref instance, ref instance.location,                          nameof(instance.location),                          4);
            AssertOffset(ref instance, ref instance.locationId,                        nameof(instance.locationId),                        8);
            AssertOffset(ref instance, ref instance.maxSimultaneousEffects,            nameof(instance.maxSimultaneousEffects),            12);
            AssertOffset(ref instance, ref instance.isConstantEffectSupported,         nameof(instance.isConstantEffectSupported),         16);
            AssertOffset(ref instance, ref instance.isRampEffectSupported,             nameof(instance.isRampEffectSupported),             17);
            AssertOffset(ref instance, ref instance.isSineWaveEffectSupported,         nameof(instance.isSineWaveEffectSupported),         18);
            AssertOffset(ref instance, ref instance.isSquareWaveEffectSupported,       nameof(instance.isSquareWaveEffectSupported),       19);
            AssertOffset(ref instance, ref instance.isTriangleWaveEffectSupported,     nameof(instance.isTriangleWaveEffectSupported),     20);
            AssertOffset(ref instance, ref instance.isSawtoothUpWaveEffectSupported,   nameof(instance.isSawtoothUpWaveEffectSupported),   21);
            AssertOffset(ref instance, ref instance.isSawtoothDownWaveEffectSupported, nameof(instance.isSawtoothDownWaveEffectSupported), 22);
            AssertOffset(ref instance, ref instance.isSpringEffectSupported,           nameof(instance.isSpringEffectSupported),           23);
            AssertOffset(ref instance, ref instance.isFrictionEffectSupported,         nameof(instance.isFrictionEffectSupported),         24);
            AssertOffset(ref instance, ref instance.isDamperEffectSupported,           nameof(instance.isDamperEffectSupported),           25);
            AssertOffset(ref instance, ref instance.isInertiaEffectSupported,          nameof(instance.isInertiaEffectSupported),          26);
        }

        {
            GameInputHapticWaveformInfo instance = default;
            AssertSize(ref instance, 16);
            AssertOffset(ref instance, ref instance.usage,                  nameof(instance.usage),                  0);
            AssertOffset(ref instance, ref instance.isDurationSupported,    nameof(instance.isDurationSupported),    4);
            AssertOffset(ref instance, ref instance.isIntensitySupported,   nameof(instance.isIntensitySupported),   5);
            AssertOffset(ref instance, ref instance.isRepeatSupported,      nameof(instance.isRepeatSupported),      6);
            AssertOffset(ref instance, ref instance.isRepeatDelaySupported, nameof(instance.isRepeatDelaySupported), 7);
            AssertOffset(ref instance, ref instance.defaultDuration,        nameof(instance.defaultDuration),        8);
        }

        {
            GameInputHapticFeedbackMotorInfo instance = default;
            AssertSize(ref instance, 24);
            AssertOffset(ref instance, ref instance.mappedRumbleMotors, nameof(instance.mappedRumbleMotors), 0);
            AssertOffset(ref instance, ref instance.location,           nameof(instance.location),           4);
            AssertOffset(ref instance, ref instance.locationId,         nameof(instance.locationId),         8);
            AssertOffset(ref instance, ref instance.waveformCount,      nameof(instance.waveformCount),      12);
            AssertOffset(&instance,    &instance.waveformInfo,          nameof(instance.waveformInfo),       16);
        }

        {
            GameInputDeviceInfo instance = default;
            AssertSize(ref instance, 320);
            AssertOffset(ref instance, ref instance.infoSize,                 nameof(instance.infoSize),                 0);
            AssertOffset(ref instance, ref instance.vendorId,                 nameof(instance.vendorId),                 4);
            AssertOffset(ref instance, ref instance.productId,                nameof(instance.productId),                6);
            AssertOffset(ref instance, ref instance.revisionNumber,           nameof(instance.revisionNumber),           8);
            AssertOffset(ref instance, ref instance.interfaceNumber,          nameof(instance.interfaceNumber),          10);
            AssertOffset(ref instance, ref instance.collectionNumber,         nameof(instance.collectionNumber),         11);
            AssertOffset(ref instance, ref instance.usage,                    nameof(instance.usage),                    12);
            AssertOffset(ref instance, ref instance.hardwareVersion,          nameof(instance.hardwareVersion),          16);
            AssertOffset(ref instance, ref instance.firmwareVersion,          nameof(instance.firmwareVersion),          24);
            AssertOffset(ref instance, ref instance.deviceId,                 nameof(instance.deviceId),                 32);
            AssertOffset(ref instance, ref instance.deviceRootId,             nameof(instance.deviceRootId),             64);
            AssertOffset(ref instance, ref instance.deviceFamily,             nameof(instance.deviceFamily),             96);
            AssertOffset(ref instance, ref instance.capabilities,             nameof(instance.capabilities),             100);
            AssertOffset(ref instance, ref instance.supportedInput,           nameof(instance.supportedInput),           104);
            AssertOffset(ref instance, ref instance.supportedRumbleMotors,    nameof(instance.supportedRumbleMotors),    108);
            AssertOffset(ref instance, ref instance.inputReportCount,         nameof(instance.inputReportCount),         112);
            AssertOffset(ref instance, ref instance.outputReportCount,        nameof(instance.outputReportCount),        116);
            AssertOffset(ref instance, ref instance.featureReportCount,       nameof(instance.featureReportCount),       120);
            AssertOffset(ref instance, ref instance.controllerAxisCount,      nameof(instance.controllerAxisCount),      124);
            AssertOffset(ref instance, ref instance.controllerButtonCount,    nameof(instance.controllerButtonCount),    128);
            AssertOffset(ref instance, ref instance.controllerSwitchCount,    nameof(instance.controllerSwitchCount),    132);
            AssertOffset(ref instance, ref instance.touchPointCount,          nameof(instance.touchPointCount),          136);
            AssertOffset(ref instance, ref instance.touchSensorCount,         nameof(instance.touchSensorCount),         140);
            AssertOffset(ref instance, ref instance.forceFeedbackMotorCount,  nameof(instance.forceFeedbackMotorCount),  144);
            AssertOffset(ref instance, ref instance.hapticFeedbackMotorCount, nameof(instance.hapticFeedbackMotorCount), 148);
            AssertOffset(ref instance, ref instance.deviceStringCount,        nameof(instance.deviceStringCount),        152);
            AssertOffset(ref instance, ref instance.deviceDescriptorSize,     nameof(instance.deviceDescriptorSize),     156);
            AssertOffset(&instance,    &instance.inputReportInfo,             nameof(instance.inputReportInfo),          160);
            AssertOffset(&instance,    &instance.outputReportInfo,            nameof(instance.outputReportInfo),         168);
            AssertOffset(&instance,    &instance.featureReportInfo,           nameof(instance.featureReportInfo),        176);
            AssertOffset(&instance,    &instance.controllerAxisInfo,          nameof(instance.controllerAxisInfo),       184);
            AssertOffset(&instance,    &instance.controllerButtonInfo,        nameof(instance.controllerButtonInfo),     192);
            AssertOffset(&instance,    &instance.controllerSwitchInfo,        nameof(instance.controllerSwitchInfo),     200);
            AssertOffset(&instance,    &instance.keyboardInfo,                nameof(instance.keyboardInfo),             208);
            AssertOffset(&instance,    &instance.mouseInfo,                   nameof(instance.mouseInfo),                216);
            AssertOffset(&instance,    &instance.touchSensorInfo,             nameof(instance.touchSensorInfo),          224);
            AssertOffset(&instance,    &instance.motionInfo,                  nameof(instance.motionInfo),               232);
            AssertOffset(&instance,    &instance.arcadeStickInfo,             nameof(instance.arcadeStickInfo),          240);
            AssertOffset(&instance,    &instance.flightStickInfo,             nameof(instance.flightStickInfo),          248);
            AssertOffset(&instance,    &instance.gamepadInfo,                 nameof(instance.gamepadInfo),              256);
            AssertOffset(&instance,    &instance.racingWheelInfo,             nameof(instance.racingWheelInfo),          264);
            AssertOffset(&instance,    &instance.uiNavigationInfo,            nameof(instance.uiNavigationInfo),         272);
            AssertOffset(&instance,    &instance.forceFeedbackMotorInfo,      nameof(instance.forceFeedbackMotorInfo),   280);
            AssertOffset(&instance,    &instance.hapticFeedbackMotorInfo,     nameof(instance.hapticFeedbackMotorInfo),  288);
            AssertOffset(&instance,    &instance.displayName,                 nameof(instance.displayName),              296);
            AssertOffset(&instance,    &instance.deviceStrings,               nameof(instance.deviceStrings),            304);
            AssertOffset(&instance,    &instance.deviceDescriptorData,        nameof(instance.deviceDescriptorData),     312);
        }

        {
            GameInputForceFeedbackEnvelope instance = default;
            AssertSize(ref instance, 48);
            AssertOffset(ref instance, ref instance.attackDuration,  nameof(instance.attackDuration),  0);
            AssertOffset(ref instance, ref instance.sustainDuration, nameof(instance.sustainDuration), 8);
            AssertOffset(ref instance, ref instance.releaseDuration, nameof(instance.releaseDuration), 16);
            AssertOffset(ref instance, ref instance.attackGain,      nameof(instance.attackGain),      24);
            AssertOffset(ref instance, ref instance.sustainGain,     nameof(instance.sustainGain),     28);
            AssertOffset(ref instance, ref instance.releaseGain,     nameof(instance.releaseGain),     32);
            AssertOffset(ref instance, ref instance.playCount,       nameof(instance.playCount),       36);
            AssertOffset(ref instance, ref instance.repeatDelay,     nameof(instance.repeatDelay),     40);
        }

        {
            GameInputForceFeedbackMagnitude instance = default;
            AssertSize(ref instance, 28);
            AssertOffset(ref instance, ref instance.linearX,  nameof(instance.linearX),  0);
            AssertOffset(ref instance, ref instance.linearY,  nameof(instance.linearY),  4);
            AssertOffset(ref instance, ref instance.linearZ,  nameof(instance.linearZ),  8);
            AssertOffset(ref instance, ref instance.angularX, nameof(instance.angularX), 12);
            AssertOffset(ref instance, ref instance.angularY, nameof(instance.angularY), 16);
            AssertOffset(ref instance, ref instance.angularZ, nameof(instance.angularZ), 20);
            AssertOffset(ref instance, ref instance.normal,   nameof(instance.normal),   24);
        }

        {
            GameInputForceFeedbackConditionParams instance = default;
            AssertSize(ref instance, 52);
            AssertOffset(ref instance, ref instance.magnitude,            nameof(instance.magnitude),            0);
            AssertOffset(ref instance, ref instance.positiveCoefficient,  nameof(instance.positiveCoefficient),  28);
            AssertOffset(ref instance, ref instance.negativeCoefficient,  nameof(instance.negativeCoefficient),  32);
            AssertOffset(ref instance, ref instance.maxPositiveMagnitude, nameof(instance.maxPositiveMagnitude), 36);
            AssertOffset(ref instance, ref instance.maxNegativeMagnitude, nameof(instance.maxNegativeMagnitude), 40);
            AssertOffset(ref instance, ref instance.deadZone,             nameof(instance.deadZone),             44);
            AssertOffset(ref instance, ref instance.bias,                 nameof(instance.bias),                 48);
        }

        {
            GameInputForceFeedbackConstantParams instance = default;
            AssertSize(ref instance, 80);
            AssertOffset(ref instance, ref instance.envelope,  nameof(instance.envelope),  0);
            AssertOffset(ref instance, ref instance.magnitude, nameof(instance.magnitude), 48);
        }

        {
            GameInputForceFeedbackPeriodicParams instance = default;
            AssertSize(ref instance, 88);
            AssertOffset(ref instance, ref instance.envelope,  nameof(instance.envelope),  0);
            AssertOffset(ref instance, ref instance.magnitude, nameof(instance.magnitude), 48);
            AssertOffset(ref instance, ref instance.frequency, nameof(instance.frequency), 76);
            AssertOffset(ref instance, ref instance.phase,     nameof(instance.phase),     80);
            AssertOffset(ref instance, ref instance.bias,      nameof(instance.bias),      84);
        }

        {
            GameInputForceFeedbackRampParams instance = default;
            AssertSize(ref instance, 104);
            AssertOffset(ref instance, ref instance.envelope,       nameof(instance.envelope),       0);
            AssertOffset(ref instance, ref instance.startMagnitude, nameof(instance.startMagnitude), 48);
            AssertOffset(ref instance, ref instance.endMagnitude,   nameof(instance.endMagnitude),   76);
        }

        {
            GameInputForceFeedbackParams instance = default;
            AssertSize(ref instance, 112);
            AssertOffset(ref instance, ref instance.kind,             nameof(instance.kind),             0);
            AssertOffset(ref instance, ref instance.constant,         nameof(instance.constant),         4);
            AssertOffset(ref instance, ref instance.ramp,             nameof(instance.ramp),             4);
            AssertOffset(ref instance, ref instance.sineWave,         nameof(instance.sineWave),         4);
            AssertOffset(ref instance, ref instance.squareWave,       nameof(instance.squareWave),       4);
            AssertOffset(ref instance, ref instance.triangleWave,     nameof(instance.triangleWave),     4);
            AssertOffset(ref instance, ref instance.sawtoothUpWave,   nameof(instance.sawtoothUpWave),   4);
            AssertOffset(ref instance, ref instance.sawtoothDownWave, nameof(instance.sawtoothDownWave), 4);
            AssertOffset(ref instance, ref instance.spring,           nameof(instance.spring),           4);
            AssertOffset(ref instance, ref instance.friction,         nameof(instance.friction),         4);
            AssertOffset(ref instance, ref instance.damper,           nameof(instance.damper),           4);
            AssertOffset(ref instance, ref instance.inertia,          nameof(instance.inertia),          4);
        }

        {
            GameInputHapticFeedbackParams instance = default;
            AssertSize(ref instance, 32);
            AssertOffset(ref instance, ref instance.waveformIndex, nameof(instance.waveformIndex), 0);
            AssertOffset(ref instance, ref instance.duration,      nameof(instance.duration),      8);
            AssertOffset(ref instance, ref instance.intensity,     nameof(instance.intensity),     16);
            AssertOffset(ref instance, ref instance.playCount,     nameof(instance.playCount),     20);
            AssertOffset(ref instance, ref instance.repeatDelay,   nameof(instance.repeatDelay),   24);
        }

        {
            GameInputRumbleParams instance = default;
            AssertSize(ref instance, 16);
            AssertOffset(ref instance, ref instance.lowFrequency,  nameof(instance.lowFrequency),  0);
            AssertOffset(ref instance, ref instance.highFrequency, nameof(instance.highFrequency), 4);
            AssertOffset(ref instance, ref instance.leftTrigger,   nameof(instance.leftTrigger),   8);
            AssertOffset(ref instance, ref instance.rightTrigger,  nameof(instance.rightTrigger),  12);
        }
    });
}