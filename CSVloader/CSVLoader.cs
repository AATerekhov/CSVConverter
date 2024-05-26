using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSVloader
{
    public class CSVLoader : Loader
    {
        public CSVLoader(string path) : base(path)
        {
            Extention = ".csv";
        }

        public override bool Save<T>(T element, string name)
        {
            try
            {
                using (var sw = GetFileEndStreamWriter(name))
                {
                    sw.WriteLine(GetSCVData(element));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }         

        }

        public override T Load<T>(string name)
        {
            var content = ConvertSrtingsForInts(GetFileContent(name).Split(";"));

            var typeElement = typeof(T);

            var constructorInfo = typeElement.GetConstructors().FirstOrDefault();
            if (constructorInfo != null)
            {
                var elemet = constructorInfo.Invoke(null);

                foreach (var propertyInfo in typeElement.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    var value =content[ConvertNamePropertyForNumber(propertyInfo.Name)];
                    propertyInfo.SetValue(elemet, value);
                }

                return (T)elemet;
            }            

            return default(T);
        }
        int ConvertNamePropertyForNumber(string name) 
        {            
            int.TryParse(name.TrimStart('i'), out var number);
            return number - 1;
        }
        int[] ConvertSrtingsForInts(string[] sv) 
        {
            int[] rezult = new int[sv.Length];

            for (int i = 0; i < sv.Length; i++)
            {
                int.TryParse(sv[i],out rezult[i]);
            }

            return rezult;
        }

        string GetSCVData<T>(T element) 
        {
            string reazult = string.Empty;
            var propertyes = element.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            for (int i = 0; i < propertyes.Length; i++)
            {
                reazult += propertyes[i].GetValue(element);
                if (i != propertyes.Length - 1)
                {
                    reazult += ";";
                }           
            }
            return reazult;
        }
    }
}
