namespace ASP_project.Models
{
    public class ObjectFileSystem
    {
        //public Guid Id { get; set; }
        //public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
        public string type { get; set; }
        //public ICollection<BlogCategory> BlogCategoryChildren { get; set; }
        //public DateTime CreatedDate { get; set; }
        public ObjectFileSystem() { }

        public ObjectFileSystem(string Name="", long Size=0, string type="f") {
            this.Name = Name;
            this.Size = Size;
            this.type = type;
        }
        
    }
}
