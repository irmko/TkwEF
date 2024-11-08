using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHostTkw
{
    public static class TkwHost
    {
        public static ServiceHost StartService()
        {
            ServiceHost hostDocBuh = null;
            try
            {
                //if (((System.ServiceModel.Configuration.ServicesSection)Globals.Config.Instance.ExeConfiguration.GetSectionGroup("system.serviceModel").Sections["services"]).Services
                //    .ContainsKey("SkyNET.Services.DocBuhService"))
                //{
                //    Uri address = new Uri(((System.ServiceModel.Configuration.ServicesSection)Globals.Config.Instance.ExeConfiguration.GetSectionGroup("system.serviceModel").Sections["services"])
                //        .Services["SkyNET.Services.DocBuhService"].Host.BaseAddresses[0].BaseAddress + "/DocBuhService");
                //    hostDocBuh = new ServiceHost(typeof(DocBuhService), address);
                //}
                //else
                //    hostDocBuh = new ServiceHost(typeof(DocBuhService));

                //Uri address = Utils.GetAddress(isTest, "DocBuhService");
                //hostDocBuh = new ServiceHost(typeof(DocBuhService), address);
                //hostDocBuh.Description.Endpoints.Clear();
                //System.ServiceModel.Description.ContractDescription contract = new System.ServiceModel.Description.ContractDescription(typeof(DocBuhService).FullName);
                //System.ServiceModel.Description.ServiceEndpoint endpoint = new System.ServiceModel.Description.ServiceEndpoint(contract);
                //hostDocBuh.Description.Endpoints.Add(endpoint);

                hostDocBuh = HelperHost.CreateHost<TkwServiceLibrary.TkwService>();
                if (hostDocBuh == null)
                    return null;

                string addresses = HelperHost.GetAddresses(hostDocBuh);
                hostDocBuh.Opening += (s, e) =>
                {
                    Console.WriteLine($"Начало старта: {DateTime.Now.ToString()}, URI = {addresses}");
                };
                hostDocBuh.Opened += (s, e) =>
                {
                    Console.WriteLine($"Окончание старта: {DateTime.Now.ToString()}");
                };
                hostDocBuh.Closing += (s, e) =>
                {
                    Console.WriteLine($"Начало остановки: {DateTime.Now.ToString()}, URI = {addresses}");
                };
                hostDocBuh.Closed += (s, e) =>
                {
                    Console.WriteLine($"Окончание остановки: {DateTime.Now.ToString()}");
                };
                hostDocBuh.Faulted += (s, e) =>
                {
                    Console.WriteLine($"Сервис перешел в состояние Faulted: {DateTime.Now.ToString()} ( {addresses} )");
                };
                hostDocBuh.Open();
            }
            catch (Exception)
            {
                hostDocBuh?.Abort();
                throw;
            }
            return hostDocBuh;
        }

        public static void StopService(ref ServiceHost host)
        {
            if (host == null)
                return;
            host.Close();
            host = null;
        }

    }
}
