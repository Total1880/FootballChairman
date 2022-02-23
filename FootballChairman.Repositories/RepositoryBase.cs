using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballChairman.Repositories
{
    public abstract class RepositoryBase
    {
        internal readonly string path = @".\Data";
        internal void CreateFiles(string file)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (!File.Exists(Path.Combine(path, file)))
            {
                var createdFile = File.Create(Path.Combine(path, file));
                createdFile.Close();
            }
        }
    }
}
