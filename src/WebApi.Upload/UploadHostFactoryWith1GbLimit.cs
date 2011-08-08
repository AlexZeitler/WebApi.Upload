using System;
using System.ServiceModel;
using Microsoft.ApplicationServer.Http.Activation;
using Microsoft.ApplicationServer.Http.Description;

namespace WebApi.Upload {
	/// <summary>
	/// Creates an instance of ApplicationHostFactory with a file upload limit of 1 GB.
	/// </summary>
	public class UploadHostFactoryWith1GbLimit : HttpConfigurableServiceHostFactory {
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses) {
			var host = base.CreateServiceHost(constructorString, baseAddresses);

			foreach (HttpEndpoint endpoint in host.Description.Endpoints) {
				endpoint.TransferMode = TransferMode.Streamed;
				endpoint.MaxReceivedMessageSize = 1024 * 1024 * 1024;
			}
			return host;
		}
	}
}