using static SharpGameInput.Common.Tests.SizeHelper;

namespace SharpGameInput.v1.Tests;

public class SizeChecks
{
    [Test] public void GameInputResult() => AssertEnumSize<GameInputResult>(4);
    [Test] public void GameInputKind() => AssertEnumSize<GameInputKind>(4);
    [Test] public void GameInputEnumerationKind() => AssertEnumSize<GameInputEnumerationKind>(4);
    [Test] public void GameInputFocusPolicy() => AssertEnumSize<GameInputFocusPolicy>(4);
    [Test] public void GameInputSwitchKind() => AssertEnumSize<GameInputSwitchKind>(4);
    [Test] public void GameInputSwitchPosition() => AssertEnumSize<GameInputSwitchPosition>(4);
    [Test] public void GameInputKeyboardKind() => AssertEnumSize<GameInputKeyboardKind>(4);
    [Test] public void GameInputMouseButtons() => AssertEnumSize<GameInputMouseButtons>(4);
    [Test] public void GameInputMousePositions() => AssertEnumSize<GameInputMousePositions>(4);
    [Test] public void GameInputArcadeStickButtons() => AssertEnumSize<GameInputArcadeStickButtons>(4);
    [Test] public void GameInputFlightStickButtons() => AssertEnumSize<GameInputFlightStickButtons>(4);
    [Test] public void GameInputGamepadButtons() => AssertEnumSize<GameInputGamepadButtons>(4);
    [Test] public void GameInputRacingWheelButtons() => AssertEnumSize<GameInputRacingWheelButtons>(4);
    [Test] public void GameInputUiNavigationButtons() => AssertEnumSize<GameInputUiNavigationButtons>(4);
    [Test] public void GameInputSystemButtons  () => AssertEnumSize<GameInputSystemButtons  >(4);
    [Test] public void GameInputDeviceStatus() => AssertEnumSize<GameInputDeviceStatus>(4);
    [Test] public void GameInputDeviceFamily() => AssertEnumSize<GameInputDeviceFamily>(4);
    [Test] public void GameInputLabel() => AssertEnumSize<GameInputLabel>(4);
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
        AssertSize(instance, 56);
        AssertField(instance, instance.buttons,           4, 0);
        AssertField(instance, instance.positionX,         8, 8);
        AssertField(instance, instance.positionY,         8, 16);
        AssertField(instance, instance.absolutePositionX, 8, 24);
        AssertField(instance, instance.absolutePositionY, 8, 32);
        AssertField(instance, instance.wheelX,            8, 40);
        AssertField(instance, instance.wheelY,            8, 48);
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
    public void GameInputUsage() => Assert.Multiple(() =>
    {
        GameInputUsage instance = default;
        AssertSize(instance, 4);
        AssertField(instance, instance.page, 2, 0);
        AssertField(instance, instance.id,   2, 2);
    });

    [Test]
    public void GameInputControllerAxisInfo() => Assert.Multiple(() =>
    {
        GameInputControllerAxisInfo instance = default;
        AssertSize(instance, 8);
        AssertField(instance, instance.mappedInputKinds,  4, 0);
        AssertField(instance, instance.label,             4, 4);
    });

    [Test]
    public void GameInputControllerButtonInfo() => Assert.Multiple(() =>
    {
        GameInputControllerButtonInfo instance = default;
        AssertSize(instance, 8);
        AssertField(instance, instance.mappedInputKinds,  4, 0);
        AssertField(instance, instance.label,             4, 4);
    });

    [Test]
    public void GameInputControllerSwitchInfo() => Assert.Multiple(() =>
    {
        GameInputControllerSwitchInfo instance = default;
        AssertSize(instance, 12);
        AssertField(instance, instance.mappedInputKinds,  4, 0);
        AssertField(instance, instance.label,             4, 4);
        AssertField(instance, instance.kind,              4, 8);
    });

    [Test]
    public void GameInputKeyboardInfo() => Assert.Multiple(() =>
    {
        GameInputKeyboardInfo instance = default;
        AssertSize(instance, 28);
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
        AssertSize(instance, 12);
        AssertField(instance, instance.supportedButtons, 4, 0);
        AssertField(instance, instance.sampleRate,       4, 4);
        AssertField(instance, instance.hasWheelX,        1, 8);
        AssertField(instance, instance.hasWheelY,        1, 9);
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
        AssertSize(instance, 16);
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
    public unsafe void GameInputDeviceInfo() => Assert.Multiple(() =>
    {
        GameInputDeviceInfo instance = default;
        AssertSize(instance, 224);
        AssertField(instance, instance.vendorId,                 2,  0);
        AssertField(instance, instance.productId,                2,  2);
        AssertField(instance, instance.usage,                    4,  4);
        AssertField(instance, instance.deviceId,                 32, 8);
        AssertField(instance, instance.deviceRootId,             32, 40);
        AssertField(instance, instance.deviceFamily,             4,  72);
        AssertField(instance, instance.supportedInput,           4,  76);
        AssertField(instance, instance.supportedRumbleMotors,    4,  80);
        AssertField(instance, instance.supportedSystemButtons,   4,  84);
        AssertField(instance, instance.displayName,                  88);
        AssertField(instance, instance.pnpPath,                      96);

        AssertField(instance, instance.keyboardInfo,                 104);
        AssertField(instance, instance.mouseInfo,                    112);
        AssertField(instance, instance.arcadeStickInfo,              120);
        AssertField(instance, instance.flightStickInfo,              128);
        AssertField(instance, instance.gamepadInfo,                  136);
        AssertField(instance, instance.racingWheelInfo,              144);
        AssertField(instance, instance.uiNavigationInfo,             152);

        AssertField(instance, instance.controllerAxisCount,      4,  160);
        AssertField(instance, instance._controllerAxisInfo,          168);
        AssertField(instance, instance.controllerButtonCount,    4,  176);
        AssertField(instance, instance._controllerButtonInfo,        184);
        AssertField(instance, instance.controllerSwitchCount,    4,  192);
        AssertField(instance, instance._controllerSwitchInfo,        200);
        AssertField(instance, instance.forceFeedbackMotorCount,  4,  208);
        AssertField(instance, instance._forceFeedbackMotorInfo,      216);
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