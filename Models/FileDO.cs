using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ASP_project.Models
{
    public class FileDO
    {
        public string startDir;

        public Stack<string> strings = new Stack<string>();
        public Dictionary<string, Description> pathToSize { get; set; } = new();


        public FileDO(string startDir)
        {
            this.startDir = startDir;
        }

        public Action<string> Write { get; set; } = Console.Write;

        public FileDO getInfoCatalog()
        {
            var d = new DirectoryInfo(startDir);
            long full_size = installDirSize(d);
            pathToSize.Add(d.FullName, new Description(full_size, startDir));
            //strings.Push($"{d.Name}: {full_size}");
            //Console.WriteLine(String.Join("\n", strings));

            return this;
        }

        private long Length(FileInfo fi) {
            try { return fi.Length; }
            catch (Exception ex) { return 0; }
        }
        private FileInfo[] GetFiles(DirectoryInfo d) {
            try {
                return d.GetFiles();
            }
            catch (Exception e) {
                return new FileInfo[] { };
            }
        }

        private DirectoryInfo[] GetDirectories(DirectoryInfo d) 
        {
            try
            {
                return d.GetDirectories();
            }
            catch (Exception e)
            {
                return new DirectoryInfo[] { };
            }
        }

        private long installDirSize(DirectoryInfo d/*, string prefix = ""*/)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = GetFiles(d);

            foreach (FileInfo fi in fis)
            {
                long sizeFile = Length(fi);
                size += sizeFile;
                //strings.Push($"{prefix}{fi.Name}: {sizeFile}");
                pathToSize.Add(fi.FullName, new Description(sizeFile, "f"));
            }

            // Add subdirectory sizes.
            DirectoryInfo[] dis = GetDirectories(d);
            foreach (DirectoryInfo di in dis)
            {
                long sizeDir = installDirSize(di/*, $"{prefix}    "*/ );
                //strings.Push($"{prefix}{di.Name}: {sizeDir}");
                pathToSize.Add(di.FullName, new Description(sizeDir, "d"));
                size += sizeDir;
            }
            return size;
        }

        //public FileDO Print(bool isSort = true)
        //{
        //    PrintTree(startDir, isSort);
        //    return this;
        //}

        //private void WriteName(FileSystemInfo fsItem, long sizeFile = 0)
        //{
        //    Console.WriteLine($"{fsItem.Name}: {sizeFile}");
        //}

        public IOrderedEnumerable<FileSystemInfo> getSortFolderAndFile(DirectoryInfo di, bool isSort=true)
        {
            if (isSort)
            {
                return from f in di.GetFileSystemInfos()
                       orderby pathToSize[f.FullName].size ascending
                       select f;
            }
            else
            {
                return from f in di.GetFileSystemInfos()
                       orderby pathToSize[f.FullName].size descending
                       select f;

            }
        }

        //private void PrintTree(string startDir, bool isSort, string prefix = "")
        //{
        //    var di = new DirectoryInfo(startDir);
        //    var fsItems = getSortFolderAndFile(di, isSort);

        //    foreach (var fsItem in fsItems)
        //    {
        //        Write(prefix);
        //        WriteName(fsItem, pathToSize[fsItem.FullName]);
        //        if (fsItem.IsDirectory())
        //        {
        //            PrintTree(fsItem.FullName, isSort, prefix + "    ");
        //        }

        //    }
        //}

    }

    public static class FileSystemInfoExtensions
    {
        public static bool IsDirectory(this FileSystemInfo fsItem)
        {
            return (fsItem.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        public static bool checkPath(string path)
        {
            return File.Exists(path) || Directory.Exists(path);
        }
    }
}
