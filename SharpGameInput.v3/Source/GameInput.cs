using System.Diagnostics.CodeAnalysis;
using SharpGameInput.Common;

namespace SharpGameInput.v3
{
    public static partial class GameInput
    {
        public static bool Create([NotNullWhen(true)] out IGameInput? gameInput)
        {
            return Create(out gameInput, out _);
        }

        public static bool Create([NotNullWhen(true)] out IGameInput? gameInput, out int result)
        {
            bool success = GameInputModule.Create(IGameInput.Iid, out var handle, out result);
            gameInput = success ? new(handle, ownsHandle: true) : null;
            return success;
        }
    }
}