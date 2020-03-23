using System;
using System.Collections.Generic;
using System.Linq;

namespace OrientDb.CodeGen
{
    public class CodeGen
    {
        public static string Generate(IEnumerable<OrientTypeScheme> orientTypes, string @namespace)
        {
            //see : https://orientdb.com/docs/last/Types.html
            var types = new Dictionary<int, Type>
            {
                {0, typeof(Boolean)},
                {1, typeof(Int32)},
                {2, typeof(Int16)},
                {3, typeof(Int64)},
                {4, typeof(Single)},
                {5, typeof(Double)},
                {6, typeof(DateTime)},
                {7, typeof(String)},
                {8, typeof(byte[])},
                {9, typeof(Object)},
                {10, typeof(String)},
                {11, typeof(String)},
                {12, typeof(String)},
                {13, typeof(String)},
                {14, typeof(String)},
                {15, typeof(String)},
                {16, typeof(String)},
                {17, typeof(Byte)},
                {18, typeof(String)},
                {19, typeof(String)},
                {20, typeof(String)},
                {21, typeof(Decimal)},
                {22, typeof(String)},
                {23, typeof(String)},
            };

            string UpFirstLetter(string s) =>
                s.Select((p, i) => i == 0 ? Char.ToUpper(p) : p).Aggregate("", (p, q) => p + q);
            string LowerFirstLetter(string s) =>
                s.Select((p, i) => i == 0 ? Char.ToLower(p) : p).Aggregate("", (p, q) => p + q);

            string GetPropertyType(OrientPropertyScheme prop)
            {
                var type = types[prop.Type];
                var typeName = type.FullName;
                // из за спецфики ориента даже если поле помечено как not null оно может быть null
                return !prop.NotNull && type.IsValueType? $"Nullable<{typeName}>" : $"{typeName}"; 
            }

            string GetProps(OrientTypeScheme typeScheme) =>
                typeScheme.Properties
                          .Where(p => !(typeScheme.SuperClass == "E" && (p.Name == "in" || p.Name == "out")))
                          .Aggregate("", (c, p) => $"{c}\r\n\t\tpublic {GetPropertyType(p)} {UpFirstLetter(p.Name)} {{get;private set; }}");

            string GenerateCtor(OrientTypeScheme typeScheme) 
            {
                var assignments =
                typeScheme.Properties
                          .Where(p => !(typeScheme.SuperClass == "E" && (p.Name == "in" || p.Name == "out")))
                          .Aggregate("", (c, p) => $"{c}\r\n\t\t\t{UpFirstLetter(p.Name)} = {LowerFirstLetter(p.Name)}; ");
                var args = 
                    typeScheme.Properties
                              .Where(p => !(typeScheme.SuperClass == "E" && (p.Name == "in" || p.Name == "out")))
                              .Aggregate("", (c, p) => $"{c},{GetPropertyType(p)} {LowerFirstLetter(p.Name)}")
                              ;
                args = args.Length <= 1 ? "" : args.Substring(1);
                var ctor = $"\t\t[JsonConstructor]\r\n\t\tpublic {typeScheme.Name}({args})\r\n\t\t{{{assignments}\r\n\t\t}}";
                return ctor;
            }
            
            string GetBaseClass(OrientTypeScheme t) => 
                $"{t.SuperClass} {(t.SuperClass != "E" ? "" : $"<{t.Properties.First(q => q.Name == "out").LinkedClass},{t.Properties.First(q => q.Name == "in").LinkedClass}>")}";
         

            var superClasses = new[] {"E", "V"};
            var classes = orientTypes
                          .Where(p => superClasses.Contains(p.SuperClass))
                          .Aggregate("",
                              (c, p) =>
                                  $"{c}\r\n\tpublic class {p.Name} : {GetBaseClass(p)}\r\n\t{{\r\n{GenerateCtor(p)}\r\n{GetProps(p)} \r\n\t}}\r\n");
       
           
            return $"using System;\r\nusing Newtonsoft.Json;\r\n\r\nnamespace {@namespace} \r\n{{\r\n{classes} }}";
        }
    }
}