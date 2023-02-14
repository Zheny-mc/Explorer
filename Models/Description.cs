namespace ASP_project.Models
{
    public class Description
    {
        public long size { get; set; } 
        public string type { get; set; }

        public Description(long size=0, string type="f")
        {
            this.size = size;
            this.type = type;
        }
    }
}
