import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { CreateTask } from "../models/createTask";
import { ListTask } from "../models/listTask";
import { catchError, Observable, throwError } from "rxjs";
import { EditTask } from "../models/editTask";

@Injectable({
    providedIn: 'root'
  })  

export class TaskusecaseService
{
    http:HttpClient=inject(HttpClient);

    postAddTask(createTask: CreateTask)
    {
        return this.http.post<CreateTask>('http://localhost:5203/api/Task/create-task',createTask,
            {headers : {'content-type':'application/json'},responseType:'text' as 'json'}).pipe(
                catchError(this.handleError)
            );
    }

    GetTasks()
    {
        return this.http.get<ListTask[]>('http://localhost:5203/api/Task/list-task',{headers: { 'accept': 'application/json' } });
    }

    deleteTask(taskname: string):Observable <any> {
        return this.http.delete<any>('http://localhost:5203/api/Task/delete-task', 
                {body : JSON.stringify(taskname), headers: { 'content-type': 'application/json' },
                responseType:'text' as 'json' }               
        ).pipe(
            catchError(this.handleError)
          );
        
    }

    editTask(taskname: string,task: EditTask):Observable <any>
    {
        return this.http.put(`http://localhost:5203/api/Task/edit-task/${taskname}`, task,
        {headers: { 'content-type': 'application/json' }}).pipe(
            catchError(this.handleError));
    }

    private handleError(error: any) {
        let errorMessage = '';
        console.log(error.error);
        // If the error is from the backend and has a response body with a message
        if (error.error) {
          errorMessage = error.error;
        } else if (error.status === 0) {
          errorMessage = 'No connection to the API, please check your network.';
        } else if (error.status === 404) {
          errorMessage = 'API endpoint not found!';
        } else if (error.status === 500) {
          errorMessage = 'Internal server error occurred!';
        }
    
        return throwError(() => new Error(errorMessage));  // Propagate error message to the component
      }
}