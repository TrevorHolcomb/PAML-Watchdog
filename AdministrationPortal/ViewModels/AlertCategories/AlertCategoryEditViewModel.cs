using System.ComponentModel.DataAnnotations;

namespace AdministrationPortal.ViewModels.AlertCategories
{
    public class AlertCategoryEditViewModel: AlertCategoryCreateViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}