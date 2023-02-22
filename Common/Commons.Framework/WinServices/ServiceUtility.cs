// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceUtility.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.WinServices
{
    #region

    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.IO;
    using System.Linq;
    using System.Management;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Threading;

    #endregion

    /// <summary>
    ///     The service utility.
    /// </summary>
    [RunInstaller(true)]
    public class ServiceUtility
    {
        /// <summary>
        ///     The settings.
        /// </summary>
        private readonly ServiceSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceUtility"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public ServiceUtility(ServiceSettings settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// The do self installation.
        /// </summary>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="serviceExePath">
        /// The service exe path.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool DoSelfInstallation(ServiceBase service, string serviceExePath, string[] args)
        {
            try
            {
                if (args?.Length > 0)
                {
                    var rootServicePath = Path.GetDirectoryName(serviceExePath);

                    if (string.IsNullOrEmpty(rootServicePath))
                    {
                        return false;
                    }

                    var mainConfPath = Path.Combine(rootServicePath, $"{Path.GetFileName(serviceExePath)}.config");

                    var allowedEnvironments = new[] { "staging", "production" };
                    var allowedOperations = new[] { "/i", "/u" };

                    var operation = args[0];
                    var targetEnvironment = args.Length >= 2 ? args[1] : null;

                    if (!allowedOperations.Contains(operation))
                    {
                        // Show Error Invalid Operation
                        Console.WriteLine(
                            @"Invalid operation provided. pass /i for installation pass /u for uninstalling");
                        return false;
                    }

                    if (!string.IsNullOrEmpty(targetEnvironment) && !allowedEnvironments.Contains(targetEnvironment))
                    {
                        // Show Error Invalid Environment
                        Console.WriteLine(
                            @"Invalid Environment Name Provided. Allowed Values are STAGING, and PRODUCTION");
                        return false;
                    }

                    var configPath = string.IsNullOrEmpty(targetEnvironment)
                                         ? null
                                         : $"{rootServicePath}\\configs\\App.{targetEnvironment}.config";

                    // check config files exist
                    if (configPath != null && !File.Exists(configPath))
                    {
                        Console.WriteLine(
                            $@"{targetEnvironment} Config File Does Not Exist.. Make Sure the Config file Exists at: {
                                    configPath
                                }");
                        return false;
                    }

                    switch (operation)
                    {
                        case "/i":

                            if (configPath != null)
                            {
                                Console.WriteLine(
                                    $@"Service: {service.ServiceName} Applying {targetEnvironment} Config File ...");
                                File.Copy(configPath, mainConfPath, true);
                            }

                            // Install service
                            Console.WriteLine($@"Installing {service.ServiceName}...");
                            ManagedInstallerClass.InstallHelper(new[] { "/i", serviceExePath });
                            Console.WriteLine($@"Service {service.ServiceName}: Installed Successfully");

                            // ServiceBase.Run(servicesToRun);
                            return true;
                        case "/u":
                            // Uninstall service  
                            Console.WriteLine($@"Uninstalling {service.ServiceName}...");
                            ManagedInstallerClass.InstallHelper(new[] { "/u", serviceExePath });
                            Console.WriteLine($@"{service.ServiceName} Uninstalled Successfully");
                            return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"ERROR OCCURED");
                Console.WriteLine(e);
                return false;
            }

            return false;
        }

        /// <summary>
        /// The ServiceController class in .NET does not provide support for quering and changing the Startup type.
        ///     We use WMI Management Objects to do this.
        /// </summary>
        /// <param name="serviceName">
        /// The service Name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsStartupManual(string serviceName)
        {
            var managementObject = GetManagementObject(serviceName);
            return managementObject["StartMode"].ToString()
                .Equals("manual", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// For Debugging a Windows Service Project
        ///     http://dotnet.dzone.com/articles/debugging-windows-service
        /// </summary>
        /// <param name="servicesToRun">
        /// The services to run.
        /// </param>
        public static void RunInteractive(ServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            var onStartMethod =
                typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var service in servicesToRun)
            {
                Console.Write("Starting {0}...", service.ServiceName);
                onStartMethod.Invoke(service, new object[] { new string[] { } });
                Console.Write("Started");
            }

            Console.WriteLine("\n\nPress any key to stop the services and end the process...");
            Console.ReadKey();
            Console.WriteLine();

            var onStopMethod = typeof(ServiceBase).GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var service in servicesToRun)
            {
                Console.Write("Stopping {0}...", service.ServiceName);
                onStopMethod.Invoke(service, null);
                Console.WriteLine("Stopped");
            }

            Console.WriteLine("All services stopped.");

            // Keep the console alive for a 2 seconds to allow the user to see the message.
            Thread.Sleep(2000);
        }

        /// <summary>
        /// The ServiceController class in .NET does not provide support for quering and changing the Startup type.
        ///     We use WMI Management Objects to do this.
        /// </summary>
        /// <param name="serviceName">
        /// The service Name.
        /// </param>
        public static void SetStartupManual(string serviceName)
        {
            var managementObject = GetManagementObject(serviceName);
            managementObject.InvokeMethod("ChangeStartMode", new object[] { "Manual" });
        }

        /// <summary>
        ///     The deploy service.
        /// </summary>
        public void DeployService()
        {
            var processInstaller = new ServiceProcessInstaller { Account = this.settings.ServiceAccount };

            // EventLogInstaller customEventLogInstaller;
            if (processInstaller.Account == ServiceAccount.User)
            {
                processInstaller.Username = this.settings.ServiceAccountUserName;
                processInstaller.Password = this.settings.ServiceAccountPassword;
            }

            var serviceInstaller = new ServiceInstaller();
            var path = $"/assemblypath={this.settings.ServiceExecutalePath}";

            // String.Format("/assemblypath={0}", @"<<path of executable of window service>>");
            string[] cmdline = { path };

            var context = new InstallContext(string.Empty, cmdline);
            serviceInstaller.Context = context;
            serviceInstaller.DisplayName = this.settings.ServiceDisplayName;
            serviceInstaller.ServiceName = this.settings.ServiceName;
            serviceInstaller.Description = this.settings.ServiceDescription;
            serviceInstaller.StartType = this.settings.ServiceStartMode;

            // usama
            serviceInstaller.Parent = processInstaller;

            //// Create an instance of 'EventLogInstaller'.
            // customEventLogInstaller = new EventLogInstaller();
            //// Set the 'Source' of the event log, to be created.
            // customEventLogInstaller.Source = "customKoKoCoLog";
            //// Set the 'Event Log' that the source is created in.
            // customEventLogInstaller.Log = "KoKoCoApplication";
            //// Add myEventLogInstaller to 'InstallerCollection'.

            // serviceInstaller.Installers.Add(customEventLogInstaller);                
            var state = new ListDictionary();
            serviceInstaller.Install(state);
        }

        /// <summary>
        ///     The get service status.
        /// </summary>
        /// <returns>
        ///     The <see cref="ServiceState" />.
        /// </returns>
        public ServiceState GetServiceStatus()
        {
            var service = new ServiceController(this.settings.ServiceName);
            service.Refresh();

            if (ServiceInstallerNativeMethods.GetServiceStatus(this.settings.ServiceName) == ServiceState.Unknown)
            {
                return ServiceState.Unknown;
            }

            if (ServiceInstallerNativeMethods.GetServiceStatus(this.settings.ServiceName) == ServiceState.NotFound)
            {
                return ServiceState.NotFound;
            }

            var st = service.Status;

            if (st == ServiceControllerStatus.PausePending || st == ServiceControllerStatus.StopPending)
            {
                return ServiceState.Stopping;
            }

            if (st == ServiceControllerStatus.Running)
            {
                return ServiceState.Run;
            }

            if (st == ServiceControllerStatus.StartPending)
            {
                return ServiceState.Starting;
            }

            if (st == ServiceControllerStatus.Stopped || st == ServiceControllerStatus.Paused
                || st == ServiceControllerStatus.ContinuePending)
            {
                return ServiceState.Stop;
            }

            // ServiceInstallerUtils.GetServiceStatus(_settings.ServiceName);
            return ServiceState.Unknown;
        }

        /// <summary>
        ///     The remove service.
        /// </summary>
        public void RemoveService()
        {
            ServiceInstallerNativeMethods.Uninstall(this.settings.ServiceName);
            ManagedInstallerClass.InstallHelper(new[] { "/u", this.settings.ServiceExecutalePath });
        }

        /// <summary>
        ///     The restart service.
        /// </summary>
        public void RestartService()
        {
            var service = new ServiceController(this.settings.ServiceName);

            var timeOutinMilli = this.settings.ServiceTimeout * 1000;
            var millisec1 = Environment.TickCount;
            var timeout = TimeSpan.FromMilliseconds(timeOutinMilli);

            service.Stop();
            service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

            // count the rest of the timeout
            var millisec2 = Environment.TickCount;
            timeout = TimeSpan.FromMilliseconds(timeOutinMilli - (millisec2 - millisec1));

            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
        }

        /// <summary>
        ///     The start service.
        /// </summary>
        public void StartService()
        {
            var service = new ServiceController(this.settings.ServiceName);

            var timeout = TimeSpan.FromSeconds(this.settings.ServiceTimeout);
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, timeout);
        }

        /// <summary>
        ///     The stop service.
        /// </summary>
        public void StopService()
        {
            var service = new ServiceController(this.settings.ServiceName);

            var timeout = TimeSpan.FromSeconds(this.settings.ServiceTimeout);

            service.Stop();
            service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
        }

        /// <summary>
        /// The get management object.
        /// </summary>
        /// <param name="serviceCode">
        /// The service code.
        /// </param>
        /// <returns>
        /// The <see cref="ManagementObject"/>.
        /// </returns>
        private static ManagementObject GetManagementObject(string serviceCode)
        {
            return new ManagementObject(new ManagementPath($"Win32_Service.Name='{serviceCode}'"));
        }
    }
}