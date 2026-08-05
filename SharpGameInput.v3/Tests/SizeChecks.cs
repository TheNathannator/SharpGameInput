using static SharpGameInput.Common.Tests.SizeHelper;

namespace SharpGameInput.v3.Tests;

public class SizeChecks
{
    [Test] public void GameInputResult() => AssertEnum<GameInputResult>(4);
    [Test] public void GameInputKind() => AssertEnum<GameInputKind>(4);
    [Test] public void GameInputEnumerationKind() => AssertEnum<GameInputEnumerationKind>(4);
    [Test] public void GameInputFocusPolicy() => AssertEnum<GameInputFocusPolicy>(4);
    [Test] public void GameInputSwitchKind() => AssertEnum<GameInputSwitchKind>(4);
    [Test] public void GameInputSwitchPosition() => AssertEnum<GameInputSwitchPosition>(4);
    [Test] public void GameInputKeyboardKind() => AssertEnum<GameInputKeyboardKind>(4);
    [Test] public void GameInputMouseButtons() => AssertEnum<GameInputMouseButtons>(4);
    [Test] public void GameInputMousePositions() => AssertEnum<GameInputMousePositions>(4);
    [Test] public void GameInputSensorsKind() => AssertEnum<GameInputSensorsKind>(4);
    [Test] public void GameInputSensorAccuracy() => AssertEnum<GameInputSensorAccuracy>(4);
    [Test] public void GameInputArcadeStickButtons() => AssertEnum<GameInputArcadeStickButtons>(4);
    [Test] public void GameInputFlightStickButtons() => AssertEnum<GameInputFlightStickButtons>(4);
    [Test] public void GameInputGamepadButtons() => AssertEnum<GameInputGamepadButtons>(4);
    [Test] public void GameInputRawDeviceReportKind() => AssertEnum<GameInputGamepadButtons>(4);
    [Test] public void GameInputRacingWheelButtons() => AssertEnum<GameInputRacingWheelButtons>(4);
    [Test] public void GameInputSystemButtons() => AssertEnum<GameInputSystemButtons>(4);
    [Test] public void GameInputFlightStickAxes() => AssertEnum<GameInputFlightStickAxes>(4);
    [Test] public void GameInputGamepadAxes() => AssertEnum<GameInputGamepadAxes>(4);
    [Test] public void GameInputRacingWheelAxes() => AssertEnum<GameInputRacingWheelAxes>(4);
    [Test] public void GameInputDeviceStatus() => AssertEnum<GameInputDeviceStatus>(4);
    [Test] public void GameInputDeviceFamily() => AssertEnum<GameInputDeviceFamily>(4);
    [Test] public void GameInputLabel() => AssertEnum<GameInputLabel>(4);
    [Test] public void GameInputFeedbackAxes() => AssertEnum<GameInputFeedbackAxes>(4);
    [Test] public void GameInputFeedbackEffectState() => AssertEnum<GameInputFeedbackEffectState>(4);
    [Test] public void GameInputForceFeedbackEffectKind() => AssertEnum<GameInputForceFeedbackEffectKind>(4);
    [Test] public void GameInputRumbleMotors() => AssertEnum<GameInputRumbleMotors>(4);

    [Test]
    public void ByteBool() => Assert.Multiple(() =>
    {
        ByteBool instance = default;
        AssertStruct(instance, 1, 1);
    });

    [Test]
    public unsafe void APP_LOCAL_DEVICE_ID() => Assert.Multiple(() =>
    {
        APP_LOCAL_DEVICE_ID instance = default;
        AssertStruct(instance, 32, 1);
        AssertFixed(instance, instance.value, 0);
    });

    [Test]
    public void GameInputKeyState() => Assert.Multiple(() =>
    {
        GameInputKeyState instance = default;
        AssertStruct(instance, 12, 4);
        AssertField(instance, instance.scanCode,   4, 0);
        AssertField(instance, instance.codePoint,  4, 4);
        AssertField(instance, instance.virtualKey, 1, 8);
        AssertField(instance, instance.isDeadKey,  1, 9);
    });

