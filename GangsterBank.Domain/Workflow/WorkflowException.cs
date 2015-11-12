namespace GangsterBank.Domain.Workflow
{
    #region

    using System;
    using System.Runtime.Serialization;

    #endregion

    [Serializable]
    public class WorkflowException : Exception
    {
        #region Constructors and Destructors

        public WorkflowException()
        {
        }

        public WorkflowException(string message)
            : base(message)
        {
        }

        public WorkflowException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WorkflowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion
    }
}