using System;

namespace Exceptions
{
    public class TileGenerationException : Exception
    {
        public TileGenerationException() { }

        public TileGenerationException(string message) 
            : base(message) { }

        public TileGenerationException(string message, Exception inner) 
            : base(message, inner) { }
    }

}