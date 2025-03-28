﻿using System;
using System.IO;

namespace Logging
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    class ConsoleLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            Console.WriteLine(message);
        }
    }

    class FileLogWritter : ILogger
    {
        public virtual void WriteError(string message)
        {
            File.WriteAllText("log.txt", message);
        }
    }

    class SecureConsoleLogWritter : ConsoleLogWritter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class SecureFileLogWritter : FileLogWritter
    {
        public override void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                base.WriteError(message);
            }
        }
    }

    class MultiLogWritter : ILogger
    {
        private readonly List<ILogger> _loggers;

        public MultiLogWritter(List<ILogger> loggers)
        {
            ArgumentNullException.ThrowIfNull(loggers);
            _loggers = loggers;
        }

        public void WriteError(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.WriteError(message);
            }
        }
    }

    class PathFinder
    {
        private readonly ILogger _logger;

        public PathFinder(ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(logger);
            _logger = logger;
        }

        public void Find()
        {
            _logger.WriteError("Сообщение");
        }
    }

    interface ILogger
    {
        void WriteError(string message);
    }
}
