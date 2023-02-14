using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ASP_project.Models
{
    public class InputPath
    {
        [Required(ErrorMessage = "Вам нужно ввести путь")]
        public string path { get; set; }

        public string status { get; set; } = "Путь";
    }
}
