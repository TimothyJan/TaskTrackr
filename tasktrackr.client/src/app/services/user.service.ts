import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { User } from '../models/user.model';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

const apiUrl = `${environment.apiUrl}/user`;

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private usersChangedSource = new Subject<void>();  // Emit events when department is added
  usersChanged$ = this.usersChangedSource.asObservable();

  private users: User[] = [
    new User(1, 'Alice Johnson', 'alice@example.com', 'Manager'),
    new User(2, 'Bob Smith', 'bob@example.com', 'Developer'),
    new User(3, 'Charlie Brown', 'charlie@example.com', 'Tester')
  ];

  constructor(private http: HttpClient) {}

  // Get all users from the backend
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(apiUrl);
  }

  // Get a user by ID from the backend
  getUserById(userId: number): Observable<User> {
    return this.http.get<User>(`${apiUrl}/${userId}`);
  }

  // Add a new user by sending a POST request to the backend
  addUser(newUser: User): Observable<User> {
    return this.http.post<User>(apiUrl, newUser);
  }

  // Update an existing user by sending a PUT request to the backend
  updateUser(updatedUser: User): Observable<User> {
    return this.http.put<User>(`${apiUrl}/${updatedUser.userId}`, updatedUser);
  }

  // Delete a user by sending a DELETE request to the backend
  deleteUser(userId: number): Observable<void> {
    return this.http.delete<void>(`${apiUrl}/${userId}`);
  }

  // Emit events for users update (e.g., after add, update, or delete)
  notifyUsersChanged(): void {
    this.usersChangedSource.next();
  }


  // // Get all users
  // getUsers(): User[] {
  //   return this.users;
  // }

  // // Get a user by ID
  // getUserById(userId: number): User | undefined {
  //   return this.users.find((user) => user.userId === userId);
  // }

  // // Add a new user
  // addUser(newUser: User): void {
  //   newUser.userId = this.users.length+1;
  //   this.users.push(newUser);
  // }

  // // Update an existing user
  // updateUser(updatedUser: User): void {
  //   const index = this.users.findIndex((user) => user.userId === updatedUser.userId);
  //   if (index !== -1) {
  //     this.users[index] = updatedUser;
  //   }
  // }

  // // Delete a user
  // deleteUser(userId: number): void {
  //   const index = this.users.findIndex(user => user.userId === userId);
  //   if (index !== -1) {
  //     this.users.splice(index, 1);
  //     this.usersChangedSource.next(); // Notify subscribers that the user list has changed
  //   }
  // }

  // // Emit events for projects update
  // notifyUsersChanged(): void {
  //   this.usersChangedSource.next();
  // }
}
