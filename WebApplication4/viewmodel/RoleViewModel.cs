using System.ComponentModel.DataAnnotations;

namespace WebApplication4.viewmodel
{
    public class RoleViewModel
    {
        [Required, StringLength(256)]
        public string Name { get; set; }
    }
}
