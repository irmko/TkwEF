﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WindowsServiceHostTkwService
{
    public partial class TkwServiceHost : ServiceBase
    {
        private ServiceHost service_host = null;

        public TkwServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (service_host != null) service_host.Close();

            string address_HTTP = "http://localhost:9001/MyService";
            string address_TCP = "net.tcp://localhost:9002/MyService";

            Uri[] address_base = { new Uri(address_HTTP), new Uri(address_TCP) };
            service_host = new ServiceHost(typeof(TkwServiceLibrary.TkwService), address_base);

            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            service_host.Description.Behaviors.Add(behavior);

            BasicHttpBinding binding_http = new BasicHttpBinding();
            service_host.AddServiceEndpoint(typeof(TkwContract.ITkwService), binding_http, address_HTTP);
            service_host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            NetTcpBinding binding_tcp = new NetTcpBinding();
            binding_tcp.Security.Mode = SecurityMode.Transport;
            binding_tcp.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            binding_tcp.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            binding_tcp.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            service_host.AddServiceEndpoint(typeof(TkwContract.ITkwService), binding_tcp, address_TCP);
            service_host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            service_host.Open();
        }

        protected override void OnStop()
        {
            if (service_host != null)
            {
                service_host.Close();
                service_host = null;
            }
        }
    }
}