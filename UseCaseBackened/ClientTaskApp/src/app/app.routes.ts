import { Routes } from '@angular/router';
import { ListTaskComponent } from './list-task/list-task.component';
import { CreateTaskComponent } from './create-task/create-task.component';
import { HomeComponent } from './home/home.component';
import { EditTaskComponent } from './edit-task/edit-task.component';

export const routes: Routes = [
    {path : '', component:HomeComponent},
    {path : 'home', component:HomeComponent},
    {path : 'list-task', component: ListTaskComponent},
    {path : 'create-task', component: CreateTaskComponent},
    {path : 'edit-task', component: EditTaskComponent}
];


