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


@Component({
  selector: 'app-home',
  imports: [ReactiveFormsModule, ToastModule, ButtonModule, RouterModule, CardModule, InputTextModule, FloatLabel, 
    PasswordModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  providers: [MessageService]
})
export class HomeComponent {
  constructor(private messageService: MessageService) {}

  mail: string = '';
  password: string = '';
  
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
    this.messageService.add({ severity: 'success', summary: 'Супер', detail: 'Вход выполнен успешно!', life: 3000 });
  }
}
