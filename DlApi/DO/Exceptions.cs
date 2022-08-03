using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    [Serializable]

    public class TheObjectIDDoesNotExist : Exception
    {
        public TheObjectIDDoesNotExist() : base() { }
        public TheObjectIDDoesNotExist(string message) : base(message) { }
        public TheObjectIDDoesNotExist(string message, Exception inner) : base(message, inner) { }
    };


    [Serializable]

    public class TheObjectIdAlreadyExist : Exception
    {
        public TheObjectIdAlreadyExist() : base() { }
        public TheObjectIdAlreadyExist(string message) : base(message) { }
        public TheObjectIdAlreadyExist(string message, Exception inner) : base(message, inner) { }
    };

    [Serializable]
    public class DroneAtFullCapacity : Exception
    {
        public DroneAtFullCapacity() : base() { }
        public DroneAtFullCapacity(string message) : base(message) { }
        public DroneAtFullCapacity(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]

    public class OutOfRangeValue : Exception
    {
        public OutOfRangeValue() : base() { }
        public OutOfRangeValue(string message) : base(message) { }
        public OutOfRangeValue(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]

    public class XMLFileLoadCreateException : Exception
    {
        public XMLFileLoadCreateException() : base() { }
        public XMLFileLoadCreateException(string message) : base(message) { }
        public XMLFileLoadCreateException(string message, Exception inner) : base(message, inner) { }
    }


    [Serializable]

    public class XMLElementDidNotFound : Exception
    {
        public XMLElementDidNotFound() : base() { }
        public XMLElementDidNotFound(string message) : base(message) { }
        public XMLElementDidNotFound(string message, Exception inner) : base(message, inner) { }
    }

}
