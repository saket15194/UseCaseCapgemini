import { Component, inject, OnInit } from '@angular/core';
import { ListTask } from '../../models/listTask';
import { TaskusecaseService } from '../../services/Taskusecase.Service';
import { FormsModule } from '@angular/forms';
import { response } from 'express';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-list-task',
  imports: [FormsModule,NgFor],
  templateUrl: './list-task.component.html',
  styleUrl: './list-task.component.css'
})
export class ListTaskComponent implements OnInit {

  listtask : ListTask[]=[];

  taskusecaseservice : TaskusecaseService=inject(TaskusecaseService);

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

  



}
