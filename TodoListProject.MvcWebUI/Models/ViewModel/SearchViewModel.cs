using System.ComponentModel.DataAnnotations;

namespace TodoListProject.MvcWebUI.Models.ViewModel {
    public class SearchViewModel
    {
        [Required]
        public string SearchText { get; set; }
    }
}
