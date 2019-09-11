using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Models
{
	[DataContract] // This *must* be the same namespace as on the ServiceContract
	public class FaultDetails
	{
		[DataMember]
		public string Message { get; set; }
	}

	[DataContract]
	public class ComplexModelInput
    {
		[DataMember]
		public string StringProperty { get; set; }

        private int intProperty;
		[DataMember]
		public int IntProperty { 
            get{
                if (intProperty > 3)
                    throw new FaultException<FaultDetails>(
                        new FaultDetails { Message = "Int should be less than 2"}, 
                        new FaultReason("unknown"), 
                        new FaultCode("34"), 
                        "cancel");
                return intProperty;
            } 
            set{
                intProperty = value;
            }
        }

		[DataMember]
		public List<string> ListProperty { get; set; }

        [DataMember]
        public DateTimeOffset DateTimeOffsetProperty { get; set; }

        [DataMember]
        public List<ComplexObject> ComplexListProperty { get; set; }
    }

    [DataContract]
    public class ComplexObject
    {
        [DataMember]
        public string StringProperty { get; set; }

        [DataMember]
        public int IntProperty { get; set; }
    }
}
