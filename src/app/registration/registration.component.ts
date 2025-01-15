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



@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, ToastModule, ButtonModule, RouterModule, CardModule, InputTextModule, FloatLabel,
     PasswordModule, DividerModule],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
  providers: [MessageService]
})
export class RegistrationComponent {
  constructor(private messageService: MessageService) {}

  show() {
      this.messageService.add({ severity: 'success', summary: 'Супер', detail: 'Регистрация выполнена успешно!', life: 3000 });
  }
}
