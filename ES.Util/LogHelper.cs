using NLog;
using System;

namespace ES.Util
{
    public static class LogHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Error(object msg, Exception exp = null)
        {
            if (exp == null)
                logger.Error(msg);
            else
                logger.Error(msg + "  " + exp.ToString());
        }

        public static void Debug(object msg, Exception exp = null)
        {
            if (exp == null)
                logger.Debug(msg);
            else
                logger.Debug(msg + "  " + exp.ToString());
        }

        public static void Info(object msg, Exception exp = null)
        {
            if (exp == null)
                logger.Info(msg);
            else
                logger.Info(msg + "  " + exp.ToString());
        }

        public static void Warn(object msg, Exception exp = null)
        {
            if (exp == null)
                logger.Warn(msg);
            else
                logger.Warn(msg + "  " + exp.ToString());
        }
    }
}
