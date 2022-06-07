using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Application.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; }
    }
}
