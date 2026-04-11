import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AccountService } from '../../../core/services/account.service';

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule, 
    CommonModule, 
    RouterLink
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountService);
  private router = inject(Router);

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.router.navigate(['/inventory']);
      },
      error: (err: any) => {
        console.error('Login failed', err);
        alert('Invalid credentials');
      }
    });
  }
  
  getInputClass(controlName: string) {
    const control = this.loginForm.get(controlName);
    if (control?.invalid && (control?.dirty || control?.touched)) 
      return 'border-red-500 bg-red-50';
    if (control?.valid && (control?.dirty || control?.touched)) 
      return 'border-green-500 bg-green-50/30';
    return 'border-gray-300';
  }
}