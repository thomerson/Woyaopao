using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomerson.Woyaopao.Fuban.Core
{
    public class Logger
    {
        NLog.Logger _logger;

        private Logger(NLog.Logger logger)
        {
            _logger = logger;
        }

        public Logger(string name) : this(LogManager.GetLogger(name))
        {

        }

        public static Logger Default { get; private set; }
        static Logger()
        {
            Default = new Logger(NLog.LogManager.GetCurrentClassLogger());
        }

        #region Debug
        public void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public void Debug(string msg, Exception err)
        {
            _logger.Debug(err, msg);
        }
        #endregion

        #region Info
        public void Info(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }

        public void Info(string msg, Exception err)
        {
            _logger.Info(err, msg);
        }

        public void Error(Exception err)
        {
            _logger.Error(err);
        }
        #endregion
    }
}
