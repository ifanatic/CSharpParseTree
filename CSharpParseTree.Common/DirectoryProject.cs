using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharpParseTree.Common
{
    public class DirectoryProject : SourceCodeProject
    {      
        private DirectoryInfo _projectDirectory = null;
        private string _fileExtension = string.Empty;
        private IEnumerable<SourceFile> _sourceFiles = new List<SourceFile>(); 

        public DirectoryProject(string directoryFullPath, string fileExtension)
        {
            _projectDirectory = new DirectoryInfo(directoryFullPath);

            if (!_projectDirectory.Exists)
            {
                throw new DirectoryNotFoundException("Папка с проектом " + directoryFullPath + " не найдена");
            }

            _fileExtension = fileExtension;

            Name = _projectDirectory.Name; 

            FileInfo[] files = _projectDirectory.GetFiles("*." + _fileExtension, SearchOption.AllDirectories);

            FilesCount = files.Length;

            if(directoryFullPath[directoryFullPath.Length - 1] == Path.DirectorySeparatorChar)
            {
                directoryFullPath = directoryFullPath.Remove(directoryFullPath.Length - 1);
            }

            _sourceFiles = from fi in files
                           select new SourceFile(fi) { PathInProject = 
                               fi.FullName.Replace(directoryFullPath, "")};
        }

        public override IEnumerator<SourceFile> GetEnumerator()
        {
            return _sourceFiles.GetEnumerator();
        }
    }
}
