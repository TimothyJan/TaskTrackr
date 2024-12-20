import { Component, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Project } from '../../../models/project.model';
import { ProjectTaskService } from '../../../services/project-task.service';
import { ProjectService } from '../../../services/project.service';
import { ProjectTaskModalComponent } from '../project-task-modal/project-task-modal.component';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrl: './project.component.css'
})
export class ProjectComponent {
  @Input() projectId: number = 0;
  project: Project = new Project(0, "", "", "Not Started");
  listOfProjectTaskIds: number[] = [];
  editMode: boolean = false;

  projectNameInvalid: boolean = false;
  descriptionInvalid: boolean = false;

  startDateString: string = '';
  dueDateString: string = '';

  constructor(
    private _projectService: ProjectService,
    private _projectTaskService: ProjectTaskService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.getProjectById();
    this.getListOfProjectTaskIdsByProjectId();

    // Subscribe to changes in the task list
    this._projectTaskService.projectTasksChanged$.subscribe(() => {
      this.getListOfProjectTaskIdsByProjectId(); // Refresh the list after a task is deleted
    });

    this.syncDateStrings();
  }

  /** Get Project by ID */
  getProjectById(): void {
    this._projectService.getProjectById(this.projectId)
    .subscribe({
      next: (data) => {
        this.project = data;
        this.syncDateStrings();
      },
      error: (error) => {
        console.log(error.message);
      }
    });
    // Sync date strings
    this.syncDateStrings();
  }

  /** Get list of ProjectTaskIds by ProjectId */
  getListOfProjectTaskIdsByProjectId(): void {
    this._projectTaskService.getTaskIdsByProjectId(this.projectId)
    .subscribe({
      next: (data) => {
        this.listOfProjectTaskIds = data;
      },
      error: (error) => {
        // console.log(error.message);
      }
    });
  }

  /** Edit Mode */
  enterEditMode(): void {
    this.editMode = true;
  }

  /** Save project and exit editMode */
  saveChanges(): void {
    this.validateFields(); // Validate fields before saving

    if (this.projectNameInvalid || this.descriptionInvalid) {
      return; // Prevent saving if there are validation errors
    }

    // Update the projectTask dates with the converted values
    this.project.startDate = this.parseDate(this.startDateString) || this.project.startDate;
    this.project.dueDate = this.parseDate(this.dueDateString) || this.project.dueDate;

    this._projectService.updateProject(this.project)
    .subscribe({
      next: () => {
        this.editMode = false; // Exit edit mode
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }

  /**Delete Project */
  deleteProject(): void {
    if (confirm('Are you sure you want to delete this department?')) {
      this._projectService.deleteProject(this.project.projectId)
        .subscribe({
          next: () => {
            this._projectService.notifyProjectsChanged();
          },
          error: (error) => {
            console.log(error.message);
          }
        });
    }
  }

  /** Open ProjectTaskModal and refresh list of ProjectTaskIds */
  openAddProjectTaskModal(): void {
    const modalRef = this.modalService.open(ProjectTaskModalComponent, {
      size: 'md',
      backdrop: 'static', // Optional: Prevent closing on outside click
      keyboard: true, // Optional: Allow closing via the Escape key
    });

    modalRef.componentInstance.projectId = this.projectId;

    modalRef.result
    .then((result) => {
      // console.log('Modal closed with result:', result);
      this.getListOfProjectTaskIdsByProjectId();
    })
    .catch((error) => {
      // console.error('Modal dismissed with error:', error);
    });
  }

  /** Convert Date objects to yyyy-MM-dd strings for binding */
  private syncDateStrings(): void {
    this.startDateString = this.formatDate(this.project.startDate!);
    this.dueDateString = this.formatDate(this.project.dueDate!);
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
    this.projectNameInvalid = !this.project.projectName.trim();
    this.descriptionInvalid = !this.project.description.trim();
  }
}
