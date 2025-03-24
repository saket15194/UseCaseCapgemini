using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
      public enum TaskStatus
         {
             NotStarted,

             InProgress,

             Completed
     }
    public class TaskDetails
    {   
        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Task name should be between 5 to 10 characters.")]
        public string? Name {get;set;}

        [Range(1,3)]
        public int Priority {get;set;}

        public TaskStatus Status {get;set;} 

    }

    // public class TaskStatus
    // {
    //     public string? Name {get;set;}
        
    // }
}