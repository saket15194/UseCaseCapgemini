import { Component, inject, OnInit } from '@angular/core';
import { ListTask } from '../../models/listTask';
import { TaskusecaseService } from '../../services/Taskusecase.Service';
import { FormsModule } from '@angular/forms';
import { response } from 'express';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-list-task',
  imports: [FormsModule,NgFor,RouterLink,NgIf],
  templateUrl: './list-task.component.html',
  styleUrl: './list-task.component.css'
})
export class ListTaskComponent implements OnInit {

  listtask : ListTask[]=[];
  selectedTask :string='';
  taskusecaseservice : TaskusecaseService=inject(TaskusecaseService);
  errorMessage:string='';
  ngOnInit(): void {
    this.fetchdata();
  }

  fetchdata() : void{

    this.taskusecaseservice.GetTasks().subscribe(
      response=>{
        this.listtask=response;
      }

    )
  }

  onDelete(taskname:string){
    this.taskusecaseservice.deleteTask(this.selectedTask).subscribe(
      (response) => {
        // On success, reload the task list
        this.fetchdata();
        
        alert('Task deleted successfully!');}
        ,
        (error) => {
          this.errorMessage = error.message;
        }
    )
    }



}
