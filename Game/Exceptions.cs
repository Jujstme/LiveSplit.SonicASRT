using System;

namespace LiveSplit.SonicASRT
{
    [Serializable]
    class GameNotfoundException : Exception
    {
        public GameNotfoundException() { }
    }

    [Serializable]
    class MemoryAddressNotfoundException : Exception
    {
        public MemoryAddressNotfoundException() { }

        public MemoryAddressNotfoundException(string name)
            : base(String.Format("Unable to find memory address for the following variable(s): {0}", name))
        {

        }
    }

    [Serializable]
    class InvalidHookException : Exception
    {
        public InvalidHookException() { }
        public InvalidHookException(string name) : base(name) { }
    }
}
