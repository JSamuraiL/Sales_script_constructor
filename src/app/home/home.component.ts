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

@Component({
  selector: 'app-home',
  imports: [ReactiveFormsModule, ToastModule, ButtonModule, RouterModule, CardModule, InputTextModule, FloatLabel, PasswordModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  providers: [MessageService]
})
export class HomeComponent {
  constructor(private messageService: MessageService) {}

  show() {
      this.messageService.add({ severity: 'success', summary: 'Супер', detail: 'Вход выполнен успешно!', life: 3000 });
  }
}
