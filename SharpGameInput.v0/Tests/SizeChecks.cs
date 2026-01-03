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
    public void ByteBool() => Assert.Multiple(() =>
    {
        ByteBool instance = default;
        AssertSize(instance, 1);
    });

    [Test]
    public unsafe void APP_LOCAL_DEVICE_ID() => Assert.Multiple(() =>
    {
        APP_LOCAL_DEVICE_ID instance = default;
        AssertSize(instance, 32);
        AssertFixed(instance, instance.value, 0);
    });

    [Test]
    public void GameInputKeyState() => Assert.Multiple(() =>
    {
        GameInputKeyState instance = default;
        AssertSize(instance, 12);
        AssertField(instance, instance.scanCode,   4, 0);
        AssertField(instance, instance.codePoint,  4, 4);
        AssertField(instance, instance.virtualKey, 1, 8);
        AssertField(instance, instance.isDeadKey,  1, 9);
    });

    [Test]
    public void GameInputMouseState() => Assert.Multiple(() =>
    {
        GameInputMouseState instance = default;
        AssertSize(instance, 40);
        AssertField(instance, instance.buttons,   4, 0);
        AssertField(instance, instance.positionX, 8, 8);
        AssertField(instance, instance.positionY, 8, 16);
        AssertField(instance, instance.wheelX,    8, 24);
        AssertField(instance, instance.wheelY,    8, 32);
    });

    [Test]
    public void GameInputTouchState() => Assert.Multiple(() =>
    {
        GameInputTouchState instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.touchId,           8, 0);
        AssertField(instance, instance.sensorIndex,       4, 8);
        AssertField(instance, instance.positionX,         4, 12);
        AssertField(instance, instance.positionY,         4, 16);
        AssertField(instance, instance.pressure,          4, 20);
        AssertField(instance, instance.proximity,         4, 24);
        AssertField(instance, instance.contactRectTop,    4, 28);
        AssertField(instance, instance.contactRectLeft,   4, 32);
        AssertField(instance, instance.contactRectRight,  4, 36);
        AssertField(instance, instance.contactRectBottom, 4, 40);
    });

    [Test]
    public void GameInputMotionState() => Assert.Multiple(() =>
    {
        GameInputMotionState instance = default;
        AssertSize(instance, 68);
        AssertField(instance, instance.accelerationX,         4, 0);
        AssertField(instance, instance.accelerationY,         4, 4);
        AssertField(instance, instance.accelerationZ,         4, 8);
        AssertField(instance, instance.angularVelocityX,      4, 12);
        AssertField(instance, instance.angularVelocityY,      4, 16);
        AssertField(instance, instance.angularVelocityZ,      4, 20);
        AssertField(instance, instance.magneticFieldX,        4, 24);
        AssertField(instance, instance.magneticFieldY,        4, 28);
        AssertField(instance, instance.magneticFieldZ,        4, 32);
        AssertField(instance, instance.orientationW,          4, 36);
        AssertField(instance, instance.orientationX,          4, 40);
        AssertField(instance, instance.orientationY,          4, 44);
        AssertField(instance, instance.orientationZ,          4, 48);
        AssertField(instance, instance.accelerometerAccuracy, 4, 52);
        AssertField(instance, instance.gyroscopeAccuracy,     4, 56);
        AssertField(instance, instance.magnetometerAccuracy,  4, 60);
        AssertField(instance, instance.orientationAccuracy,   4, 64);
    });

    [Test]
    public void GameInputArcadeStickState() => Assert.Multiple(() =>
    {
        GameInputArcadeStickState instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.buttons, 4, 0);
    });

    [Test]
    public void GameInputFlightStickState() => Assert.Multiple(() =>
    {
        GameInputFlightStickState instance = default;
        AssertSize(instance, 24);
        AssertField(instance, instance.buttons,   4, 0);
        AssertField(instance, instance.hatSwitch, 4, 4);
        AssertField(instance, instance.roll,      4, 8);
        AssertField(instance, instance.pitch,     4, 12);
        AssertField(instance, instance.yaw,       4, 16);
        AssertField(instance, instance.throttle,  4, 20);
    });

    [Test]
    public void GameInputGamepadState() => Assert.Multiple(() =>
    {
        GameInputGamepadState instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.buttons,          4, 0);
        AssertField(instance, instance.leftTrigger,      4, 4);
        AssertField(instance, instance.rightTrigger,     4, 8);
        AssertField(instance, instance.leftThumbstickX,  4, 12);
        AssertField(instance, instance.leftThumbstickY,  4, 16);
        AssertField(instance, instance.rightThumbstickX, 4, 20);
        AssertField(instance, instance.rightThumbstickY, 4, 24);
    });

    [Test]
    public void GameInputRacingWheelState() => Assert.Multiple(() =>
    {
        GameInputRacingWheelState instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.buttons,            4, 0);
        AssertField(instance, instance.patternShifterGear, 4, 4);
        AssertField(instance, instance.wheel,              4, 8);
        AssertField(instance, instance.throttle,           4, 12);
        AssertField(instance, instance.brake,              4, 16);
        AssertField(instance, instance.clutch,             4, 20);
        AssertField(instance, instance.handbrake,          4, 24);
    });

    [Test]
    public void GameInputUiNavigationState() => Assert.Multiple(() =>
    {
        GameInputUiNavigationState instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.buttons, 4, 0);
    });

    [Test]
    public void GameInputBatteryState() => Assert.Multiple(() =>
    {
        GameInputBatteryState instance = default;
        AssertSize(instance, 20);
        AssertField(instance, instance.chargeRate,         4, 0);
        AssertField(instance, instance.maxChargeRate,      4, 4);
        AssertField(instance, instance.remainingCapacity,  4, 8);
        AssertField(instance, instance.fullChargeCapacity, 4, 12);
        AssertField(instance, instance.status,             4, 16);
    });

    [Test]
    public unsafe void GameInputString() => Assert.Multiple(() =>
    {
        GameInputString instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.sizeInBytes,    4, 0);
        AssertField(instance, instance.codePointCount, 4, 4);
        AssertField(instance, instance.data,              8);
    });

    [Test]
    public void GameInputUsage() => Assert.Multiple(() =>
    {
        GameInputUsage instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.page, 2, 0);
        AssertField(instance, instance.id,   2, 2);
    });

    [Test]
    public void GameInputVersion() => Assert.Multiple(() =>
    {
        GameInputVersion instance = default;
        AssertSize(instance, 8);
        AssertField(instance, instance.major,    2, 0);
        AssertField(instance, instance.minor,    2, 2);
        AssertField(instance, instance.build,    2, 4);
        AssertField(instance, instance.revision, 2, 6);
    });

    [Test]
    public unsafe void GameInputRawDeviceItemCollectionInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceItemCollectionInfo instance = default;
        AssertSize(instance, 80);
        AssertField(instance, instance.kind,            4, 0);
        AssertField(instance, instance.childCount,      4, 4);
        AssertField(instance, instance.siblingCount,    4, 8);
        AssertField(instance, instance.usageCount,      4, 12);
        AssertField(instance, instance.usages,             16);
        AssertField(instance, instance.parent,             24);
        AssertField(instance, instance.firstSibling,       32);
        AssertField(instance, instance.previousSibling,    40);
        AssertField(instance, instance.nextSibling,        48);
        AssertField(instance, instance.lastSibling,        56);
        AssertField(instance, instance.firstChild,         64);
        AssertField(instance, instance.lastChild,          72);
    });

    [Test]
    public unsafe void GameInputRawDeviceReportItemInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceReportItemInfo instance = default;
        AssertSize(instance, 88);
        AssertField(instance, instance.bitOffset,                4, 0);
        AssertField(instance, instance.bitSize,                  4, 4);
        AssertField(instance, instance.logicalMin,               8, 8);
        AssertField(instance, instance.logicalMax,               8, 16);
        AssertField(instance, instance.physicalMin,              8, 24);
        AssertField(instance, instance.physicalMax,              8, 32);
        AssertField(instance, instance.physicalUnits,            4, 40);
        AssertField(instance, instance.rawPhysicalUnits,         4, 44);
        AssertField(instance, instance.rawPhysicalUnitsExponent, 4, 48);
        AssertField(instance, instance.flags,                    4, 52);
        AssertField(instance, instance.usageCount,               4, 56);
        AssertField(instance, instance.usages,                      64);
        AssertField(instance, instance.collection,                  72);
        AssertField(instance, instance.itemString,                  80);
    });

    [Test]
    public unsafe void GameInputRawDeviceReportInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceReportInfo instance = default;
        AssertSize(instance, 24);
        AssertField(instance, instance.kind,      4, 0);
        AssertField(instance, instance.id,        4, 4);
        AssertField(instance, instance.size,      4, 8);
        AssertField(instance, instance.itemCount, 4, 12);
        AssertField(instance, instance.items,        16);
    });

    [Test]
    public unsafe void GameInputControllerAxisInfo() => Assert.Multiple(() =>
    {
        GameInputControllerAxisInfo instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.mappedInputKinds,  4, 0);
        AssertField(instance, instance.label,             4, 4);
        AssertField(instance, instance.isContinuous,      1, 8);
        AssertField(instance, instance.isNonlinear,       1, 9);
        AssertField(instance, instance.isQuantized,       1, 10);
        AssertField(instance, instance.hasRestValue,      1, 11);
        AssertField(instance, instance.restValue,         4, 12);
        AssertField(instance, instance.resolution,        8, 16);
        AssertField(instance, instance.legacyDInputIndex, 2, 24);
        AssertField(instance, instance.legacyHidIndex,    2, 26);
        AssertField(instance, instance.rawReportIndex,    4, 28);
        AssertField(instance, instance.inputReport,          32);
        AssertField(instance, instance.inputReportItem,      40);
    });

    [Test]
    public unsafe void GameInputControllerButtonInfo() => Assert.Multiple(() =>
    {
        GameInputControllerButtonInfo instance = default;
        AssertSize(instance, 32);
        AssertField(instance, instance.mappedInputKinds,  4, 0);
        AssertField(instance, instance.label,             4, 4);
        AssertField(instance, instance.legacyDInputIndex, 2, 8);
        AssertField(instance, instance.legacyHidIndex,    2, 10);
        AssertField(instance, instance.rawReportIndex,    4, 12);
        AssertField(instance, instance.inputReport,          16);
        AssertField(instance, instance.inputReportItem,      24);
    });

    [Test]
    public unsafe void GameInputControllerSwitchInfo() => Assert.Multiple(() =>
    {
        GameInputControllerSwitchInfo instance = default;
        AssertSize(instance, 72);
        AssertField(instance, instance.mappedInputKinds,  4, 0);
        AssertField(instance, instance.label,             4, 4);
        AssertFixed(instance, instance._positionLabels,      8);
        AssertField(instance, instance.kind,              4, 44);
        AssertField(instance, instance.legacyDInputIndex, 2, 48);
        AssertField(instance, instance.legacyHidIndex,    2, 50);
        AssertField(instance, instance.rawReportIndex,    4, 52);
        AssertField(instance, instance.inputReport,          56);
        AssertField(instance, instance.inputReportItem,      64);
    });

    [Test]
    public unsafe void GameInputKeyboardInfo() => Assert.Multiple(() =>
    {
        GameInputKeyboardInfo instance = default;
        AssertSize(instance, 40);
        AssertField(instance, instance.kind,                4, 0);
        AssertField(instance, instance.layout,              4, 4);
        AssertField(instance, instance.keyCount,            4, 8);
        AssertField(instance, instance.functionKeyCount,    4, 12);
        AssertField(instance, instance.maxSimultaneousKeys, 4, 16);
        AssertField(instance, instance.platformType,        4, 20);
        AssertField(instance, instance.platformSubtype,     4, 24);
        AssertField(instance, instance.nativeLanguage,         32);
    });

    [Test]
    public void GameInputMouseInfo() => Assert.Multiple(() =>
    {
        GameInputMouseInfo instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.supportedButtons, 4, 0);
        AssertField(instance, instance.sampleRate,       4, 4);
        AssertField(instance, instance.sensorDpi,        4, 8);
        AssertField(instance, instance.hasWheelX,        1, 12);
        AssertField(instance, instance.hasWheelY,        1, 13);
    });

    [Test]
    public void GameInputTouchSensorInfo() => Assert.Multiple(() =>
    {
        GameInputTouchSensorInfo instance = default;
        AssertSize(instance, 64);
        AssertField(instance, instance.mappedInputKinds, 4, 0);
        AssertField(instance, instance.label,            4, 4);
        AssertField(instance, instance.location,         4, 8);
        AssertField(instance, instance.locationId,       4, 12);
        AssertField(instance, instance.resolutionX,      8, 16);
        AssertField(instance, instance.resolutionY,      8, 24);
        AssertField(instance, instance.shape,            4, 32);
        AssertField(instance, instance.aspectRatio,      4, 36);
        AssertField(instance, instance.orientation,      4, 40);
        AssertField(instance, instance.physicalWidth,    4, 44);
        AssertField(instance, instance.physicalHeight,   4, 48);
        AssertField(instance, instance.maxPressure,      4, 52);
        AssertField(instance, instance.maxProximity,     4, 56);
        AssertField(instance, instance.maxTouchPoints,   4, 60);
    });

    [Test]
    public void GameInputMotionInfo() => Assert.Multiple(() =>
    {
        GameInputMotionInfo instance = default;
        AssertSize(instance, 12);
        AssertField(instance, instance.maxAcceleration,          4, 0);
        AssertField(instance, instance.maxAngularVelocity,       4, 4);
        AssertField(instance, instance.maxMagneticFieldStrength, 4, 8);
    });

    [Test]
    public void GameInputArcadeStickInfo() => Assert.Multiple(() =>
    {
        GameInputArcadeStickInfo instance = default;
        AssertSize(instance, 56);
        AssertField(instance, instance.menuButtonLabel,     4, 0);
        AssertField(instance, instance.viewButtonLabel,     4, 4);
        AssertField(instance, instance.stickUpLabel,        4, 8);
        AssertField(instance, instance.stickDownLabel,      4, 12);
        AssertField(instance, instance.stickLeftLabel,      4, 16);
        AssertField(instance, instance.stickRightLabel,     4, 20);
        AssertField(instance, instance.actionButton1Label,  4, 24);
        AssertField(instance, instance.actionButton2Label,  4, 28);
        AssertField(instance, instance.actionButton3Label,  4, 32);
        AssertField(instance, instance.actionButton4Label,  4, 36);
        AssertField(instance, instance.actionButton5Label,  4, 40);
        AssertField(instance, instance.actionButton6Label,  4, 44);
        AssertField(instance, instance.specialButton1Label, 4, 48);
        AssertField(instance, instance.specialButton2Label, 4, 52);
    });

    [Test]
    public void GameInputFlightStickInfo() => Assert.Multiple(() =>
    {
        GameInputFlightStickInfo instance = default;
        AssertSize(instance, 20);
        AssertField(instance, instance.menuButtonLabel,          4, 0);
        AssertField(instance, instance.viewButtonLabel,          4, 4);
        AssertField(instance, instance.firePrimaryButtonLabel,   4, 8);
        AssertField(instance, instance.fireSecondaryButtonLabel, 4, 12);
        AssertField(instance, instance.hatSwitchKind,            4, 16);
    });

    [Test]
    public void GameInputGamepadInfo() => Assert.Multiple(() =>
    {
        GameInputGamepadInfo instance = default;
        AssertSize(instance, 56);
        AssertField(instance, instance.menuButtonLabel,            4, 0);
        AssertField(instance, instance.viewButtonLabel,            4, 4);
        AssertField(instance, instance.aButtonLabel,               4, 8);
        AssertField(instance, instance.bButtonLabel,               4, 12);
        AssertField(instance, instance.xButtonLabel,               4, 16);
        AssertField(instance, instance.yButtonLabel,               4, 20);
        AssertField(instance, instance.dpadUpLabel,                4, 24);
        AssertField(instance, instance.dpadDownLabel,              4, 28);
        AssertField(instance, instance.dpadLeftLabel,              4, 32);
        AssertField(instance, instance.dpadRightLabel,             4, 36);
        AssertField(instance, instance.leftShoulderButtonLabel,    4, 40);
        AssertField(instance, instance.rightShoulderButtonLabel,   4, 44);
        AssertField(instance, instance.leftThumbstickButtonLabel,  4, 48);
        AssertField(instance, instance.rightThumbstickButtonLabel, 4, 52);
    });

    [Test]
    public void GameInputRacingWheelInfo() => Assert.Multiple(() =>
    {
        GameInputRacingWheelInfo instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.menuButtonLabel,         4, 0);
        AssertField(instance, instance.viewButtonLabel,         4, 4);
        AssertField(instance, instance.previousGearButtonLabel, 4, 8);
        AssertField(instance, instance.nextGearButtonLabel,     4, 12);
        AssertField(instance, instance.dpadUpLabel,             4, 16);
        AssertField(instance, instance.dpadDownLabel,           4, 20);
        AssertField(instance, instance.dpadLeftLabel,           4, 24);
        AssertField(instance, instance.dpadRightLabel,          4, 28);
        AssertField(instance, instance.hasClutch,               1, 32);
        AssertField(instance, instance.hasHandbrake,            1, 33);
        AssertField(instance, instance.hasPatternShifter,       1, 34);
        AssertField(instance, instance.minPatternShifterGear,   4, 36);
        AssertField(instance, instance.maxPatternShifterGear,   4, 40);
        AssertField(instance, instance.maxWheelAngle,           4, 44);
    });

    [Test]
    public void GameInputUiNavigationInfo() => Assert.Multiple(() =>
    {
        GameInputUiNavigationInfo instance = default;
        AssertSize(instance, 84);
        AssertField(instance, instance.menuButtonLabel,        4, 0);
        AssertField(instance, instance.viewButtonLabel,        4, 4);
        AssertField(instance, instance.acceptButtonLabel,      4, 8);
        AssertField(instance, instance.cancelButtonLabel,      4, 12);
        AssertField(instance, instance.upButtonLabel,          4, 16);
        AssertField(instance, instance.downButtonLabel,        4, 20);
        AssertField(instance, instance.leftButtonLabel,        4, 24);
        AssertField(instance, instance.rightButtonLabel,       4, 28);
        AssertField(instance, instance.contextButton1Label,    4, 32);
        AssertField(instance, instance.contextButton2Label,    4, 36);
        AssertField(instance, instance.contextButton3Label,    4, 40);
        AssertField(instance, instance.contextButton4Label,    4, 44);
        AssertField(instance, instance.pageUpButtonLabel,      4, 48);
        AssertField(instance, instance.pageDownButtonLabel,    4, 52);
        AssertField(instance, instance.pageLeftButtonLabel,    4, 56);
        AssertField(instance, instance.pageRightButtonLabel,   4, 60);
        AssertField(instance, instance.scrollUpButtonLabel,    4, 64);
        AssertField(instance, instance.scrollDownButtonLabel,  4, 68);
        AssertField(instance, instance.scrollLeftButtonLabel,  4, 72);
        AssertField(instance, instance.scrollRightButtonLabel, 4, 76);
        AssertField(instance, instance.guideButtonLabel,       4, 80);
    });

    [Test]
    public void GameInputForceFeedbackMotorInfo() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackMotorInfo instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.supportedAxes,                     4, 0);
        AssertField(instance, instance.location,                          4, 4);
        AssertField(instance, instance.locationId,                        4, 8);
        AssertField(instance, instance.maxSimultaneousEffects,            4, 12);
        AssertField(instance, instance.isConstantEffectSupported,         1, 16);
        AssertField(instance, instance.isRampEffectSupported,             1, 17);
        AssertField(instance, instance.isSineWaveEffectSupported,         1, 18);
        AssertField(instance, instance.isSquareWaveEffectSupported,       1, 19);
        AssertField(instance, instance.isTriangleWaveEffectSupported,     1, 20);
        AssertField(instance, instance.isSawtoothUpWaveEffectSupported,   1, 21);
        AssertField(instance, instance.isSawtoothDownWaveEffectSupported, 1, 22);
        AssertField(instance, instance.isSpringEffectSupported,           1, 23);
        AssertField(instance, instance.isFrictionEffectSupported,         1, 24);
        AssertField(instance, instance.isDamperEffectSupported,           1, 25);
        AssertField(instance, instance.isInertiaEffectSupported,          1, 26);
    });

    [Test]
    public void GameInputHapticWaveformInfo() => Assert.Multiple(() =>
    {
        GameInputHapticWaveformInfo instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.usage,                  4, 0);
        AssertField(instance, instance.isDurationSupported,    1, 4);
        AssertField(instance, instance.isIntensitySupported,   1, 5);
        AssertField(instance, instance.isRepeatSupported,      1, 6);
        AssertField(instance, instance.isRepeatDelaySupported, 1, 7);
        AssertField(instance, instance.defaultDuration,        8, 8);
    });

    [Test]
    public unsafe void GameInputHapticFeedbackMotorInfo() => Assert.Multiple(() =>
    {
        GameInputHapticFeedbackMotorInfo instance = default;
        AssertSize(instance, 24);
        AssertField(instance, instance.mappedRumbleMotors, 4, 0);
        AssertField(instance, instance.location,           4, 4);
        AssertField(instance, instance.locationId,         4, 8);
        AssertField(instance, instance.waveformCount,      4, 12);
        AssertField(instance, instance.waveformInfo,          16);
    });

    [Test]
    public unsafe void GameInputDeviceInfo() => Assert.Multiple(() =>
    {
        GameInputDeviceInfo instance = default;
        AssertSize(instance, 320);
        AssertField(instance, instance.infoSize,                 4, 0);
        AssertField(instance, instance.vendorId,                 2, 4);
        AssertField(instance, instance.productId,                2, 6);
        AssertField(instance, instance.revisionNumber,           2, 8);
        AssertField(instance, instance.interfaceNumber,          1, 10);
        AssertField(instance, instance.collectionNumber,         1, 11);
        AssertField(instance, instance.usage,                    4, 12);
        AssertField(instance, instance.hardwareVersion,          8, 16);
        AssertField(instance, instance.firmwareVersion,          8, 24);
        AssertField(instance, instance.deviceId,                 32, 32);
        AssertField(instance, instance.deviceRootId,             32, 64);
        AssertField(instance, instance.deviceFamily,             4, 96);
        AssertField(instance, instance.capabilities,             4, 100);
        AssertField(instance, instance.supportedInput,           4, 104);
        AssertField(instance, instance.supportedRumbleMotors,    4, 108);
        AssertField(instance, instance.inputReportCount,         4, 112);
        AssertField(instance, instance.outputReportCount,        4, 116);
        AssertField(instance, instance.featureReportCount,       4, 120);
        AssertField(instance, instance.controllerAxisCount,      4, 124);
        AssertField(instance, instance.controllerButtonCount,    4, 128);
        AssertField(instance, instance.controllerSwitchCount,    4, 132);
        AssertField(instance, instance.touchPointCount,          4, 136);
        AssertField(instance, instance.touchSensorCount,         4, 140);
        AssertField(instance, instance.forceFeedbackMotorCount,  4, 144);
        AssertField(instance, instance.hapticFeedbackMotorCount, 4, 148);
        AssertField(instance, instance.deviceStringCount,        4, 152);
        AssertField(instance, instance.deviceDescriptorSize,     4, 156);
        AssertField(instance, instance.inputReportInfo,             160);
        AssertField(instance, instance.outputReportInfo,            168);
        AssertField(instance, instance.featureReportInfo,           176);
        AssertField(instance, instance._controllerAxisInfo,          184);
        AssertField(instance, instance._controllerButtonInfo,        192);
        AssertField(instance, instance._controllerSwitchInfo,        200);
        AssertField(instance, instance.keyboardInfo,                208);
        AssertField(instance, instance.mouseInfo,                   216);
        AssertField(instance, instance.touchSensorInfo,             224);
        AssertField(instance, instance.motionInfo,                  232);
        AssertField(instance, instance.arcadeStickInfo,             240);
        AssertField(instance, instance.flightStickInfo,             248);
        AssertField(instance, instance.gamepadInfo,                 256);
        AssertField(instance, instance.racingWheelInfo,             264);
        AssertField(instance, instance.uiNavigationInfo,            272);
        AssertField(instance, instance._forceFeedbackMotorInfo,      280);
        AssertField(instance, instance._hapticFeedbackMotorInfo,     288);
        AssertField(instance, instance.displayName,                 296);
        AssertField(instance, instance.deviceStrings,               304);
        AssertField(instance, instance.deviceDescriptorData,        312);
    });

    [Test]
    public void GameInputForceFeedbackEnvelope() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackEnvelope instance = default;
        AssertSize(instance, 48);
        AssertField(instance, instance.attackDuration,  8, 0);
        AssertField(instance, instance.sustainDuration, 8, 8);
        AssertField(instance, instance.releaseDuration, 8, 16);
        AssertField(instance, instance.attackGain,      4, 24);
        AssertField(instance, instance.sustainGain,     4, 28);
        AssertField(instance, instance.releaseGain,     4, 32);
        AssertField(instance, instance.playCount,       4, 36);
        AssertField(instance, instance.repeatDelay,     8, 40);
    });

    [Test]
    public void GameInputForceFeedbackMagnitude() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackMagnitude instance = default;
        AssertSize(instance, 28);
        AssertField(instance, instance.linearX,  4, 0);
        AssertField(instance, instance.linearY,  4, 4);
        AssertField(instance, instance.linearZ,  4, 8);
        AssertField(instance, instance.angularX, 4, 12);
        AssertField(instance, instance.angularY, 4, 16);
        AssertField(instance, instance.angularZ, 4, 20);
        AssertField(instance, instance.normal,   4, 24);
    });

    [Test]
    public void GameInputForceFeedbackConditionParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackConditionParams instance = default;
        AssertSize(instance, 52);
        AssertField(instance, instance.magnitude,            28, 0);
        AssertField(instance, instance.positiveCoefficient,  4,  28);
        AssertField(instance, instance.negativeCoefficient,  4,  32);
        AssertField(instance, instance.maxPositiveMagnitude, 4,  36);
        AssertField(instance, instance.maxNegativeMagnitude, 4,  40);
        AssertField(instance, instance.deadZone,             4,  44);
        AssertField(instance, instance.bias,                 4,  48);
    });

    [Test]
    public void GameInputForceFeedbackConstantParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackConstantParams instance = default;
        AssertSize(instance, 80);
        AssertField(instance, instance.envelope,  48, 0);
        AssertField(instance, instance.magnitude, 28, 48);
    });

    [Test]
    public void GameInputForceFeedbackPeriodicParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackPeriodicParams instance = default;
        AssertSize(instance, 88);
        AssertField(instance, instance.envelope,  48, 0);
        AssertField(instance, instance.magnitude, 28, 48);
        AssertField(instance, instance.frequency, 4,  76);
        AssertField(instance, instance.phase,     4,  80);
        AssertField(instance, instance.bias,      4,  84);
    });

    [Test]
    public void GameInputForceFeedbackRampParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackRampParams instance = default;
        AssertSize(instance, 104);
        AssertField(instance, instance.envelope,       48, 0);
        AssertField(instance, instance.startMagnitude, 28, 48);
        AssertField(instance, instance.endMagnitude,   28, 76);
    });

    [Test]
    public void GameInputForceFeedbackParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackParams instance = default;
        AssertSize(instance, 112);
        AssertField(instance, instance.kind,              4,   0);
        AssertField(instance, instance._constant,         80,  4);
        AssertField(instance, instance._ramp,             104, 4);
        AssertField(instance, instance._sineWave,         88,  4);
        AssertField(instance, instance._squareWave,       88,  4);
        AssertField(instance, instance._triangleWave,     88,  4);
        AssertField(instance, instance._sawtoothUpWave,   88,  4);
        AssertField(instance, instance._sawtoothDownWave, 88,  4);
        AssertField(instance, instance._spring,           52,  4);
        AssertField(instance, instance._friction,         52,  4);
        AssertField(instance, instance._damper,           52,  4);
        AssertField(instance, instance._inertia,          52,  4);
    });

    [Test]
    public void GameInputHapticFeedbackParams() => Assert.Multiple(() =>
    {
        GameInputHapticFeedbackParams instance = default;
        AssertSize(instance, 32);
        AssertField(instance, instance.waveformIndex, 4, 0);
        AssertField(instance, instance.duration,      8, 8);
        AssertField(instance, instance.intensity,     4, 16);
        AssertField(instance, instance.playCount,     4, 20);
        AssertField(instance, instance.repeatDelay,   8, 24);
    });

    [Test]
    public void GameInputRumbleParams() => Assert.Multiple(() =>
    {
        GameInputRumbleParams instance = default;
        AssertSize(instance, 16);
        AssertField(instance, instance.lowFrequency,  4, 0);
        AssertField(instance, instance.highFrequency, 4, 4);
        AssertField(instance, instance.leftTrigger,   4, 8);
        AssertField(instance, instance.rightTrigger,  4, 12);
    });
}