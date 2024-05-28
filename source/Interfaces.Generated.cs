//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;

#nullable enable

namespace SharpGameInput
{
    public sealed unsafe partial class IGameInput : InterfaceHandle
    {
        internal IGameInput(IntPtr handle) : base(handle) {}

        public ulong GetCurrentTimestamp()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong>)vtable[3];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public int GetCurrentReading(
            GameInputKind inputKind,
            IGameInputDevice? device,
            out IGameInputReading reading
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputKind, IntPtr, out IntPtr, int>)vtable[4];

            var result = fnPtr(
                thisPtr,
                inputKind,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                out IntPtr reading_handle
            );

            reading = new(reading_handle);
            return result;
        }

        public int GetNextReading(
            IGameInputReading referenceReading,
            GameInputKind inputKind,
            IGameInputDevice? device,
            out IGameInputReading reading
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, GameInputKind, IntPtr, out IntPtr, int>)vtable[5];

            var result = fnPtr(
                thisPtr,
                referenceReading.DangerousGetHandle(),
                inputKind,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                out IntPtr reading_handle
            );

            reading = new(reading_handle);
            return result;
        }

        public int GetPreviousReading(
            IGameInputReading referenceReading,
            GameInputKind inputKind,
            IGameInputDevice? device,
            out IGameInputReading reading
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, GameInputKind, IntPtr, out IntPtr, int>)vtable[6];

            var result = fnPtr(
                thisPtr,
                referenceReading.DangerousGetHandle(),
                inputKind,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                out IntPtr reading_handle
            );

            reading = new(reading_handle);
            return result;
        }

        public int GetTemporalReading(
            ulong timestamp,
            IGameInputDevice device,
            out IGameInputReading reading
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong, IntPtr, out IntPtr, int>)vtable[7];

            var result = fnPtr(
                thisPtr,
                timestamp,
                device.DangerousGetHandle(),
                out IntPtr reading_handle
            );

            reading = new(reading_handle);
            return result;
        }

        public int RegisterReadingCallback(
            IGameInputDevice? device,
            GameInputKind inputKind,
            float analogThreshold,
            void* context,
            delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, bool, void> callbackFunc,
            out ulong callbackToken
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, GameInputKind, float, void*, delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, bool, void>, out ulong, int>)vtable[8];

            var result = fnPtr(
                thisPtr,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                inputKind,
                analogThreshold,
                context,
                callbackFunc,
                out callbackToken
            );

            return result;
        }

        public int RegisterDeviceCallback(
            IGameInputDevice? device,
            GameInputKind inputKind,
            GameInputDeviceStatus statusFilter,
            GameInputEnumerationKind enumerationKind,
            void* context,
            delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, GameInputDeviceStatus, GameInputDeviceStatus, void> callbackFunc,
            out ulong callbackToken
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, GameInputKind, GameInputDeviceStatus, GameInputEnumerationKind, void*, delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, GameInputDeviceStatus, GameInputDeviceStatus, void>, out ulong, int>)vtable[9];

            var result = fnPtr(
                thisPtr,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                inputKind,
                statusFilter,
                enumerationKind,
                context,
                callbackFunc,
                out callbackToken
            );

            return result;
        }

        public int RegisterGuideButtonCallback(
            IGameInputDevice? device,
            void* context,
            delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, bool, void> callbackFunc,
            out ulong callbackToken
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, void*, delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, bool, void>, out ulong, int>)vtable[10];

            var result = fnPtr(
                thisPtr,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                context,
                callbackFunc,
                out callbackToken
            );

            return result;
        }

        public int RegisterKeyboardLayoutCallback(
            IGameInputDevice? device,
            void* context,
            delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, uint, uint, void> callbackFunc,
            out ulong callbackToken
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, void*, delegate* unmanaged[Stdcall]<ulong, void*, IntPtr, ulong, uint, uint, void>, out ulong, int>)vtable[11];

            var result = fnPtr(
                thisPtr,
                device?.DangerousGetHandle() ?? IntPtr.Zero,
                context,
                callbackFunc,
                out callbackToken
            );

            return result;
        }

        public void StopCallback(
            ulong callbackToken
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong, void>)vtable[12];

            fnPtr(
                thisPtr,
                callbackToken
            );

        }

        public bool UnregisterCallback(
            ulong callbackToken,
            ulong timeoutInMicroseconds
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong, ulong, bool>)vtable[13];

            var result = fnPtr(
                thisPtr,
                callbackToken,
                timeoutInMicroseconds
            );

            return result;
        }

        public int CreateDispatcher(
            out IGameInputDispatcher dispatcher
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out IntPtr, int>)vtable[14];

            var result = fnPtr(
                thisPtr,
                out IntPtr dispatcher_handle
            );

            dispatcher = new(dispatcher_handle);
            return result;
        }

        public int CreateAggregateDevice(
            GameInputKind inputKind,
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputKind, out IntPtr, int>)vtable[15];

            var result = fnPtr(
                thisPtr,
                inputKind,
                out IntPtr device_handle
            );

            device = new(device_handle);
            return result;
        }

        public int FindDeviceFromId(
            in APP_LOCAL_DEVICE_ID value,
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, in APP_LOCAL_DEVICE_ID, out IntPtr, int>)vtable[16];

            var result = fnPtr(
                thisPtr,
                in value,
                out IntPtr device_handle
            );

            device = new(device_handle);
            return result;
        }

        public int FindDeviceFromObject(
            nint value,
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, nint, out IntPtr, int>)vtable[17];

            var result = fnPtr(
                thisPtr,
                value,
                out IntPtr device_handle
            );

            device = new(device_handle);
            return result;
        }

        public int FindDeviceFromPlatformHandle(
            void* value,
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, void*, out IntPtr, int>)vtable[18];

            var result = fnPtr(
                thisPtr,
                value,
                out IntPtr device_handle
            );

            device = new(device_handle);
            return result;
        }

        public int FindDeviceFromPlatformString(
            char* value,
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, char*, out IntPtr, int>)vtable[19];

            var result = fnPtr(
                thisPtr,
                value,
                out IntPtr device_handle
            );

            device = new(device_handle);
            return result;
        }

        public int EnableOemDeviceSupport(
            ushort vendorId,
            ushort productId,
            byte interfaceNumber,
            byte collectionNumber
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ushort, ushort, byte, byte, int>)vtable[20];

            var result = fnPtr(
                thisPtr,
                vendorId,
                productId,
                interfaceNumber,
                collectionNumber
            );

            return result;
        }

        public void SetFocusPolicy(
            GameInputFocusPolicy policy
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputFocusPolicy, void>)vtable[21];

            fnPtr(
                thisPtr,
                policy
            );

        }
    }

    public sealed unsafe partial class IGameInputReading : InterfaceHandle
    {
        internal IGameInputReading(IntPtr handle) : base(handle) {}

        public GameInputKind GetInputKind()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputKind>)vtable[3];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public ulong GetSequenceNumber(
            GameInputKind inputKind
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputKind, ulong>)vtable[4];

            var result = fnPtr(
                thisPtr,
                inputKind
            );

            return result;
        }

        public ulong GetTimestamp()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong>)vtable[5];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public void GetDevice(
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out IntPtr, void>)vtable[6];

            fnPtr(
                thisPtr,
                out IntPtr device_handle
            );

            device = new(device_handle);
        }

        public bool GetRawReport(
            out IGameInputRawDeviceReport report
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out IntPtr, bool>)vtable[7];

            var result = fnPtr(
                thisPtr,
                out IntPtr report_handle
            );

            report = new(report_handle);
            return result;
        }

        public uint GetControllerAxisCount()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[8];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public uint GetControllerAxisState(
            uint stateArrayCount,
            float* stateArray
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, float*, uint>)vtable[9];

            var result = fnPtr(
                thisPtr,
                stateArrayCount,
                stateArray
            );

            return result;
        }

        public uint GetControllerButtonCount()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[10];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public uint GetControllerButtonState(
            uint stateArrayCount,
            bool* stateArray
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, bool*, uint>)vtable[11];

            var result = fnPtr(
                thisPtr,
                stateArrayCount,
                stateArray
            );

            return result;
        }

        public uint GetControllerSwitchCount()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[12];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public uint GetControllerSwitchState(
            uint stateArrayCount,
            GameInputSwitchPosition* stateArray
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, GameInputSwitchPosition*, uint>)vtable[13];

            var result = fnPtr(
                thisPtr,
                stateArrayCount,
                stateArray
            );

            return result;
        }

        public uint GetKeyCount()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[14];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public uint GetKeyState(
            uint stateArrayCount,
            GameInputKeyState* stateArray
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, GameInputKeyState*, uint>)vtable[15];

            var result = fnPtr(
                thisPtr,
                stateArrayCount,
                stateArray
            );

            return result;
        }

        public bool GetMouseState(
            out GameInputMouseState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputMouseState, bool>)vtable[16];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }

        public uint GetTouchCount()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[17];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public uint GetTouchState(
            uint stateArrayCount,
            GameInputTouchState* stateArray
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, GameInputTouchState*, uint>)vtable[18];

            var result = fnPtr(
                thisPtr,
                stateArrayCount,
                stateArray
            );

            return result;
        }

        public bool GetMotionState(
            out GameInputMotionState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputMotionState, bool>)vtable[19];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }

        public bool GetArcadeStickState(
            out GameInputArcadeStickState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputArcadeStickState, bool>)vtable[20];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }

        public bool GetFlightStickState(
            out GameInputFlightStickState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputFlightStickState, bool>)vtable[21];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }

        public bool GetGamepadState(
            out GameInputGamepadState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputGamepadState, bool>)vtable[22];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }

        public bool GetRacingWheelState(
            out GameInputRacingWheelState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputRacingWheelState, bool>)vtable[23];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }

        public bool GetUiNavigationState(
            out GameInputUiNavigationState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputUiNavigationState, bool>)vtable[24];

            var result = fnPtr(
                thisPtr,
                out state
            );

            return result;
        }
    }

    public sealed unsafe partial class IGameInputDevice : InterfaceHandle
    {
        internal IGameInputDevice(IntPtr handle) : base(handle) {}

        public GameInputDeviceInfo* GetDeviceInfo()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputDeviceInfo*>)vtable[3];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public GameInputDeviceStatus GetDeviceStatus()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputDeviceStatus>)vtable[4];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public void GetBatteryState(
            out GameInputBatteryState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputBatteryState, void>)vtable[5];

            fnPtr(
                thisPtr,
                out state
            );

        }

        public int CreateForceFeedbackEffect(
            uint motorIndex,
            in GameInputForceFeedbackParams ffbParams,
            out IGameInputForceFeedbackEffect effect
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, in GameInputForceFeedbackParams, out IntPtr, int>)vtable[6];

            var result = fnPtr(
                thisPtr,
                motorIndex,
                in ffbParams,
                out IntPtr effect_handle
            );

            effect = new(effect_handle);
            return result;
        }

        public bool IsForceFeedbackMotorPoweredOn(
            uint motorIndex
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, bool>)vtable[7];

            var result = fnPtr(
                thisPtr,
                motorIndex
            );

            return result;
        }

        public void SetForceFeedbackMotorGain(
            uint motorIndex,
            float masterGain
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, float, void>)vtable[8];

            fnPtr(
                thisPtr,
                motorIndex,
                masterGain
            );

        }

        public int SetHapticMotorState(
            uint motorIndex,
            in GameInputHapticFeedbackParams hapticParams
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, in GameInputHapticFeedbackParams, int>)vtable[9];

            var result = fnPtr(
                thisPtr,
                motorIndex,
                in hapticParams
            );

            return result;
        }

        public void SetRumbleState(
            in GameInputRumbleParams rumbleParams
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, in GameInputRumbleParams, void>)vtable[10];

            fnPtr(
                thisPtr,
                in rumbleParams
            );

        }

        public void SetInputSynchronizationState(
            bool enabled
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, bool, void>)vtable[11];

            fnPtr(
                thisPtr,
                enabled
            );

        }

        public void SendInputSynchronizationHint()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, void>)vtable[12];

            fnPtr(
                thisPtr
            );

        }

        public void PowerOff()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, void>)vtable[13];

            fnPtr(
                thisPtr
            );

        }

        public int CreateRawDeviceReport(
            uint reportId,
            GameInputRawDeviceReportKind reportKind,
            out IGameInputRawDeviceReport report
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, GameInputRawDeviceReportKind, out IntPtr, int>)vtable[14];

            var result = fnPtr(
                thisPtr,
                reportId,
                reportKind,
                out IntPtr report_handle
            );

            report = new(report_handle);
            return result;
        }

        public int GetRawDeviceFeature(
            uint reportId,
            out IGameInputRawDeviceReport report
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, out IntPtr, int>)vtable[15];

            var result = fnPtr(
                thisPtr,
                reportId,
                out IntPtr report_handle
            );

            report = new(report_handle);
            return result;
        }

        public int SetRawDeviceFeature(
            IGameInputRawDeviceReport report
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int>)vtable[16];

            var result = fnPtr(
                thisPtr,
                report.DangerousGetHandle()
            );

            return result;
        }

        public int SendRawDeviceOutput(
            IGameInputRawDeviceReport report
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, int>)vtable[17];

            var result = fnPtr(
                thisPtr,
                report.DangerousGetHandle()
            );

            return result;
        }

        public int SendRawDeviceOutputWithResponse(
            IGameInputRawDeviceReport requestReport,
            out IGameInputRawDeviceReport responseReport
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, IntPtr, out IntPtr, int>)vtable[18];

            var result = fnPtr(
                thisPtr,
                requestReport.DangerousGetHandle(),
                out IntPtr responseReport_handle
            );

            responseReport = new(responseReport_handle);
            return result;
        }

        public int ExecuteRawDeviceIoControl(
            uint controlCode,
            nuint inputBufferSize,
            void* inputBuffer,
            nuint outputBufferSize,
            void* outputBuffer,
            out nuint outputSize
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, nuint, void*, nuint, void*, out nuint, int>)vtable[19];

            var result = fnPtr(
                thisPtr,
                controlCode,
                inputBufferSize,
                inputBuffer,
                outputBufferSize,
                outputBuffer,
                out outputSize
            );

            return result;
        }

        public bool AcquireExclusiveRawDeviceAccess(
            ulong timeoutInMicroseconds
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong, bool>)vtable[20];

            var result = fnPtr(
                thisPtr,
                timeoutInMicroseconds
            );

            return result;
        }

        public void ReleaseExclusiveRawDeviceAccess()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, void>)vtable[21];

            fnPtr(
                thisPtr
            );

        }
    }

    public sealed unsafe partial class IGameInputDispatcher : InterfaceHandle
    {
        internal IGameInputDispatcher(IntPtr handle) : base(handle) {}

        public bool Dispatch(
            ulong quotaInMicroseconds
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, ulong, bool>)vtable[3];

            var result = fnPtr(
                thisPtr,
                quotaInMicroseconds
            );

            return result;
        }

        public int OpenWaitHandle(
            out nint waitHandle
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out nint, int>)vtable[4];

            var result = fnPtr(
                thisPtr,
                out waitHandle
            );

            return result;
        }
    }

    public sealed unsafe partial class IGameInputForceFeedbackEffect : InterfaceHandle
    {
        internal IGameInputForceFeedbackEffect(IntPtr handle) : base(handle) {}

        public void GetDevice(
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out IntPtr, void>)vtable[3];

            fnPtr(
                thisPtr,
                out IntPtr device_handle
            );

            device = new(device_handle);
        }

        public uint GetMotorIndex()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[4];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public float GetGain()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, float>)vtable[5];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public void SetGain(
            float gain
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, float, void>)vtable[6];

            fnPtr(
                thisPtr,
                gain
            );

        }

        public void GetParams(
            out GameInputForceFeedbackParams ffbParams
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out GameInputForceFeedbackParams, void>)vtable[7];

            fnPtr(
                thisPtr,
                out ffbParams
            );

        }

        public bool SetParams(
            in GameInputForceFeedbackParams ffbParams
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, in GameInputForceFeedbackParams, bool>)vtable[8];

            var result = fnPtr(
                thisPtr,
                in ffbParams
            );

            return result;
        }

        public GameInputFeedbackEffectState GetState()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputFeedbackEffectState>)vtable[9];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public void SetState(
            GameInputFeedbackEffectState state
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputFeedbackEffectState, void>)vtable[10];

            fnPtr(
                thisPtr,
                state
            );

        }
    }

    public sealed unsafe partial class IGameInputRawDeviceReport : InterfaceHandle
    {
        internal IGameInputRawDeviceReport(IntPtr handle) : base(handle) {}

        public void GetDevice(
            out IGameInputDevice device
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, out IntPtr, void>)vtable[3];

            fnPtr(
                thisPtr,
                out IntPtr device_handle
            );

            device = new(device_handle);
        }

        public GameInputRawDeviceReportInfo* GetReportInfo()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, GameInputRawDeviceReportInfo*>)vtable[4];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public nuint GetRawDataSize()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, nuint>)vtable[5];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }

        public nuint GetRawData(
            nuint bufferSize,
            void* buffer
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, nuint, void*, nuint>)vtable[6];

            var result = fnPtr(
                thisPtr,
                bufferSize,
                buffer
            );

            return result;
        }

        public bool SetRawData(
            nuint bufferSize,
            void* buffer
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, nuint, void*, bool>)vtable[7];

            var result = fnPtr(
                thisPtr,
                bufferSize,
                buffer
            );

            return result;
        }

        public bool GetItemValue(
            uint itemIndex,
            out long value
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, out long, bool>)vtable[8];

            var result = fnPtr(
                thisPtr,
                itemIndex,
                out value
            );

            return result;
        }

        public bool SetItemValue(
            uint itemIndex,
            long value
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, long, bool>)vtable[9];

            var result = fnPtr(
                thisPtr,
                itemIndex,
                value
            );

            return result;
        }

        public bool ResetItemValue(
            uint itemIndex
        )
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, uint, bool>)vtable[10];

            var result = fnPtr(
                thisPtr,
                itemIndex
            );

            return result;
        }

        public bool ResetAllItems()
        {
            var thisPtr = handle;
            var vtable = *(void***)thisPtr;
            var fnPtr = (delegate* unmanaged[Stdcall]<IntPtr, bool>)vtable[11];

            var result = fnPtr(
                thisPtr
            );

            return result;
        }
    }
}