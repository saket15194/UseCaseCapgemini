import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CreateTask } from "../models/createTask";
import { ListTask } from "../models/listTask";

@Injectable({
    providedIn: 'root'
  })  

export class TaskusecaseService
{
    http:HttpClient=inject(HttpClient);

    postAddTask(createTask: CreateTask)
    {
        return this.http.post<CreateTask>('http://localhost:5203/api/Task/create-task',createTask,{headers : {'content-type':'application/json'}});
    }

    GetTasks()
    {
        return this.http.get<ListTask[]>('http://localhost:5203/api/Task/list-task',{headers: { 'accept': 'application/json' } });
    }
}