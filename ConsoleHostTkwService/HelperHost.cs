using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHostTkwService
{
    class HelperHost
    {
        public static string GetAddresses(ServiceHost host)
        {
            string res = string.Empty;
            foreach (var item in host.Description.Endpoints)
            {
                res += string.Format("{0}\t{1};", Environment.NewLine, item.Address);
            }
            return res;
        }
        public static ServiceHost StartService<T>(bool isTest) where T : class
        {
            ServiceHost host = null;
            try
            {
                //host = new ServiceHost(typeof(T));
                host = CreateHost<T>(isTest);

                string addresses = GetAddresses(host);

                host.Opening += (s, e) =>
                {
                    Console.WriteLine($"Начало старта: {DateTime.Now.ToString()}, URI = {addresses}");
                };
                host.Opened += (s, e) =>
                {
                    Console.WriteLine($"Окончание старта: {DateTime.Now.ToString()}");
                };
                host.Closing += (s, e) =>
                {
                    Console.WriteLine($"Начало остановки: {DateTime.Now.ToString()}, URI = {addresses}");
                };
                host.Closed += (s, e) =>
                {
                    Console.WriteLine($"Окончание остановки: {DateTime.Now.ToString()}");
                };
                host.Faulted += (s, e) =>
                {
                    Console.WriteLine($"Сервис перешел в состояние Faulted: {DateTime.Now.ToString()} ( {addresses} )");
                };
#if _DEBUG

#endif
                host.Open();
            }
            catch (Exception)
            {
                host?.Abort();
                throw;
            }
            return host;
        }

        public static void StopService(ref ServiceHost host)
        {
            if (host == null)
                return;
            host.Close();
            host = null;
        }
        /// <summary>
        /// Возвращает адрес для создания хоста
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public static Uri GetAddress(bool test, string suffix)
        {
            if (test)
            {
                return new Uri(string.Format("{0}{1}{2}", "http://192.168.0.134:9001", (suffix.Trim().Length > 0 ? "/" : ""), suffix.Trim()));
            }
            else
            {
                return new Uri(string.Format("{0}{1}{2}", "net.tcp://192.168.0.134:9002", (suffix.Trim().Length > 0 ? "/" : ""), suffix.Trim()));
            }
        }

        public static ServiceHost CreateHost<T>(bool isTest = false)
        {
            try
            {
                Type type = typeof(T);
                ServiceHost host;
                if (isTest)
                {
                    #region binding

                    BasicHttpBinding binding = new BasicHttpBinding();

                    binding.AllowCookies = true;

#if _DEBUG
                    binding.CloseTimeout = TimeSpan.FromMinutes(60);
                    binding.SendTimeout = TimeSpan.FromMinutes(60);
                    binding.OpenTimeout = TimeSpan.FromMinutes(1);
                    binding.ReceiveTimeout = TimeSpan.FromMinutes(60);
#else
                    binding.CloseTimeout = TimeSpan.FromMinutes(2);
                    binding.SendTimeout = TimeSpan.FromMinutes(2);
                    binding.OpenTimeout = TimeSpan.FromMinutes(2);
                    binding.ReceiveTimeout = TimeSpan.FromMinutes(2);
#endif

                    binding.BypassProxyOnLocal = false;
                    binding.UseDefaultWebProxy = true;

                    binding.MaxBufferPoolSize = 2147483647;
                    binding.MaxReceivedMessageSize = 2147483647;

                    binding.Security.Mode = BasicHttpSecurityMode.None;

                    #endregion

                    #region endpoint adress

                    Uri endpointUri = GetAddress(isTest, type.Name);
                    Uri mexEndpointUri = new Uri(String.Format("{0}/mex", endpointUri.ToString()));

                    #endregion

                    host = new ServiceHost(type, endpointUri);

                    #region wcf behaviour setup

                    //  behoviours
                    host.Description.Behaviors.Clear();

                    ServiceBehaviorAttribute behaviour = new ServiceBehaviorAttribute();
                    behaviour.InstanceContextMode = InstanceContextMode.Single;
                    behaviour.ConcurrencyMode = ConcurrencyMode.Single;
                    behaviour.IncludeExceptionDetailInFaults = true;

                    host.Description.Behaviors.Add(behaviour);

                    ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
                    serviceMetadataBehavior.HttpGetEnabled = true;
                    host.Description.Behaviors.Add(serviceMetadataBehavior);

                    ServiceDebugBehavior serviceDebugBehavior = new ServiceDebugBehavior();
                    serviceDebugBehavior.HttpHelpPageEnabled = true;
                    serviceDebugBehavior.HttpsHelpPageEnabled = true;
                    serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
                    host.Description.Behaviors.Add(serviceDebugBehavior);

                    host.Description.Endpoints.Clear();
                    ContractDescription contract = ContractDescription.GetContract(type);
                    EndpointAddress address = new EndpointAddress(endpointUri.ToString());
                    ServiceEndpoint endpoint = new ServiceEndpoint(contract, binding, address);
                    host.Description.Endpoints.Add(endpoint);

                    #endregion

                    return host;
                }
                else
                {
                    #region binding

                    NetTcpBinding binding = new NetTcpBinding();

#if _DEBUG
                    binding.CloseTimeout = TimeSpan.FromMinutes(10);
                    binding.SendTimeout = TimeSpan.FromMinutes(10);
                    binding.OpenTimeout = TimeSpan.FromMinutes(10);
                    binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
#else
                    binding.CloseTimeout = TimeSpan.FromMinutes(2);
                    binding.SendTimeout = TimeSpan.FromMinutes(2);
                    binding.OpenTimeout = TimeSpan.FromMinutes(1);
                    binding.ReceiveTimeout = TimeSpan.FromMinutes(2);
#endif
                    binding.MaxBufferPoolSize = 2147483647;
                    binding.MaxReceivedMessageSize = 2147483647;
                    binding.ReaderQuotas.MaxArrayLength = 2147483647;

                    binding.Security.Mode = SecurityMode.Transport;

                    #endregion

                    #region endpoint adress

                    //string nameType;
                    //if (type.Equals(typeof(Services.CommonService)))
                    //    nameType = "CommonService";
                    //else if (type.Equals(typeof(Services.DocBuhService)))
                    //    nameType = "DocBuhService";
                    //else if (type.Equals(typeof(Services.RepTelService)))
                    //    nameType = "RepTelService";
                    //else if (type.Equals(typeof(Services.RequestService)))
                    //    nameType = "RequestService";
                    //else
                    //    throw new Exception("Неизвестный тип для создания хоста");
                    Uri endpointUri = GetAddress(isTest, type.Name);
                    Uri mexEndpointUri = new Uri(String.Format("{0}/mex", endpointUri.ToString()));

                    #endregion

                    host = new ServiceHost(type);

                    #region wcf behaviour setup

                    //  behoviours
                    host.Description.Behaviors.Clear();

                    ServiceBehaviorAttribute behaviour = new ServiceBehaviorAttribute();
                    behaviour.InstanceContextMode = InstanceContextMode.Single;
                    behaviour.ConcurrencyMode = ConcurrencyMode.Single;

                    host.Description.Behaviors.Add(behaviour);

                    //ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
                    //serviceMetadataBehavior.HttpGetEnabled = true;
                    //host.Description.Behaviors.Add(serviceMetadataBehavior);

                    //ServiceDebugBehavior serviceDebugBehavior = new ServiceDebugBehavior();
                    //serviceDebugBehavior.HttpHelpPageEnabled = true;
                    //serviceDebugBehavior.HttpsHelpPageEnabled = true;
                    //serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
                    //host.Description.Behaviors.Add(serviceDebugBehavior);

                    host.Description.Endpoints.Clear();
                    ContractDescription contract = ContractDescription.GetContract(type);
                    EndpointAddress address = new EndpointAddress(endpointUri.ToString());
                    ServiceEndpoint endpoint = new ServiceEndpoint(contract, binding, address);
                    host.Description.Endpoints.Add(endpoint);

                    #endregion

                    return host;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return null;
            }
        }

    }
}
