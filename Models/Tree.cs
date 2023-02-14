using System;
using System.Collections.Generic;
using System.IO;

namespace ASP_project.Models
{
    public class Tree
    {
        public bool IsSort { get; set; }
        public string init_dir { get; set; }
        public string startDir { get; set; }
        public List<ObjectFileSystem> blogCategories { get; set; }
        public FileDO fileDO { get; set; }

        public Tree(string init_dir="")
        {
            blogCategories = new List<ObjectFileSystem>();
            if (init_dir != "")
            {
                this.init_dir = init_dir;
                startDir = init_dir;
                try
                {
                    fileDO = new FileDO(init_dir).getInfoCatalog();

                    var fsItems = fileDO.getSortFolderAndFile(new DirectoryInfo(startDir), IsSort);

                    foreach (var fsItem in fsItems)
                    {
                        var info = fileDO.pathToSize.GetValueOrDefault(fsItem.FullName, new Description());
                        blogCategories.Add(new ObjectFileSystem(fsItem.Name, info.size, info.type));
                    }
                    Console.WriteLine("create");
                }
                catch (Exception ex) 
                {
                    fileDO = null;
                }
            }
        }

        public List<ObjectFileSystem> getNext(string path="")
        {
            if (fileDO == null) {
                fileDO = new FileDO(init_dir).getInfoCatalog();
            }

            if (path.IndexOf(".") != -1) {
                return blogCategories;
            }

            blogCategories.Clear();
            if (path != "")
            {
                startDir = @$"{startDir}\{path}";
            }
            try {
                var dn = new DirectoryInfo(startDir);

                var fsItems = fileDO.getSortFolderAndFile(dn, IsSort);
                foreach (var fsItem in fsItems)
                {
                    var info = fileDO.pathToSize.GetValueOrDefault(fsItem.FullName, new Description());
                    blogCategories.Add(new ObjectFileSystem(fsItem.Name, info.size, info.type));
                }
                return blogCategories;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return new List<ObjectFileSystem>();
            }
        }

        public List<ObjectFileSystem> getBack()
        {
            if (startDir != init_dir)
            {
                blogCategories.Clear();
                string path = startDir;
                startDir = startDir.Substring(0, path.LastIndexOf(@"\"));
            }
            return getNext();
        }
    }
}
