using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Models
{
    public class TableModel : BaseModel
    {
        [Required(ErrorMessage = "Ovo polje je obavezno")]
        [MinLength(3, ErrorMessage = "Naziv mora imati bar 3 slova.")]
        public string Name { get; set; }
    }
}