    [Test]
    public void GameInputMouseState() => Assert.Multiple(() =>
    {
        GameInputMouseState instance = default;
        AssertStruct(instance, 56, 8);
        AssertField(instance, instance.buttons,           4, 0);
        AssertField(instance, instance.positions,         4, 4);
        AssertField(instance, instance.positionX,         8, 8);
        AssertField(instance, instance.positionY,         8, 16);
        AssertField(instance, instance.absolutePositionX, 8, 24);
        AssertField(instance, instance.absolutePositionY, 8, 32);
        AssertField(instance, instance.wheelX,            8, 40);
        AssertField(instance, instance.wheelY,            8, 48);
    });

    [Test]
    public void GameInputVersion() => Assert.Multiple(() =>
    {
        GameInputVersion instance = default;
        AssertStruct(instance, 8, 2);
        AssertField(instance, instance.major,    2, 0);
        AssertField(instance, instance.minor,    2, 2);
        AssertField(instance, instance.build,    2, 4);
        AssertField(instance, instance.revision, 2, 6);
    });

    [Test]
    public void GameInputSensorsState() => Assert.Multiple(() =>
    {
        GameInputSensorsState instance = default;
        AssertStruct(instance, 48, 4);
        AssertField(instance, instance.accelerationInGX,                  4, 0);
        AssertField(instance, instance.accelerationInGY,                  4, 4);
        AssertField(instance, instance.accelerationInGZ,                  4, 8);
        AssertField(instance, instance.angularVelocityInRadPerSecX,       4, 12);
        AssertField(instance, instance.angularVelocityInRadPerSecY,       4, 16);
        AssertField(instance, instance.angularVelocityInRadPerSecZ,       4, 20);
        AssertField(instance, instance.headingInDegreesFromMagneticNorth, 4, 24);
        AssertField(instance, instance.headingAccuracy,                   4, 28);
        AssertField(instance, instance.orientationW,                      4, 32);
        AssertField(instance, instance.orientationX,                      4, 36);
        AssertField(instance, instance.orientationY,                      4, 40);
        AssertField(instance, instance.orientationZ,                      4, 44);
    });

    [Test]
    public void GameInputArcadeStickState() => Assert.Multiple(() =>
    {
        GameInputArcadeStickState instance = default;
        AssertStruct(instance, 4, 4);
        AssertField(instance, instance.buttons, 4, 0);
    });

    [Test]
    public void GameInputFlightStickState() => Assert.Multiple(() =>
    {
        GameInputFlightStickState instance = default;
        AssertStruct(instance, 24, 4);
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
        AssertStruct(instance, 28, 4);
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
        AssertStruct(instance, 28, 4);
        AssertField(instance, instance.buttons,            4, 0);
        AssertField(instance, instance.patternShifterGear, 4, 4);
        AssertField(instance, instance.wheel,              4, 8);
        AssertField(instance, instance.throttle,           4, 12);
        AssertField(instance, instance.brake,              4, 16);
        AssertField(instance, instance.clutch,             4, 20);
        AssertField(instance, instance.handbrake,          4, 24);
    });

    [Test]
    public void GameInputUsage() => Assert.Multiple(() =>
    {
        GameInputUsage instance = default;
        AssertStruct(instance, 4, 2);
        AssertField(instance, instance.page, 2, 0);
        AssertField(instance, instance.id,   2, 2);
    });

    [Test]
    public unsafe void GameInputControllerSwitchInfo() => Assert.Multiple(() =>
    {
        GameInputControllerSwitchInfo instance = default;
        AssertStruct(instance, 36, 4);
        AssertFixed(instance, instance._labels,     0);
        AssertField(instance, instance.kind,     4, 32);
    });

    [Test]
    public unsafe void GameInputControllerInfo() => Assert.Multiple(() =>
    {
        GameInputControllerInfo instance = default;
        AssertStruct(instance, 48, 8);
        AssertField(instance, instance.controllerAxisCount,    4, 0);
        AssertField(instance, instance._controllerAxisLabels,     8);
        AssertField(instance, instance.controllerButtonCount,  4, 16);
        AssertField(instance, instance._controllerButtonLabels,   24);
        AssertField(instance, instance.controllerSwitchCount,  4, 32);
        AssertField(instance, instance._controllerSwitchInfo,     40);
    });

