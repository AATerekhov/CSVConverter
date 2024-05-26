using System.ComponentModel.Design;
using System.IO;

namespace CSVloader
{
    public abstract class Loader: ILoader
    {
        public string  Path { get; set; }
        public string Extention { get; init; } = ".txt";

        protected Loader(string path)
        {
            Path = path;
        }
        public virtual bool Save<T>(T element, string name)
        {
            throw new NotImplementedException();
        }
        public virtual T Load<T>(string name)
        {
            throw new NotImplementedException();
        }

        internal StreamWriter GetFileEndStreamWriter(string name) => new StreamWriter(GetFileStream(name, FileMode.Create));       

        private FileStream GetFileStream(string name, FileMode fileMode)
        {
            var fullPath = System.IO.Path.Combine(Path, name + Extention);

            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);

            var fs = File.Open(fullPath, fileMode);
            return fs;
        }

        public string? GetFileContent(string name)
        {
            using (var stream = GetFileStream(name, FileMode.Open)) 
            {
                return new StreamReader(stream).ReadLine();
            }
        }
    }
}
