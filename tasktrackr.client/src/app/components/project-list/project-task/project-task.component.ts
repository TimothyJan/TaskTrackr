import { Component, Input } from '@angular/core';
import { ProjectTask } from '../../../models/project-task.model';
import { User } from '../../../models/user.model';
import { ProjectTaskService } from '../../../services/project-task.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-project-task',
  templateUrl: './project-task.component.html',
  styleUrl: './project-task.component.css'
})
export class ProjectTaskComponent {
  @Input() projectTaskId: number = 0;
  projectTask: ProjectTask = new ProjectTask(0, 0, "", "", "Not Started", 0);
  editMode: boolean = false;

  titleInvalid: boolean = false;
  descriptionInvalid: boolean = false;

  // Temporary date strings for input bindings
  startDateString: string = '';
  dueDateString: string = '';

  users: User[] = [];

  isLoading: boolean = false;

  constructor(
    private _projectTaskService: ProjectTaskService,
    private _userService: UserService
  ) {}

  ngOnInit(): void {
    this.getProjectTaskById();
    this.syncDateStrings();
    this.getUsers();
  }

  /** Get ProjectTask by ID */
  getProjectTaskById(): void {
    this._projectTaskService.getProjectTaskById(this.projectTaskId)
    .subscribe({
      next: (data) => {
        this.projectTask = data;
        this.syncDateStrings();
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }

  /** Enter Edit mode for editing projectTask list */
  enterEditMode(): void {
    this.syncDateStrings();
    this.editMode = true;
  }

  /** Gets all users */
  getUsers(): void {
    this._userService.getUsers()
    .subscribe({
      next: (data) => {
        this.users = data;
        this.isLoading = false;
      },
      error: (error) => {
        console.log(error.message);
        this.isLoading = false;
      }
    });
  }

  /** Get User by userId */
  getUserById(userId: number): string {
    if (!userId) return 'Unassigned'; // If no userId, return 'Unassigned'

    const user = this.users.find(user => user.userId === userId);
    if (user) {
      return `${user.name} (${user.role})`;
    }
    return 'Unassigned';  // Return 'Unassigned' if no matching user found
  }

  /** Update projectTask and leave Edit mode */
  saveChanges(): void {
    this.validateFields(); // Validate fields before saving

    if (this.titleInvalid || this.descriptionInvalid) {
      return; // Prevent saving if there are validation errors
    }

    // Update the projectTask dates with the converted values
    this.projectTask.startDate = this.parseDate(this.startDateString) || this.projectTask.startDate;
    this.projectTask.dueDate = this.parseDate(this.dueDateString) || this.projectTask.dueDate;

    // String to number conversion for assignedUser
    this.projectTask.assignedUserId = Number(this.projectTask.assignedUserId);

    // Update ProjectTask
    this._projectTaskService.updateProjectTask(this.projectTask)
    .subscribe({
      next: () => {
        // Refresh the projectTask from the service after saving
        this.getProjectTaskById();

        // Notify other components and exit edit mode
        this.editMode = false;
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }

  /** Delete ProjectTask */
  deleteProjectTask(): void {
    if (confirm('Are you sure you want to delete this department?')) {
      this._projectTaskService.deleteProjectTask(this.projectTaskId)
        .subscribe({
          next: () => {
            //NEED TO EMIT THAT PROJECTTASKIDS HAVE CHANGED
          },
          error: (error) => {
            console.log(error.message);
          }
        });
    }
  }

  /** Convert Date objects to yyyy-MM-dd strings for binding */
  private syncDateStrings(): void {
    this.startDateString = this.formatDate(this.projectTask.startDate!);
    this.dueDateString = this.formatDate(this.projectTask.dueDate!);
  }

  /** Format a Date object to yyyy-MM-dd */
  private formatDate(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }

  /** Convert yyyy-MM-dd strings back to Date objects */
  private parseDate(dateString: string): Date | null {
    if (!dateString) return null;
      const [year, month, day] = dateString.split('-').map(Number);
    return new Date(year, month - 1, day); // Use local timezone interpretation
  }

  /** Validation for title and description */
  validateFields(): void {
    this.titleInvalid = !this.projectTask.title.trim();
    this.descriptionInvalid = !this.projectTask.description.trim();
  }
}
