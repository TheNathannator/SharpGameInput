using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SharpGameInput.Common;

namespace SharpGameInput.v0
{
    public delegate void GameInputReadingCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputReading reading,
        bool hasOverrunOccurred
    );

    public delegate void GameInputDeviceCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputDevice device,
        ulong timestamp,
        GameInputDeviceStatus currentStatus,
        GameInputDeviceStatus previousStatus
    );

    // public delegate void GameInputGuideButtonCallback(
    //     LightGameInputCallbackToken callbackToken,
    //     object? context,
    //     LightIGameInputDevice device,
    //     ulong timestamp,
    //     bool isPressed
    // );

    public delegate void GameInputSystemButtonCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputDevice device,
        ulong timestamp,
        GameInputSystemButtons currentState,
        GameInputSystemButtons previousState
    );

    public delegate void GameInputKeyboardLayoutCallback(
        LightGameInputCallbackToken callbackToken,
        object? context,
        LightIGameInputDevice device,
        ulong timestamp,
        uint currentLayout,
        uint previousLayout
    );

    public class GameInputCallbackToken : IEquatable<GameInputCallbackToken>
    {
        internal const ulong CurrentCallbackToken = 0xFFFFFFFFFFFFFFFF;
        internal const ulong InvalidCallbackToken = 0x0000000000000000;

        private IGameInput? _gameInput;
        internal readonly ulong _callbackToken;

        public GameInputCallbackToken(IGameInput gameInput, ulong callbackToken)
        {
            ThrowHelper.CheckNull(gameInput);
            if (callbackToken is InvalidCallbackToken or CurrentCallbackToken)
                throw new ArgumentException("The given token is invalid.", nameof(callbackToken));

            _gameInput = gameInput;
            _callbackToken = callbackToken;
        }

        public void Stop()
        {
            ThrowHelper.CheckDisposed(_gameInput);
            _gameInput.StopCallback(_callbackToken);
        }

        public void Unregister(ulong timeoutInMicroseconds)
        {
            if (!TryUnregister(timeoutInMicroseconds))
                throw new TimeoutException("Could not unregister callback within the given timeout period.");
        }

        public async Task UnregisterAsync(ulong timeoutInMicroseconds)
        {
            if (!await TryUnregisterAsync(timeoutInMicroseconds))
                throw new TimeoutException("Could not unregister callback within the given timeout period.");
        }

        public bool TryUnregister(ulong timeoutInMicroseconds)
        {
            if (_gameInput == null)
                return true;

            if (!_gameInput.UnregisterCallback(_callbackToken, timeoutInMicroseconds))
                return false;

            _gameInput = null;
            return true;
        }

        public async Task<bool> TryUnregisterAsync(ulong timeoutInMicroseconds)
        {
            if (_gameInput == null)
                return true;

            if (!await Task.Run(() => _gameInput.UnregisterCallback(_callbackToken, timeoutInMicroseconds)))
                return false;

            _gameInput = null;
            return true;
        }

        public static bool operator ==(GameInputCallbackToken? left, GameInputCallbackToken? right)
        {
            if (ReferenceEquals(left, right))
                return true;

            if (left is null || right is null)
                return false;

            // GameInput interfaces can be compared directly by pointer for equality
            return left._callbackToken == right._callbackToken;
        }

        public static bool operator !=(GameInputCallbackToken? left, GameInputCallbackToken? right)
            => !(left == right);

        public bool Equals([NotNullWhen(true)] GameInputCallbackToken? obj)
            => obj == this;

        public bool Equals(LightGameInputCallbackToken obj)
            => obj == this;

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is GameInputCallbackToken other && Equals(other);

        public override int GetHashCode()
            => _callbackToken.GetHashCode();
    }

    public ref struct LightGameInputCallbackToken
    {
        private readonly IGameInput _gameInput;
        internal readonly ulong _callbackToken;

        public LightGameInputCallbackToken(IGameInput gameInput, ulong callbackToken)
        {
            ThrowHelper.CheckNull(gameInput);
            // CurrentCallbackToken is not invalid here, as it's passed when doing
            // GameInputEnumerationKind.BlockingEnumeration on RegisterDeviceCallback
            if (callbackToken is GameInputCallbackToken.InvalidCallbackToken /*or GameInputCallbackToken.CurrentCallbackToken*/)
                throw new ArgumentException("The given token is invalid.", nameof(callbackToken));

            _gameInput = gameInput;
            _callbackToken = callbackToken;
        }

        // Only StopCallback is given functionality here, as it's the
        // only available operation on them when in callbacks according to the docs
        public void Stop()
        {
            _gameInput.StopCallback(_callbackToken);
        }

        public static bool operator ==(LightGameInputCallbackToken left, LightGameInputCallbackToken right)
        {
            return left._callbackToken == right._callbackToken;
        }

        public static bool operator ==(GameInputCallbackToken? left, LightGameInputCallbackToken right)
        {
            return left?._callbackToken == right._callbackToken;
        }

        public static bool operator ==(LightGameInputCallbackToken left, GameInputCallbackToken? right)
        {
            return left._callbackToken == right?._callbackToken;
        }

        public static bool operator !=(LightGameInputCallbackToken left, LightGameInputCallbackToken right)
            => !(left == right);

        public static bool operator !=(GameInputCallbackToken? left, LightGameInputCallbackToken right)
            => !(left == right);

        public static bool operator !=(LightGameInputCallbackToken left, GameInputCallbackToken? right)
            => !(left == right);

        public bool Equals(LightGameInputCallbackToken obj)
            => obj == this;

        public bool Equals(GameInputCallbackToken obj)
            => obj == this;

#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        [Obsolete("Equals(object) on LightGameInputCallbackToken will always throw an exception. Use the equality operator instead.", true)]
        public override bool Equals(object? obj)
            => throw new NotSupportedException();
#pragma warning restore CS0809

        public override int GetHashCode()
            => _callbackToken.GetHashCode();
    }

    public ref struct CallbackTokenDisposer
    {
        private GameInputCallbackToken? _callbackToken;
        private readonly ulong _timeoutInMicroseconds;

        public CallbackTokenDisposer(GameInputCallbackToken? callbackToken, ulong timeoutInMicroseconds)
        {
            _callbackToken = callbackToken;
            _timeoutInMicroseconds = timeoutInMicroseconds;
        }

        public void Dispose()
        {
            _callbackToken?.Unregister(_timeoutInMicroseconds);
            _callbackToken = null!;
        }
    }
}
