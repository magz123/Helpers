public static class ExceptionHelper
{
        /// <summary>
        /// Get ALL error messages from exception
        /// </summary>
        /// <param name="ex">The exception being thrown</param>
        /// <param name="currentMessage">Optional. Can be used to be added with the current exceptions errors</param>
        /// <param name="inner">Optional. Can be used to be added with the current exceptions errors</param>
        /// <returns>All Error Messages Identified on the current Exception</returns>
        public static string GetAllExceptionMessages(Exception ex, string currentMessage = "", string inner = "")
        {
            //Check if the current/looped exception is null
            //if True, then return the previously combined exception messages
            if (inner == null || ex == null)
                return currentMessage.Trim();

            currentMessage = currentMessage + ex.Message + Environment.NewLine;

            if (ex.InnerException == null)
                return currentMessage.Trim();

            inner = GetAllExceptionMessages(ex.InnerException);
            if (!currentMessage.Contains(inner.Trim()))
                currentMessage += inner;

            return currentMessage.Trim();
        }
}
