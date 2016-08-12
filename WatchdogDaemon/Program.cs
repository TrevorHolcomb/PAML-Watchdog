using System;
using System.Threading;
using Ninject;
using NLog;
using WatchdogDaemon.Processes;
using WatchdogDaemon.RuleEngine;
using WatchdogDaemon.RuleEngine.TreeEngine;
using WatchdogDatabaseAccessLayer;

namespace WatchdogDaemon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
        }

        private readonly StandardKernel _kernel;
        private volatile bool _working;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly Barrier _cleanup;
        
        /// <summary>
        /// Starts the Alerter, and Preprocessor in thier own threads and waits for the user to press q to quit the program.
        /// </summary>
        public Program()
        {
            _working = true;
            _cleanup = new Barrier(3);

            _kernel = new StandardKernel();
            _kernel.Load(new EFModule());
            _kernel.Bind<IRuleEngine>().To<TreeExpressionEvaluator>();

            var preprocessorThread = new Thread(StartPreprocessor);
            var alerterThread = new Thread(StartAlerter);

            preprocessorThread.Start();
            alerterThread.Start();

            Logger.Info("Logger Thread Waiting");
            Console.WriteLine("Press \'Q\' to quit");
            while (Console.ReadKey().Key != ConsoleKey.Q){ }
            _working = false;

            _cleanup.SignalAndWait();
            _kernel.Dispose();
        }

        /// <summary>
        /// StartAlerter instantiates a new Alerter and injects it with the required dependencies.
        /// </summary>
        public void StartAlerter()
        {
            Logger.Info("Starting Alerter Thread");
            using (var alerter = new Alerter())
            {
                _kernel.Inject(alerter);

                while (_working)
                {
                    alerter.Run();
                }
                Logger.Info("Alerter Thread Waiting To Exit");
                _cleanup.SignalAndWait();
                Logger.Info("Alerter Thread Exiting");
            }
        }

        /// <summary>
        /// StartPreprocessor instantiates a new Preprocessor and injects it with the required dependencies.
        /// </summary>
        public void StartPreprocessor()
        {
            Logger.Info("Starting Preprocessor Thread");
            using (var preprocessor = new Preprocessor())
            {
                _kernel.Inject(preprocessor);

                while (_working)
                {
                    preprocessor.Run();
                }

                Logger.Info("Preprocesor Thread Waiting To Exit");
                _cleanup.SignalAndWait();
                Logger.Info("Preprocessor Thread Exiting");
            }
        }
    }
}
