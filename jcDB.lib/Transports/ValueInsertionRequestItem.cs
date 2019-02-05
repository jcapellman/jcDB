﻿using System.Runtime.Serialization;

namespace jcDB.lib.Transports {
    [DataContract]
    public class ValueInsertionRequestItem {
        [DataMember]
        public string ipAddress { get; set; }

        [DataMember]
        public string key { get; set; }

        [DataMember]
        public object objValue { get; set; }
    }
}