using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    //  public enum TaskStatus
    //     {
    //         [Display(Name = "NotStarted")]
    //         NotStarted,

    //         [Display(Name = "InProgress")]
    //         InProgress,

    //         [Display(Name = "Completed")]
    //         Completed
    //     }
    public class TaskDetails
    {
        public string? Name {get;set;}

        public int Priority {get;set;}

        public string? Status {get;set;} 

    }

    // public class TaskStatus
    // {
    //     public string? Name {get;set;}
        
    // }
}