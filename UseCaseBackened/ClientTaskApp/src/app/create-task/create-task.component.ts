import { Component, inject } from '@angular/core';
import { FormsModule, NgModel } from '@angular/forms';
import { CreateTask } from '../../models/createTask';
import { TaskusecaseService } from '../../services/Taskusecase.Service';

@Component({
  selector: 'app-create-task',
  imports: [FormsModule],
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
  onFormSubmit()
  {
    debugger;
    this.taskusecaseservice.postAddTask(this.createTask).subscribe({
      next :()=> {
        console.log('Product created successfully');
      },
    })
  }

}
