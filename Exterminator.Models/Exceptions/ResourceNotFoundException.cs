using System;

namespace Exterminator.Models.Exceptions
{
    public class RecorceNotFoundException : Exception
    {   public RecorceNotFoundException() : base("This recource was not found!") { }
        public RecorceNotFoundException(string message) : base(message) { }
        public RecorceNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}