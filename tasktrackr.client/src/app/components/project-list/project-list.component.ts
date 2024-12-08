import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProjectService } from '../../services/project.service';
import { ProjectModalComponent } from './project-modal/project-modal.component';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.css'
})
export class ProjectListComponent {
  listOfProjectIds: number[] = [];

  constructor(
    private _projectService: ProjectService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.getListOfProjectIds();
  }

  /** Get list of ProjectIds */
  getListOfProjectIds(): void {
    this.listOfProjectIds = this._projectService.getListOfProjectIds();
  }

  /** Open ProjectModal and refresh list of ProjectIds  */
  openAddProjectModal(): void {
    const modalRef = this.modalService.open(ProjectModalComponent, {
      size: 'md',
      backdrop: 'static', // Optional: Prevent closing on outside click
      keyboard: true, // Optional: Allow closing via the Escape key
    });

    modalRef.result
    .then((result) => {
      // console.log('Modal closed with result:', result);
      this.getListOfProjectIds();
    })
    .catch((error) => {
      // console.error('Modal dismissed with error:', error);
    });
  }
}
