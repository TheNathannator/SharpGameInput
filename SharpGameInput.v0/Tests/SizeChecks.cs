using static SharpGameInput.Common.Tests.SizeHelper;

namespace SharpGameInput.v0.Tests;

public class SizeChecks
{
    [Test] public void GameInputKind() => AssertEnumSize<GameInputKind>(4);
    [Test] public void GameInputEnumerationKind() => AssertEnumSize<GameInputEnumerationKind>(4);
    [Test] public void GameInputFocusPolicy() => AssertEnumSize<GameInputFocusPolicy>(4);
    [Test] public void GameInputSwitchKind() => AssertEnumSize<GameInputSwitchKind>(4);
    [Test] public void GameInputSwitchPosition() => AssertEnumSize<GameInputSwitchPosition>(4);
    [Test] public void GameInputKeyboardKind() => AssertEnumSize<GameInputKeyboardKind>(4);
    [Test] public void GameInputMouseButtons() => AssertEnumSize<GameInputMouseButtons>(4);
    [Test] public void GameInputTouchShape() => AssertEnumSize<GameInputTouchShape>(4);
    [Test] public void GameInputMotionAccuracy() => AssertEnumSize<GameInputMotionAccuracy>(4);
    [Test] public void GameInputArcadeStickButtons() => AssertEnumSize<GameInputArcadeStickButtons>(4);
    [Test] public void GameInputFlightStickButtons() => AssertEnumSize<GameInputFlightStickButtons>(4);
    [Test] public void GameInputGamepadButtons() => AssertEnumSize<GameInputGamepadButtons>(4);
    [Test] public void GameInputRacingWheelButtons() => AssertEnumSize<GameInputRacingWheelButtons>(4);
    [Test] public void GameInputUiNavigationButtons() => AssertEnumSize<GameInputUiNavigationButtons>(4);
    [Test] public void GameInputDeviceStatus() => AssertEnumSize<GameInputDeviceStatus>(4);
    [Test] public void GameInputBatteryStatus() => AssertEnumSize<GameInputBatteryStatus>(4);
    [Test] public void GameInputDeviceFamily() => AssertEnumSize<GameInputDeviceFamily>(4);
    [Test] public void GameInputDeviceCapabilities() => AssertEnumSize<GameInputDeviceCapabilities>(4);
    [Test] public void GameInputRawDeviceReportKind() => AssertEnumSize<GameInputRawDeviceReportKind>(4);
    [Test] public void GameInputRawDeviceReportItemFlags() => AssertEnumSize<GameInputRawDeviceReportItemFlags>(4);
    [Test] public void GameInputRawDeviceItemCollectionKind() => AssertEnumSize<GameInputRawDeviceItemCollectionKind>(4);
    [Test] public void GameInputRawDevicePhysicalUnitKind() => AssertEnumSize<GameInputRawDevicePhysicalUnitKind>(4);
    [Test] public void GameInputLabel() => AssertEnumSize<GameInputLabel>(4);
    [Test] public void GameInputLocation() => AssertEnumSize<GameInputLocation>(4);
    [Test] public void GameInputFeedbackAxes() => AssertEnumSize<GameInputFeedbackAxes>(4);
    [Test] public void GameInputFeedbackEffectState() => AssertEnumSize<GameInputFeedbackEffectState>(4);
    [Test] public void GameInputForceFeedbackEffectKind() => AssertEnumSize<GameInputForceFeedbackEffectKind>(4);
    [Test] public void GameInputRumbleMotors() => AssertEnumSize<GameInputRumbleMotors>(4);

    [Test]
    public unsafe void APP_LOCAL_DEVICE_ID() => Assert.Multiple(() =>
    {
        APP_LOCAL_DEVICE_ID instance = default;
        AssertSize(instance, 32);
        AssertFixed(instance, instance._inner._value, nameof(instance._inner._value), 0);
    });

    [Test]
    public unsafe void GameInputKeyState() => Assert.Multiple(() =>
    {
        GameInputKeyState instance = default;
        AssertSize(instance, 12);
        AssertField(instance, instance.scanCode,   nameof(instance.scanCode),   4, 0);
        AssertField(instance, instance.codePoint,  nameof(instance.codePoint),  4, 4);
        AssertField(instance, instance.virtualKey, nameof(instance.virtualKey), 1, 8);
        AssertField(instance, instance.isDeadKey,  nameof(instance.isDeadKey),     9);
    });

    [Test]
    public unsafe void GameInputMouseState() => Assert.Multiple(() =>
    {
        GameInputMouseState instance = default;
        AssertSize(instance, 40);
        AssertField(instance, instance.buttons,   nameof(instance.buttons),   4, 0);
        AssertField(instance, instance.positionX, nameof(instance.positionX), 8, 8);
        AssertField(instance, instance.positionY, nameof(instance.positionY), 8, 16);
        AssertField(instance, instance.wheelX,    nameof(instance.wheelX),    8, 24);
        AssertField(instance, instance.wheelY,    nameof(instance.wheelY),    8, 32);
    });

