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
import { DividerModule } from 'primeng/divider';
import { FormsModule } from '@angular/forms';




@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, ToastModule, ButtonModule, RouterModule, CardModule, InputTextModule, FloatLabel,
     PasswordModule, DividerModule, FormsModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
  providers: [MessageService]
})
export class RegistrationComponent {
  constructor(private messageService: MessageService) {}

  surname: string = '';
  name: string = '';
  patronymic: string = '';
  mail: string = '';
  password: string = '';
  confpassword: string = '';

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

  toNormal(string: string) {
    const inputElement = document.getElementById(string)
    if (inputElement) {
      inputElement.classList.remove("ng-invalid");
      inputElement.classList.remove("ng-dirty");
    } 
  }

  show() {
    if (this.checkNullInputs()) {
      return; // Возвращаем, если поле пустое
      }
      this.messageService.add({ severity: 'success', summary: 'Супер', detail: 'Регистрация выполнена успешно!', life: 3000 });
  }
}
