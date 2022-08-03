using System;

namespace BO
{

    [Serializable]
    public class TheValueOutOfRange : Exception
    {
        public TheValueOutOfRange() : base() { }
        public TheValueOutOfRange(string message) : base(message) { }
        public TheValueOutOfRange(string message, Exception inner) : base(message, inner) { }

    }



    [Serializable]
    public class ThereIsWorngDetails : Exception
    {
        public ThereIsWorngDetails() : base() { }
        public ThereIsWorngDetails(string message) : base(message) { }
        public ThereIsWorngDetails(string message, Exception inner) : base(message, inner) { }
    }


    [Serializable]
    public class DroneCanNotBeSent : Exception
    {
        public DroneCanNotBeSent() : base() { }
        public DroneCanNotBeSent(string message) : base(message) { }
        public DroneCanNotBeSent(string message, Exception inner) : base(message, inner) { }
    }


    [Serializable]
    public class ThisActionIsNotPossible : Exception
    {
        public ThisActionIsNotPossible() : base() { }
        public ThisActionIsNotPossible(string message) : base(message) { }
        public ThisActionIsNotPossible(string message, Exception inner) : base(message, inner) { }
    }


    [Serializable]
    public class ThereIsNotEnoughBattery : Exception
    {
        public ThereIsNotEnoughBattery() : base() { }
        public ThereIsNotEnoughBattery(string message) : base(message) { }
        public ThereIsNotEnoughBattery(string message, Exception inner) : base(message, inner) { }

    }


    [Serializable]
    public class TheObjectCanBeDeleted : Exception
    {
        public TheObjectCanBeDeleted() : base() { }
        public TheObjectCanBeDeleted(string message) : base(message) { }
        public TheObjectCanBeDeleted(string message, Exception inner) : base(message, inner) { }

    }

    [Serializable]
    public class BadStatusException : Exception
    {
        public BadStatusException() : base() { }
        public BadStatusException(string message) : base(message) { }
        public BadStatusException(string message, Exception inner) : base(message, inner) { }

    }


    [Serializable]
    public class TheObjectIDDoesNotExist : Exception
    {
        public TheObjectIDDoesNotExist() : base() { }
        public TheObjectIDDoesNotExist(string message) : base(message) { }
        public TheObjectIDDoesNotExist(string message, Exception inner) : base(message, inner) { }

    }

    [Serializable]
    public class TheObjectIdAlreadyExist : Exception
    {
        public TheObjectIdAlreadyExist() : base() { }
        public TheObjectIdAlreadyExist(string message) : base(message) { }
        public TheObjectIdAlreadyExist(string message, Exception inner) : base(message, inner) { }

    }

}