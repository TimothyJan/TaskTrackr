import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { UserModalComponent } from './user-modal/user-modal.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent {
  users:User[] = [];

  constructor(
    private _userService: UserService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  /** Get all users */
   getUsers(): void {
    this._userService.getUsers()
    .subscribe({
      next: (data) => {
        this.users = data;
      },
      error: (error) => {
        console.log(error.message);
      }
    });
  }

  /** Open UserModal */
  openAddUserModal(): void {
    const modalRef = this.modalService.open(UserModalComponent, {
      size: 'md',
      backdrop: 'static',
      keyboard: true,
    });

    modalRef.result
    .then(() => this.getUsers())
    .catch(() => {});
  }

  /** Edit User */
  editUser(user: User): void {
    const modalRef = this.modalService.open(UserModalComponent, {
      size: 'md',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.componentInstance.user = { ...user };

    modalRef.result
    .then(() => this.getUsers())
    .catch(() => {});
  }

  /** Delete User */
  deleteUser(userId: number): void {
    this._userService.deleteUser(userId);
    if (confirm('Are you sure you want to delete this department?')) {
      this._userService.deleteUser(userId)
        .subscribe({
          next: () => {
            this.getUsers();
          },
          error: (error) => {
            console.log(error.message);
          }
        });
    }
    this.getUsers();
  }
}
