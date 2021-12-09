using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Crud
    {
        [Key]
        public int DishId {get;set;}
        
        [Required]
        [MinLength(3 ,ErrorMessage="Field must be 3 characters or more")]
        [Display(Name = "Your Name:")] 
        public string Name {get;set;}
[Required]
        [MinLength(3 ,ErrorMessage="Field must be 3 characters or more")]
        [Display(Name = "Chef:")] 
        public string Chef {get;set;}

        [Required(ErrorMessage ="Please enter a taste score")]
        [Range(0,10,ErrorMessage ="Number should be 1 to 10")]
        [Display(Name = "Tastiness:")] 
        public int Tastiness {get;set;}
        [Required(ErrorMessage ="Please enter a Calories")]
        [Range(0,10,ErrorMessage ="Number should be 1 to 100")]
        [Display(Name = "Calories:")]
        public int Calories {get;set;}
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}