    [Test]
    public void GameInputKeyboardInfo() => Assert.Multiple(() =>
    {
        GameInputKeyboardInfo instance = default;
        AssertStruct(instance, 28, 4);
        AssertField(instance, instance.kind,                4, 0);
        AssertField(instance, instance.layout,              4, 4);
        AssertField(instance, instance.keyCount,            4, 8);
        AssertField(instance, instance.functionKeyCount,    4, 12);
        AssertField(instance, instance.maxSimultaneousKeys, 4, 16);
        AssertField(instance, instance.platformType,        4, 20);
        AssertField(instance, instance.platformSubtype,     4, 24);
    });

    [Test]
    public void GameInputMouseInfo() => Assert.Multiple(() =>
    {
        GameInputMouseInfo instance = default;
        AssertStruct(instance, 12, 4);
        AssertField(instance, instance.supportedButtons, 4, 0);
        AssertField(instance, instance.sampleRate,       4, 4);
        AssertField(instance, instance.hasWheelX,        1, 8);
        AssertField(instance, instance.hasWheelY,        1, 9);
    });

    [Test]
    public void GameInputSensorsInfo() => Assert.Multiple(() =>
    {
        GameInputSensorsInfo instance = default;
        AssertStruct(instance, 4, 4);
        AssertField(instance, instance.supportedSensors, 4, 0);
    });

    [Test]
    public void GameInputArcadeStickInfo() => Assert.Multiple(() =>
    {
        GameInputArcadeStickInfo instance = default;
        AssertStruct(instance, 64, 4);
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
        AssertField(instance, instance.extraButtonCount,    4, 56);
        AssertField(instance, instance.extraAxisCount,      4, 60);
    });

    [Test]
    public void GameInputFlightStickInfo() => Assert.Multiple(() =>
    {
        GameInputFlightStickInfo instance = default;
        AssertStruct(instance, 64, 4);
        AssertField(instance, instance.menuButtonLabel,          4, 0);
        AssertField(instance, instance.viewButtonLabel,          4, 4);
        AssertField(instance, instance.firePrimaryButtonLabel,   4, 8);
        AssertField(instance, instance.fireSecondaryButtonLabel, 4, 12);
        AssertField(instance, instance.hatSwitchUpLabel,         4, 16);
        AssertField(instance, instance.hatSwitchDownLabel,       4, 20);
        AssertField(instance, instance.hatSwitchLeftLabel,       4, 24);
        AssertField(instance, instance.hatSwitchRightLabel,      4, 28);
        AssertField(instance, instance.aButtonLabel,             4, 32);
        AssertField(instance, instance.bButtonLabel,             4, 36);
        AssertField(instance, instance.xButtonLabel,             4, 40);
        AssertField(instance, instance.yButtonLabel,             4, 44);
        AssertField(instance, instance.leftShoulderButtonLabel,  4, 48);
        AssertField(instance, instance.rightShoulderButtonLabel, 4, 52);
        AssertField(instance, instance.extraButtonCount,         4, 56);
        AssertField(instance, instance.extraAxisCount,           4, 60);
    });

    [Test]
    public void GameInputGamepadInfo() => Assert.Multiple(() =>
    {
        GameInputGamepadInfo instance = default;
        AssertStruct(instance, 76, 4);
        AssertField(instance, instance.supportedLayout,            4, 0);
        AssertField(instance, instance.menuButtonLabel,            4, 4);
        AssertField(instance, instance.viewButtonLabel,            4, 8);
        AssertField(instance, instance.aButtonLabel,               4, 12);
        AssertField(instance, instance.bButtonLabel,               4, 16);
        AssertField(instance, instance.cButtonLabel,               4, 20);
        AssertField(instance, instance.xButtonLabel,               4, 24);
        AssertField(instance, instance.yButtonLabel,               4, 28);
        AssertField(instance, instance.zButtonLabel,               4, 32);
        AssertField(instance, instance.dpadUpLabel,                4, 36);
        AssertField(instance, instance.dpadDownLabel,              4, 40);
        AssertField(instance, instance.dpadLeftLabel,              4, 44);
        AssertField(instance, instance.dpadRightLabel,             4, 48);
        AssertField(instance, instance.leftShoulderButtonLabel,    4, 52);
        AssertField(instance, instance.rightShoulderButtonLabel,   4, 56);
        AssertField(instance, instance.leftThumbstickButtonLabel,  4, 60);
        AssertField(instance, instance.rightThumbstickButtonLabel, 4, 64);
        AssertField(instance, instance.extraButtonCount,           4, 68);
        AssertField(instance, instance.extraAxisCount,             4, 72);
    });

