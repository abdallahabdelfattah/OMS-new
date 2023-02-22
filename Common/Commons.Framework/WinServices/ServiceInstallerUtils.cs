// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceInstallerUtils.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.WinServices
{
    #region

    using System;
    using System.Runtime.InteropServices;
    using System.Threading;

    #endregion

    /// <summary>
    /// </summary>
    [Flags]
    public enum ServiceManagerRights
    {
        /// <summary>
        /// </summary>
        Connect = 0x0001,

        /// <summary>
        /// </summary>
        CreateService = 0x0002,

        /// <summary>
        /// </summary>
        EnumerateService = 0x0004,

        /// <summary>
        /// </summary>
        Lock = 0x0008,

        /// <summary>
        /// </summary>
        QueryLockStatus = 0x0010,

        /// <summary>
        /// </summary>
        ModifyBootConfig = 0x0020,

        /// <summary>
        /// </summary>
        StandardRightsRequired = 0xF0000,

        /// <summary>
        /// </summary>
        AllAccess = StandardRightsRequired | Connect | CreateService | EnumerateService | Lock | QueryLockStatus
                    | ModifyBootConfig
    }

    /// <summary>
    /// </summary>
    [Flags]
    public enum ServiceRights
    {
        /// <summary>
        /// </summary>
        QueryConfig = 0x1,

        /// <summary>
        /// </summary>
        ChangeConfig = 0x2,

        /// <summary>
        /// </summary>
        QueryStatus = 0x4,

        /// <summary>
        /// </summary>
        EnumerateDependants = 0x8,

        /// <summary>
        /// </summary>
        Start = 0x10,

        /// <summary>
        /// </summary>
        Stop = 0x20,

        /// <summary>
        /// </summary>
        PauseContinue = 0x40,

        /// <summary>
        /// </summary>
        Interrogate = 0x80,

        /// <summary>
        /// </summary>
        UserDefinedControl = 0x100,

        /// <summary>
        /// </summary>
        Delete = 0x00010000,

        /// <summary>
        /// </summary>
        StandardRightsRequired = 0xF0000,

        /// <summary>
        /// </summary>
        AllAccess = StandardRightsRequired | QueryConfig | ChangeConfig | QueryStatus | EnumerateDependants | Start
                    | Stop | PauseContinue | Interrogate | UserDefinedControl
    }

    /// <summary>
    /// </summary>
    public enum ServiceBootFlag
    {
        /// <summary>
        /// </summary>
        Start = 0x00000000,

        /// <summary>
        /// </summary>
        SystemStart = 0x00000001,

        /// <summary>
        /// </summary>
        AutoStart = 0x00000002,

        /// <summary>
        /// </summary>
        DemandStart = 0x00000003,

        /// <summary>
        /// </summary>
        Disabled = 0x00000004
    }

    /// <summary>
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// </summary>
        Unknown = -1, // The state cannot be (has not been) retrieved.

        /// <summary>
        /// </summary>
        NotFound = 0, // The service is not known on the host server.

        /// <summary>
        /// </summary>
        Stop = 1, // The service is NET stopped.

        /// <summary>
        /// </summary>
        Run = 2, // The service is NET started.

        /// <summary>
        /// </summary>
        Stopping = 3,

        /// <summary>
        /// </summary>
        Starting = 4,

        /// <summary>
        ///     The paused.
        /// </summary>
        Paused = 5
    }

    /// <summary>
    /// </summary>
    public enum ServiceControl
    {
        /// <summary>
        /// </summary>
        Stop = 0x00000001,

        /// <summary>
        /// </summary>
        Pause = 0x00000002,

        /// <summary>
        /// </summary>
        Continue = 0x00000003,

        /// <summary>
        /// </summary>
        Interrogate = 0x00000004,

        /// <summary>
        /// </summary>
        Shutdown = 0x00000005,

        /// <summary>
        /// </summary>
        ParamChange = 0x00000006,

        /// <summary>
        /// </summary>
        NetBindAdd = 0x00000007,

        /// <summary>
        /// </summary>
        NetBindRemove = 0x00000008,

        /// <summary>
        /// </summary>
        NetBindEnable = 0x00000009,

        /// <summary>
        /// </summary>
        NetBindDisable = 0x0000000A
    }

    /// <summary>
    /// </summary>
    public enum ServiceError
    {
        /// <summary>
        /// </summary>
        Ignore = 0x00000000,

        /// <summary>
        /// </summary>
        Normal = 0x00000001,

        /// <summary>
        /// </summary>
        Severe = 0x00000002,

        /// <summary>
        /// </summary>
        Critical = 0x00000003
    }

    /// <summary>
    ///     Installs and provides functionality for handling windows services
    /// </summary>
    public class ServiceInstallerNativeMethods
    {
        /// <summary>
        ///     The service win 32 own process.
        /// </summary>
        private const int ServiceWin32OwnProcess = 0x00000010;

        // ReSharper disable once UnusedMember.Local
        /// <summary>
        ///     The standard rights required.
        /// </summary>
        private const int StandardRightsRequired = 0xF0000;

        /// <summary>
        /// Takes a service name and returns the
        ///     <code>
        /// ServiceState
        /// </code>
        /// of the corresponding service
        /// </summary>
        /// <param name="serviceName">
        /// The service name that we will check for his
        ///     <code>
        /// ServiceState
        /// </code>
        /// </param>
        /// <returns>
        /// The ServiceState of the service we wanted to check
        /// </returns>
        public static ServiceState GetServiceStatus(string serviceName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var hService = OpenService(scman, serviceName, ServiceRights.QueryStatus);
                if (hService == IntPtr.Zero)
                {
                    return ServiceState.NotFound;
                }

                try
                {
                    return GetServiceStatus(hService);
                }
                finally
                {
                    CloseServiceHandle(scman);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name, a service display name and the path to the service executable and installs / starts the
        ///     windows service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name that this service will have
        /// </param>
        /// <param name="displayName">
        /// The display name that this service will have
        /// </param>
        /// <param name="fileName">
        /// The path to the executable of the service
        /// </param>
        public static void InstallAndStart(string serviceName, string displayName, string fileName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect | ServiceManagerRights.CreateService);
            try
            {
                var service = OpenService(scman, serviceName, ServiceRights.QueryStatus | ServiceRights.Start);
                if (service == IntPtr.Zero)
                {
                    service = CreateService(
                        scman,
                        serviceName,
                        displayName,
                        ServiceRights.QueryStatus | ServiceRights.Start,
                        ServiceWin32OwnProcess,
                        ServiceBootFlag.AutoStart,
                        ServiceError.Normal,
                        fileName,
                        null,
                        IntPtr.Zero,
                        null,
                        null,
                        null);
                }

                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed to install service.");
                }

                try
                {
                    StartService(service);
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Accepts a service name and returns true if the service with that service name exists
        /// </summary>
        /// <param name="serviceName">
        /// The service name that we will check for existence
        /// </param>
        /// <returns>
        /// True if that service exists false otherwise
        /// </returns>
        public static bool ServiceIsInstalled(string serviceName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var service = OpenService(scman, serviceName, ServiceRights.QueryStatus);
                if (service == IntPtr.Zero)
                {
                    return false;
                }

                CloseServiceHandle(service);
                return true;
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name and starts it
        /// </summary>
        /// <param name="name">
        /// The service name
        /// </param>
        public static void StartService(string name)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var hService = OpenService(scman, name, ServiceRights.QueryStatus | ServiceRights.Start);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }

                try
                {
                    StartService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="name">
        /// The service name that will be stopped
        /// </param>
        public static void StopService(string name)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var hService = OpenService(scman, name, ServiceRights.QueryStatus | ServiceRights.Stop);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }

                try
                {
                    StopService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name and tries to stop and then uninstall the windows serviceError
        /// </summary>
        /// <param name="serviceName">
        /// The windows service name to uninstall
        /// </param>
        public static void Uninstall(string serviceName)
        {
            var scman = OpenScManager(ServiceManagerRights.Connect);
            try
            {
                var service = OpenService(
                    scman,
                    serviceName,
                    ServiceRights.StandardRightsRequired | ServiceRights.Stop | ServiceRights.QueryStatus);
                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Service not installed.");
                }

                try
                {
                    StopService(service);
                    var ret = DeleteService(service);
                    if (ret == 0)
                    {
                        var error = Marshal.GetLastWin32Error();
                        throw new ApplicationException("Could not delete service " + error);
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// The close service handle.
        /// </summary>
        /// <param name="hScObject">
        /// The h sc object.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("advapi32.dll")]
        private static extern int CloseServiceHandle(IntPtr hScObject);

        /// <summary>
        /// The control service.
        /// </summary>
        /// <param name="hService">
        /// The h service.
        /// </param>
        /// <param name="dwControl">
        /// The dw control.
        /// </param>
        /// <param name="lpServiceStatus">
        /// The lp service status.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("advapi32.dll")]
        private static extern int ControlService(
            IntPtr hService,
            ServiceControl dwControl,
            ServiceStatus lpServiceStatus);

        /// <summary>
        /// The create service.
        /// </summary>
        /// <param name="hScManager">
        /// The h sc manager.
        /// </param>
        /// <param name="lpServiceName">
        /// The lp service name.
        /// </param>
        /// <param name="lpDisplayName">
        /// The lp display name.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="dwServiceType">
        /// The dw service type.
        /// </param>
        /// <param name="dwStartType">
        /// The dw start type.
        /// </param>
        /// <param name="dwErrorControl">
        /// The dw error control.
        /// </param>
        /// <param name="lpBinaryPathName">
        /// The lp binary path name.
        /// </param>
        /// <param name="lpLoadOrderGroup">
        /// The lp load order group.
        /// </param>
        /// <param name="lpdwTagId">
        /// The lpdw tag id.
        /// </param>
        /// <param name="lpDependencies">
        /// The lp dependencies.
        /// </param>
        /// <param name="lp">
        /// The lp.
        /// </param>
        /// <param name="lpPassword">
        /// The lp password.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("advapi32.dll", EntryPoint = "CreateServiceA", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateService(
            IntPtr hScManager,
            string lpServiceName,
            string lpDisplayName,
            ServiceRights dwDesiredAccess,
            int dwServiceType,
            ServiceBootFlag dwStartType,
            ServiceError dwErrorControl,
            string lpBinaryPathName,
            string lpLoadOrderGroup,
            IntPtr lpdwTagId,
            string lpDependencies,
            string lp,
            string lpPassword);

        /// <summary>
        /// The delete service.
        /// </summary>
        /// <param name="hService">
        /// The h service.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int DeleteService(IntPtr hService);

        /// <summary>
        /// Gets the service state by using the handle of the provided windows service
        /// </summary>
        /// <param name="hService">
        /// The handle to the service
        /// </param>
        /// <returns>
        /// The
        ///     <code>
        /// ServiceState
        /// </code>
        /// of the service
        /// </returns>
        private static ServiceState GetServiceStatus(IntPtr hService)
        {
            var ssStatus = new ServiceStatus();
            if (QueryServiceStatus(hService, ssStatus) == 0)
            {
                throw new ApplicationException("Failed to query service status.");
            }

            return ssStatus.dwCurrentState;
        }

        /// <summary>
        /// Opens the service manager
        /// </summary>
        /// <param name="rights">
        /// The service manager rights
        /// </param>
        /// <returns>
        /// the handle to the service manager
        /// </returns>
        private static IntPtr OpenScManager(ServiceManagerRights rights)
        {
            var scman = OpenSCManager(null, null, rights);
            if (scman == IntPtr.Zero)
            {
                throw new ApplicationException("Could not connect to service control manager.");
            }

            return scman;
        }

        /// <summary>
        /// The open sc manager.
        /// </summary>
        /// <param name="lpMachineName">
        /// The lp machine name.
        /// </param>
        /// <param name="lpDatabaseName">
        /// The lp database name.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerA", CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenSCManager(
            string lpMachineName,
            string lpDatabaseName,
            ServiceManagerRights dwDesiredAccess);

        /// <summary>
        /// The open service.
        /// </summary>
        /// <param name="hScManager">
        /// The h sc manager.
        /// </param>
        /// <param name="lpServiceName">
        /// The lp service name.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("advapi32.dll", EntryPoint = "OpenServiceA", CharSet = CharSet.Unicode)]
        private static extern IntPtr OpenService(
            IntPtr hScManager,
            string lpServiceName,
            ServiceRights dwDesiredAccess);

        /// <summary>
        /// The query service status.
        /// </summary>
        /// <param name="hService">
        /// The h service.
        /// </param>
        /// <param name="lpServiceStatus">
        /// The lp service status.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("advapi32.dll")]
        private static extern int QueryServiceStatus(IntPtr hService, ServiceStatus lpServiceStatus);

        /// <summary>
        /// The start service.
        /// </summary>
        /// <param name="hService">
        /// The h service.
        /// </param>
        /// <param name="dwNumServiceArgs">
        /// The dw num service args.
        /// </param>
        /// <param name="lpServiceArgVectors">
        /// The lp service arg vectors.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("advapi32.dll", EntryPoint = "StartServiceA")]
        private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);

        /// <summary>
        /// Stars the provided windows service
        /// </summary>
        /// <param name="hService">
        /// The handle to the windows service
        /// </param>
        private static void StartService(IntPtr hService)
        {
            // var status = new ServiceStatus();
            StartService(hService, 0, 0);
            WaitForServiceStatus(hService, ServiceState.Starting, ServiceState.Run);
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="hService">
        /// The handle to the windows service
        /// </param>
        private static void StopService(IntPtr hService)
        {
            var status = new ServiceStatus();
            ControlService(hService, ServiceControl.Stop, status);
            WaitForServiceStatus(hService, ServiceState.Stopping, ServiceState.Stop);
        }

        /// <summary>
        /// Returns true when the service status has been changes from wait status to desired status
        ///     ,this method waits around 10 seconds for this operation.
        /// </summary>
        /// <param name="hService">
        /// The handle to the service
        /// </param>
        /// <param name="waitStatus">
        /// The current state of the service
        /// </param>
        /// <param name="desiredStatus">
        /// The desired state of the service
        /// </param>
        /// <returns>
        /// bool if the service has successfully changed states within the allowed timeline
        /// </returns>
        private static bool WaitForServiceStatus(IntPtr hService, ServiceState waitStatus, ServiceState desiredStatus)
        {
            var ssStatus = new ServiceStatus();

            QueryServiceStatus(hService, ssStatus);
            if (ssStatus.dwCurrentState == desiredStatus)
            {
                return true;
            }

            var dwStartTickCount = Environment.TickCount;
            var dwOldCheckPoint = ssStatus.dwCheckPoint;

            while (ssStatus.dwCurrentState == waitStatus)
            {
                // Do not wait longer than the wait hint. A good interval is
                // one tenth the wait hint, but no less than 1 second and no
                // more than 10 seconds.
                var dwWaitTime = ssStatus.dwWaitHint / 10;

                if (dwWaitTime < 1000)
                {
                    dwWaitTime = 1000;
                }
                else if (dwWaitTime > 10000)
                {
                    dwWaitTime = 10000;
                }

                Thread.Sleep(dwWaitTime);

                // Check the status again.
                if (QueryServiceStatus(hService, ssStatus) == 0)
                {
                    break;
                }

                if (ssStatus.dwCheckPoint > dwOldCheckPoint)
                {
                    // The service is making progress.
                    dwStartTickCount = Environment.TickCount;
                    dwOldCheckPoint = ssStatus.dwCheckPoint;
                }
                else
                {
                    if (Environment.TickCount - dwStartTickCount > ssStatus.dwWaitHint)
                    {
                        // No progress made within the wait hint
                        break;
                    }
                }
            }

            return ssStatus.dwCurrentState == desiredStatus;
        }

        /// <summary>
        ///     The service status.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class ServiceStatus
        {
            /// <summary>
            ///     The dw check point.
            /// </summary>
            public readonly int dwCheckPoint = 0;

            /// <summary>
            ///     The dw current state.
            /// </summary>
            public readonly ServiceState dwCurrentState = 0;

            /// <summary>
            ///     The dw wait hint.
            /// </summary>
            public readonly int dwWaitHint = 0;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ServiceStatus" /> class.
            /// </summary>
            public ServiceStatus()
            {
                this.DwServiceSpecificExitCode = 0;
                this.DwWin32ExitCode = 0;
                this.DwControlsAccepted = 0;
                this.DwServiceType = 0;
            }

            /// <summary>
            ///     Gets or sets the dw controls accepted.
            /// </summary>
            private int DwControlsAccepted { get; }

            /// <summary>
            ///     Gets or sets the dw service specific exit code.
            /// </summary>
            private int DwServiceSpecificExitCode { get; }

            /// <summary>
            ///     Gets or sets the dw service type.
            /// </summary>
            private int DwServiceType { get; }

            /// <summary>
            ///     Gets or sets the dw win 32 exit code.
            /// </summary>
            private int DwWin32ExitCode { get; }
        }
    }
}