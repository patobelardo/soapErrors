﻿using System.Net.Security;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Models
{


	[ServiceContract]
	public interface ISampleService
	{
		[OperationContract]
		string Ping(string s);

		[OperationContract]
		[FaultContractAttribute(typeof(FaultDetails),
			Action="http://www.contoso.com/GreetingFault")]
		ComplexModelResponse PingComplexModel(ComplexModelInput inputModel);

		[OperationContract]
		void VoidMethod(out string s);

		[OperationContract]
		Task<int> AsyncMethod();

		[OperationContract]
		int? NullableMethod(bool? arg);

		[OperationContract]
		void XmlMethod(System.Xml.Linq.XElement xml);
	}
}
