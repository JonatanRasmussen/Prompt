using System;
using System.Collections.Generic; // Add this for List<T>
using System.IO;
using System.Text;

namespace GlobalNameSpace
{
    public class LocalDirectory
    {
        public string Name { get; }
        public string FullPath { get; }
        public string RelativePath { get; }
        public LocalDirectory? Parent { get; }
        public List<LocalDirectory> SubDirectories { get; }
        private List<LocalFile> Files { get; }

        public LocalDirectory(string name, string fullPath, LocalDirectory? parent)
        {
            Name = name;
            FullPath = fullPath;
            RelativePath = GenerateRelativePath(name, fullPath);
            Parent = parent;
            SubDirectories = new List<LocalDirectory>();
            Files = new List<LocalFile>();
        }

        private class LocalFile
        {
            public string Name { get; }
            public string FullPath { get; }
            public string RelativePath { get; }
            public string Content { get; }

            public LocalFile(string name, string fullPath, string content)
            {
                Name = name;
                FullPath = fullPath;
                RelativePath = GenerateRelativePath(name, fullPath);
                Content = content;
            }
        }

        public static string SrcCodeToString()
        {
            LocalDirectory? parent = null;
            string path = ProgramPaths.Src;
            string name = Path.GetDirectoryName(path) ?? "BudoMissing";
            LocalDirectory srcDir = new(name, path, parent);
            srcDir.LoadContent();
            return srcDir.DirContentToString();
        }

        public void PrintContent()
        {
            var allFiles = GetFilesAsList();
            foreach (var file in allFiles)
            {
                Console.WriteLine($"{file.RelativePath}: {file.Content.Length} characters");
            }
            Console.WriteLine($"Total files: {allFiles.Count}");
        }

        public string DirContentToString()
        {
            StringBuilder sb = new();
            RecursivelyAppendContent(sb);
            return sb.ToString();
        }

        private void RecursivelyAppendContent(StringBuilder sb)
        {
            foreach (var file in Files)
            {
                sb.Append($"\n\n\n// {file.RelativePath}:\n\n{file.Content}");
            }

            foreach (var subDir in SubDirectories)
            {
                subDir.RecursivelyAppendContent(sb);
            }
        }

        private void LoadContent()
        {
            try
            {
                foreach (var file in Directory.GetFiles(FullPath))
                {
                    string fileName = Path.GetFileName(file);
                    string fileContent = File.ReadAllText(file);
                    LocalFile localFile = new(fileName, FullPath, fileContent);
                    Files.Add(localFile);
                }

                foreach (var dir in Directory.GetDirectories(FullPath))
                {
                    string dirName = Path.GetFileName(dir);
                    LocalDirectory subDirectory = new(dirName, dir, this);
                    SubDirectories.Add(subDirectory);
                    subDirectory.LoadContent();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied to {FullPath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading directory {FullPath}: {ex.Message}");
            }
        }

        private List<LocalFile> GetFilesAsList()
        {
            List<LocalFile> allFiles = new List<LocalFile>();
            GetFiles(allFiles);
            return allFiles;
        }

        private void GetFiles(List<LocalFile> fileList)
        {
            fileList.AddRange(Files);
            foreach (var subDir in SubDirectories)
            {
                subDir.GetFiles(fileList);
            }
        }

        private static string GenerateRelativePath(string name, string fullPath)
        {
            string filePath = Path.Combine(fullPath, name);
            string relativePath = filePath.Replace(MyLocalConfigs.AbsolutePath, string.Empty);
            return relativePath;
        }
    }
}
