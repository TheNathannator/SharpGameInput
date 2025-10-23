using System;
using System.Diagnostics.CodeAnalysis;
using SharpGameInput.Common;

namespace SharpGameInput.v0
{
    public static partial class GameInput
    {
        private static readonly Guid IID_IGameInput = new("11BE2A7E-4254-445A-9C09-FFC40F006918");

        public static bool Create([NotNullWhen(true)] out IGameInput? gameInput)
        {
            return Create(out gameInput, out _);
        }

        public static unsafe bool Create([NotNullWhen(true)] out IGameInput? gameInput, out int result)
        {
            bool success = GameInputModule.Create(IID_IGameInput, out var handle, out result);
            gameInput = success ? new(handle, ownsHandle: true) : null;
            return success;
        }
    }
}