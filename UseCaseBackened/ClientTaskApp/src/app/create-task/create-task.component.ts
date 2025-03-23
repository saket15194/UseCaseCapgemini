import { Component, inject } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { CreateTask } from '../../models/createTask';
import { TaskusecaseService } from '../../services/Taskusecase.Service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-create-task',
  imports: [FormsModule,NgIf],
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css'
})
export class CreateTaskComponent {

  createTask : CreateTask =
  {
      name:'',
      priority:0,
      status:''
  }
  taskusecaseservice: TaskusecaseService=inject(TaskusecaseService);
  errorMessage:string='';
  successMessage:string='';

  onFormSubmit()
  {
    //debugger;
    this.taskusecaseservice.postAddTask(this.createTask).subscribe({
      next :()=> {
        console.log('Product created successfully');
        this.successMessage = 'Task created successfully!';
        this.errorMessage = '';
        this.createTask.name='';
        this.createTask.priority=0;
        this.createTask.status='';
      },
      error : (error) => {
        console.error('There was an error!', error);
      
          this.errorMessage = error.message;
          this.successMessage = '';
        }
    })
  }

}
