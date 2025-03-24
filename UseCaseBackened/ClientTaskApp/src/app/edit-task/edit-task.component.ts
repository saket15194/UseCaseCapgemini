import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ListTask } from '../../models/listTask';
import { EditTask } from '../../models/editTask';
import { NgFor, NgIf } from '@angular/common';
import { TaskusecaseService } from '../../services/Taskusecase.Service';
import { error } from 'console';

@Component({
  selector: 'app-edit-task',
  imports: [FormsModule,NgFor,NgIf],
  templateUrl: './edit-task.component.html',
  styleUrl: './edit-task.component.css'
})
export class EditTaskComponent implements OnInit {

  taskservice: TaskusecaseService=inject(TaskusecaseService);
  tasks: ListTask[] = [];
  errorMessage:string='';
  successMessage:string='';
  taskStatus = ['NotStarted', 'InProgress', 'Completed'];

  selectedTaskName: string = '';
  selectedStatus: string = '';
  task: EditTask={
    name: '',
    priority: 0,
    status: ''
  };
  ngOnInit(){
   
      this.taskservice.GetTasks().subscribe(
        (data) => {
          this.tasks = data;
        });
  }

  onFormEdit() : void
  {
    debugger;
    if(this.selectedTaskName && this.task.priority >=1 && this.task.priority <= 3)
    {
      console.log(this.task.status);
      this.task.name=this.selectedTaskName;
      //this.task.status=this.selectedStatus;
      this.taskservice.editTask(this.selectedTaskName,this.task).subscribe(
        (data) => {
          console.log(data);
          this.successMessage = 'Task updated successfully!';
          this.task.name='';
          this.task.priority=0;
          this.task.status='';
        this.errorMessage = '';
        },error => { 
          this.errorMessage = error.message; 
          this.successMessage = ''; 
        });
    }
  }
  
  
}