    [Test]
    public void GameInputRacingWheelInfo() => Assert.Multiple(() =>
    {
        GameInputRacingWheelInfo instance = default;
        AssertStruct(instance, 80, 4);
        AssertField(instance, instance.menuButtonLabel,            4, 0);
        AssertField(instance, instance.viewButtonLabel,            4, 4);
        AssertField(instance, instance.previousGearButtonLabel,    4, 8);
        AssertField(instance, instance.nextGearButtonLabel,        4, 12);
        AssertField(instance, instance.dpadUpLabel,                4, 16);
        AssertField(instance, instance.dpadDownLabel,              4, 20);
        AssertField(instance, instance.dpadLeftLabel,              4, 24);
        AssertField(instance, instance.dpadRightLabel,             4, 28);
        AssertField(instance, instance.aButtonLabel,               4, 32);
        AssertField(instance, instance.bButtonLabel,               4, 36);
        AssertField(instance, instance.xButtonLabel,               4, 40);
        AssertField(instance, instance.yButtonLabel,               4, 44);
        AssertField(instance, instance.leftThumbstickButtonLabel,  4, 48);
        AssertField(instance, instance.rightThumbstickButtonLabel, 4, 52);
        AssertField(instance, instance.hasClutch,                  1, 56);
        AssertField(instance, instance.hasHandbrake,               1, 57);
        AssertField(instance, instance.hasPatternShifter,          1, 58);
        AssertField(instance, instance.minPatternShifterGear,      4, 60);
        AssertField(instance, instance.maxPatternShifterGear,      4, 64);
        AssertField(instance, instance.maxWheelAngle,              4, 68);
        AssertField(instance, instance.extraButtonCount,           4, 72);
        AssertField(instance, instance.extraAxisCount,             4, 76);
    });

