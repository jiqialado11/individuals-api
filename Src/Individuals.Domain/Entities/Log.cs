namespace Individuals.Domain.Entities
{
    public class Log:Entity
    {
        #region Constructors
        protected Log()
        {

        }
        public Log(string machineName, string logged, string level,
            string message, string logger, string callsite, string exception)
        {
            MachineName = machineName;
            Logged = logged;
            Level = level;
            Message = message;
            Logger = logger;
            Callsite = callsite;
            Exception = exception;
        }

        #endregion

        #region Properties

        public string MachineName { get; protected set; }
        public string Logged { get; protected set; }
        public string Level { get; protected set; }
        public string Message { get; protected set; }
        public string Logger { get; protected set; }
        public string Callsite { get; protected set; }
        public string Exception { get; protected set; }

        #endregion

    }
}
