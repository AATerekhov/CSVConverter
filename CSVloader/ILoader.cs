using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVloader
{
    public interface ILoader
    {
        string Path { get; set; }
        string Extention { get; init; }
        T Load<T>(string name);
        bool Save<T>(T element, string name);
        string? GetFileContent(string name);
    }
}
