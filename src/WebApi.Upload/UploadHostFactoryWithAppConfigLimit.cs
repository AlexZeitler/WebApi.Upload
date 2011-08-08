using System;
using System.Configuration;
using System.ServiceModel;
using Microsoft.ApplicationServer.Http.Activation;
using Microsoft.ApplicationServer.Http.Description;

namespace WebApi.Upload {
	/// <summary>
	/// Creates an instance of ApplicationHostFactory with a file upload limit whose value is set in web/app.config using the UploadHostFactoryLimit key / value pair. Value is in bytes as int.
	/// </summary>
	public class UploadHostFactoryWithAppConfigLimit : HttpConfigurableServiceHostFactory {
		public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses) {
			var host = base.CreateServiceHost(constructorString, baseAddresses);

			foreach (HttpEndpoint endpoint in host.Description.Endpoints) {
				endpoint.TransferMode = TransferMode.Streamed;
				string maxReceivedMessageSize = ConfigurationManager.AppSettings["UploadHostFactoryLimit"];
				if (string.IsNullOrEmpty(maxReceivedMessageSize)) 
					throw new ConfigurationErrorsException("UploadHostFactoryLimit key / value not set in Application Settings in your app/web.config");
				endpoint.MaxReceivedMessageSize = int.Parse(maxReceivedMessageSize);
			}
			return host;
		}
	}
}