import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabel } from 'primeng/floatlabel';
import { PasswordModule } from 'primeng/password';
import { DividerModule } from 'primeng/divider';
import { FormsModule } from '@angular/forms';
import { Select } from 'primeng/select';
import { HttpClient } from '@angular/common/http';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

import { NgIf } from '@angular/common';
import { path } from '../app.config';


interface Role {
  rolename: string;
}

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, ToastModule, ButtonModule, RouterModule, CardModule, InputTextModule, FloatLabel,
     PasswordModule, DividerModule, FormsModule, Select, ProgressSpinnerModule, NgIf],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
  providers: [MessageService]
})

export class RegistrationComponent {
  constructor(private messageService: MessageService, private http: HttpClient) {}

  id = crypto.randomUUID();
  surname: string = '';
  name: string = '';
  patronymic: string = '';
  mail: string = '';
  password: string = '';
  confpassword: string = '';
  roles: Role[] | undefined;
  role: Role | undefined;

  loading: boolean = false;
  
  ngOnInit(){
    this.roles = [
      {rolename: 'Менеджер'},
      {rolename: 'Продавец'}
    ]
  }

  checkNullInputs(){
    if (!this.surname){
      document.getElementById("surname")?.classList.add("ng-invalid");
      document.getElementById("surname")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.name){
      document.getElementById("name")?.classList.add("ng-invalid");
      document.getElementById("name")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.patronymic){
      document.getElementById("patronymic")?.classList.add("ng-invalid");
      document.getElementById("patronymic")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.role){
      document.getElementById("role")?.classList.add("ng-invalid");
      document.getElementById("role")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.mail){
      document.getElementById("mail")?.classList.add("ng-invalid");
      document.getElementById("mail")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.password){
      document.getElementById("password")?.classList.add("ng-invalid");
      document.getElementById("password")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.confpassword){
      document.getElementById("confpassword")?.classList.add("ng-invalid");
      document.getElementById("confpassword")?.classList.add("ng-dirty");
      return true;
    }
    return false
  }

  toNormal(id: string) {
    const inputElement = document.getElementById(id)
    if (inputElement) {
      inputElement.classList.remove("ng-invalid");
      inputElement.classList.remove("ng-dirty");
    } 
  }

  mismatchPass(){
    if (this.password !== this.confpassword){
      this.messageService.add({ severity: 'warn', summary: 'Упс', detail: 'Введенные пароли не совпадают', life: 3000 });
      return true;
    }
    return false;
  }

  createUser() {
    if (this.checkNullInputs()) {
      return; 
    }
    if (this.mismatchPass()){
      return;
    }

    this.loading = true;

    if (this.role?.rolename == "Менеджер"){
      const manager = {
        id: this.id,
        name: this.name,
        surname: this.surname,
        patronymic: this.patronymic,
        hashedPassword: this.password,
        mail: this.mail
      }
      this.http.post<any>(`${path}/api/managers`, manager)
    .subscribe({ 
      next: (manager) => { 
        this.loading = false;
          // Обработка успешного ответа для менеджера
          console.log('Созданный менеджер:', manager); 
          this.messageService.add({ 
              severity: 'success', 
              summary: 'Супер', 
              detail: `Успешное создание менеджера!`, 
              life: 3000 
          }); 
      }, 
      error: (error) => { 
        this.loading = false;
          // Обработка ошибки при запросе менеджера
          console.error('Ошибка при входе:', error); 
          this.messageService.add({ 
              severity: 'error', 
              summary: 'Ошибка', 
              detail: error.error || 'Произошла ошибка', 
              life: 3000 
          }); 
      } 
    });
    }
     else {
      const seller = {
        id: this.id,
        name: this.name,
        surname: this.surname,
        patronymic: this.patronymic,
        hashedPassword: this.password,
        mail: this.mail
      }
      this.http.post<any>(`${path}/api/sellers`, seller)
    .subscribe({ 
      next: (seller) => {
        this.loading = false; 
          // Обработка успешного ответа для продавца
          console.log('Созданный продавец:', seller); 
          this.messageService.add({ 
              severity: 'success', 
              summary: 'Супер', 
              detail: `Успешное создание продавца!`, 
              life: 3000 
          }); 
      }, 
      error: (error) => { 
        this.loading = false;
          // Обработка ошибки при запросе продавца
          console.error('Ошибка при входе:', error); 
          this.messageService.add({ 
              severity: 'error', 
              summary: 'Ошибка', 
              detail: error.error || 'Произошла ошибка', 
              life: 3000 
          }); 
      } 
    });
     } 
  }
}
