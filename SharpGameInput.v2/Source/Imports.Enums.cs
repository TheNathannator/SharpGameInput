using System;

namespace SharpGameInput.v2
{
    public enum GameInputResult
    {
        Facility = 0x38A,

        DeviceDisconnected = unchecked((int)0x838A0001),
        DeviceNotFound = unchecked((int)0x838A0002),
        ReadingNotFound = unchecked((int)0x838A0003),
        ReferenceReadingTooOld = unchecked((int)0x838A0004),
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        TimestampOutOfRange = unchecked((int)0x838A0005),
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        InsufficientForceFeedbackResources = unchecked((int)0x838A0006),
        FeedbackNotSupported = unchecked((int)0x838A0007),
        ObjectNoLongerExists = unchecked((int)0x838A0008),
        CallbackNotFound = unchecked((int)0x838A0009),
        HapticInfoNotFound = unchecked((int)0x838A000A),
        AggregateOperationNotSupported = unchecked((int)0x838A000B),
    }

    [Flags]
    public enum GameInputKind
    {
        Unknown          = 0x00000000,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        RawDeviceReport  = 0x00000001,
        ControllerAxis   = 0x00000002,
        ControllerButton = 0x00000004,
        ControllerSwitch = 0x00000008,
        Controller       = 0x0000000E,
        Keyboard         = 0x00000010,
        Mouse            = 0x00000020,
        Sensors          = 0x00000040,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        Touch            = 0x00000100,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        Motion           = 0x00001000,
        ArcadeStick      = 0x00010000,
        FlightStick      = 0x00020000,
        Gamepad          = 0x00040000,
        RacingWheel      = 0x00080000,
        UiNavigation     = 0x01000000,
        AnyKind          = unchecked((int)0xFFFFFFFF)
    }

    public enum GameInputEnumerationKind
    {
        NoEnumeration       = 0,
        AsyncEnumeration    = 1,
        BlockingEnumeration = 2
    }

    [Flags]
    public enum GameInputFocusPolicy
    {
        Default                        = 0x00000000,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        DisableBackgroundInput         = 0x00000001,
        ExclusiveForegroundInput       = 0x00000002,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        DisableBackgroundGuideButton   = 0x00000004,
        ExclusiveForegroundGuideButton = 0x00000008,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        DisableBackgroundShareButton   = 0x00000010,
        ExclusiveForegroundShareButton = 0x00000020,
        EnableBackgroundInput          = 0x00000040,
        EnableBackgroundGuideButton    = 0x00000080,
        EnableBackgroundShareButton    = 0x00000100
    }

    public enum GameInputSwitchKind
    {
        Unknown  = -1,
        TwoWay   =  0,
        FourWay  =  1,
        EightWay =  2
    }

    public enum GameInputSwitchPosition
    {
        Center    = 0,
        Up        = 1,
        UpRight   = 2,
        Right     = 3,
        DownRight = 4,
        Down      = 5,
        DownLeft  = 6,
        Left      = 7,
        UpLeft    = 8
    }

    public enum GameInputKeyboardKind
    {
        Unknown = -1,
        Ansi    =  0,
        Iso     =  1,
        Ks      =  2,
        Abnt    =  3,
        Jis     =  4
    }

    [Flags]
    public enum GameInputMouseButtons
    {
        None           = 0x00000000,
        LeftButton     = 0x00000001,
        RightButton    = 0x00000002,
        MiddleButton   = 0x00000004,
        Button4        = 0x00000008,
        Button5        = 0x00000010,
        WheelTiltLeft  = 0x00000020,
        WheelTiltRight = 0x00000040
    }

    [Flags]
    public enum GameInputMousePositions
    {
        None             = 0x00000000,
        AbsolutePosition = 0x00000001,
        RelativePosition = 0x00000002
    }

    [Flags]
    public enum GameInputSensorsKind
    {
        None            = 0x00000000,
        Accelerometer   = 0x00000001,
        Gyrometer       = 0x00000002,
        Compass         = 0x00000004,
        Orientation     = 0x00000008
    }

    public enum GameInputSensorAccuracy
    {
        Unknown      = 0x00000000,
        Unreliable   = 0x00000001,
        Approximate  = 0x00000002,
        High         = 0x00000003
    }

