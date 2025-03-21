import { createComponent } from '@angular/core';
import { Routes } from '@angular/router';
import { ListTaskComponent } from './list-task/list-task.component';
import { CreateTaskComponent } from './create-task/create-task.component';

export const routes: Routes = [
    {path : '', component: CreateTaskComponent},
    {path : 'task-list', component: ListTaskComponent},
];