    [Test]
    public unsafe void GameInputTouchState() => Assert.Multiple(() =>
    {
        GameInputTouchState instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.touchId,           nameof(instance.touchId),           8, 0);
        AssertField(instance, instance.sensorIndex,       nameof(instance.sensorIndex),       4, 8);
        AssertField(instance, instance.positionX,         nameof(instance.positionX),         4, 12);
        AssertField(instance, instance.positionY,         nameof(instance.positionY),         4, 16);
        AssertField(instance, instance.pressure,          nameof(instance.pressure),          4, 20);
        AssertField(instance, instance.proximity,         nameof(instance.proximity),         4, 24);
        AssertField(instance, instance.contactRectTop,    nameof(instance.contactRectTop),    4, 28);
        AssertField(instance, instance.contactRectLeft,   nameof(instance.contactRectLeft),   4, 32);
        AssertField(instance, instance.contactRectRight,  nameof(instance.contactRectRight),  4, 36);
        AssertField(instance, instance.contactRectBottom, nameof(instance.contactRectBottom), 4, 40);
    });

    [Test]
    public unsafe void GameInputMotionState() => Assert.Multiple(() =>
    {
        GameInputMotionState instance = default;
        AssertSize(instance, 68);
        AssertField(instance, instance.accelerationX,         nameof(instance.accelerationX),         4, 0);
        AssertField(instance, instance.accelerationY,         nameof(instance.accelerationY),         4, 4);
        AssertField(instance, instance.accelerationZ,         nameof(instance.accelerationZ),         4, 8);
        AssertField(instance, instance.angularVelocityX,      nameof(instance.angularVelocityX),      4, 12);
        AssertField(instance, instance.angularVelocityY,      nameof(instance.angularVelocityY),      4, 16);
        AssertField(instance, instance.angularVelocityZ,      nameof(instance.angularVelocityZ),      4, 20);
        AssertField(instance, instance.magneticFieldX,        nameof(instance.magneticFieldX),        4, 24);
        AssertField(instance, instance.magneticFieldY,        nameof(instance.magneticFieldY),        4, 28);
        AssertField(instance, instance.magneticFieldZ,        nameof(instance.magneticFieldZ),        4, 32);
        AssertField(instance, instance.orientationW,          nameof(instance.orientationW),          4, 36);
        AssertField(instance, instance.orientationX,          nameof(instance.orientationX),          4, 40);
        AssertField(instance, instance.orientationY,          nameof(instance.orientationY),          4, 44);
        AssertField(instance, instance.orientationZ,          nameof(instance.orientationZ),          4, 48);
        AssertField(instance, instance.accelerometerAccuracy, nameof(instance.accelerometerAccuracy), 4, 52);
        AssertField(instance, instance.gyroscopeAccuracy,     nameof(instance.gyroscopeAccuracy),     4, 56);
        AssertField(instance, instance.magnetometerAccuracy,  nameof(instance.magnetometerAccuracy),  4, 60);
        AssertField(instance, instance.orientationAccuracy,   nameof(instance.orientationAccuracy),   4, 64);
    });

    [Test]
    public unsafe void GameInputArcadeStickState() => Assert.Multiple(() =>
    {
        GameInputArcadeStickState instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.buttons, nameof(instance.buttons), 4, 0);
    });

    [Test]
    public unsafe void GameInputFlightStickState() => Assert.Multiple(() =>
    {
        GameInputFlightStickState instance = default;
        AssertSize(instance, 24);
        AssertField(instance, instance.buttons,   nameof(instance.buttons),   4, 0);
        AssertField(instance, instance.hatSwitch, nameof(instance.hatSwitch), 4, 4);
        AssertField(instance, instance.roll,      nameof(instance.roll),      4, 8);
        AssertField(instance, instance.pitch,     nameof(instance.pitch),     4, 12);
        AssertField(instance, instance.yaw,       nameof(instance.yaw),       4, 16);
        AssertField(instance, instance.throttle,  nameof(instance.throttle),  4, 20);
    });

    [Test]
    public unsafe void GameInputGamepadState() => Assert.Multiple(() =>
    {
        GameInputGamepadState instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.buttons,          nameof(instance.buttons),          4, 0);
        AssertField(instance, instance.leftTrigger,      nameof(instance.leftTrigger),      4, 4);
        AssertField(instance, instance.rightTrigger,     nameof(instance.rightTrigger),     4, 8);
        AssertField(instance, instance.leftThumbstickX,  nameof(instance.leftThumbstickX),  4, 12);
        AssertField(instance, instance.leftThumbstickY,  nameof(instance.leftThumbstickY),  4, 16);
        AssertField(instance, instance.rightThumbstickX, nameof(instance.rightThumbstickX), 4, 20);
        AssertField(instance, instance.rightThumbstickY, nameof(instance.rightThumbstickY), 4, 24);
    });

    [Test]
    public unsafe void GameInputRacingWheelState() => Assert.Multiple(() =>
    {
        GameInputRacingWheelState instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.buttons,            nameof(instance.buttons),            4, 0);
        AssertField(instance, instance.patternShifterGear, nameof(instance.patternShifterGear), 4, 4);
        AssertField(instance, instance.wheel,              nameof(instance.wheel),              4, 8);
        AssertField(instance, instance.throttle,           nameof(instance.throttle),           4, 12);
        AssertField(instance, instance.brake,              nameof(instance.brake),              4, 16);
        AssertField(instance, instance.clutch,             nameof(instance.clutch),             4, 20);
        AssertField(instance, instance.handbrake,          nameof(instance.handbrake),          4, 24);
    });

    [Test]
    public unsafe void GameInputUiNavigationState() => Assert.Multiple(() =>
    {
        GameInputUiNavigationState instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.buttons, nameof(instance.buttons), 4, 0);
    });

    [Test]
    public unsafe void GameInputBatteryState() => Assert.Multiple(() =>
    {
        GameInputBatteryState instance = default;
        AssertSize(instance, 20);
        AssertField(instance, instance.chargeRate,         nameof(instance.chargeRate),         4, 0);
        AssertField(instance, instance.maxChargeRate,      nameof(instance.maxChargeRate),      4, 4);
        AssertField(instance, instance.remainingCapacity,  nameof(instance.remainingCapacity),  4, 8);
        AssertField(instance, instance.fullChargeCapacity, nameof(instance.fullChargeCapacity), 4, 12);
        AssertField(instance, instance.status,             nameof(instance.status),             4, 16);
    });

    [Test]
    public unsafe void GameInputString() => Assert.Multiple(() =>
    {
        GameInputString instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.sizeInBytes,    nameof(instance.sizeInBytes),    4, 0);
        AssertField(instance, instance.codePointCount, nameof(instance.codePointCount), 4, 4);
        AssertField(instance, instance.data,           nameof(instance.data),              8);
    });

    [Test]
    public unsafe void GameInputUsage() => Assert.Multiple(() =>
    {
        GameInputUsage instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.page, nameof(instance.page), 2, 0);
        AssertField(instance, instance.id,   nameof(instance.id),   2, 2);
    });

    [Test]
    public unsafe void GameInputVersion() => Assert.Multiple(() =>
    {
        GameInputVersion instance = default;
        AssertSize(instance, 8);
        AssertField(instance, instance.major,    nameof(instance.major),    2, 0);
        AssertField(instance, instance.minor,    nameof(instance.minor),    2, 2);
        AssertField(instance, instance.build,    nameof(instance.build),    2, 4);
        AssertField(instance, instance.revision, nameof(instance.revision), 2, 6);
    });

    [Test]
    public unsafe void GameInputRawDeviceItemCollectionInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceItemCollectionInfo instance = default;
        AssertSize(instance, 80);
        AssertField(instance, instance.kind,            nameof(instance.kind),            4, 0);
        AssertField(instance, instance.childCount,      nameof(instance.childCount),      4, 4);
        AssertField(instance, instance.siblingCount,    nameof(instance.siblingCount),    4, 8);
        AssertField(instance, instance.usageCount,      nameof(instance.usageCount),      4, 12);
        AssertField(instance, instance.usages,          nameof(instance.usages),             16);
        AssertField(instance, instance.parent,          nameof(instance.parent),             24);
        AssertField(instance, instance.firstSibling,    nameof(instance.firstSibling),       32);
        AssertField(instance, instance.previousSibling, nameof(instance.previousSibling),    40);
        AssertField(instance, instance.nextSibling,     nameof(instance.nextSibling),        48);
        AssertField(instance, instance.lastSibling,     nameof(instance.lastSibling),        56);
        AssertField(instance, instance.firstChild,      nameof(instance.firstChild),         64);
        AssertField(instance, instance.lastChild,       nameof(instance.lastChild),          72);
    });

    [Test]
    public unsafe void GameInputRawDeviceReportItemInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceReportItemInfo instance = default;
        AssertSize(instance, 88);
        AssertField(instance, instance.bitOffset,                nameof(instance.bitOffset),                4, 0);
        AssertField(instance, instance.bitSize,                  nameof(instance.bitSize),                  4, 4);
        AssertField(instance, instance.logicalMin,               nameof(instance.logicalMin),               8, 8);
        AssertField(instance, instance.logicalMax,               nameof(instance.logicalMax),               8, 16);
        AssertField(instance, instance.physicalMin,              nameof(instance.physicalMin),              8, 24);
        AssertField(instance, instance.physicalMax,              nameof(instance.physicalMax),              8, 32);
        AssertField(instance, instance.physicalUnits,            nameof(instance.physicalUnits),            4, 40);
        AssertField(instance, instance.rawPhysicalUnits,         nameof(instance.rawPhysicalUnits),         4, 44);
        AssertField(instance, instance.rawPhysicalUnitsExponent, nameof(instance.rawPhysicalUnitsExponent), 4, 48);
        AssertField(instance, instance.flags,                    nameof(instance.flags),                    4, 52);
        AssertField(instance, instance.usageCount,               nameof(instance.usageCount),               4, 56);
        AssertField(instance, instance.usages,                   nameof(instance.usages),                      64);
        AssertField(instance, instance.collection,               nameof(instance.collection),                  72);
        AssertField(instance, instance.itemString,               nameof(instance.itemString),                  80);
    });

    [Test]
    public unsafe void GameInputRawDeviceReportInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceReportInfo instance = default;
        AssertSize(instance, 24);
        AssertField(instance, instance.kind,      nameof(instance.kind),      4, 0);
        AssertField(instance, instance.id,        nameof(instance.id),        4, 4);
        AssertField(instance, instance.size,      nameof(instance.size),      4, 8);
        AssertField(instance, instance.itemCount, nameof(instance.itemCount), 4, 12);
        AssertField(instance, instance.items,     nameof(instance.items),        16);
    });

    [Test]
    public unsafe void GameInputControllerAxisInfo() => Assert.Multiple(() =>
    {
        GameInputControllerAxisInfo instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.mappedInputKinds,  nameof(instance.mappedInputKinds),  4, 0);
        AssertField(instance, instance.label,             nameof(instance.label),             4, 4);
        AssertField(instance, instance.isContinuous,      nameof(instance.isContinuous),      1, 8);
        AssertField(instance, instance.isNonlinear,       nameof(instance.isNonlinear),       1, 9);
        AssertField(instance, instance.isQuantized,       nameof(instance.isQuantized),       1, 10);
        AssertField(instance, instance.hasRestValue,      nameof(instance.hasRestValue),      1, 11);
        AssertField(instance, instance.restValue,         nameof(instance.restValue),         4, 12);
        AssertField(instance, instance.resolution,        nameof(instance.resolution),        8, 16);
        AssertField(instance, instance.legacyDInputIndex, nameof(instance.legacyDInputIndex), 2, 24);
        AssertField(instance, instance.legacyHidIndex,    nameof(instance.legacyHidIndex),    2, 26);
        AssertField(instance, instance.rawReportIndex,    nameof(instance.rawReportIndex),    4, 28);
        AssertField(instance, instance.inputReport,       nameof(instance.inputReport),          32);
        AssertField(instance, instance.inputReportItem,   nameof(instance.inputReportItem),      40);
    });

    [Test]
    public unsafe void GameInputControllerButtonInfo() => Assert.Multiple(() =>
    {
        GameInputControllerButtonInfo instance = default;
        AssertSize(instance, 32);
        AssertField(instance, instance.mappedInputKinds,  nameof(instance.mappedInputKinds),  4, 0);
        AssertField(instance, instance.label,             nameof(instance.label),             4, 4);
        AssertField(instance, instance.legacyDInputIndex, nameof(instance.legacyDInputIndex), 2, 8);
        AssertField(instance, instance.legacyHidIndex,    nameof(instance.legacyHidIndex),    2, 10);
        AssertField(instance, instance.rawReportIndex,    nameof(instance.rawReportIndex),    4, 12);
        AssertField(instance, instance.inputReport,       nameof(instance.inputReport),          16);
        AssertField(instance, instance.inputReportItem,   nameof(instance.inputReportItem),      24);
    });

    [Test]
    public unsafe void GameInputControllerSwitchInfo() => Assert.Multiple(() =>
    {
        GameInputControllerSwitchInfo instance = default;
        AssertSize(instance, 72);
        AssertField(instance, instance.mappedInputKinds,     nameof(instance.mappedInputKinds),  4, 0);
        AssertField(instance, instance.label,                nameof(instance.label),             4, 4);
        AssertFixed(instance, instance._positionLabels,      nameof(instance._positionLabels),      8);
        AssertField(instance, instance.kind,                 nameof(instance.kind),              4, 44);
        AssertField(instance, instance.legacyDInputIndex,    nameof(instance.legacyDInputIndex), 2, 48);
        AssertField(instance, instance.legacyHidIndex,       nameof(instance.legacyHidIndex),    2, 50);
        AssertField(instance, instance.rawReportIndex,       nameof(instance.rawReportIndex),    4, 52);
        AssertField(instance, instance.inputReport,          nameof(instance.inputReport),          56);
        AssertField(instance, instance.inputReportItem,      nameof(instance.inputReportItem),      64);
    });

    [Test]
    public unsafe void GameInputKeyboardInfo() => Assert.Multiple(() =>
    {
        GameInputKeyboardInfo instance = default;
        AssertSize(instance, 40);
        AssertField(instance, instance.kind,                nameof(instance.kind),                4, 0);
        AssertField(instance, instance.layout,              nameof(instance.layout),              4, 4);
        AssertField(instance, instance.keyCount,            nameof(instance.keyCount),            4, 8);
        AssertField(instance, instance.functionKeyCount,    nameof(instance.functionKeyCount),    4, 12);
        AssertField(instance, instance.maxSimultaneousKeys, nameof(instance.maxSimultaneousKeys), 4, 16);
        AssertField(instance, instance.platformType,        nameof(instance.platformType),        4, 20);
        AssertField(instance, instance.platformSubtype,     nameof(instance.platformSubtype),     4, 24);
        AssertField(instance, instance.nativeLanguage,      nameof(instance.nativeLanguage),         32);
    });

    [Test]
    public unsafe void GameInputMouseInfo() => Assert.Multiple(() =>
    {
        GameInputMouseInfo instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.supportedButtons, nameof(instance.supportedButtons), 4, 0);
        AssertField(instance, instance.sampleRate,       nameof(instance.sampleRate),       4, 4);
        AssertField(instance, instance.sensorDpi,        nameof(instance.sensorDpi),        4, 8);
        AssertField(instance, instance.hasWheelX,        nameof(instance.hasWheelX),        1, 12);
        AssertField(instance, instance.hasWheelY,        nameof(instance.hasWheelY),        1, 13);
    });

    [Test]
    public unsafe void GameInputTouchSensorInfo() => Assert.Multiple(() =>
    {
        GameInputTouchSensorInfo instance = default;
        AssertSize(instance, 64);
        AssertField(instance, instance.mappedInputKinds, nameof(instance.mappedInputKinds), 4, 0);
        AssertField(instance, instance.label,            nameof(instance.label),            4, 4);
        AssertField(instance, instance.location,         nameof(instance.location),         4, 8);
        AssertField(instance, instance.locationId,       nameof(instance.locationId),       4, 12);
        AssertField(instance, instance.resolutionX,      nameof(instance.resolutionX),      8, 16);
        AssertField(instance, instance.resolutionY,      nameof(instance.resolutionY),      8, 24);
        AssertField(instance, instance.shape,            nameof(instance.shape),            4, 32);
        AssertField(instance, instance.aspectRatio,      nameof(instance.aspectRatio),      4, 36);
        AssertField(instance, instance.orientation,      nameof(instance.orientation),      4, 40);
        AssertField(instance, instance.physicalWidth,    nameof(instance.physicalWidth),    4, 44);
        AssertField(instance, instance.physicalHeight,   nameof(instance.physicalHeight),   4, 48);
        AssertField(instance, instance.maxPressure,      nameof(instance.maxPressure),      4, 52);
        AssertField(instance, instance.maxProximity,     nameof(instance.maxProximity),     4, 56);
        AssertField(instance, instance.maxTouchPoints,   nameof(instance.maxTouchPoints),   4, 60);
    });

    [Test]
    public unsafe void GameInputMotionInfo() => Assert.Multiple(() =>
    {
        GameInputMotionInfo instance = default;
        AssertSize(instance, 12);
        AssertField(instance, instance.maxAcceleration,          nameof(instance.maxAcceleration),          4, 0);
        AssertField(instance, instance.maxAngularVelocity,       nameof(instance.maxAngularVelocity),       4, 4);
        AssertField(instance, instance.maxMagneticFieldStrength, nameof(instance.maxMagneticFieldStrength), 4, 8);
    });

    [Test]
    public unsafe void GameInputArcadeStickInfo() => Assert.Multiple(() =>
    {
        GameInputArcadeStickInfo instance = default;
        AssertSize(instance, 56);
        AssertField(instance, instance.menuButtonLabel,     nameof(instance.menuButtonLabel),     4, 0);
        AssertField(instance, instance.viewButtonLabel,     nameof(instance.viewButtonLabel),     4, 4);
        AssertField(instance, instance.stickUpLabel,        nameof(instance.stickUpLabel),        4, 8);
        AssertField(instance, instance.stickDownLabel,      nameof(instance.stickDownLabel),      4, 12);
        AssertField(instance, instance.stickLeftLabel,      nameof(instance.stickLeftLabel),      4, 16);
        AssertField(instance, instance.stickRightLabel,     nameof(instance.stickRightLabel),     4, 20);
        AssertField(instance, instance.actionButton1Label,  nameof(instance.actionButton1Label),  4, 24);
        AssertField(instance, instance.actionButton2Label,  nameof(instance.actionButton2Label),  4, 28);
        AssertField(instance, instance.actionButton3Label,  nameof(instance.actionButton3Label),  4, 32);
        AssertField(instance, instance.actionButton4Label,  nameof(instance.actionButton4Label),  4, 36);
        AssertField(instance, instance.actionButton5Label,  nameof(instance.actionButton5Label),  4, 40);
        AssertField(instance, instance.actionButton6Label,  nameof(instance.actionButton6Label),  4, 44);
        AssertField(instance, instance.specialButton1Label, nameof(instance.specialButton1Label), 4, 48);
        AssertField(instance, instance.specialButton2Label, nameof(instance.specialButton2Label), 4, 52);
    });

    [Test]
    public unsafe void GameInputFlightStickInfo() => Assert.Multiple(() =>
    {
        GameInputFlightStickInfo instance = default;
        AssertSize(instance, 20);
        AssertField(instance, instance.menuButtonLabel,          nameof(instance.menuButtonLabel),          4, 0);
        AssertField(instance, instance.viewButtonLabel,          nameof(instance.viewButtonLabel),          4, 4);
        AssertField(instance, instance.firePrimaryButtonLabel,   nameof(instance.firePrimaryButtonLabel),   4, 8);
        AssertField(instance, instance.fireSecondaryButtonLabel, nameof(instance.fireSecondaryButtonLabel), 4, 12);
        AssertField(instance, instance.hatSwitchKind,            nameof(instance.hatSwitchKind),            4, 16);
    });

    [Test]
    public unsafe void GameInputGamepadInfo() => Assert.Multiple(() =>
    {
        GameInputGamepadInfo instance = default;
        AssertSize(instance, 56);
        AssertField(instance, instance.menuButtonLabel,            nameof(instance.menuButtonLabel),            4, 0);
        AssertField(instance, instance.viewButtonLabel,            nameof(instance.viewButtonLabel),            4, 4);
        AssertField(instance, instance.aButtonLabel,               nameof(instance.aButtonLabel),               4, 8);
        AssertField(instance, instance.bButtonLabel,               nameof(instance.bButtonLabel),               4, 12);
        AssertField(instance, instance.xButtonLabel,               nameof(instance.xButtonLabel),               4, 16);
        AssertField(instance, instance.yButtonLabel,               nameof(instance.yButtonLabel),               4, 20);
        AssertField(instance, instance.dpadUpLabel,                nameof(instance.dpadUpLabel),                4, 24);
        AssertField(instance, instance.dpadDownLabel,              nameof(instance.dpadDownLabel),              4, 28);
        AssertField(instance, instance.dpadLeftLabel,              nameof(instance.dpadLeftLabel),              4, 32);
        AssertField(instance, instance.dpadRightLabel,             nameof(instance.dpadRightLabel),             4, 36);
        AssertField(instance, instance.leftShoulderButtonLabel,    nameof(instance.leftShoulderButtonLabel),    4, 40);
        AssertField(instance, instance.rightShoulderButtonLabel,   nameof(instance.rightShoulderButtonLabel),   4, 44);
        AssertField(instance, instance.leftThumbstickButtonLabel,  nameof(instance.leftThumbstickButtonLabel),  4, 48);
        AssertField(instance, instance.rightThumbstickButtonLabel, nameof(instance.rightThumbstickButtonLabel), 4, 52);
    });

    [Test]
    public unsafe void GameInputRacingWheelInfo() => Assert.Multiple(() =>
    {
        GameInputRacingWheelInfo instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.menuButtonLabel,         nameof(instance.menuButtonLabel),         4, 0);
        AssertField(instance, instance.viewButtonLabel,         nameof(instance.viewButtonLabel),         4, 4);
        AssertField(instance, instance.previousGearButtonLabel, nameof(instance.previousGearButtonLabel), 4, 8);
        AssertField(instance, instance.nextGearButtonLabel,     nameof(instance.nextGearButtonLabel),     4, 12);
        AssertField(instance, instance.dpadUpLabel,             nameof(instance.dpadUpLabel),             4, 16);
        AssertField(instance, instance.dpadDownLabel,           nameof(instance.dpadDownLabel),           4, 20);
        AssertField(instance, instance.dpadLeftLabel,           nameof(instance.dpadLeftLabel),           4, 24);
        AssertField(instance, instance.dpadRightLabel,          nameof(instance.dpadRightLabel),          4, 28);
        AssertField(instance, instance.hasClutch,               nameof(instance.hasClutch),               1, 32);
        AssertField(instance, instance.hasHandbrake,            nameof(instance.hasHandbrake),            1, 33);
        AssertField(instance, instance.hasPatternShifter,       nameof(instance.hasPatternShifter),       1, 34);
        AssertField(instance, instance.minPatternShifterGear,   nameof(instance.minPatternShifterGear),   4, 36);
        AssertField(instance, instance.maxPatternShifterGear,   nameof(instance.maxPatternShifterGear),   4, 40);
        AssertField(instance, instance.maxWheelAngle,           nameof(instance.maxWheelAngle),           4, 44);
    });

    [Test]
    public unsafe void GameInputUiNavigationInfo() => Assert.Multiple(() =>
    {
        GameInputUiNavigationInfo instance = default;
        AssertSize(instance, 84);
        AssertField(instance, instance.menuButtonLabel,        nameof(instance.menuButtonLabel),        4, 0);
        AssertField(instance, instance.viewButtonLabel,        nameof(instance.viewButtonLabel),        4, 4);
        AssertField(instance, instance.acceptButtonLabel,      nameof(instance.acceptButtonLabel),      4, 8);
        AssertField(instance, instance.cancelButtonLabel,      nameof(instance.cancelButtonLabel),      4, 12);
        AssertField(instance, instance.upButtonLabel,          nameof(instance.upButtonLabel),          4, 16);
        AssertField(instance, instance.downButtonLabel,        nameof(instance.downButtonLabel),        4, 20);
        AssertField(instance, instance.leftButtonLabel,        nameof(instance.leftButtonLabel),        4, 24);
        AssertField(instance, instance.rightButtonLabel,       nameof(instance.rightButtonLabel),       4, 28);
        AssertField(instance, instance.contextButton1Label,    nameof(instance.contextButton1Label),    4, 32);
        AssertField(instance, instance.contextButton2Label,    nameof(instance.contextButton2Label),    4, 36);
        AssertField(instance, instance.contextButton3Label,    nameof(instance.contextButton3Label),    4, 40);
        AssertField(instance, instance.contextButton4Label,    nameof(instance.contextButton4Label),    4, 44);
        AssertField(instance, instance.pageUpButtonLabel,      nameof(instance.pageUpButtonLabel),      4, 48);
        AssertField(instance, instance.pageDownButtonLabel,    nameof(instance.pageDownButtonLabel),    4, 52);
        AssertField(instance, instance.pageLeftButtonLabel,    nameof(instance.pageLeftButtonLabel),    4, 56);
        AssertField(instance, instance.pageRightButtonLabel,   nameof(instance.pageRightButtonLabel),   4, 60);
        AssertField(instance, instance.scrollUpButtonLabel,    nameof(instance.scrollUpButtonLabel),    4, 64);
        AssertField(instance, instance.scrollDownButtonLabel,  nameof(instance.scrollDownButtonLabel),  4, 68);
        AssertField(instance, instance.scrollLeftButtonLabel,  nameof(instance.scrollLeftButtonLabel),  4, 72);
        AssertField(instance, instance.scrollRightButtonLabel, nameof(instance.scrollRightButtonLabel), 4, 76);
        AssertField(instance, instance.guideButtonLabel,       nameof(instance.guideButtonLabel),       4, 80);
    });

    [Test]
    public unsafe void GameInputForceFeedbackMotorInfo() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackMotorInfo instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.supportedAxes,                     nameof(instance.supportedAxes),                     4, 0);
        AssertField(instance, instance.location,                          nameof(instance.location),                          4, 4);
        AssertField(instance, instance.locationId,                        nameof(instance.locationId),                        4, 8);
        AssertField(instance, instance.maxSimultaneousEffects,            nameof(instance.maxSimultaneousEffects),            4, 12);
        AssertField(instance, instance.isConstantEffectSupported,         nameof(instance.isConstantEffectSupported),         1, 16);
        AssertField(instance, instance.isRampEffectSupported,             nameof(instance.isRampEffectSupported),             1, 17);
        AssertField(instance, instance.isSineWaveEffectSupported,         nameof(instance.isSineWaveEffectSupported),         1, 18);
        AssertField(instance, instance.isSquareWaveEffectSupported,       nameof(instance.isSquareWaveEffectSupported),       1, 19);
        AssertField(instance, instance.isTriangleWaveEffectSupported,     nameof(instance.isTriangleWaveEffectSupported),     1, 20);
        AssertField(instance, instance.isSawtoothUpWaveEffectSupported,   nameof(instance.isSawtoothUpWaveEffectSupported),   1, 21);
        AssertField(instance, instance.isSawtoothDownWaveEffectSupported, nameof(instance.isSawtoothDownWaveEffectSupported), 1, 22);
        AssertField(instance, instance.isSpringEffectSupported,           nameof(instance.isSpringEffectSupported),           1, 23);
        AssertField(instance, instance.isFrictionEffectSupported,         nameof(instance.isFrictionEffectSupported),         1, 24);
        AssertField(instance, instance.isDamperEffectSupported,           nameof(instance.isDamperEffectSupported),           1, 25);
        AssertField(instance, instance.isInertiaEffectSupported,          nameof(instance.isInertiaEffectSupported),          1, 26);
    });

    [Test]
    public unsafe void GameInputHapticWaveformInfo() => Assert.Multiple(() =>
    {
        GameInputHapticWaveformInfo instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.usage,                  nameof(instance.usage),                  4, 0);
        AssertField(instance, instance.isDurationSupported,    nameof(instance.isDurationSupported),    1, 4);
        AssertField(instance, instance.isIntensitySupported,   nameof(instance.isIntensitySupported),   1, 5);
        AssertField(instance, instance.isRepeatSupported,      nameof(instance.isRepeatSupported),      1, 6);
        AssertField(instance, instance.isRepeatDelaySupported, nameof(instance.isRepeatDelaySupported), 1, 7);
        AssertField(instance, instance.defaultDuration,        nameof(instance.defaultDuration),        8, 8);
    });

    [Test]
    public unsafe void GameInputHapticFeedbackMotorInfo() => Assert.Multiple(() =>
    {
        GameInputHapticFeedbackMotorInfo instance = default;
        AssertSize(instance, 24);
        AssertField(instance, instance.mappedRumbleMotors, nameof(instance.mappedRumbleMotors), 4, 0);
        AssertField(instance, instance.location,           nameof(instance.location),           4, 4);
        AssertField(instance, instance.locationId,         nameof(instance.locationId),         4, 8);
        AssertField(instance, instance.waveformCount,      nameof(instance.waveformCount),      4, 12);
        AssertField(instance, instance.waveformInfo,       nameof(instance.waveformInfo),          16);
    });

    [Test]
    public unsafe void GameInputDeviceInfo() => Assert.Multiple(() =>
    {
        GameInputDeviceInfo instance = default;
        AssertSize(instance, 320);
        AssertField(instance, instance.infoSize,                 nameof(instance.infoSize),                 4, 0);
        AssertField(instance, instance.vendorId,                 nameof(instance.vendorId),                 2, 4);
        AssertField(instance, instance.productId,                nameof(instance.productId),                2, 6);
        AssertField(instance, instance.revisionNumber,           nameof(instance.revisionNumber),           2, 8);
        AssertField(instance, instance.interfaceNumber,          nameof(instance.interfaceNumber),          1, 10);
        AssertField(instance, instance.collectionNumber,         nameof(instance.collectionNumber),         1, 11);
        AssertField(instance, instance.usage,                    nameof(instance.usage),                    4, 12);
        AssertField(instance, instance.hardwareVersion,          nameof(instance.hardwareVersion),          8, 16);
        AssertField(instance, instance.firmwareVersion,          nameof(instance.firmwareVersion),          8, 24);
        AssertField(instance, instance.deviceId,                 nameof(instance.deviceId),                 32, 32);
        AssertField(instance, instance.deviceRootId,             nameof(instance.deviceRootId),             32, 64);
        AssertField(instance, instance.deviceFamily,             nameof(instance.deviceFamily),             4, 96);
        AssertField(instance, instance.capabilities,             nameof(instance.capabilities),             4, 100);
        AssertField(instance, instance.supportedInput,           nameof(instance.supportedInput),           4, 104);
        AssertField(instance, instance.supportedRumbleMotors,    nameof(instance.supportedRumbleMotors),    4, 108);
        AssertField(instance, instance.inputReportCount,         nameof(instance.inputReportCount),         4, 112);
        AssertField(instance, instance.outputReportCount,        nameof(instance.outputReportCount),        4, 116);
        AssertField(instance, instance.featureReportCount,       nameof(instance.featureReportCount),       4, 120);
        AssertField(instance, instance.controllerAxisCount,      nameof(instance.controllerAxisCount),      4, 124);
        AssertField(instance, instance.controllerButtonCount,    nameof(instance.controllerButtonCount),    4, 128);
        AssertField(instance, instance.controllerSwitchCount,    nameof(instance.controllerSwitchCount),    4, 132);
        AssertField(instance, instance.touchPointCount,          nameof(instance.touchPointCount),          4, 136);
        AssertField(instance, instance.touchSensorCount,         nameof(instance.touchSensorCount),         4, 140);
        AssertField(instance, instance.forceFeedbackMotorCount,  nameof(instance.forceFeedbackMotorCount),  4, 144);
        AssertField(instance, instance.hapticFeedbackMotorCount, nameof(instance.hapticFeedbackMotorCount), 4, 148);
        AssertField(instance, instance.deviceStringCount,        nameof(instance.deviceStringCount),        4, 152);
        AssertField(instance, instance.deviceDescriptorSize,     nameof(instance.deviceDescriptorSize),     4, 156);
        AssertField(instance, instance.inputReportInfo,          nameof(instance.inputReportInfo),             160);
        AssertField(instance, instance.outputReportInfo,         nameof(instance.outputReportInfo),            168);
        AssertField(instance, instance.featureReportInfo,        nameof(instance.featureReportInfo),           176);
        AssertField(instance, instance.controllerAxisInfo,       nameof(instance.controllerAxisInfo),          184);
        AssertField(instance, instance.controllerButtonInfo,     nameof(instance.controllerButtonInfo),        192);
        AssertField(instance, instance.controllerSwitchInfo,     nameof(instance.controllerSwitchInfo),        200);
        AssertField(instance, instance.keyboardInfo,             nameof(instance.keyboardInfo),                208);
        AssertField(instance, instance.mouseInfo,                nameof(instance.mouseInfo),                   216);
        AssertField(instance, instance.touchSensorInfo,          nameof(instance.touchSensorInfo),             224);
        AssertField(instance, instance.motionInfo,               nameof(instance.motionInfo),                  232);
        AssertField(instance, instance.arcadeStickInfo,          nameof(instance.arcadeStickInfo),             240);
        AssertField(instance, instance.flightStickInfo,          nameof(instance.flightStickInfo),             248);
        AssertField(instance, instance.gamepadInfo,              nameof(instance.gamepadInfo),                 256);
        AssertField(instance, instance.racingWheelInfo,          nameof(instance.racingWheelInfo),             264);
        AssertField(instance, instance.uiNavigationInfo,         nameof(instance.uiNavigationInfo),            272);
        AssertField(instance, instance.forceFeedbackMotorInfo,   nameof(instance.forceFeedbackMotorInfo),      280);
        AssertField(instance, instance.hapticFeedbackMotorInfo,  nameof(instance.hapticFeedbackMotorInfo),     288);
        AssertField(instance, instance.displayName,              nameof(instance.displayName),                 296);
        AssertField(instance, instance.deviceStrings,            nameof(instance.deviceStrings),               304);
        AssertField(instance, instance.deviceDescriptorData,     nameof(instance.deviceDescriptorData),        312);
    });

    [Test]
    public unsafe void GameInputForceFeedbackEnvelope() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackEnvelope instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.attackDuration,  nameof(instance.attackDuration),  8, 0);
        AssertField(instance, instance.sustainDuration, nameof(instance.sustainDuration), 8, 8);
        AssertField(instance, instance.releaseDuration, nameof(instance.releaseDuration), 8, 16);
        AssertField(instance, instance.attackGain,      nameof(instance.attackGain),      4, 24);
        AssertField(instance, instance.sustainGain,     nameof(instance.sustainGain),     4, 28);
        AssertField(instance, instance.releaseGain,     nameof(instance.releaseGain),     4, 32);
        AssertField(instance, instance.playCount,       nameof(instance.playCount),       4, 36);
        AssertField(instance, instance.repeatDelay,     nameof(instance.repeatDelay),     8, 40);
    });

    [Test]
    public unsafe void GameInputForceFeedbackMagnitude() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackMagnitude instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.linearX,  nameof(instance.linearX),  4, 0);
        AssertField(instance, instance.linearY,  nameof(instance.linearY),  4, 4);
        AssertField(instance, instance.linearZ,  nameof(instance.linearZ),  4, 8);
        AssertField(instance, instance.angularX, nameof(instance.angularX), 4, 12);
        AssertField(instance, instance.angularY, nameof(instance.angularY), 4, 16);
        AssertField(instance, instance.angularZ, nameof(instance.angularZ), 4, 20);
        AssertField(instance, instance.normal,   nameof(instance.normal),   4, 24);
    });

    [Test]
    public unsafe void GameInputForceFeedbackConditionParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackConditionParams instance = default;
        AssertSize(instance, 52);
        AssertField(instance, instance.magnitude,            nameof(instance.magnitude),            28, 0);
        AssertField(instance, instance.positiveCoefficient,  nameof(instance.positiveCoefficient),  4,  28);
        AssertField(instance, instance.negativeCoefficient,  nameof(instance.negativeCoefficient),  4,  32);
        AssertField(instance, instance.maxPositiveMagnitude, nameof(instance.maxPositiveMagnitude), 4,  36);
        AssertField(instance, instance.maxNegativeMagnitude, nameof(instance.maxNegativeMagnitude), 4,  40);
        AssertField(instance, instance.deadZone,             nameof(instance.deadZone),             4,  44);
        AssertField(instance, instance.bias,                 nameof(instance.bias),                 4,  48);
    });

    [Test]
    public unsafe void GameInputForceFeedbackConstantParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackConstantParams instance = default;
        AssertSize(instance, 80);
        AssertField(instance, instance.envelope,  nameof(instance.envelope),  48, 0);
        AssertField(instance, instance.magnitude, nameof(instance.magnitude), 28, 48);
    });

    [Test]
    public unsafe void GameInputForceFeedbackPeriodicParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackPeriodicParams instance = default;
        AssertSize(instance, 88);
        AssertField(instance, instance.envelope,  nameof(instance.envelope),  48, 0);
        AssertField(instance, instance.magnitude, nameof(instance.magnitude), 28, 48);
        AssertField(instance, instance.frequency, nameof(instance.frequency), 4,  76);
        AssertField(instance, instance.phase,     nameof(instance.phase),     4,  80);
        AssertField(instance, instance.bias,      nameof(instance.bias),      4,  84);
    });

    [Test]
    public unsafe void GameInputForceFeedbackRampParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackRampParams instance = default;
        AssertSize(instance, 104);
        AssertField(instance, instance.envelope,       nameof(instance.envelope),       48, 0);
        AssertField(instance, instance.startMagnitude, nameof(instance.startMagnitude), 28, 48);
        AssertField(instance, instance.endMagnitude,   nameof(instance.endMagnitude),   28, 76);
    });

    [Test]
    public unsafe void GameInputForceFeedbackParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackParams instance = default;
        AssertSize(instance, 112);
        AssertField(instance, instance.kind,              nameof(instance.kind),             4,   0);
        AssertField(instance, instance._constant,         nameof(instance._constant),         80,  4);
        AssertField(instance, instance._ramp,             nameof(instance._ramp),             104, 4);
        AssertField(instance, instance._sineWave,         nameof(instance._sineWave),         88,  4);
        AssertField(instance, instance._squareWave,       nameof(instance._squareWave),       88,  4);
        AssertField(instance, instance._triangleWave,     nameof(instance._triangleWave),     88,  4);
        AssertField(instance, instance._sawtoothUpWave,   nameof(instance._sawtoothUpWave),   88,  4);
        AssertField(instance, instance._sawtoothDownWave, nameof(instance._sawtoothDownWave), 88,  4);
        AssertField(instance, instance._spring,           nameof(instance._spring),           52,  4);
        AssertField(instance, instance._friction,         nameof(instance._friction),         52,  4);
        AssertField(instance, instance._damper,           nameof(instance._damper),           52,  4);
        AssertField(instance, instance._inertia,          nameof(instance._inertia),          52,  4);
    });

    [Test]
    public unsafe void GameInputHapticFeedbackParams() => Assert.Multiple(() =>
    {
        GameInputHapticFeedbackParams instance = default;
        AssertSize(instance, 32);
        AssertField(instance, instance.waveformIndex, nameof(instance.waveformIndex), 4, 0);
        AssertField(instance, instance.duration,      nameof(instance.duration),      8, 8);
        AssertField(instance, instance.intensity,     nameof(instance.intensity),     4, 16);
        AssertField(instance, instance.playCount,     nameof(instance.playCount),     4, 20);
        AssertField(instance, instance.repeatDelay,   nameof(instance.repeatDelay),   8, 24);
    });

    [Test]
    public unsafe void GameInputRumbleParams() => Assert.Multiple(() =>
    {
        GameInputRumbleParams instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.lowFrequency,  nameof(instance.lowFrequency),  4, 0);
        AssertField(instance, instance.highFrequency, nameof(instance.highFrequency), 4, 4);
        AssertField(instance, instance.leftTrigger,   nameof(instance.leftTrigger),   4, 8);
        AssertField(instance, instance.rightTrigger,  nameof(instance.rightTrigger),  4, 12);
    });
}