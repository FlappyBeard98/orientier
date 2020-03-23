﻿using System.Collections.Generic;

 namespace OrientDb.CodeGen
{
    public class OrientTypeScheme
    {
        public List<object> CustomFields { get; set; }
        public int DefaultClusterId { get; set; }
        public bool StrictMode { get; set; }
        public string Description { get; set; }
        public bool Abstract;
        public List<int> ClusterIds { get; set; }
        public string SuperClass { get; set; }
        public string Name { get; set; }
        public string ClusterSelection { get; set; }
        public string ShortName { get; set; }
        public float OverSize { get; set; }
        public List<OrientPropertyScheme> Properties { get; set; }
        public List<string> SuperClasses { get; set; }
    }
}