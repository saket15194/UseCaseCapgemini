import { TaskStatus } from "../constandfile/taskstatus";

export interface CreateTask
{
    name:string;
    priority:number;
    status:string;
}