    [Flags]
    public enum GameInputArcadeStickButtons
    {
        None     = 0x00000000,
        Menu     = 0x00000001,
        View     = 0x00000002,
        Up       = 0x00000004,
        Down     = 0x00000008,
        Left     = 0x00000010,
        Right    = 0x00000020,
        Action1  = 0x00000040,
        Action2  = 0x00000080,
        Action3  = 0x00000100,
        Action4  = 0x00000200,
        Action5  = 0x00000400,
        Action6  = 0x00000800,
        Special1 = 0x00001000,
        Special2 = 0x00002000
    }

    [Flags]
    public enum GameInputFlightStickButtons
    {
        None          = 0x00000000,
        Menu          = 0x00000001,
        View          = 0x00000002,
        FirePrimary   = 0x00000004,
        FireSecondary = 0x00000008
    }

    [Flags]
    public enum GameInputGamepadButtons
    {
        None            = 0x00000000,
        Menu            = 0x00000001,
        View            = 0x00000002,
        A               = 0x00000004,
        B               = 0x00000008,
        X               = 0x00000010,
        Y               = 0x00000020,
        DPadUp          = 0x00000040,
        DPadDown        = 0x00000080,
        DPadLeft        = 0x00000100,
        DPadRight       = 0x00000200,
        LeftShoulder    = 0x00000400,
        RightShoulder   = 0x00000800,
        LeftThumbstick  = 0x00001000,
        RightThumbstick = 0x00002000
    }

    [Flags]
    public enum GameInputRacingWheelButtons
    {
        None         = 0x00000000,
        Menu         = 0x00000001,
        View         = 0x00000002,
        PreviousGear = 0x00000004,
        NextGear     = 0x00000008,
        DpadUp       = 0x00000010,
        DpadDown     = 0x00000020,
        DpadLeft     = 0x00000040,
        DpadRight    = 0x00000080
    }

    [Flags]
    public enum GameInputUiNavigationButtons
    {
        None        = 0x00000000,
        Menu        = 0x00000001,
        View        = 0x00000002,
        Accept      = 0x00000004,
        Cancel      = 0x00000008,
        Up          = 0x00000010,
        Down        = 0x00000020,
        Left        = 0x00000040,
        Right       = 0x00000080,
        Context1    = 0x00000100,
        Context2    = 0x00000200,
        Context3    = 0x00000400,
        Context4    = 0x00000800,
        PageUp      = 0x00001000,
        PageDown    = 0x00002000,
        PageLeft    = 0x00004000,
        PageRight   = 0x00008000,
        ScrollUp    = 0x00010000,
        ScrollDown  = 0x00020000,
        ScrollLeft  = 0x00040000,
        ScrollRight = 0x00080000
    }

    [Flags]
    public enum GameInputSystemButtons  
    {  
        None  = 0x00000000,
        Guide = 0x00000001,
        Share = 0x00000002 
    }

    [Flags]
    public enum GameInputDeviceStatus
    {
        NoStatus        = 0x00000000,
        Connected       = 0x00000001,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        InputEnabled    = 0x00000002,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        OutputEnabled   = 0x00000004,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        RawIoEnabled    = 0x00000008,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        AudioCapture    = 0x00000010,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        AudioRender     = 0x00000020,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        Synchronized    = 0x00000040,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        Wireless        = 0x00000080,
        [Obsolete("This value is not available in GameInput v2. It is only retained here for display purposes.")]
        UserIdle        = 0x00100000,
        HapticInfoReady = 0x00200000,
        AnyStatus       = unchecked((int)0xFFFFFFFF)
    }

    public enum GameInputDeviceFamily
    {
        Virtual   = -1,
        Aggregate =  0,
        XboxOne   =  1,
        Xbox360   =  2,
        Hid       =  3,
        I8042     =  4
    }

