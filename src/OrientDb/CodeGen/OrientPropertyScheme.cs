﻿namespace OrientDb.CodeGen
{

   public class OrientPropertyScheme
   {
      public string Collate { get; set; }
      public object Min { get; set; }
      public bool Readonly { get; set; }
      public bool NotNull { get; set; }
      public object Max { get; set; }
      public object DefaultValue { get; set; }
      public object CustomFields { get; set; }
      public string Name { get; set; }
      public int GlobalId { get; set; }
      public string Description { get; set; }
      public int Type { get; set; }
      public bool Mandatory { get; set; }
      public string LinkedClass { get; set; }
   }

   
}