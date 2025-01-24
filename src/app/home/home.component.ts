import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabel } from 'primeng/floatlabel';
import { PasswordModule } from 'primeng/password';
import { FormsModule } from '@angular/forms';

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ProgressSpinnerModule } from 'primeng/progressspinner';

import { NgIf } from '@angular/common';
import { path } from '../app.config';


@Injectable({
  providedIn: 'root'
})

@Component({
  selector: 'app-home',
  imports: [ReactiveFormsModule, ToastModule, ButtonModule, RouterModule, CardModule, InputTextModule, FloatLabel, 
    PasswordModule, FormsModule, ProgressSpinnerModule, NgIf],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  providers: [MessageService]
})
export class HomeComponent {
  constructor(private messageService: MessageService, private http: HttpClient) {}

  mail: string = '';
  password: string = '';
  loading: boolean = false;


  checkNullInputs(){
    if (!this.mail){
      document.getElementById("mail")?.classList.add("ng-invalid");
      document.getElementById("mail")?.classList.add("ng-dirty");
      return true;
    }
    else if (!this.password){
      document.getElementById("password")?.classList.add("ng-invalid");
      document.getElementById("password")?.classList.add("ng-dirty");
      return true;
    }
    return false;
  }

  toNormal(id: string) {
    const inputElement = document.getElementById(id)
    if (inputElement) {
      inputElement.classList.remove("ng-invalid");
      inputElement.classList.remove("ng-dirty");
    } 
  }

  enter() {
    if (this.checkNullInputs()) {
      return; 
    }

    this.loading = true;

    this.http.get<any>(`${path}/api/managers/byMail/${this.mail}?password=${this.password}`)
    .subscribe({
      next: (manager) => {
          this.loading = false;
          // Обработка успешного ответа
          console.log('Полученный менеджер:', manager);
          this.messageService.add({ severity: 'success', summary: 'Супер', detail: `Добро пожаловать, ${manager.surname} ${manager.name} ${manager.patronymic}!`, life: 3000 });
      },
      error: (error) => {
          this.loading = false;
          // Обработка ошибки
          if (error.status == 404) { // Предполагаем, что статус 404 означает, что менеджер не найден
            this.checkSeller();
        } else {
          console.error('Ошибка при входе:', error);
          this.messageService.add({ severity: 'error', summary: 'Ошибка', detail: error.error || 'Произошла ошибка', life: 3000 });
        }
      }
  });
  }
  checkSeller() {
    this.http.get<any>(`${path}/api/sellers/byMail/${this.mail}?password=${this.password}`)
    .subscribe({ 
      next: (seller) => {
          this.loading = false; 
          // Обработка успешного ответа для продавца
          console.log('Полученный продавец:', seller); 
          this.messageService.add({ 
              severity: 'success', 
              summary: 'Супер', 
              detail: `Добро пожаловать, ${seller.surname} ${seller.name} ${seller.patronymic}!`, 
              life: 3000 
          }); 
      }, 
      error: (error) => { 
          this.loading = false;
          if (error.status == 404) {
            console.error('Ошибка при входе: пользователь с такой почтой не найден');
            this.messageService.add({ 
              severity: 'error', 
              summary: 'Ошибка', 
              detail: 'Пользователь с такой почтой не найден', 
              life: 3000 
          }); 
          }
          else{
          // Обработка ошибки при запросе продавца
          console.error('Ошибка при входе:', error); 
          this.messageService.add({ 
              severity: 'error', 
              summary: 'Ошибка', 
              detail: error.error || 'Произошла ошибка', 
              life: 3000 
          }); 
        }
      } 
  });
}
}