    public enum GameInputLabel
    {
        Unknown                  =  -1,
        None                     =   0,
        XboxGuide                =   1,
        XboxBack                 =   2,
        XboxStart                =   3,
        XboxMenu                 =   4,
        XboxView                 =   5,
        XboxA                    =   7,
        XboxB                    =   8,
        XboxX                    =   9,
        XboxY                    =  10,
        XboxDPadUp               =  11,
        XboxDPadDown             =  12,
        XboxDPadLeft             =  13,
        XboxDPadRight            =  14,
        XboxLeftShoulder         =  15,
        XboxLeftTrigger          =  16,
        XboxLeftStickButton      =  17,
        XboxRightShoulder        =  18,
        XboxRightTrigger         =  19,
        XboxRightStickButton     =  20,
        XboxPaddle1              =  21,
        XboxPaddle2              =  22,
        XboxPaddle3              =  23,
        XboxPaddle4              =  24,
        LetterA                  =  25,
        LetterB                  =  26,
        LetterC                  =  27,
        LetterD                  =  28,
        LetterE                  =  29,
        LetterF                  =  30,
        LetterG                  =  31,
        LetterH                  =  32,
        LetterI                  =  33,
        LetterJ                  =  34,
        LetterK                  =  35,
        LetterL                  =  36,
        LetterM                  =  37,
        LetterN                  =  38,
        LetterO                  =  39,
        LetterP                  =  40,
        LetterQ                  =  41,
        LetterR                  =  42,
        LetterS                  =  43,
        LetterT                  =  44,
        LetterU                  =  45,
        LetterV                  =  46,
        LetterW                  =  47,
        LetterX                  =  48,
        LetterY                  =  49,
        LetterZ                  =  50,
        Number0                  =  51,
        Number1                  =  52,
        Number2                  =  53,
        Number3                  =  54,
        Number4                  =  55,
        Number5                  =  56,
        Number6                  =  57,
        Number7                  =  58,
        Number8                  =  59,
        Number9                  =  60,
        ArrowUp                  =  61,
        ArrowUpRight             =  62,
        ArrowRight               =  63,
        ArrowDownRight           =  64,
        ArrowDown                =  65,
        ArrowDownLLeft           =  66,
        ArrowLeft                =  67,
        ArrowUpLeft              =  68,
        ArrowUpDown              =  69,
        ArrowLeftRight           =  70,
        ArrowUpDownLeftRight     =  71,
        ArrowClockwise           =  72,
        ArrowCounterClockwise    =  73,
        ArrowReturn              =  74,
        IconBranding             =  75,
        IconHome                 =  76,
        IconMenu                 =  77,
        IconCross                =  78,
        IconCircle               =  79,
        IconSquare               =  80,
        IconTriangle             =  81,
        IconStar                 =  82,
        IconDPadUp               =  83,
        IconDPadDown             =  84,
        IconDPadLeft             =  85,
        IconDPadRight            =  86,
        IconDialClockwise        =  87,
        IconDialCounterClockwise =  88,
        IconSliderLeftRight      =  89,
        IconSliderUpDown         =  90,
        IconWheelUpDown          =  91,
        IconPlus                 =  92,
        IconMinus                =  93,
        IconSuspension           =  94,
        Home                     =  95,
        Guide                    =  96,
        Mode                     =  97,
        Select                   =  98,
        Menu                     =  99,
        View                     = 100,
        Back                     = 101,
        Start                    = 102,
        Options                  = 103,
        Share                    = 104,
        Up                       = 105,
        Down                     = 106,
        Left                     = 107,
        Right                    = 108,
        LB                       = 109,
        LT                       = 110,
        LSB                      = 111,
        L1                       = 112,
        L2                       = 113,
        L3                       = 114,
        RB                       = 115,
        RT                       = 116,
        RSB                      = 117,
        R1                       = 118,
        R2                       = 119,
        R3                       = 120,
        P1                       = 121,
        P2                       = 122,
        P3                       = 123,
        P4                       = 124
    }

    [Flags]
    public enum GameInputFeedbackAxes
    {
        None     = 0x00000000,
        LinearX  = 0x00000001,
        LinearY  = 0x00000002,
        LinearZ  = 0x00000004,
        AngularX = 0x00000008,
        AngularY = 0x00000010,
        AngularZ = 0x00000020,
        Normal   = 0x00000040
    }

    public enum GameInputFeedbackEffectState
    {
        Stopped = 0,
        Running = 1,
        Paused  = 2
    }

    public enum GameInputForceFeedbackEffectKind
    {
        Constant         = 0,
        Ramp             = 1,
        SineWave         = 2,
        SquareWave       = 3,
        TriangleWave     = 4,
        SawtoothUpWave   = 5,
        SawtoothDownWave = 6,
        Spring           = 7,
        Friction         = 8,
        Damper           = 9,
        Inertia          = 10
    }

    [Flags]
    public enum GameInputRumbleMotors
    {
        None          = 0x00000000,
        LowFrequency  = 0x00000001,
        HighFrequency = 0x00000002,
        LeftTrigger   = 0x00000004,
        RightTrigger  = 0x00000008
    }
}
