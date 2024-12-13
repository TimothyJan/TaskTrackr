import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ProjectTask } from '../models/project-task.model';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

const apiUrl = `${environment.apiUrl}/projecttask`;

@Injectable({
  providedIn: 'root'
})
export class ProjectTaskService {

  private projectTasksChangedSource = new Subject<void>();  // Emit events when department is added
  projectTasksChanged$ = this.projectTasksChangedSource.asObservable();

  constructor(private http: HttpClient) {}

  // Get all project tasks
  getProjectTasks(): Observable<ProjectTask[]> {
    return this.http.get<ProjectTask[]>(`${apiUrl}`);
  }

  // Get tasks by project ID
  getTasksByProjectId(projectId: number): Observable<ProjectTask[]> {
    return this.http.get<ProjectTask[]>(`${apiUrl}/project/${projectId}`);
  }

  // Get a project task by ID
  getProjectTaskById(taskId: number): Observable<ProjectTask> {
    return this.http.get<ProjectTask>(`${apiUrl}/${taskId}`);
  }

  // Add a new project task
  addProjectTask(newProjectTask: ProjectTask): Observable<ProjectTask> {
    return this.http.post<ProjectTask>(`${apiUrl}`, newProjectTask);
  }

  // Update an existing project task
  updateProjectTask(updatedTask: ProjectTask): Observable<void> {
    return this.http.put<void>(`${apiUrl}/${updatedTask.projectTaskId}`, updatedTask);
  }

  // Delete a project task
  deleteProjectTask(projectTaskId: number): Observable<void> {
    return this.http.delete<void>(`${apiUrl}/${projectTaskId}`);
  }

  // Get a list of project task IDs by project ID
  getTaskIdsByProjectId(projectId: number): Observable<number[]> {
    return this.http.get<number[]>(`${apiUrl}/getTaskIdsByProjectId/${projectId}`);
  }

  // Notify subscribers about project tasks update
  notifyProjectTasksChanged(): void {
    this.projectTasksChangedSource.next();
  }
}
