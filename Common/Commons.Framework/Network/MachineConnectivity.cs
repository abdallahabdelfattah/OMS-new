using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.Network
{
    public class MachineConnectivity
    {
        public enum ConnectivityMethod
        {
            Ping = 0,
            WMI = 1
        }
        public static bool IsMachineUp(string hostName, ConnectivityMethod connectivityMethod = ConnectivityMethod.Ping)
        {
            if (connectivityMethod == ConnectivityMethod.Ping)
            {
                return CheckIfMachineUpByBing(hostName);
            }
            else
            {
                return CheckIfMachineUpByWMI(hostName);
            }
        }

        public static bool IsMachineUp(IPAddress iPAddress, ConnectivityMethod connectivityMethod = ConnectivityMethod.Ping)
        {
            if (connectivityMethod == ConnectivityMethod.Ping)
            {
                return CheckIfMachineUpByBing(iPAddress);
            }
            else
            {
                return CheckIfMachineUpByWMI(iPAddress.ToString());
            }
        }

        private static bool CheckIfMachineUpByBing(IPAddress iPAddress)
        {
            bool retVal = false;
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "Check If the terminal online Or Offline";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                PingReply reply = pingSender.Send(iPAddress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }

        private static bool CheckIfMachineUpByBing(string hostName)
        {
            bool retVal = false;
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "Check If the terminal online Or Offline";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;

                PingReply reply = pingSender.Send(hostName, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }

        private static bool CheckIfMachineUpByWMI(string hostName)
        {
            bool retVal = false;
            ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", hostName));
            ManagementClass os = new ManagementClass(scope, new ManagementPath("Win32_OperatingSystem"), null);

            try
            {
                ManagementObjectCollection instances = os.GetInstances();
                retVal = true;
            }
            catch (Exception ex)
            {
                retVal = false;
                Console.WriteLine(ex.Message);
            }
            return retVal;
        }

    }
}