    [Test]
    public void GameInputForceFeedbackMotorInfo() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackMotorInfo instance = default;
        AssertStruct(instance, 16, 4);
        AssertField(instance, instance.supportedAxes,                     4, 0);
        AssertField(instance, instance.isConstantEffectSupported,         1, 4);
        AssertField(instance, instance.isRampEffectSupported,             1, 5);
        AssertField(instance, instance.isSineWaveEffectSupported,         1, 6);
        AssertField(instance, instance.isSquareWaveEffectSupported,       1, 7);
        AssertField(instance, instance.isTriangleWaveEffectSupported,     1, 8);
        AssertField(instance, instance.isSawtoothUpWaveEffectSupported,   1, 9);
        AssertField(instance, instance.isSawtoothDownWaveEffectSupported, 1, 10);
        AssertField(instance, instance.isSpringEffectSupported,           1, 11);
        AssertField(instance, instance.isFrictionEffectSupported,         1, 12);
        AssertField(instance, instance.isDamperEffectSupported,           1, 13);
        AssertField(instance, instance.isInertiaEffectSupported,          1, 14);
    });

    [Test]
    public unsafe void GameInputRawDeviceReportInfo() => Assert.Multiple(() =>
    {
        GameInputRawDeviceReportInfo instance = default;
        AssertStruct(instance, 12, 4);
        AssertField(instance, instance.kind,      4, 0);
        AssertField(instance, instance.id,        4, 4);
        AssertField(instance, instance.size,      4, 8);
    });

    [Test]
    public unsafe void GameInputDeviceInfo() => Assert.Multiple(() =>
    {
        GameInputDeviceInfo instance = default;
        AssertStruct(instance, 256, 8);
        AssertField(instance, instance.vendorId,                 2,  0);
        AssertField(instance, instance.productId,                2,  2);
        AssertField(instance, instance.revisionNumber,           2,  4);
        AssertField(instance, instance.usage,                    4,  6);
        AssertField(instance, instance.hardwareVersion,          8,  10);
        AssertField(instance, instance.firmwareVersion,          8,  18);
        AssertField(instance, instance.deviceId,                 32, 26);
        AssertField(instance, instance.deviceRootId,             32, 58);
        AssertField(instance, instance.deviceFamily,             4,  92);
        AssertField(instance, instance.supportedInput,           4,  96);
        AssertField(instance, instance.supportedRumbleMotors,    4,  100);
        AssertField(instance, instance.supportedSystemButtons,   4,  104);
        AssertField(instance, instance.containerId,              16, 108);
        AssertField(instance, instance.displayName,                  128);
        AssertField(instance, instance.pnpPath,                      136);

        AssertField(instance, instance.keyboardInfo,                 144);
        AssertField(instance, instance.mouseInfo,                    152);
        AssertField(instance, instance.sensorsInfo,                  160);
        AssertField(instance, instance.controllerInfo,               168);
        AssertField(instance, instance.arcadeStickInfo,              176);
        AssertField(instance, instance.flightStickInfo,              184);
        AssertField(instance, instance.gamepadInfo,                  192);
        AssertField(instance, instance.racingWheelInfo,              200);

        AssertField(instance, instance.forceFeedbackMotorCount,  4,  208);
        AssertField(instance, instance._forceFeedbackMotorInfo,      216);
        AssertField(instance, instance.inputReportCount,         4,  224);
        AssertField(instance, instance._inputReportInfo,             232);
        AssertField(instance, instance.outputReportCount,        4,  240);
        AssertField(instance, instance._outputReportInfo,            248);
    });

    [Test]
    public unsafe void GameInputHapticInfo() => Assert.Multiple(() =>
    {
        GameInputHapticInfo instance = default;
        AssertStruct(instance, 644, 4);
        AssertFixed(instance, instance.audioEndpointId,     0);
        AssertField(instance, instance.locationCount,    4, 512);
        AssertFixed(instance, instance._locations,          516);
    });

    [Test]
    public void GameInputForceFeedbackEnvelope() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackEnvelope instance = default;
        AssertStruct(instance, 48, 8);
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
        AssertStruct(instance, 28, 4);
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
        AssertStruct(instance, 52, 4);
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
        AssertStruct(instance, 80, 8);
        AssertField(instance, instance.envelope,  48, 0);
        AssertField(instance, instance.magnitude, 28, 48);
    });

    [Test]
    public void GameInputForceFeedbackPeriodicParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackPeriodicParams instance = default;
        AssertStruct(instance, 88, 8);
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
        AssertStruct(instance, 104, 8);
        AssertField(instance, instance.envelope,       48, 0);
        AssertField(instance, instance.startMagnitude, 28, 48);
        AssertField(instance, instance.endMagnitude,   28, 76);
    });

    [Test]
    public void GameInputForceFeedbackParams() => Assert.Multiple(() =>
    {
        GameInputForceFeedbackParams instance = default;
        AssertStruct(instance, 112, 8);
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
    public void GameInputRumbleParams() => Assert.Multiple(() =>
    {
        GameInputRumbleParams instance = default;
        AssertStruct(instance, 16, 4);
        AssertField(instance, instance.lowFrequency,  4, 0);
        AssertField(instance, instance.highFrequency, 4, 4);
        AssertField(instance, instance.leftTrigger,   4, 8);
        AssertField(instance, instance.rightTrigger,  4, 12);
    });

    [Test] public void GameInputElementKind() => AssertEnum<GameInputElementKind>(4);

    [Test]
    public void GameInputAxisMapping() => Assert.Multiple(() =>
    {
        GameInputAxisMapping instance = default;
        AssertStruct(instance, 20, 4);
        AssertField(instance, instance.controllerElementKind, 4, 0);
        AssertField(instance, instance.controllerIndex,       4, 4);
        AssertField(instance, instance.isInverted,            1, 8);
        AssertField(instance, instance.fromTwoButtons,        1, 9);
        AssertField(instance, instance.buttonMinIndexValue,   4, 12);
        AssertField(instance, instance.referenceDirection,    4, 16);
    });

    [Test]
    public void GameInputButtonMapping() => Assert.Multiple(() =>
    {
        GameInputButtonMapping instance = default;
        AssertStruct(instance, 16, 4);
        AssertField(instance, instance.controllerElementKind, 4, 0);
        AssertField(instance, instance.controllerIndex,       4, 4);
        AssertField(instance, instance.isInverted,            1, 8);
        AssertField(instance, instance.switchPosition,        4, 12);
    });
}